namespace GestionInventario.Vista
{
    partial class LoginForm
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
            this.txtRut = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.labelRut = new System.Windows.Forms.Label();
            this.labelContra = new System.Windows.Forms.Label();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRut
            // 
            this.txtRut.Location = new System.Drawing.Point(133, 77);
            this.txtRut.Margin = new System.Windows.Forms.Padding(2);
            this.txtRut.Name = "txtRut";
            this.txtRut.Size = new System.Drawing.Size(102, 20);
            this.txtRut.TabIndex = 0;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(133, 127);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(2);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(102, 20);
            this.txtContraseña.TabIndex = 1;
            // 
            // labelRut
            // 
            this.labelRut.AutoSize = true;
            this.labelRut.Location = new System.Drawing.Point(48, 82);
            this.labelRut.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRut.Name = "labelRut";
            this.labelRut.Size = new System.Drawing.Size(41, 13);
            this.labelRut.TabIndex = 2;
            this.labelRut.Text = "Correo:";
            // 
            // labelContra
            // 
            this.labelContra.AutoSize = true;
            this.labelContra.Location = new System.Drawing.Point(48, 129);
            this.labelContra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelContra.Name = "labelContra";
            this.labelContra.Size = new System.Drawing.Size(64, 13);
            this.labelContra.TabIndex = 3;
            this.labelContra.Text = "Contraseña:";
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Location = new System.Drawing.Point(152, 180);
            this.btnIniciarSesion.Margin = new System.Windows.Forms.Padding(2);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(83, 26);
            this.btnIniciarSesion.TabIndex = 4;
            this.btnIniciarSesion.Text = "Iniciar sesion";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(37, 183);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 269);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.labelContra);
            this.Controls.Add(this.labelRut);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtRut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRut;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label labelRut;
        private System.Windows.Forms.Label labelContra;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.Button btnExit;
    }
}