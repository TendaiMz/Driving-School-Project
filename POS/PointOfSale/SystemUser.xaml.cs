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
using DAL;
using BUL;
using System.Security.Cryptography;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class SystemUser : UserControl
    {
        public SystemUser()
        {
            InitializeComponent();
          
            cmbUserType.ItemsSource = DbAccess.GetUserType();
            cmbUserName.ItemsSource = DbAccess.GetAllUsers();
            LoadDataGrid();

        }



              
        private void dtgUsers_CurrentCellChanged(object sender, EventArgs e)
        {
            var selectedRow = dtgUsers.SelectedItem as User;
            if (dtgUsers.SelectedItem!=null)
            {                
                DbInsert.UpdateUserData(selectedRow);
                dtgUsers.SelectedIndex=-1;
                LoadDataGrid();
            }
           
            
        }

        void LoadDataGrid()
        {
            dtgUsers.ItemsSource = DbAccess.GetAllUsers();
        }

        private void dtgUsers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("Value Changed","USER DATA",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            wndUpdateUser.Show();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            PaswordReset nwPassReset = new PaswordReset() 
            {
                Date=DateTime.Now,
                SupervisorID=Globals.LogInID,
                UserID=Convert.ToByte(cmbUserName.SelectedValue)
            };

            var hashedUserPass= PasswordHashing.HashPassword("###");
            var hashedSupervisorPass = PasswordHashing.HashPassword(txtPassword.Password);

            
            if (DbAccess.GetUserPasswordHashByID(Globals.LogInID).Any())
            {
                if (PasswordHashing.ValidatePassword(txtPassword.Password, DbAccess.GetUserPasswordHashByID(Globals.LogInID)))
                {
                    DbInsert.ResetPassword(nwPassReset,hashedUserPass);
                    MessageBox.Show("Password Reset", "USER DATA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Supervisor Password does not match", "USER DATA", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
               
             }

          }


        //private static string CreateSalt()
        //{
        //    string myPassword = "password";
        //    string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
        //    //mySalt == "$2a$10$rBV2JDeWW3.vKyeQcM8fFO"
        //    string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);
        //    //myHash == "$2a$10$rBV2JDeWW3.vKyeQcM8fFO4777l4bVeQgDL6VIkxqlzQ7TCalQvla"
        //    bool doesPasswordMatch = BCrypt.Net.BCrypt.Verify(myPassword, myHash);
        //}

        //private static string CreatePasswordHash(string password, string salt)
        //{
        //    string passwrodSalt = String.Concat(password, salt);
        //    string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(passwrodSalt, "sha1");
        //    return hashedPwd;
        //}


    }
}
