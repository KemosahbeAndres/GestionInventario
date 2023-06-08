
namespace GestionInventario.Vista
{
    partial class ProductsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.buttonsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.txtNewCategory = new System.Windows.Forms.TextBox();
            this.categoryList = new System.Windows.Forms.ListBox();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.topLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.listProductsView = new System.Windows.Forms.ListView();
            this.colEmpty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEAN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnModifyCategory = new System.Windows.Forms.Button();
            this.panelLeft.SuspendLayout();
            this.buttonsLayout.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.topLayout.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.buttonsLayout);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 541);
            this.panelLeft.TabIndex = 0;
            // 
            // buttonsLayout
            // 
            this.buttonsLayout.Controls.Add(this.btnAddCategory);
            this.buttonsLayout.Controls.Add(this.txtNewCategory);
            this.buttonsLayout.Controls.Add(this.categoryList);
            this.buttonsLayout.Controls.Add(this.btnDeleteCategory);
            this.buttonsLayout.Controls.Add(this.btnModifyCategory);
            this.buttonsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.buttonsLayout.Location = new System.Drawing.Point(0, 0);
            this.buttonsLayout.Name = "buttonsLayout";
            this.buttonsLayout.Padding = new System.Windows.Forms.Padding(10);
            this.buttonsLayout.Size = new System.Drawing.Size(200, 541);
            this.buttonsLayout.TabIndex = 0;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.Location = new System.Drawing.Point(10, 10);
            this.btnAddCategory.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(180, 30);
            this.btnAddCategory.TabIndex = 0;
            this.btnAddCategory.Text = "Agregar Categoria";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // txtNewCategory
            // 
            this.txtNewCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewCategory.Location = new System.Drawing.Point(10, 50);
            this.txtNewCategory.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.txtNewCategory.Name = "txtNewCategory";
            this.txtNewCategory.Size = new System.Drawing.Size(180, 24);
            this.txtNewCategory.TabIndex = 1;
            // 
            // categoryList
            // 
            this.categoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryList.FormattingEnabled = true;
            this.categoryList.ItemHeight = 18;
            this.categoryList.Location = new System.Drawing.Point(13, 87);
            this.categoryList.Name = "categoryList";
            this.categoryList.Size = new System.Drawing.Size(174, 274);
            this.categoryList.TabIndex = 2;
            this.categoryList.SelectedIndexChanged += new System.EventHandler(this.categoryList_SelectedIndexChanged);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(10, 374);
            this.btnDeleteCategory.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(180, 30);
            this.btnDeleteCategory.TabIndex = 3;
            this.btnDeleteCategory.Text = "Borrar Categoria";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.topLayout);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelTop.Location = new System.Drawing.Point(200, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(769, 40);
            this.panelTop.TabIndex = 100;
            // 
            // topLayout
            // 
            this.topLayout.Controls.Add(this.txtSearch);
            this.topLayout.Controls.Add(this.btnSearch);
            this.topLayout.Controls.Add(this.btnRefresh);
            this.topLayout.Controls.Add(this.btnAddProduct);
            this.topLayout.Controls.Add(this.btnEditProduct);
            this.topLayout.Controls.Add(this.btnDeleteProduct);
            this.topLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLayout.Location = new System.Drawing.Point(0, 0);
            this.topLayout.Name = "topLayout";
            this.topLayout.Padding = new System.Windows.Forms.Padding(5);
            this.topLayout.Size = new System.Drawing.Size(769, 40);
            this.topLayout.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(8, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(198, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(212, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(293, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.Location = new System.Drawing.Point(389, 8);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(90, 23);
            this.btnAddProduct.TabIndex = 3;
            this.btnAddProduct.Text = "Nuevo";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProduct.Location = new System.Drawing.Point(485, 8);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.Size = new System.Drawing.Size(87, 23);
            this.btnEditProduct.TabIndex = 4;
            this.btnEditProduct.Text = "Modificar";
            this.btnEditProduct.UseVisualStyleBackColor = true;
            this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteProduct.Location = new System.Drawing.Point(578, 8);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(82, 23);
            this.btnDeleteProduct.TabIndex = 5;
            this.btnDeleteProduct.Text = "Borrar";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.listProductsView);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(200, 40);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(769, 501);
            this.panelContent.TabIndex = 101;
            // 
            // listProductsView
            // 
            this.listProductsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEmpty,
            this.columnID,
            this.columnEAN,
            this.columnName,
            this.columnCat,
            this.columnCost,
            this.columnStock,
            this.columnDesc});
            this.listProductsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listProductsView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listProductsView.FullRowSelect = true;
            this.listProductsView.GridLines = true;
            this.listProductsView.HideSelection = false;
            this.listProductsView.Location = new System.Drawing.Point(0, 0);
            this.listProductsView.Name = "listProductsView";
            this.listProductsView.Size = new System.Drawing.Size(769, 501);
            this.listProductsView.TabIndex = 0;
            this.listProductsView.UseCompatibleStateImageBehavior = false;
            this.listProductsView.View = System.Windows.Forms.View.Details;
            this.listProductsView.SelectedIndexChanged += new System.EventHandler(this.listProductsView_SelectedIndexChanged);
            // 
            // colEmpty
            // 
            this.colEmpty.Text = "";
            this.colEmpty.Width = 20;
            // 
            // columnID
            // 
            this.columnID.Text = "ID";
            this.columnID.Width = 30;
            // 
            // columnEAN
            // 
            this.columnEAN.Text = "Codigo EAN";
            this.columnEAN.Width = 105;
            // 
            // columnName
            // 
            this.columnName.Text = "Producto";
            this.columnName.Width = 109;
            // 
            // columnCat
            // 
            this.columnCat.Text = "Categoria";
            this.columnCat.Width = 104;
            // 
            // columnCost
            // 
            this.columnCost.Text = "Precio";
            this.columnCost.Width = 85;
            // 
            // columnStock
            // 
            this.columnStock.Text = "Inventario";
            this.columnStock.Width = 80;
            // 
            // columnDesc
            // 
            this.columnDesc.Text = "Descripcion";
            this.columnDesc.Width = 219;
            // 
            // btnModifyCategory
            // 
            this.btnModifyCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyCategory.Location = new System.Drawing.Point(10, 424);
            this.btnModifyCategory.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.btnModifyCategory.Name = "btnModifyCategory";
            this.btnModifyCategory.Size = new System.Drawing.Size(180, 30);
            this.btnModifyCategory.TabIndex = 3;
            this.btnModifyCategory.Text = "Modificar Categoria";
            this.btnModifyCategory.UseVisualStyleBackColor = true;
            this.btnModifyCategory.Click += new System.EventHandler(this.btnModifyCategory_Click);
            // 
            // ProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 541);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Inventario - Productos";
            this.Load += new System.EventHandler(this.ProductsForm_Load);
            this.panelLeft.ResumeLayout(false);
            this.buttonsLayout.ResumeLayout(false);
            this.buttonsLayout.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.topLayout.ResumeLayout(false);
            this.topLayout.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.FlowLayoutPanel buttonsLayout;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.ListView listProductsView;
        private System.Windows.Forms.ColumnHeader colEmpty;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnEAN;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnCat;
        private System.Windows.Forms.ColumnHeader columnCost;
        private System.Windows.Forms.ColumnHeader columnStock;
        private System.Windows.Forms.ColumnHeader columnDesc;
        private System.Windows.Forms.FlowLayoutPanel topLayout;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtNewCategory;
        private System.Windows.Forms.ListBox categoryList;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnEditProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnModifyCategory;
    }
}