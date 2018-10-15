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
    public partial class Form9 : Form
    {
        
        Form10 frm10 = new Form10();
        Form11 frm11 = new Form11();

        string u_id,m_id,p_id,c_id = "";
        public string secilenurun,secilenmusteri,secilenpersonel,secilenurunid,yil = "";
        bool durum;

        public Form9()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("select cikis.c_id,urun.u_kod, urun.u_marka, urun.u_model , urun.u_tur, cikis.c_satistarih, cikis.c_satisfiyat, musteri.m_ad, musteri.m_soyad, personel.p_ad, personel.p_soyad from urun, cikis, personel, musteri  where urun.u_id=cikis.u_id and personel.p_id=cikis.p_id and musteri.m_id=cikis.m_id and urun.u_kod='" + arama + "' or urun.u_marka like '" + arama + "%' or urun.u_model like '" + arama + "%' or urun.u_tur='" + arama + "' order by cikis.c_id ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    log.Bagla.Close();
                    log.urun_satis_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
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

        private void Form9_Load(object sender, EventArgs e)
        {
            yenile();
        }
        private void yenile()
        {
            log.Bagla.Open();
            log.Adaptor = new MySqlDataAdapter("select cikis.c_id,urun.u_kod, urun.u_marka, urun.u_model ,urun.u_tur, cikis.c_satistarih, cikis.c_satisfiyat, musteri.m_ad, musteri.m_soyad, personel.p_ad, personel.p_soyad, urun.u_id,musteri.m_id,personel.p_id from urun, cikis, personel, musteri  where urun.u_id=cikis.u_id and personel.p_id=cikis.p_id and musteri.m_id=cikis.m_id order by cikis.c_id", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            dataGridView1.DataSource = log.Bs;
            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[1].Width = 40;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Ürün Kodu";
            dataGridView1.Columns[2].HeaderText = "Marka";
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[4].HeaderText = "Tür";
            dataGridView1.Columns[5].HeaderText = "Satış Tarihi";
            dataGridView1.Columns[6].HeaderText = "Satış Fiyat(TL)";
            dataGridView1.Columns[7].HeaderText = "Müşteri Adi";
            dataGridView1.Columns[8].HeaderText = "Müşteri Soyadı";
            dataGridView1.Columns[9].HeaderText = "Personel Adı";
            dataGridView1.Columns[10].HeaderText = "Personel Soyadı";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            log.Bagla.Close();
            button7.Text = "Düzenle";
            button7.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox9.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            comboBox2.Enabled = false;
            comboBox1.Enabled = false;
            comboBox4.Enabled = false;
            textBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox9.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string gunay;
                u_id = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                m_id = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                p_id = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                c_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                gunay = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                comboBox4.Text = gunay.Substring(0, 2);
                comboBox1.Text = gunay.Substring(3, 2);
                yil = gunay.Substring(6);
                textBox2.Text= dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox3.Text=dataGridView1.CurrentRow.Cells[8].Value.ToString();
                textBox7.Text=dataGridView1.CurrentRow.Cells[9].Value.ToString();
                textBox8.Text=dataGridView1.CurrentRow.Cells[10].Value.ToString();
                button7.Enabled = true;
                
            }
            catch (Exception)
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (button7.Text == "Düzenle")
            {
                button7.Text = "Kaydet";
                textBox4.Enabled = true;
                textBox3.Enabled = true;
                textBox8.Enabled = true;
                textBox7.Enabled = true;
                textBox2.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox9.Enabled = true;
                comboBox2.Enabled = true;
                comboBox4.Enabled = true;
                comboBox1.Enabled = true;
            }
            else if (button7.Text == "Kaydet")
            {
                try
                {
                    string atarih = comboBox4.Text + "/" + comboBox1.Text + "/" + yil;
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Update urun set u_kod='" + textBox5.Text + "', u_marka='" + textBox6.Text + "' , u_model='" + textBox4.Text + "' , u_tur='" + comboBox2.Text + "'  where u_id='" + u_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.komut = new MySqlCommand("Update musteri set m_ad='" + textBox2.Text + "', m_soyad='" + textBox3.Text + "'  where m_id='" + m_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.komut = new MySqlCommand("Update personel set p_ad='" + textBox7.Text + "', p_soyad='" + textBox8.Text + "' where p_id='" + p_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.komut = new MySqlCommand("Update cikis set c_satistarih='" + atarih + "', c_satisfiyat='" + textBox9.Text + "'  where u_id='" + c_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.urun_satis_duzenle(Form1.adi, Form1.soyadi, Form1.id, u_id);
                    MessageBox.Show("Ürün satış bilgileri güncellendi..", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();

                }
                catch (Exception)
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen kayıt için geçerli parametreler giriniz!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm10.ShowDialog();
            timer1.Start();
            durum = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           frm11.ShowDialog();
           timer1.Start();
           durum = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("personel"+frm11.personel_id + frm11.musteri_id);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (frm10.sonuc == DialogResult.Yes && durum)
            {
                label9.Text = "Kod NO :" + frm10.urun_kodu;
                secilenurun = frm10.urun_kodu;
                log.Bagla.Open();
                log.komut = new MySqlCommand("select * from urun where u_kod='" + secilenurun + "'", log.Bagla);
                MySqlDataReader dr = log.komut.ExecuteReader();
                while (dr.Read())
                {
                    secilenurunid = dr["u_id"].ToString();
                    label12.Text = dr["u_marka"].ToString();
                    label13.Text = dr["u_model"].ToString();
                    label14.Text = dr["u_alisfiyati"].ToString() + " TL";
                }
                button3.Enabled = true;
                log.Bagla.Close();
                timer1.Stop();
                return;
            }
            if (frm11.sonuc == DialogResult.Yes && !durum)
            {
                label19.Text = frm11.musteri_id;
                label18.Text = frm11.personel_id;
                secilenmusteri = frm11.musteri_id;
                secilenpersonel = frm11.personel_id;
                log.Bagla.Open();
                log.komut = new MySqlCommand("select * from personel where p_id='" + secilenpersonel + "'", log.Bagla);
                MySqlDataReader dr = log.komut.ExecuteReader();
                while (dr.Read())
                {
                    label19.Text = dr["p_ad"].ToString();
                    label17.Text = dr["p_soyad"].ToString();

                } log.Bagla.Close();
                log.Bagla.Open();
                log.komut = new MySqlCommand("select * from musteri where m_id='" + secilenmusteri + "'", log.Bagla);
                dr = log.komut.ExecuteReader();
                while (dr.Read())
                {
                    label18.Text = dr["m_ad"].ToString();
                    label16.Text = dr["m_soyad"].ToString();
                }
                textBox10.Enabled = true;
                button6.Enabled = true;
                log.Bagla.Close();
                timer1.Stop();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("İşlemde hata! Lütfen tekrar seçiniz!", "Seçme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button3.Enabled = false;
                label12.ResetText();
                label13.ResetText();
                label14.ResetText();
                label9.ResetText();
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string tarih;
            tarih = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

            try
            {
                if (textBox10.Text.Trim() != string.Empty)
                {
                    DialogResult sonuc;
                    sonuc = MessageBox.Show(" Satış işleminin tamamlanmasını onaylıyor musunuz ?", "Satış İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (sonuc == DialogResult.Yes)
                    {
                        log.Bagla.Open();
                        log.komut = new MySqlCommand("Insert into cikis (u_id, m_id, p_id ,c_satistarih, c_satisfiyat) VALUES  ('" + secilenurunid + "','" + secilenmusteri + "','" + secilenpersonel + "','" + tarih + "','" + textBox10.Text + "')", log.Bagla);
                        log.komut.ExecuteNonQuery();
                        MessageBox.Show("Satış işlemi başarıyla gerçekleşti!", "Satış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        log.urun_satis_tamamla(Form1.adi, Form1.soyadi, Form1.id, secilenurunid);
                        log.Bagla.Close();
                        yenile();
                    }
                }
                else
                {
                    MessageBox.Show("Satış işlemi için fiyat verdiğinizden eminin olun!", "Satış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                  	 		 	 
            }
            catch (Exception)
            {
                
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
