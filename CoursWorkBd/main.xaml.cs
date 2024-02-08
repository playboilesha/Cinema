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

namespace CoursWorkBd
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Window
    {
      
        
        InfiClass info = new InfiClass();
        public main()
        {
            InitializeComponent();
          Login.Content = Login1.login;
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
           

        }
        private void Seanse_Click(object sender, RoutedEventArgs e)
        {
           var sea = new Seanse();
            
            this.Close();
            sea.Show();
        }
        private void Genre_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = new Genre();
            this.Close();
            genre.Show();
        }
        private void Director_Click(object sender, RoutedEventArgs e)
        {
           Director dir = new Director();
            this.Close();
            dir.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
