using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder comBui;

        //Подключение базы данных
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\KOLЬKA12\Desktop\вещи Маши\Новая папка\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf"";Integrated Security=True";
        string sql = "SELECT * FROM Users";

        public Form2()
        {
            InitializeComponent();

            //Добавление столбцов в таблицу, отключение автодобавления строк
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                //Делаем недоступным столбцы для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;

                //названия столбцов
                dataGridView1.Columns["Id"].HeaderText = "Код";
                dataGridView1.Columns["login"].HeaderText = "Логин";
                dataGridView1.Columns["passw"].HeaderText = "Пароль";
                dataGridView1.Columns["rol"].HeaderText = "Роль";
            }
        }

        //Добавление строки
        private void button2_Click(object sender, EventArgs e)
        {
            //Добавление новой строки в DataTable
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }
        
        //Удаляние выделенной строки из dataGridView1
        private void button3_Click(object sender, EventArgs e)
        {           
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        //Сохранение изменений
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                comBui = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("Procedure1", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 50, "login"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@passw", SqlDbType.NVarChar, 50, "passw"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@rol", SqlDbType.NVarChar, 50, "rol"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        //Выход на экран авторизации
        private void button1_Click(object sender, EventArgs e)
        {
            Form newForm1 = new Form1();
            newForm1.Show();
            this.Close();
        }

        //Поиск по таблице
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Id LIKE '%{0}%'", textBox1.Text);
            }
        }

        //Создание отчета
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                streamWriter.WriteLine("Код документа: " + textBox2.Text + " Название документа: " + textBox3.Text
                    + " Номер документа: " + textBox4.Text + " Дата документа: " + textBox5.Text);
                streamWriter.Close();
            }
        }

        /*
         * СОЗДАНИЕ БД
           CREATE TABLE [dbo].[Tab1] (
               [Id] INT IDENTITY (1, 1) NOT NULL,
               [Name1]    NVARCHAR (50) NULL,
               [Name2]    NVARCHAR (50) NOT NULL,
               PRIMARY KEY CLUSTERED ([Id] ASC)
           );

           СОЗДАНИЕ ПРОЦЕДУРЫ
           CREATE PROCEDURE [dbo].[Procedure1]
	           @Name1 NVarChar(50),
	           @Name2 NVarChar(50),
	           @Date date,
	           @Id int out
           AS
	           INSERT INTO TabS1(Name1, Name2, Date)
	           VALUES (@Name1, @Name2, @Date)
               SET @Id=SCOPE_IDENTITY()


        //Заполнение полей значениями из выбранной строки в таблице для отчета
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox8.Text = row.Cells["Id"].Value.ToString();
                textBox1.Text = row.Cells["name1"].Value.ToString();
                textBox2.Text = row.Cells["nom"].Value.ToString();
                textBox3.Text = row.Cells["date1"].Value.ToString();
                textBox4.Text = row.Cells["xar"].Value.ToString();
                textBox5.Text = row.Cells["reg"].Value.ToString();
                textBox6.Text = row.Cells["sve"].Value.ToString();
            }
        }

        //Отчистка всех полей
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox8.Text = " ";
        }
        */
    }
}
