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
using System.Windows.Shapes;
using DAL;
using BUL;


namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void txtUser1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUser1.Visibility = System.Windows.Visibility.Collapsed;
            txtUser.Visibility = System.Windows.Visibility.Visible;
            txtUser.Focus();
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtUser.Text==string.Empty)
            {
                txtUser.Visibility = System.Windows.Visibility.Hidden;
                txtUser1.Visibility = System.Windows.Visibility.Visible;
            }
               
        }

        private void pssPassword1_GotFocus(object sender, RoutedEventArgs e)
        {
            pssPassword1.Visibility = System.Windows.Visibility.Collapsed;
            pssPassword.Visibility = System.Windows.Visibility.Visible;
            pssPassword.Focus();
        }

         private void txtUser1_KeyDown(object sender, KeyEventArgs e)
        {
            
     
        }

        private void pssPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if ( pssPassword.Password== string.Empty)
            {
                pssPassword.Visibility = System.Windows.Visibility.Hidden;
                pssPassword1.Visibility = System.Windows.Visibility.Visible;
            }
              
        }

        private void txtName1_GotFocus(object sender, RoutedEventArgs e)
        {
             txtName1.Visibility = System.Windows.Visibility.Collapsed;
             txtName.Visibility = System.Windows.Visibility.Visible;
             txtName.Focus();
        }

        private void txtSurname1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSurname1.Visibility = System.Windows.Visibility.Collapsed;
            txtSurname.Visibility = System.Windows.Visibility.Visible;
            txtSurname.Focus();
        }

        private void pssRegPassword1_GotFocus(object sender, RoutedEventArgs e)
        {
            pssRegPassword1.Visibility = System.Windows.Visibility.Collapsed;
            pssRegPassword.Visibility = System.Windows.Visibility.Visible;
            pssRegPassword.Focus();
        }

        private void pssConfirmPass1_GotFocus(object sender, RoutedEventArgs e)
        {
            pssConfirmPass1.Visibility = System.Windows.Visibility.Collapsed;
            pssConfirmPass.Visibility = System.Windows.Visibility.Visible;
            pssConfirmPass.Focus();
        }

        private void pssConfirmPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pssConfirmPass.Password == string.Empty)
            {
                pssConfirmPass.Visibility = System.Windows.Visibility.Hidden;
                pssConfirmPass1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void pssRegPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pssRegPassword.Password == string.Empty)
            {
                pssRegPassword.Visibility = System.Windows.Visibility.Hidden;
                pssRegPassword1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void txtSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSurname.Text == string.Empty)
            {
                txtSurname.Visibility = System.Windows.Visibility.Hidden;
                txtSurname1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == string.Empty)
            {
                txtName.Visibility = System.Windows.Visibility.Hidden;
                txtName1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pssRegPassword.Password == pssConfirmPass.Password)
                {
                    User nwUser = new User()
                    {
                        Name = txtName.Text,
                        Surname = txtSurname.Text,
                        Activated = false,
                        PasswordHash = PasswordHashing.HashPassword(pssRegPassword.Password),
                        UserName = txtUserName.Text
                    };
                    DbInsert.InsertUser(nwUser);
                    MessageBox.Show("New User " + nwUser.Name + " " + nwUser.Surname + " Created!", "PASSWORD MISMATCH", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtName.Text = string.Empty;
                    txtSurname.Text = string.Empty;
                    pssConfirmPass.Password = string.Empty;
                    pssRegPassword.Password = string.Empty;
                }
                else
                {
                    pssConfirmPass.Password = string.Empty;
                    pssRegPassword.Password = string.Empty;
                    MessageBox.Show("Password does not match", "PASSWORD MISMATCH", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }


            }
            catch (Exception)
            {

               // 
            }
           
           
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var hashedPassword = DbAccess.GetUserPasswordHashByUserName(txtUser.Text);
                if (DbAccess.GetUserPasswordHashByUserName(txtUser.Text).Any())
                {
                    if (PasswordHashing.ValidatePassword(pssPassword.Password, hashedPassword))
                    {
                        if (DbAccess.IsUserNameIsAcivated(txtUser.Text))
                        {
                            //set the global value for userid
                            Globals.LogInID = DbAccess.GetUserIDByUserName(txtUser.Text);
                            MainWindow nwMain = new MainWindow();
                            nwMain.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Your Account has not been activated\n Please contact system admin", "ACOOUNT INACTIVE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtUser.Text = string.Empty;
                            pssPassword.Password = string.Empty;
                        }

                    }
                    else
                    {
                        txtUser.Text = string.Empty;
                        pssPassword.Password = string.Empty;
                        MessageBox.Show("User Name and Password does not match", "PASSWORD MISMATCH", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("User not in the system", "USER NOT REGISTERED", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
          
           

        }

        private void txtUser1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtUserName1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName1.Visibility = System.Windows.Visibility.Hidden;
            txtUserName.Visibility = System.Windows.Visibility.Visible;
            txtUserName.Focus();
        }

        private void txtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == string.Empty)
            {
                txtUserName.Visibility = System.Windows.Visibility.Hidden;
                txtUserName1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DbAccess.CheckIfUserNameIsInUse(txtUserName.Text))
            {
                MessageBox.Show("Choose another username", "USERNAME IN USE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtUserName.Visibility = System.Windows.Visibility.Hidden;
                txtUserName1.Visibility = System.Windows.Visibility.Visible;
                popRegister.IsOpen = true;
            }
            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            popRegister.IsOpen = true;
        }

        private void wndLogIn_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DbAccess.CheckConnection();
            }
            catch (Exception)
            {
                
                MessageBox.Show("The system failed to connect to the database.","DATABASE CONNECTION FAILURE",MessageBoxButton.OK,MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
