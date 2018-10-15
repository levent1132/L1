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
    public partial class Form8 : Form
    {
        
        public Form8()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ktarih;
            
            try
            {

                if (textBox1.Text.Trim() != String.Empty && textBox2.Text.Trim() != String.Empty && textBox3.Text.Trim() != String.Empty && comboBox2.Text.Trim() != String.Empty && textBox9.Text.Trim() != String.Empty)
                {
                    ktarih = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Insert into urun (u_kod, u_marka, u_model, u_tur,u_alisfiyati, u_alistarihi) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "','" + textBox9.Text + "','" + ktarih + "')", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.urun_ekleme(Form1.adi, Form1.soyadi, Form1.id, textBox1.Text);
                    MessageBox.Show("Yeni ürün başarıyla kayıt olmuştur..", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("Kayıt işleminde hata! Lütfen tüm bilgileri girdiğinizden emin olun !", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
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

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

       
    }
}
