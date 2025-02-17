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

namespace GörselProgramlamaÖdec
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }

        int i = 0;
        SqlConnection con = new SqlConnection(@"Data Source=ARDAKOCAK\SQLEXPRESS;Initial Catalog=okul2;Integrated Security=True");

        public void kayıtları_getir()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string getir = "Select * from Urunler";
                    SqlCommand komut = new SqlCommand(getir, con);
                    SqlDataAdapter ad = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("!Bağlantı Hatası!" + hata.Message);
            }
        }
        
        private void urunekle_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayıtları_getir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            u_adi.Text=dataGridView1.Rows[i].Cells[1].Value.ToString();
            u_kategorı.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            u_mensei.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            u_fiyat.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            
        }

        private void çıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void u_fiyat_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u_adi.Text) || string.IsNullOrWhiteSpace(u_fiyat.Text) || string.IsNullOrWhiteSpace(u_kategorı.Text) || string.IsNullOrWhiteSpace(u_mensei.Text))
                {
                    MessageBox.Show("Ürün Bilgeri Boş Bırakılmaz!");
                    return;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string kayit = "insert into Urunler (urunAdi,Kategori,Mensei,Fiyat) " +
                        "values(@ad,@kategori,@mensei,@fiyat)";
                    SqlCommand komut = new SqlCommand(kayit, con);
                    komut.Parameters.AddWithValue("@ad", u_adi.Text);
                    komut.Parameters.AddWithValue("@kategori", u_kategorı.Text);
                    komut.Parameters.AddWithValue("@mensei", u_mensei.Text);
                    komut.Parameters.AddWithValue("@fiyat", u_fiyat.Text);

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ürün Başarılı Bir Şekilde Eklendi");
                    con.Close();
                    u_adi.Clear();
                    u_kategorı.Clear();
                    u_mensei.Clear();
                    u_fiyat.Clear();
                    kayıtları_getir();
                }
            }
            catch(Exception hata)
            {
                MessageBox.Show("!Bağlantı Hatası!"+ hata.Message);
            }
        }

        private void btn_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u_adi.Text) || string.IsNullOrWhiteSpace(u_fiyat.Text) || string.IsNullOrWhiteSpace(u_kategorı.Text) || string.IsNullOrWhiteSpace(u_mensei.Text))
                {
                    MessageBox.Show("Ürün Bilgeri Boş Bırakılmaz!");
                    return;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlCommand komut = new SqlCommand("Update Urunler set urunAdi = @urunAdi, Kategori = @Kategori, Mensei = @Mensei, Fiyat = @Fiyat where UrunID = @id", con);
                    komut.Parameters.AddWithValue("@urunAdi", u_adi.Text);
                    komut.Parameters.AddWithValue("@Kategori", u_kategorı.Text);
                    komut.Parameters.AddWithValue("@Mensei", u_mensei.Text);
                    komut.Parameters.AddWithValue("@Fiyat", decimal.Parse(u_fiyat.Text));
                    komut.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);
                    komut.ExecuteNonQuery();
                    con.Close();
                    u_adi.Clear();
                    u_kategorı.Clear();
                    u_mensei.Clear();
                    u_fiyat.Clear();
                    MessageBox.Show("Ürün Başarılı Bir Şekilde Güncellendi");
                    kayıtları_getir();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("!Bağlantı Hatası!"+ hata.Message);
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u_adi.Text) || string.IsNullOrWhiteSpace(u_fiyat.Text) || string.IsNullOrWhiteSpace(u_kategorı.Text) || string.IsNullOrWhiteSpace(u_mensei.Text))
                {
                    MessageBox.Show("Ürün Bilgeri Boş Bırakılmaz!");
                    return;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand komut = new SqlCommand("DELETE FROM Urunler WHERE urunID = @id", con);
                komut.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);

                int sonuc = komut.ExecuteNonQuery();
                if (sonuc > 0)
                {
                    MessageBox.Show("Seçili ürün başarıyla silindi!");
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız! Geçerli bir ID bulunamadı.");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Bağlantı Hatası: " + Hata.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    u_adi.Clear();
                    u_kategorı.Clear();
                    u_mensei.Clear();
                    u_fiyat.Clear();
                    kayıtları_getir();
                }
            }
        }
    }
}