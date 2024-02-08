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
    /// Логика взаимодействия для SeanseRoot.xaml
    /// </summary>
    public partial class SeanseRoot : Window
    {
        public SeanseRoot()
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
            try
            {

                InfiClass info = new InfiClass();
                var hall_id1 = (ComboBoxItem)hall_id.SelectedValue;
                var session_status1 = (ComboBoxItem)session_status.SelectedValue;
                if (hall_id1.Content.ToString().Length > 0 && film_name.Text.Length > 0 && start.Text.Length > 0 && session_price.Text.Length > 0 && session_status1.Content.ToString().Length > 0)
                {

                    using (OracleConnection objConn = new OracleConnection(info.connect))
                    {

                        OracleCommand cmd = new OracleCommand(info.ProcedureInsertSessions, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam1, OracleType.VarChar).Value = (string)hall_id1.Content;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam2, OracleType.VarChar).Value = film_name.Text;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam3, OracleType.VarChar).Value = start.Text;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam4, OracleType.VarChar).Value = session_price.Text;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam5, OracleType.VarChar).Value = (string)session_status1.Content;
                        cmd.Parameters.Add(info.ProcedureInsertSessionParam6, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureInsertSessionParam6].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message.Text = cmd.Parameters[info.ProcedureInsertSessionParam6].Value.ToString();



                        objConn.Close();
                    }
                }
                else
                {
                    Message.Text = "Press data";
                }
            }
            catch (Exception ex)
            {

                Message.Text = "Press data";
            }

        }

        private void session_start_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = session_start.SelectedDate;

           start.Text = selectedDate.Value.Date.ToShortDateString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();


            if (session_id.Text.Length > 0)
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {

                        OracleCommand cmd = new OracleCommand(info.ProcedureDeleteSessions, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureDeleteSessionsParam1, OracleType.VarChar).Value = session_id.Text;

                        cmd.Parameters.Add(info.ProcedureDeleteSessionsParam2, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureDeleteSessionsParam2].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message1.Text = cmd.Parameters[info.ProcedureDeleteSessionsParam2].Value.ToString();
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
            var session_status2 = (ComboBoxItem)session_status1.SelectedValue;

            if (session_id1.Text.Length > 0)
            {
                using (OracleConnection objConn = new OracleConnection(info.connect))
                {
                    try
                    {

                        OracleCommand cmd = new OracleCommand(info.ProcedureUpdateSessions, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureUpdateSessionsParam1, OracleType.Number).Value =Convert.ToInt32(session_id1.Text);
                        cmd.Parameters.Add(info.ProcedureUpdateSessionsParam2, OracleType.VarChar).Value = (string)session_status2.Content;

                        cmd.Parameters.Add(info.ProcedureUpdateSessionsParam3, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureUpdateSessionsParam3].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message2.Text = cmd.Parameters[info.ProcedureUpdateSessionsParam3].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        objConn.Close();
                        Message2.Text = ex.ToString();
                    }
                    objConn.Close();
                }
            }
            else
            {
                Message2.Text = "press data";
            }

        }
    }
}
