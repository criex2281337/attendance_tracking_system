using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace accendente
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";

        public Form1()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID_Сотрудника, Фамилия, Имя, Отчество FROM Сотрудники";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        lblInfo.Text = $"Таблица: Сотрудники ({dt.Rows.Count} чел.)";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}");
            }
        }

        private void LoadAttendance()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Посещаемость";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        lblInfo.Text = $"Таблица: Посещаемость ({dt.Rows.Count} записей)";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки посещаемости: {ex.Message}");
            }
        }

        private void Calculate()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string query1 = "SELECT COUNT(*) FROM Сотрудники";
                    string query2 = "SELECT COUNT(*) FROM Посещаемость";

                    using (OleDbCommand cmd1 = new OleDbCommand(query1, conn))
                    using (OleDbCommand cmd2 = new OleDbCommand(query2, conn))
                    {
                        int count1 = Convert.ToInt32(cmd1.ExecuteScalar());
                        int count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                        int total = count1 + count2;

                        MessageBox.Show(
                            $"Статистика базы данных:\n\n" +
                            $"Сотрудников: {count1}\n" +
                            $"Записей посещаемости: {count2}\n" +
                            $"Всего записей: {total}",
                            "Статистика"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вычисления: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}