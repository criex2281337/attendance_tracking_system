using System;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace accendente
{
    public partial class AddSotrud : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";

        public AddSotrud()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Сотрудники (Фамилия, Имя, Отчество, ID_Должности, ID_Отдела) VALUES (?, ?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", textBox1.Text);
                        cmd.Parameters.AddWithValue("?", textBox2.Text);
                        cmd.Parameters.AddWithValue("?", textBox3.Text);
                        cmd.Parameters.AddWithValue("?", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("?", numericUpDown2.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Сотрудник добавлен");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}