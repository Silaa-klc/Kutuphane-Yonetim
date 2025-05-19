using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void TabloyuYukle()
        {
            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Kitaplar";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tablo yüklenirken hata oluştu: " + ex.Message);
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            dataGridView1.ReadOnly = true; // Tüm hücreler sadece okunabilir olur
            dataGridView1.AllowUserToAddRows = false; // Kullanıcı yeni satır ekleyemez
            dataGridView1.AllowUserToDeleteRows = false; // Satır silemez


            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Integrated Security=True;"; // SQL Server'a bağlanma stringi
            string query = "SELECT * FROM Kitaplar"; // Veritabanındaki tablonun adı

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // Verileri alacak SqlDataAdapter
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Verileri tutacak DataTable
                    DataTable dataTable = new DataTable();

                    // DataAdapter ile veriyi alıp DataTable'a doldur
                    dataAdapter.Fill(dataTable);

                    // DataGridView'e DataTable'ı bağla
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Satır geçerli mi kontrolü
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtKitapID.Text = row.Cells["KitapID"].Value?.ToString();
                txtAd.Text = row.Cells["Ad"].Value?.ToString();
                txtYazar.Text = row.Cells["Yazar"].Value?.ToString();
                txtYayınevi.Text = row.Cells["Yayinevi"].Value?.ToString();
                txtBasimYili.Text = row.Cells["BasimYili"].Value?.ToString();
                txtResimLinki.Text = row.Cells["ResimLinki"].Value?.ToString();
                try
                {
                    string url = txtResimLinki.Text.Trim();

                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        pictureBox1.Load(url);
                    }
                    else
                    {
                        pictureBox1.Image = null; // Hatalıysa resmi temizle
                        MessageBox.Show("Geçerli bir resim bağlantısı girilmemiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    pictureBox1.Image = null;
                    MessageBox.Show("Resim yüklenemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // 1. Boşluk ve geçerlilik kontrolü
            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtYazar.Text))
            {
                MessageBox.Show("Lütfen 'Ad' ve 'Yazar' alanlarını doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(txtBasimYili.Text))
            {
                if (!int.TryParse(txtBasimYili.Text, out _))
                {
                    MessageBox.Show("Lütfen 'Basım Yılı' alanına sadece sayı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // 2. Veritabanı bağlantısı
            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO Kitaplar (Ad, Yazar, Yayinevi, BasimYili, ResimLinki, Durum)
                             VALUES (@Ad, @Yazar, @Yayinevi, @BasimYili, @ResimLinki, 1)"; // Durum varsayılan olarak 1

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 3. Parametreleri al
                        cmd.Parameters.AddWithValue("@Ad", txtAd.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yazar", txtYazar.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yayinevi", txtYayınevi.Text.Trim());
                        cmd.Parameters.AddWithValue("@BasimYili", string.IsNullOrEmpty(txtBasimYili.Text.Trim()) ? (object)DBNull.Value : Convert.ToInt32(txtBasimYili.Text));
                        cmd.Parameters.AddWithValue("@ResimLinki", txtResimLinki.Text.Trim());

                        // 4. Komutu çalıştır
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Kitap başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Tabloyu yenile
                        TabloyuYukle();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKitapID.Text))
            {
                MessageBox.Show("Lütfen silmek için bir kitap seçin.");
                return;
            }

            DialogResult result = MessageBox.Show("Seçilen kitabı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Kitaplar WHERE KitapID = @KitapID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@KitapID", Convert.ToInt32(txtKitapID.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Kitap başarıyla silindi.");
                        }

                        // Tablonun güncellenmesi
                        TabloyuYukle();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKitapID.Text))
            {
                MessageBox.Show("Lütfen güncellemek için bir kitap seçin.");
                return;
            }

            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE Kitaplar
                             SET Ad = @Ad,
                                 Yazar = @Yazar,
                                 Yayinevi = @Yayinevi,
                                 BasimYili = @BasimYili,
                                 ResimLinki = @ResimLinki
                             WHERE KitapID = @KitapID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KitapID", Convert.ToInt32(txtKitapID.Text));
                        cmd.Parameters.AddWithValue("@Ad", txtAd.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yazar", txtYazar.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yayinevi", txtYayınevi.Text.Trim());
                        cmd.Parameters.AddWithValue("@BasimYili", string.IsNullOrEmpty(txtBasimYili.Text.Trim()) ? (object)DBNull.Value : Convert.ToInt32(txtBasimYili.Text));
                        cmd.Parameters.AddWithValue("@ResimLinki", txtResimLinki.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kitap bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // DataGridView'i güncelle
                    TabloyuYukle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme hatası: " + ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kitaplar kitaplar = new Kitaplar();
            kitaplar.Show();
        }

        private void btnKiralananlar_Click(object sender, EventArgs e)
        {
            KirralananKitaplar kiralıklar = new KirralananKitaplar();
            kiralıklar.ShowDialog();
        }
    }
}
