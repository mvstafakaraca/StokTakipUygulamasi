using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProgramlamaÖdec
{
    public partial class kayıtol : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source= ARDAKOCAK\SQLEXPRESS;Initial Catalog=okul2;Integrated Security=True");

        public kayıtol()
        {
            InitializeComponent();
        }

        private void kayıtol_Load(object sender, EventArgs e)
        {

        }

        private void btn_kayıt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(k_adi.Text) || string.IsNullOrWhiteSpace(sifre.Text))
                {
                    MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!");
                    return;
                }

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string query = "INSERT INTO kullanici (KullaniciAdi, Sifre) VALUES (@KullaniciAdi, @Sifre)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KullaniciAdi", k_adi.Text);
                cmd.Parameters.AddWithValue("@Sifre", sifre.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı başarıyla kaydedildi!");
                this.Hide();

                Login loginForm = new Login();
                loginForm.Show();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        private void çıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}