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
    /// Логика взаимодействия для SeanseUser.xaml
    /// </summary>
    public partial class SeanseUser : Window
    {
        public SeanseUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login.Text = Login1.login;
            InfiClass info = new InfiClass();

            try
            {
                OracleConnection conn = new OracleConnection(info.connect);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = info.ProcedureCheckBookingUser;
                OracleParameter oraP = new OracleParameter();
                oraP.ParameterName = info.ProcedureCheckBookingUserParem2;
                oraP.OracleType = OracleType.Cursor;
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(info.ProcedureCheckBookingUserParem1, OracleType.VarChar).Value = (string)Login.Text;
                cmd.Parameters.Add(oraP);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.listViewSeance.Items.Add(new Check_Booking_User { session_id = reader.GetValue(0).ToString(), film_name = reader.GetValue(1).ToString(), session_start = reader.GetValue(2).ToString(), session_price = reader.GetValue(3).ToString(), place_number = reader.GetValue(4).ToString() });
                }
                reader.Close();
                conn.Close();
            }
            catch (OracleException ex)
            {
                Login.Text = ex.ToString();
            }

        }
        internal class Check_Booking_User
        {
            public string session_id { get; set; }
            public string film_name { get; set; }
            public string session_start { get; set; }
            public string session_price { get; set; }
            public string place_number { get; set; }
        }
    }
}
