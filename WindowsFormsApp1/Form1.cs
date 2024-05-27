using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //Подключение базы данных
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\мш\демо\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        //Кнопка вход
        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            if (login == string.Empty || password == string.Empty)
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
                return;
            }

            if (AdminAuthorizeUser(login, password))
            {
                Form newForm2 = new Form2();
                newForm2.Show();
                this.Hide();
            }
            else if (UserAuthorizeUser(login, password))
            {
                Form newForm3 = new Form3();
                newForm3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка");
            }
        }

        //Авторизация администратора
        private bool AdminAuthorizeUser(string login, string password)
        {
            bool isAuthorized = false;

            var dbQeury = new DBquery();


            using (SqlConnection con = new SqlConnection(dbQeury.StringCon()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE login ='{login}' and passw = '{password}' and rol = 'admin'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["passw"].ToString() == password && reader["login"].ToString() == login)
                            {
                                isAuthorized = true;
                                MessageBox.Show("Вход успешно выполнен!", "Успех");
                            }
                        }
                    }
                }
            }
            return isAuthorized;
        }

        //Авторизация пользователя
        private bool UserAuthorizeUser(string login, string password)
        {
            bool isAuthorized = false;

            var dbQeury = new DBquery();


            using (SqlConnection con = new SqlConnection(dbQeury.StringCon()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE login ='{login}' and passw = '{password}' and rol = 'user'", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["passw"].ToString() == password && reader["login"].ToString() == login)
                            {
                                isAuthorized = true;
                                MessageBox.Show("Вход успешно выполнен!", "Успех");
                            }
                        }
                    }
                }
            }
            return isAuthorized;
        }

        //Выход из программы
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
