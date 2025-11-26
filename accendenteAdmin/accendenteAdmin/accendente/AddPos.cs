using System;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace accendente
{
    public partial class AddPos : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";

        public AddPos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Посещаемость (ID_Сотрудника, Дата, Время, Тип_события) VALUES (?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("?", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("?", TimeSpan.Parse(maskedTextBox1.Text));
                        cmd.Parameters.AddWithValue("?", comboBox1.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Запись добавлена");
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

        private void AddPos_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "Приход", "Уход" });
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }
    }
}