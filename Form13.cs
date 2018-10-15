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
    public partial class Form13 : Form
    {
        
        string servis_id,pid,pAd,pSoyad,personel = "";
        public Form13()
        {
            InitializeComponent();
        }
        private void yenile()
        {
            button4.Text = "Ürün Seç";
            button2.Text = "Düzenle";
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox7.Enabled = false;
            textBox9.Enabled = false;
            comboBox4.Enabled = false;
            textBox1.Clear();
            textBox5.Clear();
            textBox7.Clear();
            textBox9.Clear();
            dataGridView1.DataSource = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            yenile();
            string arama = textBox8.Text;
            try
            {
                if (textBox8.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("select servis.s_id  , personel.p_ad , personel.p_soyad,servis.u_kod ,servis.c_satistarih ,servis.s_giristarih ,servis.s_cikistarih ,servis.s_sorun ,servis.s_ucret from personel, servis  where servis.p_id=personel.p_id and (servis.u_kod='" + arama + "' or servis.s_id= '" + arama + "') ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    dataGridView1.Columns[0].Width = 30;
                    dataGridView1.Columns[1].Width = 70;
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].Width = 80;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Personel Adı";
                    dataGridView1.Columns[2].HeaderText = "Personel Soyadı";
                    dataGridView1.Columns[3].HeaderText = "Ürün Kodu";
                    dataGridView1.Columns[4].HeaderText = "Satış Tarihi";
                    dataGridView1.Columns[5].HeaderText = "Servis Giriş";
                    dataGridView1.Columns[6].HeaderText = "Servis Çıkış";
                    dataGridView1.Columns[7].HeaderText = "Sorun";
                    dataGridView1.Columns[8].HeaderText = "Ücret (TL)";
                    log.Bagla.Close();
                    log.servis_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
                }
                else
                {
                    MessageBox.Show("Arama işleminde hata! Lütfen arama alanının boş olmadığından emin olun!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox8.Clear();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox8.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pAd = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            pSoyad = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            servis_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            personel = pAd + " " + pSoyad;
            comboBox4.Text = personel;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Düzenle")
            {
                button2.Text = "Kaydet";
                textBox1.Enabled = true;
                textBox5.Enabled = true;
                textBox7.Enabled = true;
                comboBox4.Enabled = true;
            }
            else if (button2.Text == "Kaydet")
            {
                try
                {
                    int sonuc = 0;
                    string prsnel, prsnelAD, prsnelSOY = "";
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("select cikis.* from cikis,urun where urun.u_id=cikis.u_id and urun.u_kod='" + textBox5.Text + "' ",log.Bagla);
                    sonuc = Convert.ToInt32(log.komut.ExecuteScalar());
                    log.Bagla.Close();
                    if (sonuc == 0)
                    {
                        MessageBox.Show("Güncellediğiniz ürün kodu yok yada cıkış tablosunda yer ALMAMAKTADIR !", "Güncelleme işlemi !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        yenile();
                        return ;
                    }
                    prsnel = comboBox4.Text.ToString();
                    int bosluk = prsnel.IndexOf(" ", 0);
                    prsnelAD = prsnel.Substring(0, bosluk);
                    prsnelSOY = prsnel.Substring(bosluk + 1);
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("select * from personel where p_ad='" + prsnelAD + "' and p_soyad='" + prsnelSOY + "' ", log.Bagla);
                    MySqlDataReader dr = log.komut.ExecuteReader();
                    while (dr.Read())
                    {
                        pid = dr["p_id"].ToString();
                    }
                    log.Bagla.Close();

                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Update servis set p_id='" + pid +  "', u_kod='" + textBox5.Text + "' , s_sorun='" + textBox7.Text + "' , s_ucret='" + textBox1.Text + "' where s_id='" + servis_id+  "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.servis_duzenle(Form1.adi, Form1.soyadi, Form1.id,servis_id);
                    MessageBox.Show("Servis bilgileri güncellendi..", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();

                }
                catch (Exception)
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen kayıt için geçerli parametreler giriniz!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            log.Bagla.Open();
            log.komut = new MySqlCommand("select p_ad,p_soyad from personel", log.Bagla);
            MySqlDataReader dr = log.komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["p_ad"].ToString() + " " + dr["p_soyad"].ToString());
            }
            log.Bagla.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc;
                sonuc = MessageBox.Show("Ürün kodu  '" + textBox5.Text + "' olan ürünü servisten  Silmek istediğinzden emin misiniz? ", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Delete from servis where s_id='" + servis_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.servis_silme(Form1.adi, Form1.soyadi, Form1.id,servis_id);
                    MessageBox.Show("Servis ürünü silindi..", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Silme işleminde hata! Lütfen silinecek personeli seçiniz!", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text=="Ürün Seç")
            {
                string kontrol = string.Empty;
                log.Bagla.Open();
                log.komut = new MySqlCommand("select * from servis where u_kod='" + textBox5.Text + "' ", log.Bagla);
                MySqlDataReader dr = log.komut.ExecuteReader();

                while (dr.Read())
                {
                    kontrol = dr["s_cikistarih"].ToString();
                }
                log.Bagla.Close();

                if (kontrol != string.Empty)
                {
                    MessageBox.Show("Seçtiğiniz ürün zaten çıkış yapılmıştır !", "Çıkış işlemi !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    yenile();
                    return;
                }
                button4.Text="Çıkış Yap";
                textBox9.Enabled = true;
            }
            else if (button4.Text=="Çıkış Yap")
            {
                if (textBox9.Text.Trim() != string.Empty)
                {
                    string dtarih;
                    dtarih = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Update servis set s_cikistarih='" + dtarih + "', s_ucret='" + textBox9.Text + "'  where u_kod='" + textBox5.Text + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.servis_cikis(Form1.adi, Form1.soyadi, Form1.id, servis_id);
                    MessageBox.Show("Ürün çıkışı tamamlandı..", "Çıkış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                    button4.Text = "Ürün Seç";
                }
                else
                {
                    MessageBox.Show("İşlem Ücreti yazmadınız !","Çıkış İşlemi !",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }

            
            

        }
    }
}
