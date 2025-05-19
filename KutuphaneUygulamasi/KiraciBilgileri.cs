using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneUygulamasi
{
    public partial class KiraciBilgileri : Form
    {
        private string kitapAdi;
        private string kitapYazar;
        private string kitapYayinevi;
        private string kitapYili;

        public KiraciBilgileri(string kitapAd, string yazar, string yayinevi, string basimYili, string resimLinki)
        {
            InitializeComponent();

            lblKitapAdi.Text = "Kitap Adı: " + kitapAd;
            lblYazar.Text = "Yazar: " + yazar;
            lblYayınEvı.Text = "Yayınevi: " + yayinevi;
            lblYıl.Text = "Basım Yılı: " + basimYili;

            try
            {
                if (!string.IsNullOrEmpty(resimLinki))
                {
                    pictureBox1.Load(resimLinki); // Sağdaki boş kutuya resim ekle
                }
            }
            catch
            {
                // Hatalı resim durumunda sessizce geç
            }
        }

        private void KitapBilgileriniGoster()
        {
            lblKitapAdi.Text = "Kitap: " + kitapAdi;
            lblYazar.Text = "Yazar: " + kitapYazar;
            lblYayınEvı.Text = "Yayınevi: " + kitapYayinevi;
            lblYıl.Text = "Basım Yılı: " + kitapYili;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adSoyad = txtAdSoyad.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string kiralamaSuresi = txtKiralamaSüresi.Text.Trim();
            DateTime kiralamaTarihi = DateTime.Now; // Şu anki tarih-saat
            string kitapAdi = lblKitapAdi.Text.Replace("Kitap Adı: ", ""); // Label'dan kitap adını al

            string mesaj = $"Ad Soyad: {adSoyad}\nTelefon: {telefon}\nKiralama Süresi: {kiralamaSuresi} gün\n\nBilgiler doğru mu?";

            DialogResult result = MessageBox.Show(mesaj, "Onayla", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Veritabanına ekle
                string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;"; // 🔁 Burayı kendi veritabanı bağlantı bilgine göre düzenle

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO KiralamaIslemleri 
                             (AdSoyad, Telefon, KiralamaSuresi, KiralamaTarihi, KitapAdi)
                             VALUES (@AdSoyad, @Telefon, @KiralamaSuresi, @KiralamaTarihi, @KitapAdi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        command.Parameters.AddWithValue("@Telefon", telefon);
                        command.Parameters.AddWithValue("@KiralamaSuresi", int.Parse(kiralamaSuresi));
                        command.Parameters.AddWithValue("@KiralamaTarihi", kiralamaTarihi);
                        command.Parameters.AddWithValue("@KitapAdi", kitapAdi);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Kiralama işlemi başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Kiralama işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void KiraciBilgileri_Load(object sender, EventArgs e)
        {
            // Opsiyonel: Form yüklendiğinde başka bir şey yapılacaksa
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void txtTelefon_TextChanged(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
