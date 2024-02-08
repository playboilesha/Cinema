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
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using System.Windows.Navigation;
using System.Data.OracleClient;


namespace CoursWorkBd
{
    /// <summary>
    /// Логика взаимодействия для Seanse.xaml
    /// </summary>
    public partial class Seanse : Window
    {
        InfiClass info = new InfiClass();
        public Seanse()
        {
            InitializeComponent();
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            main amain = new main();
            this.Close();
            amain.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Films films = new Films();
            this.Close();
            films.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Login.Text = Login1.login;
            
            try
            {
                OracleConnection conn = new OracleConnection(info.connect);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = info.ProcedureInfoSession;
                OracleParameter oraP = new OracleParameter();
                oraP.ParameterName = info.ProcedureInfoSessionParam1;
                oraP.OracleType = OracleType.Cursor;
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(info.ProcedureInfoSessionParam2, OracleType.VarChar).Value = "not started";
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.listViewSeance.Items.Add(new Seance1 { session_id = reader.GetValue(0).ToString(), cinema_name = reader.GetValue(1).ToString(), hall_id = reader.GetValue(2).ToString(), film_name = reader.GetValue(3).ToString(), session_start = reader.GetValue(4).ToString(), session_time = reader.GetValue(5).ToString(), session_price = reader.GetValue(6).ToString(), session_status = reader.GetValue(7).ToString() });
                }
                reader.Close();
                conn.Close();
            }
            catch (OracleException ex)
            {
                Login.Text = ex.ToString();
            }

        }
        private void Seanse_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = new Genre();
            this.Close();
            genre.Show();
        }
        private void Director_Click(object sender, RoutedEventArgs e)
        {
            Director director = new Director();
            this.Close();
            director.Show();
        }
       
            private void Bokking_Click(object sender, RoutedEventArgs e)
        {
          
            int index = listViewSeance.SelectedIndex;
            //ListViewItem listViewItem = new ListViewItem();
            //string index = listviewUsers.ItemsSource.ToString();
            ListViewItem listViewItem = new ListViewItem();
            //listviewUsers.SelectedItems[index] = listViewItem.IsSelected;



            //получаем элемент
            //Удаляем элемент

            //fill the text boxes

            Seanse_number seanse = new Seanse_number();

          
            seanse.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SeanseUser seanseUser = new SeanseUser();
            seanseUser.Show();
        }
    }
    internal class Seance1
    {
        public string session_id { get; set; }
        public string cinema_name { get; set; }
        public string hall_id { get; set; }
        public string film_name { get; set; }
        public string session_start { get; set; }
        public string session_time { get; set; }
        public string session_price { get; set; }
        public string session_status { get; set; }

    }
}
