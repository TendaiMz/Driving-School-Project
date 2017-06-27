using BUL;
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

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for CancelSale.xaml
    /// </summary>
    public partial class CancelSale : UserControl
    {
        public CancelSale()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
                 System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["invoiceViewSource"];
                myCollectionViewSource.Source = DbAccess.GetInvoices();
             }
        }

        private void chkComplete_Checked(object sender, RoutedEventArgs e)
        {
            var selectedItem = invoiceDataGrid.SelectedItem as InvoiceData;
            if (selectedItem != null)
            {
                selectedItem.Cancelled = true;
                selectedItem.CancelledBy = Globals.LogInID;
                selectedItem.CancellationReason = "Double Sale";
                DbInsert.CancelSale(selectedItem);

            }
        }

        private void chkComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = invoiceDataGrid.SelectedItem as InvoiceData;
            if (selectedItem != null)
            {
                selectedItem.Cancelled = false;
                selectedItem.CancelledBy = 0;
                selectedItem.CancellationReason = "";
                DbInsert.CancelSale(selectedItem);

            }
        }
    }
}
