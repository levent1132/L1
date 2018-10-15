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
    public partial class Form14 : Form
    {
        

        public Form14()
        {
            InitializeComponent();
        }
        private void Form14_Load(object sender, EventArgs e)
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
            
            dataGridView2.Columns[1].HeaderText = "İsim";
            dataGridView2.Columns[2].HeaderText = "Soyisim";
            dataGridView2.Columns[3].HeaderText = "E-mail";
            dataGridView2.Columns[4].HeaderText = "Kayıt Tarih";
            dataGridView2.Columns[5].HeaderText = "Telefon";
            dataGridView2.Columns[6].HeaderText = "Adres";

            dataGridView3.Columns[1].HeaderText = "İsim";
            dataGridView3.Columns[2].HeaderText = "Soyisim";
            dataGridView3.Columns[3].HeaderText = "Doğum Tarih";
            dataGridView3.Columns[4].HeaderText = "Pozisyon";
            
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
        private void yenile(int parametre)
        {
            string sorgu = "";
            textBox1.Clear();
            log.Bagla.Open();
            DataGridView tablo1 = new DataGridView();

            if (parametre == 1)
            { sorgu = "select u_id,u_kod,u_marka,u_model,u_tur,u_alistarihi from urun"; tablo1 = dataGridView1; }
            else if (parametre == 2)
            { sorgu = "select * from musteri"; tablo1 = dataGridView2; }
            else if (parametre == 3)
            { sorgu = "select p_id,p_ad,p_soyad,p_dogumtarih,p_pozisyon from personel"; tablo1 = dataGridView3; }
            else if (parametre == 4)
            { sorgu = "select * from servis"; tablo1 = dataGridView4; }

            log.Adaptor = new MySqlDataAdapter(sorgu, log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            tablo1.DataSource = log.Bs;
            tablo1.Columns[0].Width = 25;
            tablo1.Columns[0].HeaderText = "ID";
            log.Bagla.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            log.log_cikis(Form1.adi, Form1.soyadi, Form1.id);
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            yenile(1);
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

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form9 frm9 = new Form9();
            frm9.ShowDialog();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("SELECT p_id,p_ad,p_soyad,p_dogumtarih,p_pozisyon FROM personel where p_id='" + arama + "' or p_ad like '" + arama + "%' or p_soyad like '" + arama + "%'  ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView3.DataSource = log.Bs;
                    log.Bagla.Close();
                    log.personel_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
                }
                else
                {
                    MessageBox.Show("Arama işleminde hata! Lütfen arama alanının boş olmadığından emin olun!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.log_cikis(Form1.adi, Form1.soyadi, Form1.id);
            Application.Exit();
        }

        
    }
}
