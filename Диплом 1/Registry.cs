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
    public partial class Registry : Form
    {
        public string path = "Host=localhost;Username=postgres;Password=cxNTVJas;Database=Families";
        public string id_member, id_family;

        public Registry()
        {
            InitializeComponent();
        }

        // Кнопка Зарегистрироваться

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count =0;
                int count2 = 0;
                using (NpgsqlConnection con = new NpgsqlConnection(path))
                {
                    string query = "select id_member from modes order by id_member desc limit 1";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                    }
                }
                using (NpgsqlConnection con = new NpgsqlConnection(path))
                {
                    string query2 = "select id_family from families order by id_family desc limit 1";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                    con.Open();
                    NpgsqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            count2 = Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                    }
                }


                NpgsqlConnection contact2 = new NpgsqlConnection(path);
                string sql2 = "insert into families(id_family) values(@id_family)";
                NpgsqlCommand command2 = new NpgsqlCommand(sql2, contact2);
                command2.Parameters.AddWithValue("@id_family", count2 + 1);
                contact2.Open();
                command2.ExecuteNonQuery();
                contact2.Close();

                NpgsqlConnection contact3 = new NpgsqlConnection(path);
                string sql3 = "insert into members(id_member,id_family,surname,name, patronymic,member) values(@id_member,@id_family,@surname,@name,@patronymic, @member)";
                NpgsqlCommand command3 = new NpgsqlCommand(sql3, contact3);
                command3.Parameters.AddWithValue("@id_member", count + 1);
                command3.Parameters.AddWithValue("@id_family", count2 + 1);
                command3.Parameters.AddWithValue("@surname", textBox1.Text);
                command3.Parameters.AddWithValue("@name", textBox2.Text);
                command3.Parameters.AddWithValue("@patronymic", textBox3.Text);
                command3.Parameters.AddWithValue("@member", "Глава");
                contact3.Open();
                command3.ExecuteNonQuery();
                contact3.Close();

                NpgsqlConnection contact = new NpgsqlConnection(path);
                string sql = "insert into modes(id_member,mode,login,password) values(@id_member,@mode,@login,@password)";
                NpgsqlCommand command = new NpgsqlCommand(sql, contact);
                command.Parameters.AddWithValue("@id_member", count + 1);
                command.Parameters.AddWithValue("@mode", "Администратор");
                command.Parameters.AddWithValue("@login", textBox4.Text);
                command.Parameters.AddWithValue("@password", textBox5.Text);
                contact.Open();
                command.ExecuteNonQuery();
                contact.Close();
                id_member = Convert.ToString(count + 1);
                id_family = Convert.ToString(count2 + 1);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        // Нажатие на кнопку Выйти
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Entrance ip = new Entrance();
            ip.Show();
        }
    }
}
