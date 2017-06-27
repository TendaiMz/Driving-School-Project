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
    /// Interaction logic for ViewCustomer.xaml
    /// </summary>
    public partial class ViewCustomer : UserControl
    {
        public ViewCustomer()
        {
            InitializeComponent();
            dtgCutomers.ItemsSource = DbAccess.GetAllCustomers();
        }

        private void dtgCutomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            //get selected customerID and display more customer details in the main window         
            NewCustomer nwCus = new NewCustomer();
            var selected = dtgCutomers.SelectedValue as Customer;
            nwCus.DataContext = DbAccess.GetCustomersByID(Convert.ToInt32(selected.ID));
            nwCus.btnSave.Visibility = System.Windows.Visibility.Collapsed;
            nwCus.stkNav.Visibility = System.Windows.Visibility.Collapsed;
            nwCus.wrpSearch.Visibility = System.Windows.Visibility.Collapsed;
            var mainWinGrid = Application.Current.MainWindow.FindName("grdMain") as Grid;          
            mainWinGrid.Children.Clear();
            mainWinGrid.Children.Add(nwCus);
          

        }
    }
}
