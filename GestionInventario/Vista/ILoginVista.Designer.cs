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
            this.SuspendLayout();
            // 
            // txtRut
            // 
            this.txtRut.Location = new System.Drawing.Point(255, 195);
            this.txtRut.Name = "txtRut";
            this.txtRut.Size = new System.Drawing.Size(135, 22);
            this.txtRut.TabIndex = 0;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(255, 256);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(135, 22);
            this.txtContraseña.TabIndex = 1;
            // 
            // labelRut
            // 
            this.labelRut.AutoSize = true;
            this.labelRut.Location = new System.Drawing.Point(141, 201);
            this.labelRut.Name = "labelRut";
            this.labelRut.Size = new System.Drawing.Size(30, 16);
            this.labelRut.TabIndex = 2;
            this.labelRut.Text = "Rut:";
            // 
            // labelContra
            // 
            this.labelContra.AutoSize = true;
            this.labelContra.Location = new System.Drawing.Point(141, 259);
            this.labelContra.Name = "labelContra";
            this.labelContra.Size = new System.Drawing.Size(79, 16);
            this.labelContra.TabIndex = 3;
            this.labelContra.Text = "Contraseña:";
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Location = new System.Drawing.Point(189, 316);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(111, 23);
            this.btnIniciarSesion.TabIndex = 4;
            this.btnIniciarSesion.Text = "Iniciar sesion";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 450);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.labelContra);
            this.Controls.Add(this.labelRut);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtRut);
            this.Name = "LoginForm";
            this.Text = "ILoginVista";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRut;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label labelRut;
        private System.Windows.Forms.Label labelContra;
        private System.Windows.Forms.Button btnIniciarSesion;
    }
}