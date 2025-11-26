using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace accendente
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string confirm = textBox3.Text.Trim();

            if (login == "" || password == "" || confirm == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string checkSql = "SELECT COUNT(*) FROM [Users] WHERE [Login] = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("?", login);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Такой логин уже существует!");
                            return;
                        }
                    }

                    string insertSql = "INSERT INTO [Users] ([Login], [Password], [Role]) VALUES (?, ?, ?)";
                    using (OleDbCommand insertCmd = new OleDbCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("?", login);
                        insertCmd.Parameters.AddWithValue("?", password);
                        insertCmd.Parameters.AddWithValue("?", 1);
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Регистрация успешна!");
                    Login frml = new Login();
                    frml.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login frml = new Login();
            frml.Show();
            this.Hide();
        }
    }
}