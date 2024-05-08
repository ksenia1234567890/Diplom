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
        string id_member;
        string id_family;

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

                // Передача данных о пользователе

                NpgsqlConnection contact = new NpgsqlConnection(path);
                string sql = "select id_member from modes where login=@login and password=@password";
                NpgsqlCommand command = new NpgsqlCommand(sql, contact);
                command.Parameters.AddWithValue("@login", textBox1.Text);
                command.Parameters.AddWithValue("@password", textBox2.Text);
                contact.Open();
                id_member = command.ExecuteScalar().ToString();
                contact.Close();

                NpgsqlConnection connect2 = new NpgsqlConnection(path);
                string query2 = "select id_family from members where id_member=@id_member";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, connect2);
                cmd.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                connect.Open();
                id_family = (string)cmd.ExecuteScalar();
                connect.Close();

                if (mode == string.Empty || id_member == string.Empty)
                {
                    throw new Exception("Пользователь не найден");
                }
                MessageBox.Show($"{id_member} {mode} {id_family}");
                ImportantPage ip = new ImportantPage(id_member, mode, id_family);
                this.Hide();
                ip.ShowDialog();

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
