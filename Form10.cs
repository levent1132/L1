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
    public partial class Form10 : Form
    {
        
        public string urun_kodu = "";
        public DialogResult sonuc;

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            log.Bagla.Open();
            log.Adaptor = new MySqlDataAdapter("select  urun.* from urun,cikis  where urun.u_id  NOT IN  (select urun.u_id from cikis,urun where urun.u_id=cikis.u_id)  group by urun.u_id", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            dataGridView1.DataSource = log.Bs;
            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[1].Width = 45;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Ürün Kodu";
            dataGridView1.Columns[2].HeaderText = "Marka";
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[4].HeaderText = "Tür";
            dataGridView1.Columns[5].HeaderText = "Alış Tarihi";
            dataGridView1.Columns[6].HeaderText = "Alış Fiyat(TL)";
            log.Bagla.Close();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                label3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                label4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                label2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                label5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                label6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                urun_kodu = label1.Text.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            try
            {
                if (textBox1.Text.Trim() != String.Empty) //arama alanı kontrolü
                {
                    log.Bagla.Open();
                    log.Adaptor = new MySqlDataAdapter("select  urun.* from urun,cikis  where urun.u_id  NOT IN  (select urun.u_id from cikis,urun where urun.u_id=cikis.u_id) and urun.u_kod='"+ textBox1.Text +"'  group by urun.u_id", log.Bagla);
                    log.Ds = new DataSet();
                    log.Bs = new BindingSource();
                    log.Adaptor.Fill(log.Ds, "veri");
                    log.Bs.DataSource = log.Ds.Tables["veri"];
                    dataGridView1.DataSource = log.Bs;
                    log.Bagla.Close();
                }
                else // arama alanı eğer boş ise
                {
                    MessageBox.Show("Arama işleminde hata! Lütfen arama alanının boş olmadığından emin olun!   *Sadece Ürün kod numarasına göre arama yapınız.", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }

            }
            catch (Exception) //parametrele dışı arama
            {
                MessageBox.Show("Arama işleminde hata! Lütfen arama için geçerli parametreler giriniz!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text.Trim() != string.Empty) // bir ürün secildiğini onay
            {//en son kullanıcı kontrolü
                sonuc = MessageBox.Show(" '" + label3.Text + " " + label4.Text + "' Ürünü seçmek istediğinzden emin misiniz? ", "Ekleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sonuc == DialogResult.Yes) // onay verilirse
                {
                    MessageBox.Show("Ürün eklendi..", "Ekleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen istenilen ürünün seçili olduğundan eminin olun!", "Arama İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
