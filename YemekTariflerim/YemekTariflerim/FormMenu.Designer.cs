namespace YemekTariflerim
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTarifSil = new System.Windows.Forms.Button();
            this.btnTarifGuncelle = new System.Windows.Forms.Button();
            this.btnTarifEkle = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Name = "label1";
            // 
            // btnTarifSil
            // 
            this.btnTarifSil.BackColor = System.Drawing.Color.Linen;
            resources.ApplyResources(this.btnTarifSil, "btnTarifSil");
            this.btnTarifSil.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnTarifSil.Name = "btnTarifSil";
            this.btnTarifSil.UseVisualStyleBackColor = false;
            this.btnTarifSil.Click += new System.EventHandler(this.btnTarifSil_Click_1);
            // 
            // btnTarifGuncelle
            // 
            this.btnTarifGuncelle.BackColor = System.Drawing.Color.Linen;
            resources.ApplyResources(this.btnTarifGuncelle, "btnTarifGuncelle");
            this.btnTarifGuncelle.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnTarifGuncelle.Name = "btnTarifGuncelle";
            this.btnTarifGuncelle.UseVisualStyleBackColor = false;
            this.btnTarifGuncelle.Click += new System.EventHandler(this.btnTarifGuncelle_Click);
            // 
            // btnTarifEkle
            // 
            this.btnTarifEkle.BackColor = System.Drawing.Color.Linen;
            resources.ApplyResources(this.btnTarifEkle, "btnTarifEkle");
            this.btnTarifEkle.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnTarifEkle.Name = "btnTarifEkle";
            this.btnTarifEkle.UseVisualStyleBackColor = false;
            this.btnTarifEkle.Click += new System.EventHandler(this.btnTarifEkle_Click);
            // 
            // FormMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTarifEkle);
            this.Controls.Add(this.btnTarifGuncelle);
            this.Controls.Add(this.btnTarifSil);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMenu";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTarifSil;
        private System.Windows.Forms.Button btnTarifGuncelle;
        private System.Windows.Forms.Button btnTarifEkle;
    }
}