using AsyncDemo.Helpers;
using System.Reflection;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace AsyncDemo
{
    public partial class MainForm : Form
    {
        private Dictionary<int, IActionFactory> _actionMethods = new();
        private Dictionary<string, string> _methodCodeDictionary = new();
        private Dictionary<int, string> _methodNameDictionary = new();
        private Dictionary<string, string> _methodCommentsDictionary = new();
        private Action _selectedAction;
        private IActionFactory _factory;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize dictionary with factories
            _actionMethods = new ActionsFactory().Actions;
            PopulateMaps();
            PopulateComboBox();
        }

        #region UI Setup
        private void PopulateMaps()
        {
            var nameMapper = new NameToCodeMapper(@"Data\methods.json");

            _methodCodeDictionary = nameMapper.MethodNameCodeMap;

            int i = 1;
            _methodNameDictionary = nameMapper.MethodNumberList.
                Select(methodCodeDictionary => new { Key = i++, Value = methodCodeDictionary.Name }).
                ToDictionary(x => x.Key, x => x.Value);
            _methodCommentsDictionary = nameMapper.MethodCommentDictionary;
        }
        private void PopulateComboBox()
        {
            if (_methodNameDictionary == null)
                throw new Exception("Error populating combobox.");

            List<ComboBoxItem> list = _methodNameDictionary.Select(kvp => new ComboBoxItem(kvp.Value, kvp.Key)).ToList();
            comboExamples.DataSource = list;
            comboExamples.DisplayMember = "DisplayText";
            comboExamples.ValueMember = "Value";


        }
        #endregion


        #region textbox population
        private Action GetAction(int key)
        {
            if (_actionMethods.TryGetValue(key, out IActionFactory factory))
            {
                ((BaseActionFactory)factory).MessageGenerated -= ActionFactory_MessageGenerated;
                ((BaseActionFactory)factory).MessageGenerated += ActionFactory_MessageGenerated;
                _factory = factory;
                return factory.GetAction();
            }
            else
            {
                // Handle case where key is not found
                return () => Console.WriteLine("Action not found for key " + key);
            }
        }
        private void PopulateComments(string methodName)
        {
            txtComments.Clear();
            string comments = _methodCommentsDictionary.ContainsKey(methodName) ? _methodCommentsDictionary[methodName] : "";
            txtComments.AppendText(comments);
        }
        private void PrintMethodBody(string methodName)
        {
            txtCode.Clear();
            txtCode.SelectionIndent = 20;
            txtCode.AppendText($"Method: {methodName}");
            txtCode.AppendText("\n");

            if (_methodCodeDictionary.TryGetValue(methodName, value: out string methodBody))
            {
                txtCode.AppendText($"Method Body:\n{methodBody}");
                FormatComments();
            }
            else
            {
                txtCode.AppendText("Method body not available.");
            }

            txtCode.SelectAll();
        }
        private void ActionFactory_MessageGenerated(object sender, string message)
        {
            AppendTextToTextBox(message);
        }

        private void AppendTextToTextBox(string text)
        {
            Console.WriteLine("Append" + text);
            if (txtResults.InvokeRequired)
            {
                txtResults.Invoke(new Action(() => AppendTextToTextBox(text)));
            }
            else
            {
                txtResults.AppendText(text);
            }
        }
        private void FormatComments()
        {
            // Save the current selection start and length
            int selectionStart = txtCode.SelectionStart;
            int selectionLength = txtCode.SelectionLength;

            // Iterate through each line
            for (int i = 0; i < txtCode.Lines.Length; i++)
            {
                string line = txtCode.Lines[i];
                if (line.TrimStart().StartsWith(@"//"))
                {
                    // Select the line
                    txtCode.Select(txtCode.GetFirstCharIndexFromLine(i), line.Length);

                    // Apply blue color to the selected text
                    txtCode.SelectionColor = Color.Green;
                }
            }

            // Restore the selection to its original position
            txtCode.Select(selectionStart, selectionLength);

            // Ensure the caret is visible
            txtCode.ScrollToCaret();
        }
        private void UpdateButtons()
        {
            btnCancel.Enabled = (_factory is ExampleCancellingTasksFactory);
        }
        #endregion

        #region event handlers
        private void comboExamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtResults.Clear();
            var selected = comboExamples.SelectedItem.ToString();
            if (selected != null)
            {
                PrintMethodBody(selected.Trim());
                _selectedAction = GetAction(((ComboBoxItem)comboExamples.SelectedItem).Value);
                UpdateButtons();
                PopulateComments(selected.Trim());
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (_selectedAction != null)
            {
                _selectedAction.Invoke();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_factory != null)
                _factory.CancelTask();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}