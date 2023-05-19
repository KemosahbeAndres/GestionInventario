
namespace GestionInventario.Vista
{
    partial class usersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usersForm));
            this.usersListView = new System.Windows.Forms.ListView();
            this.colEmpty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRole = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.usersLeftLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnModifyUser = new System.Windows.Forms.Button();
            this.btnUserDelete = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblConection = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.usersLeftPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.usersLeftLayout.SuspendLayout();
            this.usersLeftPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // usersListView
            // 
            this.usersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEmpty,
            this.colId,
            this.colName,
            this.colEmail,
            this.colPhone,
            this.colRole});
            this.usersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersListView.FullRowSelect = true;
            this.usersListView.GridLines = true;
            this.usersListView.HideSelection = false;
            this.usersListView.Location = new System.Drawing.Point(0, 0);
            this.usersListView.MultiSelect = false;
            this.usersListView.Name = "usersListView";
            this.usersListView.Size = new System.Drawing.Size(695, 541);
            this.usersListView.TabIndex = 2;
            this.usersListView.UseCompatibleStateImageBehavior = false;
            this.usersListView.View = System.Windows.Forms.View.Details;
            this.usersListView.SelectedIndexChanged += new System.EventHandler(this.usersListView_SelectedIndexChanged);
            // 
            // colEmpty
            // 
            this.colEmpty.Text = "";
            this.colEmpty.Width = 19;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 38;
            // 
            // colName
            // 
            this.colName.Text = "Nombre";
            this.colName.Width = 182;
            // 
            // colEmail
            // 
            this.colEmail.Text = "Correo";
            this.colEmail.Width = 193;
            // 
            // colPhone
            // 
            this.colPhone.Text = "Telefono";
            this.colPhone.Width = 115;
            // 
            // colRole
            // 
            this.colRole.Text = "Rol";
            this.colRole.Width = 133;
            // 
            // usersLeftLayout
            // 
            this.usersLeftLayout.Controls.Add(this.btnRefresh);
            this.usersLeftLayout.Controls.Add(this.btnCreateUser);
            this.usersLeftLayout.Controls.Add(this.btnModifyUser);
            this.usersLeftLayout.Controls.Add(this.btnUserDelete);
            this.usersLeftLayout.Controls.Add(this.btnAccept);
            this.usersLeftLayout.Controls.Add(this.lblSelected);
            this.usersLeftLayout.Controls.Add(this.lblConection);
            this.usersLeftLayout.Controls.Add(this.lblData);
            this.usersLeftLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersLeftLayout.Location = new System.Drawing.Point(0, 0);
            this.usersLeftLayout.Name = "usersLeftLayout";
            this.usersLeftLayout.Size = new System.Drawing.Size(200, 541);
            this.usersLeftLayout.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::GestionInventario.Properties.Resources.re_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(10, 10);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(180, 75);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateUser.Image = global::GestionInventario.Properties.Resources.re_add_user;
            this.btnCreateUser.Location = new System.Drawing.Point(10, 95);
            this.btnCreateUser.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(180, 75);
            this.btnCreateUser.TabIndex = 1;
            this.btnCreateUser.Text = "Nuevo";
            this.btnCreateUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // btnModifyUser
            // 
            this.btnModifyUser.Image = global::GestionInventario.Properties.Resources.re_edit;
            this.btnModifyUser.Location = new System.Drawing.Point(10, 180);
            this.btnModifyUser.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnModifyUser.Name = "btnModifyUser";
            this.btnModifyUser.Size = new System.Drawing.Size(180, 75);
            this.btnModifyUser.TabIndex = 2;
            this.btnModifyUser.Text = "Modificar";
            this.btnModifyUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModifyUser.UseVisualStyleBackColor = true;
            this.btnModifyUser.Click += new System.EventHandler(this.btnModifyUser_Click);
            // 
            // btnUserDelete
            // 
            this.btnUserDelete.Image = global::GestionInventario.Properties.Resources.re_delete;
            this.btnUserDelete.Location = new System.Drawing.Point(10, 265);
            this.btnUserDelete.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnUserDelete.Name = "btnUserDelete";
            this.btnUserDelete.Size = new System.Drawing.Size(180, 75);
            this.btnUserDelete.TabIndex = 3;
            this.btnUserDelete.Text = "Eliminar";
            this.btnUserDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserDelete.UseVisualStyleBackColor = true;
            this.btnUserDelete.Click += new System.EventHandler(this.btnUserDelete_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(10, 355);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(180, 40);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Visible = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(10, 415);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.lblSelected.MinimumSize = new System.Drawing.Size(180, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(180, 13);
            this.lblSelected.TabIndex = 5;
            this.lblSelected.Text = "No hay seleccion";
            this.lblSelected.Visible = false;
            // 
            // lblConection
            // 
            this.lblConection.AutoSize = true;
            this.lblConection.Location = new System.Drawing.Point(10, 448);
            this.lblConection.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.lblConection.MinimumSize = new System.Drawing.Size(180, 0);
            this.lblConection.Name = "lblConection";
            this.lblConection.Size = new System.Drawing.Size(180, 13);
            this.lblConection.TabIndex = 6;
            this.lblConection.Text = "...";
            this.lblConection.Visible = false;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.usersLeftLayout.SetFlowBreak(this.lblData, true);
            this.lblData.Location = new System.Drawing.Point(10, 481);
            this.lblData.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.lblData.MinimumSize = new System.Drawing.Size(180, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(180, 13);
            this.lblData.TabIndex = 7;
            this.lblData.Text = "Datos: ";
            this.lblData.Visible = false;
            // 
            // usersLeftPanel
            // 
            this.usersLeftPanel.Controls.Add(this.usersLeftLayout);
            this.usersLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.usersLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.usersLeftPanel.Name = "usersLeftPanel";
            this.usersLeftPanel.Size = new System.Drawing.Size(200, 541);
            this.usersLeftPanel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.usersListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 541);
            this.panel1.TabIndex = 5;
            // 
            // usersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.usersLeftPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "usersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestion Inventario - Usuarios";
            this.Load += new System.EventHandler(this.usersForm_Load);
            this.usersLeftLayout.ResumeLayout(false);
            this.usersLeftLayout.PerformLayout();
            this.usersLeftPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView usersListView;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colEmail;
        private System.Windows.Forms.ColumnHeader colPhone;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.FlowLayoutPanel usersLeftLayout;
        private System.Windows.Forms.Button btnModifyUser;
        private System.Windows.Forms.Panel usersLeftPanel;
        private System.Windows.Forms.Button btnUserDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblConection;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.ColumnHeader colEmpty;
        private System.Windows.Forms.ColumnHeader colRole;
    }
}