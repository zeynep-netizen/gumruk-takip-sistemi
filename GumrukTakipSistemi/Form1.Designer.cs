namespace GumrukTakipSistemi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtFirmaAdi = new TextBox();
            label2 = new Label();
            cmbIsTipi = new ComboBox();
            label3 = new Label();
            txtAciklama = new TextBox();
            btnEkle = new Button();
            dataGridView1 = new DataGridView();
            btnSil = new Button();
            btnGuncelle = new Button();
            label4 = new Label();
            cmbDurum = new ComboBox();
            txtAra = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 19);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 0;
            label1.Text = "FirmaAdi";
            // 
            // txtFirmaAdi
            // 
            txtFirmaAdi.Location = new Point(21, 37);
            txtFirmaAdi.Name = "txtFirmaAdi";
            txtFirmaAdi.Size = new Size(100, 23);
            txtFirmaAdi.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(226, 17);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 2;
            label2.Text = "İş Tipi";
            // 
            // cmbIsTipi
            // 
            cmbIsTipi.FormattingEnabled = true;
            cmbIsTipi.Location = new Point(188, 37);
            cmbIsTipi.Name = "cmbIsTipi";
            cmbIsTipi.Size = new Size(121, 23);
            cmbIsTipi.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(451, 19);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 4;
            label3.Text = "Açıklama";
            // 
            // txtAciklama
            // 
            txtAciklama.Location = new Point(385, 37);
            txtAciklama.Name = "txtAciklama";
            txtAciklama.Size = new Size(203, 23);
            txtAciklama.TabIndex = 5;
            // 
            // btnEkle
            // 
            btnEkle.Location = new Point(799, 37);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(75, 23);
            btnEkle.TabIndex = 6;
            btnEkle.Text = "Ekle";
            btnEkle.UseVisualStyleBackColor = true;
            btnEkle.Click += btnEkle_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 161);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(782, 160);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnSil
            // 
            btnSil.Location = new Point(21, 414);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(78, 24);
            btnSil.TabIndex = 8;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = true;
            btnSil.Click += btnSil_Click;
            // 
            // btnGuncelle
            // 
            btnGuncelle.Location = new Point(207, 415);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(75, 23);
            btnGuncelle.TabIndex = 9;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = true;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(689, 19);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 10;
            label4.Text = "Durum";
            // 
            // cmbDurum
            // 
            cmbDurum.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDurum.FormattingEnabled = true;
            cmbDurum.Location = new Point(652, 37);
            cmbDurum.Name = "cmbDurum";
            cmbDurum.Size = new Size(121, 23);
            cmbDurum.TabIndex = 11;
            cmbDurum.SelectedIndexChanged += cmbDurum_SelectedIndexChanged;
            // 
            // txtAra
            // 
            txtAra.Location = new Point(373, 414);
            txtAra.Name = "txtAra";
            txtAra.Size = new Size(100, 23);
            txtAra.TabIndex = 12;
            txtAra.Text = "ARA";
            txtAra.TextChanged += txtAra_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 450);
            Controls.Add(txtAra);
            Controls.Add(cmbDurum);
            Controls.Add(label4);
            Controls.Add(btnGuncelle);
            Controls.Add(btnSil);
            Controls.Add(dataGridView1);
            Controls.Add(btnEkle);
            Controls.Add(txtAciklama);
            Controls.Add(label3);
            Controls.Add(cmbIsTipi);
            Controls.Add(label2);
            Controls.Add(txtFirmaAdi);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFirmaAdi;
        private Label label2;
        private ComboBox cmbIsTipi;
        private Label label3;
        private TextBox txtAciklama;
        private Button btnEkle;
        private DataGridView dataGridView1;
        private Button btnSil;
        private Button btnGuncelle;
        private Label label4;
        private ComboBox cmbDurum;
        private TextBox txtAra;
    }
}
