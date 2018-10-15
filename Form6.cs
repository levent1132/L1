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
    public partial class Form6 : Form
    {
        
        public Form6()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show("Kopyala/Yapıştır Özelliği kapalıdır.");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tarih;
            tarih = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            string dtarih;
            dtarih = comboBox1.Text + "/" + comboBox2.Text + "/" + comboBox3.Text;
            try
            {

                if (textBox1.Text.Trim() != String.Empty && textBox2.Text.Trim() != String.Empty && textBox3.Text.Trim() != String.Empty && textBox4.Text.Trim() != String.Empty && textBox5.Text.Trim() != String.Empty && textBox6.Text.Trim() != String.Empty && textBox7.Text.Trim() != String.Empty && textBox9.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Insert into personel (p_tcno, p_ad, p_soyad,p_dogumtarih, p_mail, p_sifre, p_giristarih,p_pozisyon,p_maas, p_tel, p_adres)  VALUES  ('" + textBox3.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + dtarih + "','" + textBox4.Text + "','" + textBox6.Text + "','" + tarih + "','" + comboBox4.Text + "','" + textBox9.Text + "','" + textBox7.Text + "')", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.personel_ekleme(Form1.adi, Form1.soyadi, Form1.id, textBox3.Text);
                    MessageBox.Show("Yeni müşteri başarıyla kayıt olmuştur..", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen tüm bilgileri girdiğinizden emin olun !", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox9.Clear();
                    comboBox1.SelectedIndex = 0;
                    comboBox2.SelectedIndex = 0;
                    comboBox3.SelectedIndex = 0;
                    comboBox4.SelectedIndex = 0;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt işleminde hata! Lütfen tüm bilgileri girdiğinizden emin olun !", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
