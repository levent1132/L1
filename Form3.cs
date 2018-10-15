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
    public partial class Form3 : Form
    {
        
        string urun_id = "";
        string yil = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("SELECT * FROM urun where u_id='" + arama + "' or u_kod='" + arama + "' or u_marka like '" + arama + "%' or u_model like '" + arama + "%' or u_tur='" + arama + "'  ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    dataGridView1.Columns[0].Width = 25;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Ürün Kodu";
                    dataGridView1.Columns[2].HeaderText = "Marka";
                    dataGridView1.Columns[3].HeaderText = "Model";
                    dataGridView1.Columns[4].HeaderText = "Ürün Türü";
                    dataGridView1.Columns[5].HeaderText = "Alış Tarih";
                    dataGridView1.Columns[6].HeaderText = "Fiyat (TL)";
                    log.Bagla.Close();
                    log.urun_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc;
                sonuc = MessageBox.Show(" '" + textBox6.Text + " " + textBox4.Text + "' Silmek istediğinzden emin misiniz? ", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Delete from urun where u_id='" + urun_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.urun_silme(Form1.adi, Form1.soyadi, Form1.id, urun_id);
                    MessageBox.Show("Ürün silindi..", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Silme işleminde hata! Lütfen silinecek personeli seçiniz!", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string gunay;
                urun_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                gunay = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                comboBox4.Text = gunay.Substring(0, 2);
                comboBox1.Text = gunay.Substring(3, 2);
                yil = gunay.Substring(6);
                button7.Enabled = true;
                button1.Enabled = true;
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
                    log.komut = new MySqlCommand("Update urun set u_kod='" + textBox5.Text + "', u_marka='" + textBox6.Text + "' , u_model='" + textBox4.Text + "' , u_tur='" + comboBox2.Text + "', u_alisfiyati='"+textBox9.Text +"', u_alistarihi='"+ atarih +"'  where u_id='" + urun_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.urun_duzenle(Form1.adi, Form1.soyadi, Form1.id, urun_id);
                    MessageBox.Show("Ürün bilgileri güncellendi..", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();

                }
                catch (Exception)
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen kayıt için geçerli parametreler giriniz!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void yenile()
        {
            button7.Text = "Düzenle";
            button7.Enabled = false;
            button1.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox9.Enabled = false;
            comboBox2.Enabled = false;
            comboBox1.Enabled = false;
            comboBox4.Enabled = false;
            textBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox9.Clear();
            dataGridView1.DataSource = null;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        


       
    }
}
