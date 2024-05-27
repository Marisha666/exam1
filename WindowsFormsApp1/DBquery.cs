﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //Подключение БД для авторизации
    public class DBquery
    {
        public string StringCon()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\KOLЬKA12\Desktop\вещи Маши\Новая папка\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf"";Integrated Security=True";
        }
        public SqlDataAdapter queryExecute(string query)
        {
            try
            {
            SqlConnection myCon = new SqlConnection(StringCon());
            myCon.Open();

            SqlDataAdapter SDA = new SqlDataAdapter(query, myCon);

            SDA.SelectCommand.ExecuteNonQuery();
            MessageBox.Show("Действие успешно выполнено!", "Успех");
            return SDA;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при выполнении запроса.", "Ошибка");
                return null;
            }
        }
        public DataTable queryReturnData(string query, DataGridView grid)
        {
            try
            {
            SqlConnection myCon = new SqlConnection(StringCon());
            myCon.Open();

            SqlDataAdapter SDA = new SqlDataAdapter(query, myCon);
            SDA.SelectCommand.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SDA.Fill(dt);

            grid.DataSource = dt;
            MessageBox.Show("Действие успешно выполнено!", "Успех");
            return dt;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при выполнении запроса.", "Ошибка");
                return null;
            }
        }
    }
}