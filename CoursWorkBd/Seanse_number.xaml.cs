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
    /// Логика взаимодействия для Seanse_number.xaml
    /// </summary>
    public partial class Seanse_number : Window
    {
        public Seanse_number()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            InfiClass info = new InfiClass();
            using (OracleConnection objConn = new OracleConnection(info.connect))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand(info.ProcedureCheck_session, objConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(info.ProcedureCheck_sessionParam1, OracleType.VarChar).Value = Number.Text;
                    cmd.Parameters.Add(info.ProcedureCheck_sessionParam2, OracleType.VarChar, 150);
                    cmd.Parameters[info.ProcedureCheck_sessionParam2].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(info.ProcedureCheck_sessionParam3, OracleType.VarChar, 150);
                    cmd.Parameters[info.ProcedureCheck_sessionParam3].Direction = System.Data.ParameterDirection.Output;
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    Message.Text = cmd.Parameters[info.ProcedureCheck_sessionParam2].Value.ToString();
                    bool status = Convert.ToBoolean(cmd.Parameters[info.ProcedureCheck_sessionParam3].Value.ToString());
                    if (status)
                    {
                        objConn.Close();
                        booking.Number_Session.Content = Number.Text;
                        this.Close();
                        booking.Show();
                      

                        
                       
                    }
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
