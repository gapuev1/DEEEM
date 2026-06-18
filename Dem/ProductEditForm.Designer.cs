namespace Demo
{
    partial class ProductEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductEditForm));
            btnCancel = new Button();
            btnSave = new Button();
            txtPhoto = new TextBox();
            txtDescription = new TextBox();
            txtStock = new TextBox();
            txtDiscount = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtPrice = new TextBox();
            txtUnit = new TextBox();
            txtName = new TextBox();
            txtArticle = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            btnSelectPhoto = new Button();
            pbProductImage = new PictureBox();
            cbCategory = new ComboBox();
            cbManufacturer = new ComboBox();
            cbSupplier = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pbProductImage).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(516, 385);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 31;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(251, 385);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 30;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // txtPhoto
            // 
            txtPhoto.Location = new Point(533, 172);
            txtPhoto.Name = "txtPhoto";
            txtPhoto.Size = new Size(125, 27);
            txtPhoto.TabIndex = 29;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(533, 129);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 28;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(533, 82);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(125, 27);
            txtStock.TabIndex = 27;
            // 
            // txtDiscount
            // 
            txtDiscount.Location = new Point(533, 37);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(125, 27);
            txtDiscount.TabIndex = 26;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(78, 129);
            label3.Name = "label3";
            label3.Size = new Size(151, 20);
            label3.TabIndex = 25;
            label3.Text = "Единица измерения";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(152, 82);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 24;
            label2.Text = "Название";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(143, 38);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 23;
            label1.Text = "Артикул";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(243, 172);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 19;
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(243, 126);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(125, 27);
            txtUnit.TabIndex = 18;
            // 
            // txtName
            // 
            txtName.Location = new Point(243, 79);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 17;
            // 
            // txtArticle
            // 
            txtArticle.Location = new Point(243, 37);
            txtArticle.Name = "txtArticle";
            txtArticle.Size = new Size(125, 27);
            txtArticle.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(152, 175);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 32;
            label4.Text = "Цена";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(134, 223);
            label5.Name = "label5";
            label5.Size = new Size(86, 20);
            label5.TabIndex = 33;
            label5.Text = "Поставщик";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(152, 268);
            label6.Name = "label6";
            label6.Size = new Size(81, 20);
            label6.TabIndex = 34;
            label6.Text = "Категория";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(119, 317);
            label7.Name = "label7";
            label7.Size = new Size(118, 20);
            label7.TabIndex = 35;
            label7.Text = "Производитель";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(409, 40);
            label8.Name = "label8";
            label8.Size = new Size(57, 20);
            label8.TabIndex = 36;
            label8.Text = "Скидка";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(409, 89);
            label9.Name = "label9";
            label9.Size = new Size(58, 20);
            label9.TabIndex = 37;
            label9.Text = "Кол-во";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(409, 136);
            label10.Name = "label10";
            label10.Size = new Size(79, 20);
            label10.TabIndex = 38;
            label10.Text = "Описание";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(409, 175);
            label11.Name = "label11";
            label11.Size = new Size(44, 20);
            label11.TabIndex = 39;
            label11.Text = "Фото";
            // 
            // btnSelectPhoto
            // 
            btnSelectPhoto.Location = new Point(516, 339);
            btnSelectPhoto.Name = "btnSelectPhoto";
            btnSelectPhoto.Size = new Size(217, 29);
            btnSelectPhoto.TabIndex = 40;
            btnSelectPhoto.Text = "Добавить фото";
            btnSelectPhoto.UseVisualStyleBackColor = true;
            btnSelectPhoto.Click += btnSelectPhoto_Click;
            // 
            // pbProductImage
            // 
            pbProductImage.Location = new Point(516, 216);
            pbProductImage.Name = "pbProductImage";
            pbProductImage.Size = new Size(217, 107);
            pbProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbProductImage.TabIndex = 41;
            pbProductImage.TabStop = false;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(243, 264);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(151, 28);
            cbCategory.TabIndex = 42;
            // 
            // cbManufacturer
            // 
            cbManufacturer.FormattingEnabled = true;
            cbManufacturer.Location = new Point(243, 314);
            cbManufacturer.Name = "cbManufacturer";
            cbManufacturer.Size = new Size(151, 28);
            cbManufacturer.TabIndex = 43;
            // 
            // cbSupplier
            // 
            cbSupplier.FormattingEnabled = true;
            cbSupplier.Location = new Point(243, 216);
            cbSupplier.Name = "cbSupplier";
            cbSupplier.Size = new Size(151, 28);
            cbSupplier.TabIndex = 44;
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbSupplier);
            Controls.Add(cbManufacturer);
            Controls.Add(cbCategory);
            Controls.Add(pbProductImage);
            Controls.Add(btnSelectPhoto);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtPhoto);
            Controls.Add(txtDescription);
            Controls.Add(txtStock);
            Controls.Add(txtDiscount);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPrice);
            Controls.Add(txtUnit);
            Controls.Add(txtName);
            Controls.Add(txtArticle);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProductEditForm";
            Text = "Редактирование товара";
            ((System.ComponentModel.ISupportInitialize)pbProductImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private TextBox txtPhoto;
        private TextBox txtDescription;
        private TextBox txtStock;
        private TextBox txtDiscount;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtPrice;
        private TextBox txtUnit;
        private TextBox txtName;
        private TextBox txtArticle;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Button btnSelectPhoto;
        private PictureBox pbProductImage;
        private ComboBox cbCategory;
        private ComboBox cbManufacturer;
        private ComboBox cbSupplier;
    }
}