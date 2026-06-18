namespace Demo
{
    partial class OrderEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderEditForm));
            txtOrderNumber = new TextBox();
            txtClientFullName = new TextBox();
            txtPickupCode = new TextBox();
            dtpOrderDate = new DateTimePicker();
            dtpDeliveryDate = new DateTimePicker();
            cbPickupPoint = new ComboBox();
            cbStatus = new ComboBox();
            dgvItems = new DataGridView();
            btnAddItem = new Button();
            btnRemoveItem = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // txtOrderNumber
            // 
            txtOrderNumber.Location = new Point(181, 52);
            txtOrderNumber.Name = "txtOrderNumber";
            txtOrderNumber.Size = new Size(125, 27);
            txtOrderNumber.TabIndex = 0;
            // 
            // txtClientFullName
            // 
            txtClientFullName.Location = new Point(181, 103);
            txtClientFullName.Name = "txtClientFullName";
            txtClientFullName.Size = new Size(125, 27);
            txtClientFullName.TabIndex = 1;
            // 
            // txtPickupCode
            // 
            txtPickupCode.Location = new Point(181, 150);
            txtPickupCode.Name = "txtPickupCode";
            txtPickupCode.Size = new Size(125, 27);
            txtPickupCode.TabIndex = 2;
            // 
            // dtpOrderDate
            // 
            dtpOrderDate.Location = new Point(432, 52);
            dtpOrderDate.Name = "dtpOrderDate";
            dtpOrderDate.Size = new Size(250, 27);
            dtpOrderDate.TabIndex = 3;
            // 
            // dtpDeliveryDate
            // 
            dtpDeliveryDate.Location = new Point(432, 98);
            dtpDeliveryDate.Name = "dtpDeliveryDate";
            dtpDeliveryDate.Size = new Size(250, 27);
            dtpDeliveryDate.TabIndex = 4;
            // 
            // cbPickupPoint
            // 
            cbPickupPoint.FormattingEnabled = true;
            cbPickupPoint.Location = new Point(432, 151);
            cbPickupPoint.Name = "cbPickupPoint";
            cbPickupPoint.Size = new Size(250, 28);
            cbPickupPoint.TabIndex = 5;
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(433, 207);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(249, 28);
            cbStatus.TabIndex = 6;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new Point(712, 47);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersWidth = 51;
            dgvItems.Size = new Size(491, 285);
            dgvItems.TabIndex = 7;
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(249, 425);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(94, 29);
            btnAddItem.TabIndex = 8;
            btnAddItem.Text = "Добавить";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Location = new Point(391, 425);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(94, 29);
            btnRemoveItem.TabIndex = 9;
            btnRemoveItem.Text = "Удалить";
            btnRemoveItem.UseVisualStyleBackColor = true;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(546, 425);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 10;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(685, 425);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 52);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 12;
            label1.Text = "№ Заказа";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 103);
            label2.Name = "label2";
            label2.Size = new Size(103, 20);
            label2.TabIndex = 13;
            label2.Text = "ФИО Клиента";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 151);
            label3.Name = "label3";
            label3.Size = new Size(163, 20);
            label3.TabIndex = 14;
            label3.Text = "Код получения заказа";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(319, 52);
            label4.Name = "label4";
            label4.Size = new Size(90, 20);
            label4.TabIndex = 15;
            label4.Text = "Дата заказа";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(319, 103);
            label5.Name = "label5";
            label5.Size = new Size(107, 20);
            label5.TabIndex = 16;
            label5.Text = "Дата доставки";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(321, 157);
            label6.Name = "label6";
            label6.Size = new Size(105, 20);
            label6.TabIndex = 17;
            label6.Text = "Пункт выдачи";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(325, 215);
            label7.Name = "label7";
            label7.Size = new Size(101, 20);
            label7.TabIndex = 18;
            label7.Text = "Статус заказа";
            // 
            // OrderEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1298, 511);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnRemoveItem);
            Controls.Add(btnAddItem);
            Controls.Add(dgvItems);
            Controls.Add(cbStatus);
            Controls.Add(cbPickupPoint);
            Controls.Add(dtpDeliveryDate);
            Controls.Add(dtpOrderDate);
            Controls.Add(txtPickupCode);
            Controls.Add(txtClientFullName);
            Controls.Add(txtOrderNumber);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrderEditForm";
            Text = "Редактирование заказа";
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtOrderNumber;
        private TextBox txtClientFullName;
        private TextBox txtPickupCode;
        private DateTimePicker dtpOrderDate;
        private DateTimePicker dtpDeliveryDate;
        private ComboBox cbPickupPoint;
        private ComboBox cbStatus;
        private DataGridView dgvItems;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}