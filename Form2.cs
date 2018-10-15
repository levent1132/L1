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
    public partial class Form2 : Form
    {
        

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            yenile(1);
            yenile(2);
            yenile(3);
            yenile(4);
            dataGridView1.Columns[1].HeaderText = "Ürün Kodu";
            dataGridView1.Columns[2].HeaderText = "Marka";
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[4].HeaderText = "Tür";
            dataGridView1.Columns[5].HeaderText = "Alış Tarih";
            dataGridView1.Columns[6].HeaderText = "Fiyat (TL)";

            dataGridView2.Columns[1].HeaderText = "İsim";
            dataGridView2.Columns[2].HeaderText = "Soyisim";
            dataGridView2.Columns[3].HeaderText = "E-mail";
            dataGridView2.Columns[4].HeaderText = "Kayıt Tarih";
            dataGridView2.Columns[5].HeaderText = "Telefon";
            dataGridView2.Columns[6].HeaderText = "Adres";

            dataGridView3.Columns[1].HeaderText = "TC Kimlik";
            dataGridView3.Columns[2].HeaderText = "İsim";
            dataGridView3.Columns[3].HeaderText = "Soyisim";
            dataGridView3.Columns[4].HeaderText = "Doğum Tarih";
            dataGridView3.Columns[5].HeaderText = "E-mail";
            dataGridView3.Columns[6].HeaderText = "Şifre";
            dataGridView3.Columns[7].HeaderText = "Giriş Tarih";
            dataGridView3.Columns[8].HeaderText = "Pozisyon";
            dataGridView3.Columns[9].HeaderText = "Maaş (TL)";
            dataGridView3.Columns[10].HeaderText = "Telefon";
            dataGridView3.Columns[11].HeaderText = "Adres";

            dataGridView4.Columns[1].HeaderText = "Personel No";
            dataGridView4.Columns[2].HeaderText = "Ürün Kod";
            dataGridView4.Columns[3].HeaderText = "Satış Tarih";
            dataGridView4.Columns[4].HeaderText = "Servis Giriş";
            dataGridView4.Columns[5].HeaderText = "Servis Çıkış";
            dataGridView4.Columns[6].HeaderText = "Ürün Sorun";
            dataGridView4.Columns[7].HeaderText = "Servis Ücreti (TL)";
            dataGridView4.Columns[0].Width = 25;
            dataGridView4.Columns[1].Width = 60;
            dataGridView4.Columns[2].Width = 50;
            dataGridView4.Columns[3].Width = 90;
            dataGridView4.Columns[4].Width = 90;
            dataGridView4.Columns[5].Width = 90;
            dataGridView4.Columns[6].Width = 250;


          
        }

        private void hesapla(bool boo)
        {
            string ilktarih = comboBox2.Text + "/" + comboBox3.Text;
            if (boo && comboBox3.Text.Trim() != String.Empty && comboBox2.Text.Trim() != String.Empty)
            {
                dataGridView5.DataSource = cari.gelir(ilktarih);
                dataGridView5.Columns[0].Width = 140;
                dataGridView5.Columns[1].Width = 170;
                dataGridView5.Columns[2].Width = 130;
                dataGridView5.Columns[0].HeaderText = "TOPLAM GELİR (TL)";
                dataGridView5.Columns[1].HeaderText = "SATILAN ÜRÜN GELİR (TL)";
                dataGridView5.Columns[2].HeaderText = "SERVİS GELİR (TL)";
                log.cari_gelir(Form1.adi, Form1.soyadi, Form1.id);
            }
            else if (!boo && comboBox3.Text.Trim() != String.Empty && comboBox2.Text.Trim() != String.Empty)
            {
                dataGridView5.DataSource = cari.gider(ilktarih);
                dataGridView5.Columns[0].Width = 140;
                dataGridView5.Columns[1].Width = 170;
                dataGridView5.Columns[2].Width = 130;
                dataGridView5.Columns[3].Width = 170;
                dataGridView5.Columns[0].HeaderText = "TOPLAM GİDER (TL)";
                dataGridView5.Columns[1].HeaderText = "PERSONEL MAAŞ (TL)";
                dataGridView5.Columns[2].HeaderText = "ÜRÜN GİDER (TL)";
                dataGridView5.Columns[3].HeaderText = "YÖNETİCİ MAAŞ (TL)";
                log.cari_gider(Form1.adi, Form1.soyadi, Form1.id);
            }
            else
            {
                MessageBox.Show("İşleminde hata! Lütfen tarih alanının boş olmadığından emin olun!", "İşlem Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

            
        private void yenile(int parametre)
        {
            string tabloadi = "";

            log.Bagla.Open();
            DataGridView tablo1 = new DataGridView();
            
            if (parametre == 1)
            { tabloadi = "urun";         tablo1 = dataGridView1;  }
            else if (parametre == 2)
            { tabloadi = "musteri";      tablo1 = dataGridView2; }
            else if (parametre == 3)
            { tabloadi = "personel";     tablo1 = dataGridView3; }
            else if (parametre == 4)
            { tabloadi = "servis";         tablo1 = dataGridView4; }

            log.Adaptor = new MySqlDataAdapter("SELECT * FROM " + tabloadi + "", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            tablo1.DataSource = log.Bs;
            tablo1.Columns[0].Width = 25;
            tablo1.Columns[0].HeaderText  = "ID";
            log.Bagla.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            yenile(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            log.log_cikis(Form1.adi, Form1.soyadi, Form1.id);
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yenile(2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            yenile(3);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            yenile(4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8();
            frm8.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form9 frm9 = new Form9();
            frm9.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form12 frm12 = new Form12();
            frm12.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form13 frm13 = new Form13();
            frm13.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hesapla(true);  
        }

        private void button16_Click(object sender, EventArgs e)
        {
            hesapla(false);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.log_cikis(Form1.adi, Form1.soyadi, Form1.id);
            Application.Exit();
        }

      
        
    }
}
