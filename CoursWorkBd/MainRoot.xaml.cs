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
    /// Логика взаимодействия для MainRoot.xaml
    /// </summary>
    public partial class MainRoot : Window
    {
        public MainRoot()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilmRoot filmRoot = new FilmRoot();
            this.Close();
            filmRoot.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SeanseRoot seanseRoot = new SeanseRoot();
            this.Close();
            seanseRoot.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DirectorRoot directorRoot = new DirectorRoot();
            this.Close();
            directorRoot.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CheckUserRoot checkUserRoot = new CheckUserRoot();
            this.Close();
            checkUserRoot.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
