using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Luttop_2015
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }
        bool a = true; //ilk acılacak giris penceresi
        private void button2_Click(object sender, EventArgs e)
        {
            if (a) //acılacak olan pencerenin kontrolü
            {
                groupBox1.Hide();
                groupBox2.Show();
                a = false;
            }
            else if (!a)
            {
                groupBox2.Hide();
                groupBox1.Show();
                a = true;
            }
        
        }

        
        public string k_mail;
        public static string id;
        public static string adi;
        public static string soyadi;

        private void Baglan(bool degisken)
        {
            string k_sifre="Luttop";
            string tablo="Tablo ismi";
            string sutun_kullanici="Kullanıcı adı";
            string sutun_sifre="sifresi";
            string kolon_ad = "kolona adı";
            string kolon_id = "koolon ıd";
            string kolon_soyad = "kolon soyad";
            try
            {
                if (degisken)//personel giriş yaparsa 
                {
                    k_sifre = textBox2.Text;
                    k_mail = textBox1.Text;
                    tablo = "personel";
                    sutun_kullanici = "p_mail";
                    sutun_sifre = "p_sifre";
                    kolon_ad = "p_ad";
                    kolon_soyad = "p_soyad";
                    kolon_id = "p_id";
                }
                else if (!degisken) //yonetici giriş yaparsa
                {
                    k_sifre = textBox3.Text;
                    k_mail = textBox4.Text;
                    tablo = "yonetici";
                    sutun_kullanici = "y_mail";
                    sutun_sifre = "y_sifre";
                    kolon_ad = "y_ad";
                    kolon_soyad = "y_soyad";
                    kolon_id = "y_id";
                }

                log.Bagla.Open(); 
                log.Adaptor = new MySqlDataAdapter("SELECT * FROM " + tablo + " where " + sutun_kullanici + " = '" + k_mail + "' and " + sutun_sifre + " = '" + k_sifre + "'", log.Bagla);
                log.Ds = new DataSet();
                log.Ds.Clear();
                log.Adaptor.Fill(log.Ds, "veri");

                if (log.Ds.Tables[0].Rows.Count == 1) //giriş verileri doğru ise
                {
                    adi     = log.Ds.Tables[0].Rows[0][kolon_ad].ToString();
                    soyadi  = log.Ds.Tables[0].Rows[0][kolon_soyad].ToString();
                    id      = log.Ds.Tables[0].Rows[0][kolon_id].ToString();

                    MessageBox.Show("Giriş Yapıldı..  Hoş Geldiniz  :" + adi + " " + soyadi, "Hoş geldiniz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (degisken)// personel yektisine göre giris yapılacak form
                    {
                        log.log_giris(adi, soyadi, id); //kimin giris yaptıgını log kaydı
                        log.Bagla.Close();
                        Form14 frm14 = new Form14();
                        frm14.Show();
                        this.Hide();
                        
                     }
                    else if (!degisken)//yönetici yetkisisne göre giris yapılacak form
                    {
                        log.log_giris(adi, soyadi, id);
                        log.Bagla.Close();
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        this.Hide();
                    }
                   

                    log.Bagla.Close();
                }
                else  // hatalı ise
                {

                    MessageBox.Show("Hatalı E-mail adresi : " + k_mail + " veya şifre : " + k_sifre + "  Tekrar Gözden Geçiriniz", "Hatalı Giriş!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox1.DataBindings.Clear();
                    textBox2.DataBindings.Clear();
                    log.Ds.Clear();
                    Application.Restart();
                }
            }

            catch (Exception) //dışında kalan hatalar
            {
                MessageBox.Show("Bağlantı veya internet Hatası","Connection Error !",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Baglan(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Baglan(false);
        }
    }
}
