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
    public partial class Form5 : Form
    {
        
        string musteri_id = "";

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty)
                {
                    
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("SELECT * FROM musteri where m_id='" + arama + "' or m_ad like '" + arama + "%' or m_soyad like '" + arama + "%' or m_mail='" + arama + "'  ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    dataGridView1.Columns[0].Width = 25;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "İsim";
                    dataGridView1.Columns[2].HeaderText = "Soyisim";
                    dataGridView1.Columns[3].HeaderText = "E-mail";
                    dataGridView1.Columns[4].HeaderText = "Kayıt Tarih";
                    dataGridView1.Columns[5].HeaderText = "Telefon";
                    dataGridView1.Columns[6].HeaderText = "Adres";
                    log.Bagla.Close();
                    log.musteri_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
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
        private void yenile()
        {
            button2.Text = "Düzenle";
            button2.Enabled = false;
            button3.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            dataGridView1.DataSource = null; 
        }
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                musteri_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                button2.Enabled = true;
                button3.Enabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text=="Düzenle")
            {
                button2.Text = "Kaydet";
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            else if (button2.Text=="Kaydet")
            {
                try
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Update musteri set m_ad='" + textBox2.Text + "' , m_soyad='" + textBox3.Text + "' , m_mail='" + textBox4.Text + "' , m_tel='" + textBox5.Text + "', m_adres='" + textBox6.Text + "' where m_id='"+musteri_id +"' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.musteri_duzenle(Form1.adi, Form1.soyadi, Form1.id,musteri_id);
                    MessageBox.Show("Müşteri bilgileri güncellendi..", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen kayıt için geçerli parametreler giriniz!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc;
                sonuc = MessageBox.Show(" '"+textBox2.Text+" " + textBox3.Text+"' Silmek istediğinzden emin misiniz? ", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Delete from musteri where m_id='" + musteri_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.urun_duzenle(Form1.adi, Form1.soyadi, Form1.id, musteri_id);
                    MessageBox.Show("Müşteri silindi..", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                }
                

            }
            catch (Exception)
            {
                MessageBox.Show("Silme işleminde hata! Lütfen silinecek müşteriyi seçiniz!", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
