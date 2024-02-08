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
    /// Логика взаимодействия для Films.xaml
    /// </summary>
    /// 
    public partial class Films : Window
    {
        InfiClass info = new InfiClass();
     
       
        public Films()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login.Text = Login1.login;
            try
            {   
                OracleConnection conn = new OracleConnection(info.connect);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = info.ProcedureInfoFilms;
                OracleParameter oraP = new OracleParameter();
                oraP.ParameterName = info.ProcedureInfoFilmsParam1;
                oraP.OracleType = OracleType.Cursor;
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   this.listviewFilms.Items.Add(new Film { Name = reader.GetValue(0).ToString(), Director = reader.GetValue(1).ToString(), Genre = reader.GetValue(2).ToString(), Duration = reader.GetValue(3).ToString(), Year = reader.GetValue(4).ToString(), Opis = reader.GetValue(5).ToString() });  
                }
                reader.Close();
                conn.Close();    
            }
            catch (OracleException ex)
            {
            }  
        }
        private void Session_Click(object sender, RoutedEventArgs e)
        {
            Seanse seanse = new Seanse();
            this.Close();
            seanse.Show();
        }
        private void Director_Click(object sender, RoutedEventArgs e)
        {
            Director director = new Director();
            this.Close();
            director.Show();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main amain = new main();
            this.Close();
            amain.Show();
        }
 
        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = new Genre();
            genre.Show();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }


    internal class Film
    {
        public string Name { get; set; }
        public string Director { get; set; }

        public string Genre { get; set; }
        public string Duration { get; set; }
        public string Year { get; set; }
        public string Opis { get; set; }
    }
}
