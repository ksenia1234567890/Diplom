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
    public partial class ImportantPage : Form
    {
        public string id_member;
        public string mode;
        public string id_family;
        public string path = "Host=localhost;Username=postgres;Password=cxNTVJas;Database=Families";

        Label descriptionA = new Label();
        TextBox descriptionB = new TextBox();
        Label category = new Label();
        TextBox categoryB = new TextBox();
        Label type = new Label();
        TextBox typeB = new TextBox();
        Label price = new Label();
        TextBox priceB = new TextBox();

        Label typeA = new Label();
        TextBox typeC = new TextBox();
        Label priceA = new Label();
        TextBox priceC = new TextBox();

        Label description = new Label();
        TextBox descriptionF = new TextBox();
        Label amount = new Label();
        TextBox amountB = new TextBox();

        Label surnameA = new Label();
        TextBox surnameB = new TextBox();
        Label nameA = new Label();
        TextBox nameB = new TextBox();
        Label patronymicA = new Label();
        TextBox patronymicB = new TextBox();
        Label memberA = new Label();
        TextBox memberB = new TextBox();

        public ImportantPage(string id_member, string mode, string id_family)
        {
            InitializeComponent();
            this.id_member = id_member;
            this.mode = mode;
            this.id_family = id_family;
        }

        // При загрузке формы загружается сумма бюджета
        private void ImportantPage_Load(object sender, EventArgs e)
        {
            button5.Hide();
            button6.Hide();
            button7.Hide();
            comboBox3.Hide();
            button4.Hide();

            MessageBox.Show($"{id_member} {mode} {id_family}");

            // Подсчёт доходов
            try {
                
                int income=0;
                using (NpgsqlConnection connect = new NpgsqlConnection(path))
                {
                    string commandSql = $"select price from income";
                    NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                    connect.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            income += Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                    }
                }

                // Подсчёт расходов

                int expenses = 0;
                using (NpgsqlConnection connect = new NpgsqlConnection(path))
                {
                    string commandSql = $"select price from expenses ";
                    NpgsqlCommand command = new NpgsqlCommand(commandSql, connect);
                    connect.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            expenses += Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                    }
                }

                // Итоговая сумма

                int price = income - expenses;
                label4.Text = price.ToString();

               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        // Нажатие на кнопку Отобразить

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Расходы")
                {
                    comboBox3.Show();
                    button4.Show();

                    // Добавление категорий в комбобокс

                    NpgsqlConnection conn = new NpgsqlConnection(path);
                    string commandSql = "select category from expenses";
                    NpgsqlCommand command = new NpgsqlCommand(commandSql, conn);
                    conn.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (comboBox3.Items.Contains(reader.GetValue(0).ToString()) == false)
                            {
                                comboBox3.Items.Add(reader.GetValue(0).ToString());
                            }
                        }
                    }
                    conn.Close();

                    RefreshExpenses();
                    panel1.Controls.Clear();
                    label5.Hide();


                    descriptionA.Parent = panel1;
                    descriptionA.Location = new Point(27, 15);
                    descriptionA.Text = "Описание";
                    descriptionA.Show();

                    descriptionB.Parent = panel1;
                    descriptionB.Location = new Point(25, 39);
                    descriptionB.Size = new Size(241, 20);

                    category.Parent = panel1;
                    category.Location = new Point(27, 90);
                    category.Text = "Категория";

                    categoryB.Parent = panel1;
                    categoryB.Location = new Point(25, 114);
                    categoryB.Size = new Size(241, 20);


                    type.Parent = panel1;
                    type.Location = new Point(27, 170);
                    type.Text = "Тип";

                    typeB.Parent = panel1;
                    typeB.Location = new Point(25, 194);
                    typeB.Size = new Size(241, 20);


                    price.Parent = panel1;
                    price.Location = new Point(27, 243);
                    price.Text = "Цена";

                    priceB.Parent = panel1;
                    priceB.Location = new Point(25, 267);
                    priceB.Size = new Size(241, 20);

                    button5.Show();
                    button5.Location = new Point(2, 306);
                    button5.Parent = panel1;
                    button6.Show();
                    button6.Parent = panel1;
                    button6.Location = new Point(99, 306);
                    button7.Show();
                    button7.Location = new Point(196, 306);
                    button7.Parent = panel1;

                }
                if(comboBox1.Text == "Члены семьи")
                {
                    comboBox3.Hide();
                    button4.Hide();
                    RefreshMembers();
                    panel1.Controls.Clear();
                label5.Hide();


                surnameA.Parent = panel1;
                surnameA.Location = new Point(27, 15);
                surnameA.Text = "Фамилия";
                surnameA.Show();

                surnameB.Parent = panel1;
                surnameB.Location = new Point(25, 39);
                surnameB.Size = new Size(241, 20);
                surnameB.Show();

                nameA.Parent = panel1;
                nameA.Location = new Point(27, 90);
                nameA.Text = "Имя";
                    nameA.Show();

                nameB.Parent = panel1;
                nameB.Location = new Point(25, 114);
                nameB.Size = new Size(241, 20);
                    nameB.Show();


                patronymicA.Parent = panel1;
                patronymicA.Location = new Point(27, 170);
                patronymicA.Text = "Отчество";
                    patronymicA.Show();

                patronymicB.Parent = panel1;
                patronymicB.Location = new Point(25, 194);
                patronymicB.Size = new Size(241, 20);
                    patronymicB.Show();

                    memberA.Parent = panel1;
                    memberA.Location = new Point(27, 243);
                    memberA.Text = "Член семьи";
                    memberA.Show();

                    memberB.Parent = panel1;
                    memberB.Location = new Point(25, 267);
                    memberB.Size = new Size(241, 20);
                    memberB.Show();

                    button5.Show();
                    button5.Location = new Point(2, 306);
                    button5.Parent = panel1;
                    button6.Show();
                    button6.Parent = panel1;
                    button6.Location = new Point(99, 306);
                    button7.Show();
                    button7.Location = new Point(196, 306);
                    button7.Parent = panel1;

                }

                if (comboBox1.Text == "Доходы")
                {
                    comboBox3.Hide();
                    button4.Hide();
                    RefreshIncome();
                    panel1.Controls.Clear();
                    label5.Hide();
                    if (mode == "Администратор")
                    {

                        typeA.Parent = panel1;
                        typeA.Location = new Point(27, 15);
                        typeA.Text = "Тип";

                        typeC.Parent = panel1;
                        typeC.Location = new Point(25, 39);
                        typeC.Size = new Size(241, 20);


                        priceA.Parent = panel1;
                        priceA.Location = new Point(27, 90);
                        priceA.Text = "Сумма";

                        priceC.Parent = panel1;
                        priceC.Location = new Point(25, 114);
                        priceC.Size = new Size(241, 20);

                        button5.Show();
                        button5.Location = new Point(0, 151);
                        button5.Parent = panel1;
                        button6.Show();
                        button6.Location = new Point(97, 151);
                        button6.Parent = panel1;
                        button7.Show();
                        button7.Location = new Point(194, 151);
                        button7.Parent = panel1;
                    }

                }
                if (comboBox1.Text == "Накопления")
                {
                    comboBox3.Hide();
                    button4.Hide();
                    RefreshSavings();
                    panel1.Controls.Clear();
                    label5.Hide();
                    if (mode == "Администратор")
                    {
                        description.Parent = panel1;
                        description.Location = new Point(27, 15);
                        description.Text = "Описание";

                        descriptionF.Parent = panel1;
                        descriptionF.Location = new Point(25, 39);
                        descriptionF.Size = new Size(241, 20);


                        amount.Parent = panel1;
                        amount.Location = new Point(27, 90);
                        amount.Text = "Сумма";

                        amountB.Parent = panel1;
                        amountB.Location = new Point(25, 114);
                        amountB.Size = new Size(241, 20);

                        button5.Show();
                        button5.Location = new Point(0, 151);
                        button5.Parent = panel1;
                        button6.Show();
                        button6.Location = new Point(97, 151);
                        button6.Parent = panel1;
                        button7.Show();
                        button7.Location = new Point(194, 151);
                        button7.Parent = panel1;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Нажатие на кнопку Добавить

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Расходы")
                {
                    int count = 0;
                    
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string query = "select id_expenses from expenses order by id_expenses desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader.GetValue(0).ToString());
                            }
                        }
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO expenses(id_expenses, id_member, description, category, type, price, date_expenses)" +
                        "VALUES(@id_expenses, @id_member, @description, @category, @type, @price, @date_expenses)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_expenses", count + 1);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@description", descriptionB.Text);
                    cmd2.Parameters.AddWithValue("@category", categoryB.Text);
                    cmd2.Parameters.AddWithValue("@type", typeB.Text);
                    cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(priceB.Text));
                    cmd2.Parameters.AddWithValue("@date_expenses", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshExpenses();

                }
                if (comboBox1.Text == "Доходы")
                {
                   
                    int count = 0;
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string query = "select id_income from income order by id_income desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader.GetValue(0).ToString());
                            }
                        }
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO income(id_income, id_member, type, price, date_income)" +
                        "VALUES(@id_income, @id_member, @type, @price, @date_income)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_income", count + 1);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@type", typeC.Text);
                    cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(priceC.Text));
                    cmd2.Parameters.AddWithValue("@date_income", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshIncome();
                }

                if (comboBox1.Text == "Накопления")
                {
                    int count = 0;
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string query = "select id_saving from savings order by id_saving desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader.GetValue(0).ToString());
                            }
                        }
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO savings(id_saving, id_member, description, amount)" +
                        "VALUES(@id_saving, @id_member, @description, @amount)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_saving", count + 1);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@description", descriptionF.Text);
                    cmd2.Parameters.AddWithValue("@amount", Convert.ToInt32(amountB.Text));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshSavings();
                }
                if (comboBox1.Text == "Члены семьи")
                {
                    int count = 0;
                    using (NpgsqlConnection connect = new NpgsqlConnection(path))
                    {
                        string query = "select id_member from members order by id_member desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader.GetValue(0).ToString());
                            }
                        }
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO members(id_member, id_family, surname, name, patronymic, member)" +
                        "VALUES(@id_member, @id_family, @surname, @name, @patronymic, @member)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_member", count+1);
                    cmd2.Parameters.AddWithValue("@id_family", Convert.ToInt32(id_family));
                    cmd2.Parameters.AddWithValue("@surname", surnameB.Text);
                    cmd2.Parameters.AddWithValue("@name", nameB.Text);
                    cmd2.Parameters.AddWithValue("@patronymic", patronymicB.Text);
                    cmd2.Parameters.AddWithValue("@member", memberB.Text);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();

                    NpgsqlConnection con3 = new NpgsqlConnection(path);
                    string query3 = "INSERT INTO modes(id_member, mode, login, password)" +
                        "VALUES(@id_member, @mode, @login, @password)";
                    NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con3);
                    cmd2.Parameters.AddWithValue("@id_member", count + 1);
                    cmd2.Parameters.AddWithValue("@mode", "Пользователь");
                    cmd2.Parameters.AddWithValue("@login", surnameB.Text+"1");
                    cmd2.Parameters.AddWithValue("@pawwword", nameB.Text+"2");
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();

                    RefreshMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Нажатие на кнопку Изменить
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Расходы")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "UPDATE expenses" +
                        "SET id_member=@id_member, description=@description, category=@category, " +
                        "type=@type, price=@price, date_expenses=@date_expenses WHERE id_expenses = @id_expenses";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_expenses", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@description", descriptionB.Text);
                    cmd2.Parameters.AddWithValue("@category", categoryB.Text);
                    cmd2.Parameters.AddWithValue("@type", typeB.Text);
                    cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(priceB.Text));
                    cmd2.Parameters.AddWithValue("@date_expenses", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshExpenses();
                }

                if (comboBox1.Text == "Члены семьи")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "UPDATE members" +
                        "SET id_family=@id_family, surname=@surname, " +
                        "name=@name, patronymic=@patronymic, memder=@member WHERE id_member=@id_member";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_family", Convert.ToInt32(id_family));
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    cmd2.Parameters.AddWithValue("@surname", descriptionB.Text);
                    cmd2.Parameters.AddWithValue("@name", categoryB.Text);
                    cmd2.Parameters.AddWithValue("@patronymic", typeB.Text);
                    cmd2.Parameters.AddWithValue("@member", priceB.Text);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshMembers();
                }

                if (comboBox1.Text == "Доходы")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "UPDATE income SET id_member=@id_member, type=@type, price=@price, date_income=@date_income" +
                        "WHERE id_income = @id_income";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_income", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@type", typeC.Text);
                    cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(priceC.Text));
                    cmd2.Parameters.AddWithValue("@date_income", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshIncome();
                }

                if (comboBox1.Text == "Накопления")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "UPDATE savings SET id_member=@id_member, description=@description, amount=@amount" +
                        "WHERE id_saving=@id_saving)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_saving", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@description", descriptionF.Text);
                    cmd2.Parameters.AddWithValue("@amount", Convert.ToInt32(amountB.Text));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshSavings();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Нажатие на кнопку Удалить

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Расходы")
                {
                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "DELETE FROM expenses WHERE id_expenses=@id_expenses";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_expenses", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshExpenses();
                }
                if (comboBox1.Text == "Доходы")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "DELETE FROM income WHERE id_income = @id_income";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_income", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshIncome();
                }

                if (comboBox1.Text == "Члены семьи")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "DELETE FROM members WHERE id_member = @id_member";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshMembers();
                }

                if (comboBox1.Text == "Накопления")
                {

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "DELETE FROM savings WHERE id_saving=@id_saving)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_saving", Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()));
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    RefreshSavings();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Нажатие на кнопку Отсортировать

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection connect = new NpgsqlConnection(path);
                string query = $"select * from expenses where category='{comboBox3.Text}'";
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connect);
                DataSet ds = new DataSet();
                connect.Open();
                dataAdapter.Fill(ds, "expenses");
                dataGridView1.DataSource = ds.Tables["expenses"];
                connect.Close();
                dataGridView1.Columns[0].HeaderText = "Идентификатор";
                dataGridView1.Columns[1].HeaderText = "Код члена семьи";
                dataGridView1.Columns[2].HeaderText = "Описание";
                dataGridView1.Columns[3].HeaderText = "Категория";
                dataGridView1.Columns[4].HeaderText = "Тип";
                dataGridView1.Columns[5].HeaderText = "Сумма";
                dataGridView1.Columns[6].HeaderText = "Дата";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Нажатие на кнопку Посмотреть отчёт

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Report rep = new Report(id_member, mode, id_family);
            rep.ShowDialog();
        }

        public void RefreshExpenses()
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Код члена семьи";
            dataGridView1.Columns[2].HeaderText = "Описание";
            dataGridView1.Columns[3].HeaderText = "Категория";
            dataGridView1.Columns[4].HeaderText = "Тип";
            dataGridView1.Columns[5].HeaderText = "Сумма";
            dataGridView1.Columns[6].HeaderText = "Дата";

            List<int> members = new List<int>();
            SignMembers(members);
            using (NpgsqlConnection connect = new NpgsqlConnection(path))
            {
                for(int i=0; i<members.Count; i++)
                {
                    string str = $"select * from expenses where id_member = {Convert.ToInt32(members[i])}";
                    NpgsqlCommand cmd = new NpgsqlCommand(str, connect);
                    connect.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);
                            row.Cells[0].Value = reader.GetValue(0).ToString();
                            row.Cells[1].Value = reader.GetValue(1).ToString();
                            row.Cells[2].Value = reader.GetValue(2).ToString();
                            row.Cells[3].Value = reader.GetValue(3).ToString();
                            row.Cells[4].Value = reader.GetValue(4).ToString();
                            row.Cells[5].Value = reader.GetValue(5).ToString();
                            row.Cells[6].Value = reader.GetValue(6).ToString();
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        public void RefreshIncome()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Код члена семьи";
            dataGridView1.Columns[2].HeaderText = "Тип";
            dataGridView1.Columns[3].HeaderText = "Cумма";
            dataGridView1.Columns[4].HeaderText = "Дата";

            List<int> members = new List<int>();
            SignMembers(members);
            for (int i = 0; i < members.Count; i++)
            {
                using (NpgsqlConnection connect = new NpgsqlConnection(path))
                {
                    string command = "select * from income where id_member = @id_member";
                    NpgsqlCommand cmd = new NpgsqlCommand(command, connect);
                    cmd.Parameters.AddWithValue("@id_member", members[i]);
                    connect.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);
                            row.Cells[0].Value = reader.GetValue(0).ToString();
                            row.Cells[1].Value = reader.GetValue(1).ToString();
                            row.Cells[2].Value = reader.GetValue(2).ToString();
                            row.Cells[3].Value = reader.GetValue(3).ToString();
                            row.Cells[4].Value = reader.GetValue(4).ToString();
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        public void RefreshSavings()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Код члена семьи";
            dataGridView1.Columns[2].HeaderText = "Описание";
            dataGridView1.Columns[3].HeaderText = "Cумма";
            List<int> members = new List<int>();
            SignMembers(members);
            using (NpgsqlConnection connect = new NpgsqlConnection(path))
            {
                for (int i = 0; i < members.Count; i++)
                {
                    string str = $"select * from expenses where id_member = {Convert.ToInt32(members[i])}";
                    NpgsqlCommand cmd = new NpgsqlCommand(str, connect);
                    connect.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);
                            row.Cells[0].Value = reader.GetValue(0).ToString();
                            row.Cells[1].Value = reader.GetValue(1).ToString();
                            row.Cells[2].Value = reader.GetValue(2).ToString();
                            row.Cells[3].Value = reader.GetValue(3).ToString();
                            row.Cells[4].Value = reader.GetValue(4).ToString();
                            row.Cells[5].Value = reader.GetValue(5).ToString();
                            row.Cells[6].Value = reader.GetValue(6).ToString();
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }

            }

        public void RefreshMembers()
        {
            dataGridView1.ColumnCount = 6; 
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Код семьи";
            dataGridView1.Columns[2].HeaderText = "Фамилия";
            dataGridView1.Columns[3].HeaderText = "Имя";
            dataGridView1.Columns[4].HeaderText = "Отчество";
            dataGridView1.Columns[5].HeaderText = "Член семьи";
            List<int> members = new List<int>();
            SignMembers(members);
            using (NpgsqlConnection connect = new NpgsqlConnection(path))
            {
                for (int i = 0; i < members.Count; i++)
                {
                    string str = $"select * from expenses where id_member = {Convert.ToInt32(members[i])}";
                    NpgsqlCommand cmd = new NpgsqlCommand(str, connect);
                    connect.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);
                            row.Cells[0].Value = reader.GetValue(0).ToString();
                            row.Cells[1].Value = reader.GetValue(1).ToString();
                            row.Cells[2].Value = reader.GetValue(2).ToString();
                            row.Cells[3].Value = reader.GetValue(3).ToString();
                            row.Cells[4].Value = reader.GetValue(4).ToString();
                            row.Cells[5].Value = reader.GetValue(5).ToString();
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
            }

        // Нажатие на кнопку Выйти
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void SignMembers(List<int> members)
        {
            using (NpgsqlConnection npgsql = new NpgsqlConnection(path))
            {
                string commandSql = $"select id_member from members where id_family={Convert.ToInt32(id_family)}";
                NpgsqlCommand command = new NpgsqlCommand(commandSql, npgsql);
                npgsql.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        members.Add(Convert.ToInt32(reader.GetValue(0).ToString()));

                    }
                }
                npgsql.Close();
            }
        }
    }

}
