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
    /// Логика взаимодействия для DirectorRoot.xaml
    /// </summary>
    public partial class DirectorRoot : Window
    {
        public DirectorRoot()
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
            if (film_name.Text.Length > 0 && derector.Text.Length > 0 && opis_film.Text.Length > 0)
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand(info.ProcedureInsertDirector, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureInsertDirectorParam1, OracleType.VarChar).Value = derector.Text;
                        cmd.Parameters.Add(info.ProcedureInsertDirectorParam2, OracleType.VarChar).Value = film_name.Text;
                        cmd.Parameters.Add(info.ProcedureInsertDirectorParam3, OracleType.VarChar).Value = opis_film.Text;
                        cmd.Parameters.Add(info.ProcedureInsertDirectorParam4, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureInsertDirectorParam4].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message.Text = cmd.Parameters[info.ProcedureInsertDirectorParam4].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        objConn.Close();
                        Message.Text = "Press data";
                    }
                    objConn.Close();
                }

            }
            else
            {
                Message.Text = "press data";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
            if (director_name1.Text.Length > 0)
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand(info.ProcedureDeleteDirector, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureDeleteDirectorParam1, OracleType.VarChar).Value = director_name1.Text;
                        cmd.Parameters.Add(info.ProcedureDeleteDirectorParam2, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureDeleteDirectorParam2].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message1.Text = cmd.Parameters[info.ProcedureDeleteDirectorParam2].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        objConn.Close();
                        Message1.Text = ex.ToString();
                    }
                    objConn.Close();
                }

            }
            else
            {
                Message1.Text = "press data";
            }
        }
    }
}
