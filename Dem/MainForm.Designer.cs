namespace Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dgvProducts = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnManageOrders = new Button();
            panelFilter = new Panel();
            label4 = new Label();
            btnFilte = new Button();
            label3 = new Label();
            label2 = new Label();
            cbSortBy = new ComboBox();
            cbManufacturer = new ComboBox();
            cbCategory = new ComboBox();
            label1 = new Label();
            txtSearch = new TextBox();
            pbProductImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbProductImage).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(12, 12);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.Size = new Size(776, 394);
            dgvProducts.TabIndex = 0;
            dgvProducts.RowPrePaint += dgvProducts_RowPrePaint;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(73, 467);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(189, 467);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(94, 29);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(299, 467);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnManageOrders
            // 
            btnManageOrders.Location = new Point(418, 467);
            btnManageOrders.Name = "btnManageOrders";
            btnManageOrders.Size = new Size(185, 29);
            btnManageOrders.TabIndex = 4;
            btnManageOrders.Text = "Управление заказами";
            btnManageOrders.UseVisualStyleBackColor = true;
            btnManageOrders.Click += btnManageOrders_Click;
            // 
            // panelFilter
            // 
            panelFilter.Controls.Add(label4);
            panelFilter.Controls.Add(btnFilte);
            panelFilter.Controls.Add(label3);
            panelFilter.Controls.Add(label2);
            panelFilter.Controls.Add(cbSortBy);
            panelFilter.Controls.Add(cbManufacturer);
            panelFilter.Controls.Add(cbCategory);
            panelFilter.Controls.Add(label1);
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Location = new Point(59, 540);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(942, 109);
            panelFilter.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(437, 68);
            label4.Name = "label4";
            label4.Size = new Size(92, 20);
            label4.TabIndex = 7;
            label4.Text = "Сортировка";
            // 
            // btnFilte
            // 
            btnFilte.Location = new Point(732, 17);
            btnFilte.Name = "btnFilte";
            btnFilte.Size = new Size(154, 29);
            btnFilte.TabIndex = 6;
            btnFilte.Text = "Применить";
            btnFilte.UseVisualStyleBackColor = true;
            btnFilte.Click += btnFilte_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 68);
            label3.Name = "label3";
            label3.Size = new Size(118, 20);
            label3.TabIndex = 6;
            label3.Text = "Производитель";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(437, 21);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 5;
            label2.Text = "Категория";
            // 
            // cbSortBy
            // 
            cbSortBy.FormattingEnabled = true;
            cbSortBy.Location = new Point(534, 60);
            cbSortBy.Name = "cbSortBy";
            cbSortBy.Size = new Size(151, 28);
            cbSortBy.TabIndex = 4;
            // 
            // cbManufacturer
            // 
            cbManufacturer.FormattingEnabled = true;
            cbManufacturer.Location = new Point(138, 65);
            cbManufacturer.Name = "cbManufacturer";
            cbManufacturer.Size = new Size(186, 28);
            cbManufacturer.TabIndex = 3;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(534, 18);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(151, 28);
            cbCategory.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 17);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 1;
            label1.Text = "Поиск";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(138, 14);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(186, 27);
            txtSearch.TabIndex = 0;
            // 
            // pbProductImage
            // 
            pbProductImage.Location = new Point(846, 12);
            pbProductImage.Name = "pbProductImage";
            pbProductImage.Size = new Size(377, 273);
            pbProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbProductImage.TabIndex = 7;
            pbProductImage.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1276, 674);
            Controls.Add(pbProductImage);
            Controls.Add(panelFilter);
            Controls.Add(btnManageOrders);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvProducts);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbProductImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProducts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnManageOrders;
        private Panel panelFilter;
        private Label label1;
        private TextBox txtSearch;
        private Button btnFilte;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox cbSortBy;
        private ComboBox cbManufacturer;
        private ComboBox cbCategory;
        private PictureBox pbProductImage;
    }
}