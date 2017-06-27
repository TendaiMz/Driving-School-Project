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
    /// Interaction logic for Fueling.xaml
    /// </summary>
    public partial class Fueling : UserControl
    {
        public Fueling()
        {
            InitializeComponent();
            cmbCarReg.ItemsSource = DbAccess.GetAllVehicles();
            cmbCarReg1.ItemsSource = DbAccess.GetAllVehicles();
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();
            cmbInstructor1.ItemsSource = DbAccess.GetAllInstructors();
            LoadDataGrid();
        }

        private void dtgFueling_CurrentCellChanged(object sender, EventArgs e)
        {
            var selectedRow = dtgFueling.SelectedItem as VehicleFueling;
            if (selectedRow != null)
            {
                DbInsert.UpdateFuelingeData(selectedRow);
            }
        }

        private void dtgFueling_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("Value Changed", "FUEL DATA", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        void LoadDataGrid()
        {
            dtgFueling.ItemsSource = DbAccess.GetAllFuelingData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCarReg.SelectedValue == null)
            {
                MessageBox.Show("Car Registration Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dtpDate.SelectedDate == null)
            {
                MessageBox.Show("Date Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtFuelAmount.Text == null)
            {
                MessageBox.Show("Fuel Amount Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (Convert.ToDecimal(txtFuelAmount.Text)<=0)
            {
                MessageBox.Show("Please Enter The Correct Fuel Amount", "DATA INCORRECT", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbInstructor.SelectedValue == null)
            {
                MessageBox.Show("Instructor Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtAmount.Text == null)
            {
                MessageBox.Show("Amount Paid Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (Convert.ToDecimal(ExtractNumber(txtAmount.Text)) <=0)
            {
                MessageBox.Show("Please Enter The Correct Amount Paid", "DATA INCORRECT", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtInvoice.Text == string.Empty)
            {
                MessageBox.Show("Invoice Number Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtMilage.Value == null)
            {
                MessageBox.Show("Milage Missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!DbAccess.GetCheckMilage(Convert.ToDecimal(txtMilage.Text), Convert.ToByte(cmbCarReg.SelectedValue)))
            {
                MessageBox.Show("Milage Cannot be less than the previous milage", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            else
            {
               
                VehicleFueling nwVehicleFueling = new VehicleFueling()
                {
                    Date = Convert.ToDateTime(dtpDate.SelectedDate),
                    FuelAmount = Convert.ToDecimal(txtFuelAmount.Text),
                    InstructorID = Convert.ToInt32(cmbInstructor.SelectedValue),
                    InvoiceNumber = txtInvoice.Text,
                    Milage = Convert.ToDecimal(txtMilage.Text),
                    Notes = txtNotes.Text,
                    Price = Convert.ToDecimal(ExtractNumber(txtAmount.Text)) * (decimal)0.01,
                    UserID = Globals.LogInID,
                    VehicleID = Convert.ToByte(cmbCarReg.SelectedValue)
                };

                DbInsert.InsertFuelingData(nwVehicleFueling);
                LoadDataGrid();
                MessageBox.Show("Fueling Data Added", "DATA ADDED", MessageBoxButton.OK, MessageBoxImage.Information);
                var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                Fueling nwFueling = new Fueling();
                mainWindow.grdMain.Children.Clear();
                mainWindow.grdMain.Children.Add(nwFueling);
            }


        }

        public static string ExtractNumber(string original)
        {
            return new string(original.Where(c => Char.IsDigit(c)).ToArray());
        }

        private void cmbCarReg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbInstructor.SelectedValue = DbAccess.GetInstructorByCar(Convert.ToInt16(cmbCarReg.SelectedValue));
        }
    }
}
