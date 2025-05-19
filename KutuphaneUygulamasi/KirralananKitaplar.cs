using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace KutuphaneUygulamasi
{
    public partial class KirralananKitaplar : Form
    {
        public KirralananKitaplar()
        {
            InitializeComponent();
        }

        private void VerileriYukle()
        {
            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM KiralamaIslemleri"; // Tablo adını ihtiyacına göre güncelle

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable tablo = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veriler yüklenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void KirralananKitaplar_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM KiralamaIslemleri"; // Tablo adını tam doğru yaz

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable tablo = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(tablo);
                    dataGridView1.DataSource = tablo; // DataGridView adını formdaki ismine göre kontrol et
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri çekme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKiralamaIptal_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 && dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir hücre veya satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = -1;

            // Satır seçilmişse
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value); // "Id" sütun adını veritabanına göre kontrol et
            }
            // Hücre seçilmişse
            else if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            }

            if (id == -1)
            {
                MessageBox.Show("Geçerli bir ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Seçili kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string connectionString = "Server=SILA;Database=KutuphaneYonetim;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM KiralamaIslemleri WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            VerileriYukle(); // Tablonun güncellenmesi için tekrar yükle
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Silme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
