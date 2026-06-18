namespace Demo
{
    partial class ImportExcel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExcel));
            btnSelectFolder = new Button();
            btnImport = new Button();
            richLog = new RichTextBox();
            lblFolderPath = new Label();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.AutoSize = true;
            btnSelectFolder.Location = new Point(204, 375);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(123, 30);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Выбрать папку";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // btnImport
            // 
            btnImport.AutoSize = true;
            btnImport.Location = new Point(414, 375);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(164, 30);
            btnImport.TabIndex = 1;
            btnImport.Text = "Импортировать в бд";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // richLog
            // 
            richLog.Location = new Point(202, 98);
            richLog.Name = "richLog";
            richLog.Size = new Size(376, 223);
            richLog.TabIndex = 2;
            richLog.Text = "";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Location = new Point(202, 55);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(100, 20);
            lblFolderPath.TabIndex = 3;
            lblFolderPath.Text = "Путь к папке:";
            // 
            // ImportExcel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblFolderPath);
            Controls.Add(richLog);
            Controls.Add(btnImport);
            Controls.Add(btnSelectFolder);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ImportExcel";
            Text = "Импорт из Excel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFolder;
        private Button btnImport;
        private RichTextBox richLog;
        private Label lblFolderPath;
    }
}