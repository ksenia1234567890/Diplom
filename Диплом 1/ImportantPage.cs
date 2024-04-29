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
        public string mode = "";
        public string path = "Host=localhost;Username=postgres;Password=cxNTVJas;Database=Families";
        
        public ImportantPage(string mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        // При загрузке формы загружается сумма бюджета
        private void ImportantPage_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Расходы")
            {
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
            }
        }
    }
}
