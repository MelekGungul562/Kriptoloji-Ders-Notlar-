namespace KRiptoloji_odev_deneme
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtGiris = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYontem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAnahtar1 = new System.Windows.Forms.TextBox();
            this.txtAnahtar2 = new System.Windows.Forms.TextBox();
            this.lblAnahtar2 = new System.Windows.Forms.Label();
            this.btnSifrele = new System.Windows.Forms.Button();
            this.btnCoz = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCikti = new System.Windows.Forms.TextBox();
            this.grpEmail = new System.Windows.Forms.GroupBox();
            this.btnIndir = new System.Windows.Forms.Button();
            this.btnGonder = new System.Windows.Forms.Button();
            this.txtAliciEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Metin Girişi:";
            // 
            // txtGiris
            // 
            this.txtGiris.Location = new System.Drawing.Point(20, 43);
            this.txtGiris.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGiris.Multiline = true;
            this.txtGiris.Name = "txtGiris";
            this.txtGiris.Size = new System.Drawing.Size(425, 122);
            this.txtGiris.TabIndex = 1;
            this.txtGiris.TextChanged += new System.EventHandler(this.txtGiris_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(480, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Şifreleme Yöntemi:";
            // 
            // cmbYontem
            // 
            this.cmbYontem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYontem.FormattingEnabled = true;
            this.cmbYontem.Location = new System.Drawing.Point(484, 43);
            this.cmbYontem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbYontem.Name = "cmbYontem";
            this.cmbYontem.Size = new System.Drawing.Size(332, 24);
            this.cmbYontem.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Anahtar 1 / Kelime:";
            // 
            // txtAnahtar1
            // 
            this.txtAnahtar1.Location = new System.Drawing.Point(484, 106);
            this.txtAnahtar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAnahtar1.Name = "txtAnahtar1";
            this.txtAnahtar1.Size = new System.Drawing.Size(159, 22);
            this.txtAnahtar1.TabIndex = 5;
            // 
            // txtAnahtar2
            // 
            this.txtAnahtar2.Location = new System.Drawing.Point(657, 106);
            this.txtAnahtar2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAnahtar2.Name = "txtAnahtar2";
            this.txtAnahtar2.Size = new System.Drawing.Size(159, 22);
            this.txtAnahtar2.TabIndex = 7;
            // 
            // lblAnahtar2
            // 
            this.lblAnahtar2.AutoSize = true;
            this.lblAnahtar2.Location = new System.Drawing.Point(653, 86);
            this.lblAnahtar2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnahtar2.Name = "lblAnahtar2";
            this.lblAnahtar2.Size = new System.Drawing.Size(66, 16);
            this.lblAnahtar2.TabIndex = 6;
            this.lblAnahtar2.Text = "Anahtar 2:";
            // 
            // btnSifrele
            // 
            this.btnSifrele.Location = new System.Drawing.Point(484, 150);
            this.btnSifrele.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSifrele.Name = "btnSifrele";
            this.btnSifrele.Size = new System.Drawing.Size(160, 49);
            this.btnSifrele.TabIndex = 8;
            this.btnSifrele.Text = "ŞİFRELE";
            this.btnSifrele.UseVisualStyleBackColor = true;
            this.btnSifrele.Click += new System.EventHandler(this.btnSifrele_Click);
            // 
            // btnCoz
            // 
            this.btnCoz.Location = new System.Drawing.Point(657, 150);
            this.btnCoz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCoz.Name = "btnCoz";
            this.btnCoz.Size = new System.Drawing.Size(160, 49);
            this.btnCoz.TabIndex = 9;
            this.btnCoz.Text = "ÇÖZ";
            this.btnCoz.UseVisualStyleBackColor = true;
            this.btnCoz.Click += new System.EventHandler(this.btnCoz_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 197);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Sonuç Metni:";
            // 
            // txtCikti
            // 
            this.txtCikti.Location = new System.Drawing.Point(20, 217);
            this.txtCikti.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCikti.Multiline = true;
            this.txtCikti.Name = "txtCikti";
            this.txtCikti.Size = new System.Drawing.Size(425, 122);
            this.txtCikti.TabIndex = 11;
            // 
            // grpEmail
            // 
            this.grpEmail.Controls.Add(this.btnIndir);
            this.grpEmail.Controls.Add(this.btnGonder);
            this.grpEmail.Controls.Add(this.txtAliciEmail);
            this.grpEmail.Controls.Add(this.label5);
            this.grpEmail.Location = new System.Drawing.Point(20, 369);
            this.grpEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEmail.Name = "grpEmail";
            this.grpEmail.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEmail.Size = new System.Drawing.Size(797, 98);
            this.grpEmail.TabIndex = 12;
            this.grpEmail.TabStop = false;
            this.grpEmail.Text = "E-posta İşlemleri";
            // 
            // btnIndir
            // 
            this.btnIndir.Location = new System.Drawing.Point(597, 62);
            this.btnIndir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIndir.Name = "btnIndir";
            this.btnIndir.Size = new System.Drawing.Size(160, 37);
            this.btnIndir.TabIndex = 3;
            this.btnIndir.Text = "E-Posta İndir";
            this.btnIndir.UseVisualStyleBackColor = true;
            this.btnIndir.Click += new System.EventHandler(this.btnIndir_Click);
            // 
            // btnGonder
            // 
            this.btnGonder.Location = new System.Drawing.Point(597, 25);
            this.btnGonder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(160, 37);
            this.btnGonder.TabIndex = 2;
            this.btnGonder.Text = "E-Posta Gönder";
            this.btnGonder.UseVisualStyleBackColor = true;
            this.btnGonder.Click += new System.EventHandler(this.btnGonder_Click);
            // 
            // txtAliciEmail
            // 
            this.txtAliciEmail.Location = new System.Drawing.Point(113, 44);
            this.txtAliciEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAliciEmail.Name = "txtAliciEmail";
            this.txtAliciEmail.Size = new System.Drawing.Size(452, 22);
            this.txtAliciEmail.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Alıcı Email:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 492);
            this.Controls.Add(this.grpEmail);
            this.Controls.Add(this.txtCikti);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCoz);
            this.Controls.Add(this.btnSifrele);
            this.Controls.Add(this.txtAnahtar2);
            this.Controls.Add(this.lblAnahtar2);
            this.Controls.Add(this.txtAnahtar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbYontem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGiris);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Kriptoloji Ödevi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpEmail.ResumeLayout(false);
            this.grpEmail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGiris;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYontem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAnahtar1;
        private System.Windows.Forms.TextBox txtAnahtar2;
        private System.Windows.Forms.Label lblAnahtar2;
        private System.Windows.Forms.Button btnSifrele;
        private System.Windows.Forms.Button btnCoz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCikti;
        private System.Windows.Forms.GroupBox grpEmail;
        private System.Windows.Forms.Button btnGonder;
        private System.Windows.Forms.Button btnIndir;
        private System.Windows.Forms.TextBox txtAliciEmail;
        private System.Windows.Forms.Label label5;
    }
}

