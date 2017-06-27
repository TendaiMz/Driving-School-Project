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

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for Maintenance.xaml
    /// </summary>
    public partial class MaintenanceCard : UserControl
    {
        public MaintenanceCard()
        {
            InitializeComponent();
            LoadDataGrid();
            cmbCarReg.ItemsSource = DbAccess.GetAllVehicles();
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();
            cmbType.ItemsSource = DbAccess.GetAllMaintananceType();
            cmbCarReg1.ItemsSource = DbAccess.GetAllVehicles();
            cmbInstructor1.ItemsSource = DbAccess.GetAllInstructors();
            cmbType1.ItemsSource = DbAccess.GetAllMaintananceType();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Maintenace nwMaintenace = new Maintenace()
            { 
                AmountPaid = Convert.ToDecimal(txtAmount.Text),
                CarID=Convert.ToByte(cmbCarReg.SelectedValue),
                Date = Convert.ToDateTime(dtpDate.SelectedDate),
                Description = txtDescription.Text,
                InstructorID=Convert.ToInt32(cmbInstructor.SelectedValue),
                InvoiceNumber= txtInvoice.Text,           
                TypeID=Convert.ToByte(cmbType.SelectedValue),
                Notes = txtNotes.Text
                //UserID=
            };

            DbInsert.InsertMaintenaceData(nwMaintenace);
            LoadDataGrid();
            MessageBox.Show("Vehicle Added", "NEW VEHICLE", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void dtgMaintenance_CurrentCellChanged(object sender, EventArgs e)
        {
            var selectedRow = dtgMaintenance.SelectedItem as Maintenace;
            if (selectedRow!=null)
            {
                DbInsert.UpdateMaintenaceData(selectedRow);
            }
           
        }

        private void dtgMaintenance_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("Value Changed", "MAINTENANCE DATA", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadDataGrid()
        {
               dtgMaintenance.ItemsSource = DbAccess.GetAllMaintenanceDetails();
        }
    }
}
