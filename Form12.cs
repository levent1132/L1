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
    public partial class Form12 : Form
    {
        
        string personel = "";

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            log.Bagla.Open();// personel isim listesi combolara aktarımı
            log.komut = new MySqlCommand("select p_ad,p_soyad from personel", log.Bagla);
            MySqlDataReader dr = log.komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["p_ad"].ToString()+" "+dr["p_soyad"].ToString());

            }
            log.Bagla.Close();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int urun = 0;
            string tarih, s_tarih = "", personelad, personelsoyad, personelid = "";
            if (textBox4.Text.Trim() != String.Empty && textBox5.Text.Trim() != String.Empty && comboBox1.Text.Trim() != String.Empty)
            {// kayıt icin tüm alalnların kontrolu
                
                tarih = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                personel = comboBox1.SelectedItem.ToString();
                int bosluk = personel.IndexOf(" ", 0);
                personelad = personel.Substring(0, bosluk);
                personelsoyad = personel.Substring(bosluk + 1);
            }
            else
            {
                MessageBox.Show("Kayıt işleminde hata! Ürün kod hatalı yada boş kayıt alanıları mevcut!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Clear();
                textBox5.Clear();
                return;
            }
            
            try
            {
                log.Bagla.Open();
                log.komut = new MySqlCommand("select cikis.u_id from cikis,urun where urun.u_id=cikis.u_id and urun.u_kod='" + textBox4.Text + "' ", log.Bagla);
                urun = Convert.ToInt32(log.komut.ExecuteScalar());
                log.Bagla.Close();
                if (urun!=0)
                {
                    
                    log.Bagla.Open();
                    log.komut = new MySqlCommand("select cikis.c_satistarih from cikis,urun where urun.u_id=cikis.u_id and urun.u_kod='"+textBox4.Text +"' ", log.Bagla);
                    MySqlDataReader dr = log.komut.ExecuteReader();
                    while (dr.Read())
                    {
                        s_tarih = dr["c_satistarih"].ToString();
                    }
                    log.Bagla.Close();


                    log.Bagla.Open();
                    log.komut = new MySqlCommand("select * from personel where p_ad='"+personelad+"' and p_soyad='"+personelsoyad+"' ", log.Bagla);
                    dr = log.komut.ExecuteReader();
                    while (dr.Read())
                    {
                        personelid = dr["p_id"].ToString();
                    }
                    log.Bagla.Close();
                    

                    log.Bagla.Open();
                    log.komut = new MySqlCommand("Insert into servis (p_id,u_kod,c_satistarih,s_giristarih,s_sorun)VALUES  ('" +personelid +"','" + textBox4.Text + "','" + s_tarih + "','" + tarih + "','" + textBox5.Text + "')", log.Bagla);
                    log.komut.ExecuteNonQuery();
                    log.Bagla.Close();
                    log.servis_kayit(Form1.adi, Form1.soyadi, Form1.id, textBox4.Text);
                    MessageBox.Show("Servise ürün başarıyla kayıt olmuştur..", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    Form2 form = new Form2();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Kayıt işleminde hata! Ürün kod hatalı yada boş kayıt alanıları mevcut!", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        

        
    }
}
