using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Types;
using System.Data.OracleClient;

namespace CoursWorkBd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public string login; 
        
       
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            InfiClass info = new InfiClass();
            using (OracleConnection objConn = new OracleConnection(info.connect))
            {
                 try
                {
                    OracleCommand cmd = new OracleCommand(info.ProcedureLoginUsers, objConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("user_name1", OracleType.VarChar).Value = Login.Text;
                    cmd.Parameters.Add("user_password1", OracleType.VarChar).Value = Password.Password;
                    cmd.Parameters.Add("message", OracleType.VarChar, 150);
                    cmd.Parameters["message"].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("status", OracleType.VarChar, 150);
                    cmd.Parameters["status"].Direction = System.Data.ParameterDirection.Output;
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    Message.Text = cmd.Parameters["message"].Value.ToString();
                    bool status = Convert.ToBoolean(cmd.Parameters["status"].Value.ToString());
                    if(status)
                    {
                        if(Login.Text == "root"  && Password.Password == "root")
                        {
                            MainRoot mainRoot = new MainRoot();
                            this.Close();
                            mainRoot.Show();

                            objConn.Close();

                        }
                        else
                        {

                       
                        main na = new main();
                        Login1.login = Login.Text;
                        na.Login.Content = Login.Text;
                        objConn.Close();
                        
                        this.Close();
                        na.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Text =  ex.ToString();
                    objConn.Close();
                }
                objConn.Close();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Register registers = new Register();
            
            this.Close();
            registers.Show();
        }
    }
}
