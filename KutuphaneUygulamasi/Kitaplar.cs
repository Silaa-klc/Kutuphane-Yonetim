using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace KutuphaneUygulamasi
{
    public partial class Kitaplar : Form
    {
        string connectionString = "Server=SILA;Database=KutuphaneYonetim;Integrated Security=True;";

        public Kitaplar()
        {
            InitializeComponent();
            KitaplariYukle();
        }

        private void KitaplariYukle()
        {
            flowLayoutPanel1.Controls.Clear(); // Daha önceki içerikleri temizle

            string query = "SELECT * FROM Kitaplar";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Reader'dan bilgileri çek
                        string kitapAd = reader["Ad"].ToString();
                        string yazar = reader["Yazar"].ToString();
                        string yayinevi = reader["Yayinevi"].ToString();
                        string basimYili = reader["BasimYili"].ToString();
                        string resimLinki = reader["ResimLinki"].ToString();
                       

                        // Kitap paneli oluştur
                        Panel kitapPanel = new Panel();
                        kitapPanel.BorderStyle = BorderStyle.FixedSingle;
                        kitapPanel.Size = new Size(200, 390);
                        kitapPanel.Margin = new Padding(10);

                        // Resim kutusu
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Size = new Size(180, 250);
                        pictureBox.Location = new Point(10, 10);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                        try
                        {
                            if (!string.IsNullOrEmpty(resimLinki))
                                pictureBox.Load(resimLinki);
                        }
                        catch
                        {
                            // Resim yüklenemezse hata verme
                        }

                        // Etiketler
                        Label lblAd = new Label();
                        lblAd.Text = "Ad: " + kitapAd;
                        lblAd.Location = new Point(10, 270);
                        lblAd.AutoSize = true;

                        Label lblYazar = new Label();
                        lblYazar.Text = "Yazar: " + yazar;
                        lblYazar.Location = new Point(10, 285);
                        lblYazar.AutoSize = true;

                        Label lblYayinevi = new Label();
                        lblYayinevi.Text = "Yayınevi: " + yayinevi;
                        lblYayinevi.Location = new Point(10, 300);
                        lblYayinevi.AutoSize = true;

                        Label lblBasimYili = new Label();
                        lblBasimYili.Text = "Basım: " + basimYili;
                        lblBasimYili.Location = new Point(10, 315);
                        lblBasimYili.AutoSize = true;

                        // Ödünç Ver Butonu
                        Button btnOduncAl = new Button();
                        btnOduncAl.Text = "Ödünç Ver";
                        btnOduncAl.Size = new Size(180, 30);
                        btnOduncAl.Location = new Point(10, 340);
                        btnOduncAl.BackColor = Color.Beige;

                        // Butona tıklanınca kiracı formunu aç
                        btnOduncAl.Click += (s, e) =>
                        {
                            KiraciBilgileri kiraciForm = new KiraciBilgileri(kitapAd, yazar, yayinevi, basimYili,resimLinki);
                            kiraciForm.ShowDialog();
                        };

                        // Elemanları ekle
                        kitapPanel.Controls.Add(pictureBox);
                        kitapPanel.Controls.Add(lblAd);
                        kitapPanel.Controls.Add(lblYazar);
                        kitapPanel.Controls.Add(lblYayinevi);
                        kitapPanel.Controls.Add(lblBasimYili);
                        kitapPanel.Controls.Add(btnOduncAl);

                        flowLayoutPanel1.Controls.Add(kitapPanel);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri çekme hatası: " + ex.Message);
                }
            }
        }
    }
}
