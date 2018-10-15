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
    public partial class Form11 : Form
    {
        
        public string personel_id,musteri_id = "";
        public DialogResult sonuc;
                
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            /*-------   müşteri ------------*/
            log.Bagla.Open();
            log.Adaptor = new MySqlDataAdapter("select m_id,m_ad,m_soyad from musteri", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            dataGridView1.DataSource = log.Bs;

            dataGridView1.Columns[1].Width = 85;
            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[2].Width = 64;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Adı";
            dataGridView1.Columns[2].HeaderText = "Soyadı";

            /*-------   personel ------------*/
            log.Adaptor = new MySqlDataAdapter("select p_id,p_ad,p_soyad from personel", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            dataGridView2.DataSource = log.Bs;
            dataGridView2.Columns[1].Width = 90;
            dataGridView2.Columns[0].Width = 25;
            dataGridView2.Columns[2].Width = 64;
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[1].HeaderText = "Adı";
            dataGridView2.Columns[2].HeaderText = "Soyadı";
            
            log.Bagla.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty) // aramaalını boş kontrol
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("select m_id,m_ad,m_soyad from musteri  where m_ad like '" + arama + "%' or m_soyad like '" + arama + "%'  ", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    log.Bagla.Close();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            musteri_id  = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            label4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            personel_id = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text.Trim() != string.Empty && label3.Text.Trim() != string.Empty) //müşteri ve personel secme kontrolü
            {
                sonuc = MessageBox.Show(" '" + label1.Text +" "+ label2.Text + " ve " + label3.Text +" "+ label4.Text + "' Kişilerini seçmek istediğinzden emin misiniz? ", "Seçme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes)// secim işlemi onayı
                {
                    MessageBox.Show("Kişiler seçildi..", "Seçme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen istenilen ürünün seçili olduğundan eminin olun!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
