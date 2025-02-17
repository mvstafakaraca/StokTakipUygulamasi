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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void çıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string user = k_adi.Text;
            string password = sifre.Text;
            SqlConnection con = new SqlConnection(@"Data Source=ARDAKOCAK\SQLEXPRESS;Initial Catalog=okul2;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Kullanici where KullaniciAdi='"+k_adi.Text+"'"+"And sifre='"+sifre.Text+"'",con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (string.IsNullOrWhiteSpace(k_adi.Text) || string.IsNullOrWhiteSpace(sifre.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!");
            }
            if (dr.Read())
            {
                MessageBox.Show("Giriş Yapıldı");
                anasayfa frmanasayfa = new anasayfa();
                frmanasayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ve Şifre");
            }
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btn_kayıt_Click(object sender, EventArgs e)
        {
            kayıtol frmkayıtol = new kayıtol();
            frmkayıtol.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}