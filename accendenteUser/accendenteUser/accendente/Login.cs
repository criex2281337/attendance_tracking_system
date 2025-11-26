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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bd.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Login=? AND Password=?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", login);
                        cmd.Parameters.AddWithValue("?", password);
                        OleDbDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            MessageBox.Show("Добро пожаловать, " + login + "!");

                            Form1 mainForm = new Form1();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль!");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration frmr = new Registration();
            frmr.Show();
            this.Hide();
        }   
    }
}