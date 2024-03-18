﻿namespace AsyncDemo
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
            richTextBox1 = new RichTextBox();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
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
            tableLayoutPanel1.Controls.Add(richTextBox1, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 4);
            tableLayoutPanel1.Controls.Add(txtComments, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(2044, 1090);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(1025, 59);
            label3.Name = "label3";
            label3.Size = new Size(105, 38);
            label3.TabIndex = 5;
            label3.Text = "Result:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(comboExamples);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(2038, 53);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(157, 30);
            label1.TabIndex = 0;
            label1.Text = "Select Example:";
            // 
            // comboExamples
            // 
            comboExamples.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboExamples.FormattingEnabled = true;
            comboExamples.Location = new Point(166, 3);
            comboExamples.Name = "comboExamples";
            comboExamples.Size = new Size(708, 38);
            comboExamples.TabIndex = 1;
            comboExamples.SelectedIndexChanged += comboExamples_SelectedIndexChanged;
            // 
            // txtCode
            // 
            txtCode.BackColor = SystemColors.InfoText;
            txtCode.Dock = DockStyle.Fill;
            txtCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtCode.ForeColor = Color.Green;
            txtCode.Location = new Point(3, 102);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(1016, 696);
            txtCode.TabIndex = 2;
            txtCode.Text = "";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.WindowText;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("Courier New", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.ForeColor = SystemColors.Window;
            richTextBox1.Location = new Point(1025, 102);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1016, 696);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(3, 59);
            label2.Name = "label2";
            label2.Size = new Size(91, 38);
            label2.TabIndex = 4;
            label2.Text = "Code:";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(button1);
            flowLayoutPanel2.Controls.Add(button2);
            flowLayoutPanel2.Controls.Add(btnCancel);
            flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new Point(1511, 1038);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(530, 49);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(396, 3);
            button1.Name = "button1";
            button1.Size = new Size(131, 40);
            button1.TabIndex = 7;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(259, 3);
            button2.Name = "button2";
            button2.Size = new Size(131, 40);
            button2.TabIndex = 8;
            button2.Text = "Run";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(122, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(131, 40);
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
            txtComments.Location = new Point(3, 804);
            txtComments.Name = "txtComments";
            txtComments.Size = new Size(2038, 228);
            txtComments.TabIndex = 7;
            txtComments.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2044, 1090);
            Controls.Add(tableLayoutPanel1);
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
        private RichTextBox richTextBox1;
        private Label label2;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button button1;
        private Button button2;
        private Button btnCancel;
        private RichTextBox txtComments;
    }
}