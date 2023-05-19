
namespace GestionInventario.Vista
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnBuys = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnCloseSession = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.BackColor = System.Drawing.Color.LightSteelBlue;
            this.mainLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainLayout.Controls.Add(this.btnUsers);
            this.mainLayout.Controls.Add(this.btnSales);
            this.mainLayout.Controls.Add(this.btnBuys);
            this.mainLayout.Controls.Add(this.btnProducts);
            this.mainLayout.Controls.Add(this.btnCloseSession);
            this.mainLayout.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(20);
            this.mainLayout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainLayout.Size = new System.Drawing.Size(318, 239);
            this.mainLayout.TabIndex = 0;
            // 
            // btnUsers
            // 
            this.btnUsers.BackgroundImage = global::GestionInventario.Properties.Resources.re_user;
            this.btnUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.Location = new System.Drawing.Point(23, 23);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(75, 90);
            this.btnUsers.TabIndex = 0;
            this.btnUsers.Text = "Usuarios";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSales
            // 
            this.btnSales.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSales.BackgroundImage")));
            this.btnSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSales.Location = new System.Drawing.Point(104, 23);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(75, 90);
            this.btnSales.TabIndex = 1;
            this.btnSales.Text = "Ventas";
            this.btnSales.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSales.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSales.UseVisualStyleBackColor = true;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnBuys
            // 
            this.btnBuys.BackgroundImage = global::GestionInventario.Properties.Resources.re_buy;
            this.btnBuys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuys.Location = new System.Drawing.Point(185, 23);
            this.btnBuys.Name = "btnBuys";
            this.btnBuys.Size = new System.Drawing.Size(75, 90);
            this.btnBuys.TabIndex = 2;
            this.btnBuys.Text = "Compras";
            this.btnBuys.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuys.UseVisualStyleBackColor = true;
            // 
            // btnProducts
            // 
            this.btnProducts.BackgroundImage = global::GestionInventario.Properties.Resources.re_package;
            this.btnProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProducts.Location = new System.Drawing.Point(23, 119);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(75, 90);
            this.btnProducts.TabIndex = 4;
            this.btnProducts.Text = "Productos";
            this.btnProducts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnCloseSession
            // 
            this.btnCloseSession.BackgroundImage = global::GestionInventario.Properties.Resources.re_exit;
            this.btnCloseSession.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCloseSession.Location = new System.Drawing.Point(104, 119);
            this.btnCloseSession.Name = "btnCloseSession";
            this.btnCloseSession.Size = new System.Drawing.Size(75, 90);
            this.btnCloseSession.TabIndex = 3;
            this.btnCloseSession.Text = "Salir";
            this.btnCloseSession.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCloseSession.UseVisualStyleBackColor = true;
            this.btnCloseSession.Click += new System.EventHandler(this.button4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 239);
            this.Controls.Add(this.mainLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Inventario";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mainLayout;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnBuys;
        private System.Windows.Forms.Button btnCloseSession;
        private System.Windows.Forms.Button btnProducts;
    }
}

