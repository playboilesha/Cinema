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
    /// Логика взаимодействия для Genre.xaml
    /// </summary>
    public partial class Genre : Window
    {
        public Genre()
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
            Login.Text = Login1.login;
           
        }

        private void Genre_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            main amain = new main();
            amain.Show();
            this.Close();

        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {

            int a = listviewFilms.Items.Count;
            if (listviewFilms.Items.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < a; i++)
                {

                    listviewFilms.Items.RemoveAt(0);

                }
            }

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




            int a = listviewFilms.Items.Count;
            if (listviewFilms.Items.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < a; i++)
                {

                    listviewFilms.Items.RemoveAt(0);

                }
            }

            InfiClass info = new InfiClass();
            MainWindow mainWindow = new MainWindow();
           
            try
            {
                var item = (ComboBoxItem)comboBox1.SelectedValue;
                OracleConnection conn = new OracleConnection(info.connect);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = info.ProcedureInfoGenre;
                OracleParameter oraP = new OracleParameter();
                oraP.ParameterName = info.ProcedureInfoGenreParam2;
                oraP.OracleType = OracleType.Cursor;
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(info.ProcedureInfoGenreParam1, OracleType.VarChar).Value = (string)item.Content;
                cmd.Parameters.Add(oraP);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.listviewFilms.Items.Add(new Film { Name = reader.GetValue(1).ToString() });
                }
                reader.Close();
                conn.Close();
            }
            catch (OracleException ex)
            {
            }


         






        }
    }
}
