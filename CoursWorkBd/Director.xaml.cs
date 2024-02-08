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
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Window
    {
        InfiClass info = new InfiClass();
        public Director()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Films films = new Films();
            this.Close();
            films.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Login.Text = Login1.login;

        }
        private void Seanse_Click(object sender, RoutedEventArgs e)
        {
            Seanse seanse = new Seanse();
            this.Close();
            seanse.Show();

        }
        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = new Genre();
            this.Close();
            genre.Show();
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            main amain = new main();
            this.Close();
            amain.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (Search.Text.Length > 0)
                {
                    l0.Content = "";
                   
                    OracleConnection conn = new OracleConnection(info.connect);
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = info.ProcedureInfoDirector;
                    OracleParameter oraP = new OracleParameter();
                    oraP.ParameterName = info.ProcedureInfoDirectorParam2;
                    oraP.OracleType = OracleType.Cursor;
                    oraP.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(info.ProcedureInfoDirectorParam1, OracleType.VarChar).Value = (string)Search.Text;
                    cmd.Parameters.Add(oraP);
                    cmd.Parameters.Add(info.ProcedureInfoDirectorParam3, OracleType.VarChar, 150);
                    cmd.Parameters[info.ProcedureInfoDirectorParam3].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters[info.ProcedureInfoDirectorParam3].Value.ToString() == "no search")
                    {

                        l0.Content = "no search";

                    }
                    else
                    {
                        l0.Content = cmd.Parameters[info.ProcedureInfoDirectorParam3].Value.ToString();
                        l1.Visibility = Visibility.Visible;
                        l2.Visibility = Visibility.Visible;
                        l3.Visibility = Visibility.Visible;
                        OracleDataReader reader = cmd.ExecuteReader();
                       
                            while (reader.Read())
                            {
                                Film_Name.Text = reader.GetValue(1).ToString();
                                Director_Name.Text = reader.GetValue(0).ToString();
                                Opis.Text = reader.GetValue(2).ToString();

                            }
                            reader.Close();
                            conn.Close();
                        
                    }
                }
                else
                {
                    l0.Content = "write!";
                }
            }
            catch (OracleException ex)
            {
                l1.Visibility = Visibility.Hidden;
                l2.Visibility = Visibility.Hidden;
                l3.Visibility = Visibility.Hidden;
                l0.Content = "no search";
                Film_Name.Text = "";
                Director_Name.Text = "";
                Opis.Text = "";
            }

        }


    }
}
