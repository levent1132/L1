using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Luttop_2015
{
    public partial class Form4 : Form
    {
        
        
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
         private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show("Kopyala/Yapıştır Özelliği kapalıdır.");
            }
         }
        private void button1_Click(object sender, EventArgs e)
        {
            string tarih;
            tarih = DateTime.Now.ToString("dd") +"/"+ DateTime.Now.ToString("MM") +"/"+ DateTime.Now.ToString("yyyy");

            try
            {
                
                if (textBox1.Text.Trim() != String.Empty && textBox2.Text.Trim() != String.Empty && textBox3.Text.Trim() != String.Empty && textBox4.Text.Trim() != String.Empty && textBox5.Text.Trim() != String.Empty)
                {
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Insert into musteri (m_ad, m_soyad, m_mail, m_kayitarih, m_tel, m_adres)VALUES  ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + tarih + "','" + textBox4.Text + "','" + textBox5.Text + "')", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.musteri_ekleme(Form1.adi, Form1.soyadi, Form1.id,textBox3.Text);
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
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt işleminde hata! Lütfen tüm bilgileri girdiğinizden emin olun !", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
