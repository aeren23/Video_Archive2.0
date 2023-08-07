using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Video_Archive2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"SQLDatabase_String_Name");

        void addVideoToList()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT NAME,CATEGORY FROM VIDEOS", baglanti);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["NAME"].ToString() + " --" + reader["CATEGORY"].ToString());
            }
            reader.Close();
            sqlCommand.Dispose();
            baglanti.Close();
        }

        void addVideoToCombo()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT NAME FROM VIDEOS", baglanti);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["NAME"].ToString());
            }
            reader.Close();
            sqlCommand.Dispose();
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addVideoToList();
            addVideoToCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO VIDEOS (NAME,CATEGORY,LINK) values (@P1,@P2,@P3)", baglanti);
            cmd.Parameters.AddWithValue("@P1", textBox1.Text);
            cmd.Parameters.AddWithValue("@P2", comboBox1.Text);
            cmd.Parameters.AddWithValue("@P3", textBox3.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            baglanti.Close();
            listBox1.Items.Clear();
            comboBox2.Items.Clear();
            addVideoToCombo();
            addVideoToList();
            MessageBox.Show("Video Arşive Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string link;
            string VideoName = comboBox2.Text;
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT LINK FROM VIDEOS WHERE NAME=@P1", baglanti);
            sqlCommand.Parameters.AddWithValue("@P1", VideoName);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                link = reader["LINK"].ToString();
                webBrowser1.Navigate(link);
            }
            reader.Close();
            sqlCommand.Dispose();
            baglanti.Close();
        }
    }
}
