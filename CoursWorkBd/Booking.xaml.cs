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
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking : Window
    {
        public Booking()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            InfiClass info = new InfiClass();
            using (OracleConnection objConn = new OracleConnection(info.connect))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand(info.ProcedureSearchPlace, objConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(info.ProcedureSearchPlaceParam1, OracleType.Number).Value = Convert.ToInt32(Number_Session.Content);
                    OracleParameter oraP = new OracleParameter();
                    oraP.ParameterName = info.ProcedureSearchPlaceParam2;
                    oraP.OracleType = OracleType.Cursor;
                    oraP.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(oraP);
                    cmd.Parameters.Add(info.ProcedureSearchPlaceParam3, OracleType.Number, 150);
                    cmd.Parameters[info.ProcedureSearchPlaceParam3].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(info.ProcedureSearchPlaceParam4, OracleType.VarChar, 150);
                    cmd.Parameters[info.ProcedureSearchPlaceParam4].Direction = System.Data.ParameterDirection.Output;
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    Num_place.Content = "Введите номер места которое хотите забронировать 0-" + cmd.Parameters[info.ProcedureSearchPlaceParam3].Value.ToString();
                    Film_name.Content = cmd.Parameters[info.ProcedureSearchPlaceParam4].Value.ToString();
                    OracleDataReader reader = cmd.ExecuteReader();
                    string place = "";
                    
                    while (reader.Read())
                    {
                        place += reader.GetValue(0).ToString() + ", ";
                      
                       
                    }

                  
                    Place_zan.Text = place;
                    reader.Close();
                  
                  
                      
                 




                    
                }
                catch (Exception ex)
                {
                    Message.Text = ex.ToString();
                   
                    objConn.Close();
                }
                objConn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
            int s;
            if (Number.Text.Length == 0 || int.TryParse(Number.Text, out s) == false)
            {
                Message.Text = "incorrect value";
            }
            else
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand(info.ProcedureBokkingUser, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureBokkingUserParam1, OracleType.VarChar).Value = Login1.login;
                        cmd.Parameters.Add(info.ProcedureBokkingUserParam2, OracleType.Number).Value = Convert.ToInt32(Number_Session.Content);
                        cmd.Parameters.Add(info.ProcedureBokkingUserParam3, OracleType.Number).Value = Convert.ToInt32(Number.Text);
                        cmd.Parameters.Add(info.ProcedureBokkingUserParam4, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureBokkingUserParam4].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message.Text = cmd.Parameters[info.ProcedureBokkingUserParam4].Value.ToString();
















                    }
                    catch (Exception ex)
                    {
                        Message.Text = ex.ToString();

                        objConn.Close();
                    }
                    objConn.Close();
                }
            }

        }
    }
}
