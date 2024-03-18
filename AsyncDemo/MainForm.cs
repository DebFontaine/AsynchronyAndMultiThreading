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
        private Action _currentAction;
        private IActionFactory _factory;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize dictionary with factories
            _actionMethods = new ActionsFactory().Actions;

            // Add other factories as needed
            Console.WriteLine("Populating data....");
            PopulateMaps();
            PopulateComboBox();

        }
        public Action GetAction(int key)
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
        public void PopulateMaps()
        {
            var nameMapper = new NameToCodeMapper("methods.json");

            _methodCodeDictionary = nameMapper.MethodNameCodeMap;

            int i = 1;
            _methodNameDictionary = nameMapper.MethodNumberList.
                Select(methodCodeDictionary => new { Key = i++, Value = methodCodeDictionary.Name }).
                ToDictionary(x => x.Key, x => x.Value);
            _methodCommentsDictionary = nameMapper.MethodCommentDictionary;
        }
        public void PopulateComboBox()
        {
            if (_methodNameDictionary == null)
                throw new Exception("Error populating combobox.");

            List<ComboBoxItem> list = _methodNameDictionary.Select(kvp => new ComboBoxItem(kvp.Value, kvp.Key)).ToList();
            comboExamples.DataSource = list;
            comboExamples.DisplayMember = "DisplayText";
            comboExamples.ValueMember = "Value";


        }

        private void comboExamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Clear();
            richTextBox1.Clear();
            var selected = comboExamples.SelectedItem.ToString();
            if (selected != null)
            {
                PrintMethodBody(selected.Trim());
                Action act = GetAction(((ComboBoxItem)comboExamples.SelectedItem).Value);
                _currentAction = act;
                PopulateComments();
            }
        }
        private void PopulateComments()
        {
            txtComments.Clear();
            string methodName = ((ComboBoxItem)comboExamples.SelectedItem).DisplayText;
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
            }
            else
            {
                txtCode.AppendText("Method body not available.");
            }

            txtCode.SelectAll();
        }
        private void ActionFactory_MessageGenerated(object sender, string message)
        {
            // Update the TextBox with the generated message
            AppendTextToTextBox(message);
        }

        private void AppendTextToTextBox(string text)
        {
            Console.WriteLine("Append" + text);
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => AppendTextToTextBox(text)));
            }
            else
            {
                richTextBox1.AppendText(text);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_currentAction != null)
            {
                _currentAction.Invoke();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_factory is ExampleCancellingTasksFactory)
                _factory.CancelTask();
        }
    }
}