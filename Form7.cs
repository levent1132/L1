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
    public partial class Form7 : Form
    {
        
        string personel_id = "";

        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arama = textBox8.Text;
            try
            {
                if (textBox8.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("SELECT * FROM personel where p_id='" + arama + "' or p_tcno='" + arama + "' or p_ad like '" + arama + "%' or p_soyad like '" + arama + "%' or p_mail='" + arama + "' or p_tel='" + arama + "' ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    dataGridView1.Columns[0].Width = 25;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "TC Kimlik No";
                    dataGridView1.Columns[2].HeaderText = "İsim";
                    dataGridView1.Columns[3].HeaderText = "Soyisim";
                    dataGridView1.Columns[4].HeaderText = "Doğum Tarih";
                    dataGridView1.Columns[5].HeaderText = "E-mail";
                    dataGridView1.Columns[6].HeaderText = "Şifre";
                    dataGridView1.Columns[7].HeaderText = "Kayıt Tarih";
                    dataGridView1.Columns[8].HeaderText = "Pozisyon";
                    dataGridView1.Columns[9].HeaderText = "Maaş";
                    dataGridView1.Columns[10].HeaderText = "Telefon";
                    dataGridView1.Columns[11].HeaderText = "Adres";
                    log.Bagla.Close();
                    log.personel_arama(Form1.adi, Form1.soyadi, Form1.id, arama);
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
        private void yenile()
        {
            button2.Text = "Düzenle";
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox9.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            dataGridView1.DataSource = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string dtgunayil;
                personel_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                dtgunayil = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                comboBox4.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                comboBox1.Text = dtgunayil.Substring(0, 2);
                comboBox2.Text = dtgunayil.Substring(3, 2);
                comboBox3.Text = dtgunayil.Substring(6);
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
            if (button2.Text == "Düzenle")
            {
                button2.Text = "Kaydet";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox9.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }
            else if (button2.Text == "Kaydet")
            {
                try
                {
                    string dtarih;
                    dtarih = comboBox1.Text + "/" + comboBox2.Text + "/" + comboBox3.Text;
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Update personel set p_tcno='"+textBox3.Text +"', p_ad='" + textBox1.Text + "' , p_soyad='" + textBox2.Text + "' , p_mail='" + textBox4.Text + "' , p_tel='" + textBox5.Text + "', p_adres='" + textBox7.Text + "', p_sifre='"+textBox6.Text +"', p_pozisyon='"+comboBox4.Text +"', p_maas='"+textBox9.Text+"', p_dogumtarih='"+ dtarih +"' where p_id='" + personel_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.personel_duzenle(Form1.adi, Form1.soyadi, Form1.id, personel_id);
                    MessageBox.Show("Personel bilgileri güncellendi..", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sonuc = MessageBox.Show(" '" + textBox1.Text + " " + textBox2.Text + "' Silmek istediğinzden emin misiniz? ", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Delete from personel where p_id='" + personel_id + "' ", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.personel_silme(Form1.adi, Form1.soyadi, Form1.id, personel_id);
                    MessageBox.Show("Personel silindi..", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Silme işleminde hata! Lütfen silinecek personeli seçiniz!", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
