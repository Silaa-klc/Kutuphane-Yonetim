﻿namespace KutuphaneUygulamasi
{
    partial class KirralananKitaplar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KirralananKitaplar));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnKiralamaIptal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(802, 270);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnKiralamaIptal
            // 
            this.btnKiralamaIptal.Location = new System.Drawing.Point(303, 315);
            this.btnKiralamaIptal.Name = "btnKiralamaIptal";
            this.btnKiralamaIptal.Size = new System.Drawing.Size(151, 69);
            this.btnKiralamaIptal.TabIndex = 1;
            this.btnKiralamaIptal.Text = "Kiralamayı İptal Et";
            this.btnKiralamaIptal.UseVisualStyleBackColor = true;
            this.btnKiralamaIptal.Click += new System.EventHandler(this.btnKiralamaIptal_Click);
            // 
            // KirralananKitaplar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKiralamaIptal);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KirralananKitaplar";
            this.Text = "Kiralanan Kitaplar";
            this.Load += new System.EventHandler(this.KirralananKitaplar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnKiralamaIptal;
    }
}