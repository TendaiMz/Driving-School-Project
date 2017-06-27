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
using BUL;
using DAL;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : UserControl
    {
        private ListCollectionView view;
        private ICollection<Customer> customers;
        public NewCustomer()
        {
            InitializeComponent();

            customers= (ICollection<Customer>)DbAccess.GetAllCustomers();
            this.DataContext = customers;
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
            view.CurrentChanged += new EventHandler(view_CurrentChanged);

            //Search for customer
            cmbCustomerSearch.ItemsSource = customers;
                
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToNext();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToPrevious();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer cust = new Customer()
                {
                    ID = Convert.ToInt32(txtID.Text),
                    DateOfBirth = Convert.ToDateTime(dtpDOB.SelectedDate),
                    ExpiryDate = Convert.ToDateTime(dtpExpire.SelectedDate),
                    FirstName = txtName.Text,
                    IdentityNumber = txtIDNum.Text,
                    NextOfKin = txtKin.Text,
                    //PDLNumber = txtPDL.Text,
                    NextOfKinPhoneNumber = txtKinContact.Text,
                    PhoneNumber1 = txtPhoneNum1.Text,
                    PhoneNumber2 = txtPhoneNum2.Text,
                    Surname = txtSurname.Text,
                    Valid = dtpValid.SelectedDate
                };
                DbInsert.UpdateCustomerData(cust);
                MessageBox.Show("Customer Data Updated!", "Data Changed", MessageBoxButton.OK, MessageBoxImage.Information);
    
            }
            catch (Exception)
            {
                MessageBox.Show("SYSTEM ERROR!", "An error occurred contact system admin.", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }


        private void view_CurrentChanged(object sender, EventArgs e)
        {
            lblPosition.Text = "Record " + (view.CurrentPosition + 1).ToString() + " of " + view.Count.ToString();
            btnPrev.IsEnabled = view.CurrentPosition > 0;
            btnNext.IsEnabled = view.CurrentPosition < view.Count - 1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //var curr = customers.Where(c => c.CustomerNumber == txtCustomerSearch.Text); //DbAccess.GetCustomersByCustomerNumber(txtCustomerSearch.Text);
            //view.MoveCurrentTo(curr);
        }

        private void cmbCustomerSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.MoveCurrentTo(cmbCustomerSearch.SelectedItem);
        }
    }
}
