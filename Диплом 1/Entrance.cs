using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Диплом_1
{
    public partial class Entrance : Form
    {
        public string path = "Host=localhost;Username=postgres;Password=cxNTVJas;Database=Families";
        string mode;

        public Entrance()
        {
            InitializeComponent();
        }


        // Вход в систему
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection connect = new NpgsqlConnection(path);
                string query = "select mode from modes where login=@login and password=@password";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@login", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                connect.Open();
                mode = (string)cmd.ExecuteScalar();
                connect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Переход на страницу регистрации

        private void button2_Click(object sender, EventArgs e)
        {
            Registry registry = new Registry();
            registry.ShowDialog();
            this.Hide();
        }
    }
}
