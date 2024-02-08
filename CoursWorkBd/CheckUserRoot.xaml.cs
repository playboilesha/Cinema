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
using System.Data;

namespace CoursWorkBd
{
    /// <summary>
    /// Логика взаимодействия для CheckUserRoot.xaml
    /// </summary>
    public partial class CheckUserRoot : Window
    {
        public CheckUserRoot()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox.SelectedIndex = 0;
            InfiClass info = new InfiClass();
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
             
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int a = listViewCheck_User.Items.Count;
         
           
                for (int i = 0; i < a; i++)
                {

                listViewCheck_User.Items.RemoveAt(0);

                }
                InfiClass info = new InfiClass();
            try
            {
                OracleConnection conn = new OracleConnection(info.connect);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = info.ProcedureInfoCheckUser;
                OracleParameter oraP = new OracleParameter();
                oraP.ParameterName = info.ProcedureInfoCheckUserParam2;
                oraP.OracleType = OracleType.Cursor;
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(info.ProcedureInfoCheckUserParam1, OracleType.VarChar).Value = Number.Text;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(info.ProcedureInfoCheckUserParam3, OracleType.VarChar, 150);
                cmd.Parameters[info.ProcedureInfoCheckUserParam3].Direction = System.Data.ParameterDirection.Output;
              
               
                OracleDataReader reader = cmd.ExecuteReader();
                Message.Text = cmd.Parameters[info.ProcedureInfoCheckUserParam3].Value.ToString();
                if (cmd.Parameters[info.ProcedureInfoCheckUserParam3].Value.ToString() == "no search session")
                {
                    Message.Text = "no search session";
                }
                else {
                    while (reader.Read())
                    {
                        this.listViewCheck_User.Items.Add(new check_user { check_user_id = reader.GetValue(0).ToString(), user_name = reader.GetValue(1).ToString(), session_id = reader.GetValue(2).ToString(), num_place = reader.GetValue(3).ToString(), status = reader.GetValue(4).ToString() });
                    }
                   
                }
                reader.Close();
                conn.Close();

            }
            catch (OracleException ex)
            {
                Message.Text = "no search session";
            }

        }
        internal class check_user
        {
            public string check_user_id { get; set; }
            public string user_name { get; set; }
            public string session_id { get; set; }
            public string num_place { get; set; }
            public string status { get; set; }

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            InfiClass info = new InfiClass();

            try
            {

                var item = (ComboBoxItem)ComboBox.SelectedValue;
                if (item.Content.ToString().Length == 0)
                {
                    Message.Text = "Enter status";
                }
                else
                {
                    var name = (listViewCheck_User.SelectedItem as check_user).check_user_id;
                    using (OracleConnection objConn = new OracleConnection(info.connect))
                    {

                        OracleCommand cmd = new OracleCommand(info.ProcedureUpdateCheckUser, objConn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(info.ProcedureUpdateCheckUserParem1, OracleType.Number).Value = Convert.ToInt32(name);
                        cmd.Parameters.Add(info.ProcedureUpdateCheckUserParem2, OracleType.VarChar).Value = (string)item.Content;
                        cmd.Parameters.Add(info.ProcedureUpdateCheckUserParem3, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureUpdateCheckUserParem3].Direction = System.Data.ParameterDirection.Output;
                        objConn.Open();
                        cmd.ExecuteNonQuery();
                        Message.Text = cmd.Parameters[info.ProcedureUpdateCheckUserParem3].Value.ToString();
                        objConn.Close();
                    }
                    int a = listViewCheck_User.Items.Count;


                    for (int i = 0; i < a; i++)
                    {

                        listViewCheck_User.Items.RemoveAt(0);

                    }
                   
                    try
                    {
                        OracleConnection conn = new OracleConnection(info.connect);
                        conn.Open();
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = info.ProcedureInfoCheckUser;
                        OracleParameter oraP = new OracleParameter();
                        oraP.ParameterName = info.ProcedureInfoCheckUserParam2;
                        oraP.OracleType = OracleType.Cursor;
                        oraP.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(info.ProcedureInfoCheckUserParam1, OracleType.VarChar).Value = Number.Text;
                        cmd.Parameters.Add(oraP);
                        cmd.Parameters.Add(info.ProcedureInfoCheckUserParam3, OracleType.VarChar, 150);
                        cmd.Parameters[info.ProcedureInfoCheckUserParam3].Direction = System.Data.ParameterDirection.Output;


                        OracleDataReader reader = cmd.ExecuteReader();
                    cmd.Parameters[info.ProcedureInfoCheckUserParam3].Value.ToString();
                        if (cmd.Parameters[info.ProcedureInfoCheckUserParam3].Value.ToString() == "no search session")
                        {
                            Message.Text = "no search session";
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                this.listViewCheck_User.Items.Add(new check_user { check_user_id = reader.GetValue(0).ToString(), user_name = reader.GetValue(1).ToString(), session_id = reader.GetValue(2).ToString(), num_place = reader.GetValue(3).ToString(), status = reader.GetValue(4).ToString() });
                            }

                        }
                        reader.Close();
                        conn.Close();

                    }
                    catch (OracleException ex)
                    {
                        Message.Text = "no search session";
                    }

                }
            }
            catch (OracleException ex)
            {
                Message.Text = ex.ToString();
            }
           
            }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainRoot mainRoot = new MainRoot();
            mainRoot.Show();
            this.Close();
        }
    }
    }

