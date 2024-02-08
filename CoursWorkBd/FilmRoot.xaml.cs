using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OracleClient;



namespace CoursWorkBd
{
    /// <summary>
    /// Логика взаимодействия для FilmRoot.xaml
    /// </summary>
    public partial class FilmRoot : Window
    {
        public FilmRoot()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainRoot mainRoot = new MainRoot();
            this.Close();
            mainRoot.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
            var item = (ComboBoxItem)comboBox1.SelectedValue;
            

            using (OracleConnection objConn = new OracleConnection(info.connect))
            {
                try
                {

                    OracleCommand cmd = new OracleCommand(info.ProcedureInsertFilm, objConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam1, OracleType.VarChar).Value = film_name.Text;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam2, OracleType.VarChar).Value = derector.Text;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam3, OracleType.VarChar).Value = (string)item.Content;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam4, OracleType.VarChar).Value = duration_film.Text;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam5, OracleType.VarChar).Value = year_film.Text;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam6, OracleType.VarChar).Value = opis_film.Text;
                    cmd.Parameters.Add(info.ProcedureInsertFilmParam7, OracleType.VarChar,150);
                    cmd.Parameters[info.ProcedureInsertFilmParam7].Direction = System.Data.ParameterDirection.Output;
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    Message.Text = cmd.Parameters[info.ProcedureInsertFilmParam7].Value.ToString();
                    /* objConn.Open();
                     string command = @"select user_name from users";
                     OracleCommand objCmd = new OracleCommand(command, objConn);
                     OracleDataReader reader = objCmd.ExecuteReader();
                     // читаем результат
                     while (reader.Read())
                     {
                         // элементы массива [] - это значения столбцов из запроса SELECT
                         Message.Text = reader[0].ToString();
                     }
                     reader.Close(); // закрываем reader*/
                    /*objCmd.ExecuteNonQuery();
                    Message.Text = objCmd.Parameters["message"].Value.ToString();*/
                }
                catch (Exception ex)
                {
                    objConn.Close();
                   opis_film.Text = ex.ToString();
                }
                objConn.Close();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
           


            using (OracleConnection objConn = new OracleConnection(info.connect))
            {
                try
                {

                    OracleCommand cmd = new OracleCommand(info.ProcedureDeleteFilm, objConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(info.ProcedureDeleteFilmParam1, OracleType.VarChar).Value = film_name1.Text;
                   
                    cmd.Parameters.Add(info.ProcedureDeleteFilmParam2, OracleType.VarChar, 150);
                    cmd.Parameters[info.ProcedureDeleteFilmParam2].Direction = System.Data.ParameterDirection.Output;
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    Message1.Text = cmd.Parameters[info.ProcedureDeleteFilmParam2].Value.ToString();
                    /* objConn.Open();
                     string command = @"select user_name from users";
                     OracleCommand objCmd = new OracleCommand(command, objConn);
                     OracleDataReader reader = objCmd.ExecuteReader();
                     // читаем результат
                     while (reader.Read())
                     {
                         // элементы массива [] - это значения столбцов из запроса SELECT
                         Message.Text = reader[0].ToString();
                     }
                     reader.Close(); // закрываем reader*/
                    /*objCmd.ExecuteNonQuery();
                    Message.Text = objCmd.Parameters["message"].Value.ToString();*/
                }
                catch (Exception ex)
                {
                    objConn.Close();
                    opis_film.Text = ex.ToString();
                }
                objConn.Close();
            }

        }
    }
}
