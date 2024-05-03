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
        Label priceE = new Label();
        TextBox priceG = new TextBox();



        public ImportantPage(string id_member, string mode)
        {
            InitializeComponent();
            this.id_member = id_member;
            this.mode = mode;
        }

        // При загрузке формы загружается сумма бюджета
        private void ImportantPage_Load(object sender, EventArgs e)
        {
            // Подсчёт доходов

            int income = 0;
            NpgsqlConnection connect = new NpgsqlConnection(path);
            string query = "select count(id_income) from income";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            int countIncome = (Int32)cmd.ExecuteScalar();
            connect.Close();

            for(int i= 0; i<=countIncome;i++)
            {
                NpgsqlConnection contact = new NpgsqlConnection(path);
                string sql = $"select price frome income where id_income={i}";
                NpgsqlCommand command = new NpgsqlCommand(sql, contact);
                contact.Open();
                income += (Int32)command.ExecuteScalar();
                contact.Close();
            }

            // Подсчёт расходов

            int expenses = 0;
            NpgsqlConnection connect2 = new NpgsqlConnection(path);
            string query2 = "select count(id_expenses) from expenses";
            NpgsqlCommand cmd2 = new NpgsqlCommand(query2, connect2);
            connect2.Open();
            int countExpenses = (Int32)cmd2.ExecuteScalar();
            connect2.Close();

            for (int i = 0; i <= countExpenses; i++)
            {
                NpgsqlConnection contact = new NpgsqlConnection(path);
                string sql = $"select price frome expenses where id_income={i}";
                NpgsqlCommand command = new NpgsqlCommand(sql, contact);
                contact.Open();
                expenses += (Int32)command.ExecuteScalar();
                contact.Close();
            }

            // Итоговая сумма

            int price = income - expenses;
            label4.Text = price.ToString();

            button5.Hide();
            button6.Hide();
            button7.Hide();
            comboBox3.Hide();
            button4.Hide();
        }

        // Нажатие на кнопку Отобразить

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Расходы")
            {
                comboBox3.Show();
                button4.Show();
                NpgsqlConnection connect = new NpgsqlConnection(path);
                string query = "select * from expenses";
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

                label5.Hide();

                   
                    descriptionA.Parent = this;
                    descriptionA.Location = new Point(686, 95);
                    descriptionA.Text = "Описание";
                    
                    descriptionB.Parent = this;
                    descriptionB.Location = new Point(689, 133);
                    descriptionB.Size = new Size(241, 20);

                category.Parent = this;
                    category.Location = new Point(686, 173);
                    category.Text = "Категория";
                    
                    categoryB.Parent = this;
                    categoryB.Location = new Point(689, 211);
                    categoryB.Size = new Size(241, 20);

                    
                    type.Parent = this;
                    type.Location = new Point(686, 261);
                    type.Text = "Тип";
                    
                    typeB.Parent = this;
                    typeB.Location = new Point(689, 299);
                    typeB.Size = new Size(241, 20);

                    
                price.Parent = this;
                    price.Location = new Point(686, 336);
                    price.Text = "Цена";
                   
                    priceB.Parent = this;
                    priceB.Location = new Point(689, 374);
                    priceB.Size = new Size(241, 20);

                    button5.Show();
                    button5.Location = new Point(685, 415);
                    button6.Show();
                    button6.Location = new Point(782, 415);
                    button7.Show();
                    button7.Location = new Point(879, 415);
                
            }
            if (comboBox1.Text == "Доходы")
            {
                NpgsqlConnection connect = new NpgsqlConnection(path);
                string query = "select * from income";
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connect);
                DataSet ds = new DataSet();
                connect.Open();
                dataAdapter.Fill(ds, "income");
                dataGridView1.DataSource = ds.Tables["income"];
                connect.Close();
                dataGridView1.Columns[0].HeaderText = "Идентификатор";
                dataGridView1.Columns[1].HeaderText = "Код члена семьи";
                dataGridView1.Columns[2].HeaderText = "Тип";
                dataGridView1.Columns[3].HeaderText = "Cумма";
                dataGridView1.Columns[4].HeaderText = "Дата";

                if(mode == "Администратор")
                {
                    
                    typeA.Parent = this;
                    typeA.Location = new Point(686, 95);
                    typeA.Text = "Тип";
                    
                    typeC.Parent = this;
                    typeC.Location = new Point(689, 133);
                    typeC.Size = new Size(241, 20);

                   
                    priceA.Parent = this;
                    priceA.Location = new Point(686, 173);
                    priceA.Text = "Сумма";
                    
                    priceC.Parent = this;
                    priceC.Location = new Point(689, 211);
                    priceC.Size = new Size(241, 20);

                    button5.Show();
                    button5.Location = new Point(675, 251);
                    button6.Show();
                    button6.Location = new Point(772, 251);
                    button7.Show();
                    button7.Location = new Point(869, 251);
                }

            }
            if (comboBox1.Text == "Накопления")
            {
                NpgsqlConnection connect = new NpgsqlConnection(path);
                string query = "select * from savings";
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connect);
                DataSet ds = new DataSet();
                connect.Open();
                dataAdapter.Fill(ds, "savings");
                dataGridView1.DataSource = ds.Tables["savings"];
                connect.Close();
                dataGridView1.Columns[0].HeaderText = "Идентификатор";
                dataGridView1.Columns[1].HeaderText = "Код члена семьи";
                dataGridView1.Columns[2].HeaderText = "Описание";
                dataGridView1.Columns[3].HeaderText = "Cумма";

                if (mode == "Администратор")
                {
                    description.Parent = this;
                    description.Location = new Point(686, 95);
                    description.Text = "Описание";
                    
                    descriptionF.Parent = this;
                    descriptionF.Location = new Point(689, 133);
                    descriptionF.Size = new Size(241, 20);

                    
                    amount.Parent = this;
                    amount.Location = new Point(686, 173);
                    amount.Text = "Сумма";
                   
                    amountB.Parent = this;
                    amountB.Location = new Point(689, 211);
                    amountB.Size = new Size(241, 20);

                    
                    priceE.Parent = this;
                    priceE.Location = new Point(686, 261);
                    priceE.Text = "Внесение";
                   
                    priceG.Parent = this;
                    priceG.Location = new Point(689, 299);
                    priceG.Size = new Size(241, 20);

                    button5.Show();
                    button5.Location = new Point(673, 331);
                    button6.Show();
                    button6.Location = new Point(770, 331);
                    button7.Show();
                    button7.Location = new Point(867, 331);
                }
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
                    NpgsqlConnection con = new NpgsqlConnection(path);
                    string com = "select count(id_expenses) from expenses";
                    NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                    con.Open();
                    int num = (Int32)npgsql.ExecuteScalar();
                    con.Close();
                    if (num == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        NpgsqlConnection connect = new NpgsqlConnection(path);
                        string query = "select id_expenses from expenses order by id_expenses desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        count = (Int32)cmd.ExecuteScalar();
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
                    cmd2.Parameters.AddWithValue("@price", priceB.Text);
                    cmd2.Parameters.AddWithValue("@date_expenses", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();

                }
                if (comboBox1.Text == "Доходы")
                {
                    int count = 0;
                    NpgsqlConnection con = new NpgsqlConnection(path);
                    string com = "select count(id_income) from income";
                    NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                    con.Open();
                    int num = (Int32)npgsql.ExecuteScalar();
                    con.Close();
                    if (num == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        NpgsqlConnection connect = new NpgsqlConnection(path);
                        string query = "select id_income from income order by id_income desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        count = (Int32)cmd.ExecuteScalar();
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO income(id_income, id_member, type, price, date_income)" +
                        "VALUES(@id_income, @id_member, @type, @price, @date_income)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_income", count + 1);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@type", typeC.Text);
                    cmd2.Parameters.AddWithValue("@price", priceC.Text);
                    cmd2.Parameters.AddWithValue("@date_income", DateTime.Today);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }

                if (comboBox1.Text == "Накопления")
                {
                    int count = 0;
                    NpgsqlConnection con = new NpgsqlConnection(path);
                    string com = "select count(id_saving) from savings";
                    NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                    con.Open();
                    int num = (Int32)npgsql.ExecuteScalar();
                    con.Close();
                    if (num == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        NpgsqlConnection connect = new NpgsqlConnection(path);
                        string query = "select id_saving from savings order by id_saving desc limit 1";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                        connect.Open();
                        count = (Int32)cmd.ExecuteScalar();
                        connect.Close();
                    }

                    NpgsqlConnection con2 = new NpgsqlConnection(path);
                    string query2 = "INSERT INTO savings(id_saving, id_member, description, amount)" +
                        "VALUES(@id_saving, @id_member, @description, @amount)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id_saving", count + 1);
                    cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                    cmd2.Parameters.AddWithValue("@description", descriptionF.Text);
                    cmd2.Parameters.AddWithValue("@amount", amountB.Text);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
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
            if (comboBox1.Text == "Расходы")
            {
                int count = 0;
                NpgsqlConnection con = new NpgsqlConnection(path);
                string com = "select count(id_expenses) from expenses";
                NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                con.Open();
                int num = (Int32)npgsql.ExecuteScalar();
                con.Close();
                if (num == 0)
                {
                    count = 0;
                }
                else
                {
                    NpgsqlConnection connect = new NpgsqlConnection(path);
                    string query = "select id_expenses from expenses order by id_expenses desc limit 1";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    connect.Open();
                    count = (Int32)cmd.ExecuteScalar();
                    connect.Close();
                }

                NpgsqlConnection con2 = new NpgsqlConnection(path);
                string query2 = "UPDATE expenses" +
                    "SET id_member=@id_member, description=@description, category=@category, " +
                    "type=@type, price=@price, date_expenses=@date_expenses WHERE @id_expenses";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@id_expenses", count + 1);
                cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                cmd2.Parameters.AddWithValue("@description", descriptionB.Text);
                cmd2.Parameters.AddWithValue("@category", categoryB.Text);
                cmd2.Parameters.AddWithValue("@type", typeB.Text);
                cmd2.Parameters.AddWithValue("@price", priceB.Text);
                cmd2.Parameters.AddWithValue("@date_expenses", DateTime.Today);
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }

            if (comboBox1.Text == "Доходы")
            {
                int count = 0;
                NpgsqlConnection con = new NpgsqlConnection(path);
                string com = "select count(id_income) from income";
                NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                con.Open();
                int num = (Int32)npgsql.ExecuteScalar();
                con.Close();
                if (num == 0)
                {
                    count = 0;
                }
                else
                {
                    NpgsqlConnection connect = new NpgsqlConnection(path);
                    string query = "select id_income from income order by id_income desc limit 1";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    connect.Open();
                    count = (Int32)cmd.ExecuteScalar();
                    connect.Close();
                }

                NpgsqlConnection con2 = new NpgsqlConnection(path);
                string query2 = "UPDATE income SET id_member=@id_member, type=@type, price=@price, date_income=@date_income" +
                    "WHERE income = @id_income";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@id_income", count + 1);
                cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                cmd2.Parameters.AddWithValue("@type", typeC.Text);
                cmd2.Parameters.AddWithValue("@price", priceC.Text);
                cmd2.Parameters.AddWithValue("@date_income", DateTime.Today);
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }

            if (comboBox1.Text == "Накопления")
            {
                int count = 0;
                NpgsqlConnection con = new NpgsqlConnection(path);
                string com = "select count(id_saving) from savings";
                NpgsqlCommand npgsql = new NpgsqlCommand(com, con);
                con.Open();
                int num = (Int32)npgsql.ExecuteScalar();
                con.Close();
                if (num == 0)
                {
                    count = 0;
                }
                else
                {
                    NpgsqlConnection connect = new NpgsqlConnection(path);
                    string query = "select id_saving from savings order by id_saving desc limit 1";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    connect.Open();
                    count = (Int32)cmd.ExecuteScalar();
                    connect.Close();
                }

                NpgsqlConnection con2 = new NpgsqlConnection(path);
                string query2 = "UPDATE savings SET id_member=@id_member, description=@description, amount=@amount" +
                    "WHERE id_saving=@id_saving)";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@id_saving", count + 1);
                cmd2.Parameters.AddWithValue("@id_member", Convert.ToInt32(id_member));
                cmd2.Parameters.AddWithValue("@description", descriptionF.Text);
                cmd2.Parameters.AddWithValue("@amount", amountB.Text);
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
