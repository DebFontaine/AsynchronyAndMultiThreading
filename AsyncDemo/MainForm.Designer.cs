namespace AsyncDemo
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            comboExamples = new ComboBox();
            txtCode = new RichTextBox();
            txtResults = new RichTextBox();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            btnExit = new Button();
            btnRun = new Button();
            btnCancel = new Button();
            txtComments = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label3, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(txtCode, 0, 2);
            tableLayoutPanel1.Controls.Add(txtResults, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 4);
            tableLayoutPanel1.Controls.Add(txtComments, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1192, 545);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(598, 30);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(61, 20);
            label3.TabIndex = 5;
            label3.Text = "Result:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(comboExamples);
            flowLayoutPanel1.Location = new Point(2, 2);
            flowLayoutPanel1.Margin = new Padding(2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1188, 26);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 0);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Select Example:";
            // 
            // comboExamples
            // 
            comboExamples.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboExamples.FormattingEnabled = true;
            comboExamples.Location = new Point(95, 2);
            comboExamples.Margin = new Padding(2);
            comboExamples.Name = "comboExamples";
            comboExamples.Size = new Size(415, 23);
            comboExamples.TabIndex = 1;
            comboExamples.SelectedIndexChanged += comboExamples_SelectedIndexChanged;
            // 
            // txtCode
            // 
            txtCode.BackColor = SystemColors.InfoText;
            txtCode.Dock = DockStyle.Fill;
            txtCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtCode.ForeColor = SystemColors.Window;
            txtCode.Location = new Point(2, 52);
            txtCode.Margin = new Padding(2);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(592, 346);
            txtCode.TabIndex = 2;
            txtCode.Text = "";
            // 
            // txtResults
            // 
            txtResults.BackColor = SystemColors.WindowText;
            txtResults.Dock = DockStyle.Fill;
            txtResults.Font = new Font("Courier New", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point);
            txtResults.ForeColor = SystemColors.Window;
            txtResults.Location = new Point(598, 52);
            txtResults.Margin = new Padding(2);
            txtResults.Name = "txtResults";
            txtResults.Size = new Size(592, 346);
            txtResults.TabIndex = 3;
            txtResults.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(2, 30);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 4;
            label2.Text = "Code:";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(btnExit);
            flowLayoutPanel2.Controls.Add(btnRun);
            flowLayoutPanel2.Controls.Add(btnCancel);
            flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new Point(881, 518);
            flowLayoutPanel2.Margin = new Padding(2);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(309, 24);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(231, 2);
            btnExit.Margin = new Padding(2);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(76, 20);
            btnExit.TabIndex = 7;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(151, 2);
            btnRun.Margin = new Padding(2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(76, 20);
            btnRun.TabIndex = 8;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(71, 2);
            btnCancel.Margin = new Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(76, 20);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel Task";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtComments
            // 
            tableLayoutPanel1.SetColumnSpan(txtComments, 2);
            txtComments.Dock = DockStyle.Fill;
            txtComments.Font = new Font("Segoe UI", 9.857143F, FontStyle.Regular, GraphicsUnit.Point);
            txtComments.Location = new Point(2, 402);
            txtComments.Margin = new Padding(2);
            txtComments.Name = "txtComments";
            txtComments.Size = new Size(1188, 112);
            txtComments.TabIndex = 7;
            txtComments.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 545);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private ComboBox comboExamples;
        private RichTextBox txtCode;
        private RichTextBox txtResults;
        private Label label2;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button btnExit;
        private Button btnRun;
        private Button btnCancel;
        private RichTextBox txtComments;
    }
}