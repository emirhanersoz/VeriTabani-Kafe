using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriTabanı_Kafe
{
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti;
        NpgsqlCommand komut;
        NpgsqlDataAdapter da;
        
        public Form1()
        {
            InitializeComponent();
         
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void MusteriGetir()
        {
            baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=Cafe; userID=postgres ; password=3707");
            baglanti.Open();
            da = new NpgsqlDataAdapter("SELECT *FROM musteri", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO musteri(musteri_no,kullanici_adi,sifre,adi,soyadi) VALUES (@musteri_no,@kullanici_adi,@sifre,@adi,@soyadi)";
            komut = new NpgsqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@musteri_no", int.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@kullanici_adi", textBox2.Text);
            komut.Parameters.AddWithValue("@sifre", textBox3.Text.ToString());
            komut.Parameters.AddWithValue("@adi", textBox4.Text);
            komut.Parameters.AddWithValue("@soyadi", textBox5.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM musteri WHERE musteri_no = @musteri_no";
            komut = new NpgsqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@musteri_no", Convert.ToInt32(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE musteri SET kullanici_adi = @kullanici_adi, sifre = @sifre, adi = @adi, soyadi = @soyadi WHERE musteri_no = @musteri_no";
            komut = new NpgsqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@musteri_no", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@kullanici_adi", textBox2.Text);
            komut.Parameters.AddWithValue("@sifre", textBox3.Text);
            komut.Parameters.AddWithValue("@adi", textBox4.Text);
            komut.Parameters.AddWithValue("@soyadi", textBox5.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tablo = new DataTable();
            da = new NpgsqlDataAdapter("SELECT * FROM musteri WHERE kullanici_adi LIKE '%" + textBox2.Text + "%'", baglanti);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }
    }
}
