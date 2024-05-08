using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Диплом_1
{
    public partial class Report : Form
    {
        public string path = "Host=localhost;Username=postgres;Password=cxNTVJas;Database=Families";
        public string id_member = "";
        public string mode = "";
        public string id_family = "";


        public Report(string id_member, string mode, string id_family)
        {
            InitializeComponent();
            this.id_member = id_member;
            this.mode = mode;
            this.id_family = id_family;
        }
        

        // Нажатие на кнопку Выйти

        private void button2_Click(object sender, EventArgs e)
        {
            ImportantPage ip = new ImportantPage(id_member, mode, id_family);
            this.Close();
            ip.Show();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 0));

        }

        // Нажатие на кнопку Распечатать

        private void button5_Click(object sender, EventArgs e)
        {
            PrintDialog letter = new PrintDialog();
            letter.Document = printDocument1;
            DialogResult result = letter.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "Отчёт за неделю")
                {
                    richTextBox2.Clear();
                    string str = "";
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string commandSql = "SELECT * FROM expenses WHERE date_expenses>=current_date-interval ' 7 days'";
                        NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                        connect.Open();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                str += "\nНомер: " + (reader.GetValue(0).ToString());
                                str += "\nИдентфикатор: " + (reader.GetValue(1).ToString());
                                str += "\nОписание: " + (reader.GetValue(2).ToString());
                                str += "\nКатегория: " + (reader.GetValue(3).ToString());
                                str += "\nТип: " + (reader.GetValue(4).ToString());
                                str += "\nСумма: " + (reader.GetValue(5).ToString());
                                str += "\nДата: " + (reader.GetValue(6).ToString()) + "\n";
                            }
                        }
                        connect.Close();
                        richTextBox2.Text += str;
                    }
                    using (NpgsqlConnection connection = new NpgsqlConnection(path))
                    {
                        richTextBox2.Text += "\n---------------------";
                        string command = "SELECT * FROM income WHERE date_income >=current_date-interval ' 7 days'";
                        NpgsqlCommand commanda = new NpgsqlCommand(command, connection);
                        connection.Open();
                        NpgsqlDataReader reader2 = commanda.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                str += "\nНомер: " + (reader2.GetValue(0).ToString());
                                str += "\nИдентификатор: " + (reader2.GetValue(1).ToString());
                                str += "\nТип: " + (reader2.GetValue(2).ToString());
                                str += "\nСумма: " + (reader2.GetValue(3).ToString());
                                str += "\nДата: " + (reader2.GetValue(4).ToString()) + "\n";
                            }
                        }
                        connection.Close();
                        richTextBox2.Text += str;
                    }
                }
                if (comboBox2.Text == "Отчёт за месяц")
                {
                    richTextBox2.Clear();
                    string str = "";
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string commandSql = "SELECT * FROM expenses WHERE date_expenses >= current_date-interval ' 30 days'";
                        NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                        connect.Open();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                str += "\nНомер: " + (reader.GetValue(0).ToString());
                                str += "\nИдентфикатор: " + (reader.GetValue(1).ToString());
                                str += "\nОписание: " + (reader.GetValue(2).ToString());
                                str += "\nКатегория: " + (reader.GetValue(3).ToString());
                                str += "\nТип: " + (reader.GetValue(4).ToString());
                                str += "\nСумма: " + (reader.GetValue(5).ToString());
                                str += "\nДата: " + (reader.GetValue(6).ToString()) + "\n";
                            }
                        }
                        connect.Close();
                        richTextBox2.Text += str;
                    }
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        richTextBox2.Text += "\n---------------------";
                        string commandSql = "SELECT * FROM income WHERE date_income >= current_date-interval ' 30 days'";
                        NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                        connect.Open();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                str += "\nНомер: " + (reader.GetValue(0).ToString());
                                str += "\nИдентфикатор: " + (reader.GetValue(1).ToString());
                                str += "\nТип: " + (reader.GetValue(2).ToString());
                                str += "\nСумма: " + (reader.GetValue(3).ToString());
                                str += "\nДата: " + (reader.GetValue(4).ToString()) + "\n";
                                
                            }
                        }
                        connect.Close();
                        richTextBox2.Text += str;
                    }
                }
                if (comboBox2.Text == "Отчёт по накоплениям")
                {
                    string str = "";
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string commandSql = "SELECT * FROM savings";
                        NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                        connect.Open();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                str += "\nНомер: " + (reader.GetValue(0).ToString());
                                str += "\nИдентфикатор: " + (reader.GetValue(1).ToString());
                                str += "\nОписание: " + (reader.GetValue(2).ToString());
                                str += "\nСумма: " + (reader.GetValue(3).ToString()) + "\n";
                            }
                        }
                        connect.Close();
                        richTextBox2.Text += str;
                    }
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
