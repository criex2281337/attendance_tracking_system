using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace accendente
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";
        private string currentTable = "";

        public Form1()
        {
            InitializeComponent();
            LoadTableNames();
        }

        private void LoadTableNames()
        {
            comboBox1.Items.AddRange(new string[] {
                "Сотрудники",
                "Должности",
                "Отделы",
                "Посещаемость"
            });

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void LoadTableData(string tableName)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT * FROM [{tableName}]";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        currentTable = tableName;
                        label2.Text = $"Таблица: {tableName} ({dt.Rows.Count} записей)";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                MessageBox.Show("Выберите таблицу");
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{currentTable}]", conn);
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

                    adapter.Update(dt);
                    MessageBox.Show("Данные сохранены");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                MessageBox.Show("Выберите таблицу");
                return;
            }
            LoadTableData(currentTable);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedTable = comboBox1.SelectedItem.ToString();
                LoadTableData(selectedTable);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                MessageBox.Show("Выберите таблицу");
                return;
            }

            switch (currentTable)
            {
                case "Сотрудники":
                    AddSotrud formSotrud = new AddSotrud();
                    if (formSotrud.ShowDialog() == DialogResult.OK)
                    {
                        LoadTableData(currentTable);
                    }
                    break;
                case "Должности":
                    AddDoljnost formDoljnost = new AddDoljnost();
                    if (formDoljnost.ShowDialog() == DialogResult.OK)
                    {
                        LoadTableData(currentTable);
                    }
                    break;
                case "Отделы":
                    AddOtdel formOtdel = new AddOtdel();
                    if (formOtdel.ShowDialog() == DialogResult.OK)
                    {
                        LoadTableData(currentTable);
                    }
                    break;
                case "Посещаемость":
                    AddPos formPos = new AddPos();
                    if (formPos.ShowDialog() == DialogResult.OK)
                    {
                        LoadTableData(currentTable);
                    }
                    break;
            }
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}