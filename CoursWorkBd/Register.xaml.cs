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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        MainWindow mainWindow = new MainWindow();
        InfiClass info = new InfiClass();
        public Register()
        {
            InitializeComponent();
        }
        private void Regisr_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length == 0 && Password1.Password.Length == 0 && Password2.Password.Length == 0)
            {
                Message.Text = "enter data";
            }
            else
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {

                        OracleCommand cmd = new OracleCommand(info.ProcedureRegisterUser, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureRegisterUserParam1, OracleType.VarChar).Value = Login.Text;
                        cmd.Parameters.Add(info.ProcedureRegisterUserParam2, OracleType.VarChar).Value = Password1.Password;
                        cmd.Parameters.Add(info.ProcedureRegisterUserParam3, OracleType.VarChar).Value = Password2.Password;
                        cmd.Parameters.Add(info.ProcedureRegisterUserParam4, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureRegisterUserParam4].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message.Text = cmd.Parameters["message"].Value.ToString();
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
                        Login.Text = ex.ToString();
                    }
                    objConn.Close();
                }
            }
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }
    }
}
