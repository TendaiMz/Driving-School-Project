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
    /// Interaction logic for VehicleCard.xaml
    /// </summary>
    public partial class VehicleCard : UserControl
    {
        public VehicleCard()
        {
            InitializeComponent();
            cmbColor.ItemsSource = DbAccess.GetCarColor();
            cmbFuel.ItemsSource = DbAccess.GetCarFuel();
            cmbMake.ItemsSource = DbAccess.GetCarMake();
            cmbModel.ItemsSource = DbAccess.GetCarModel();
            cmbType.ItemsSource = DbAccess.GetCarType();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtReg.Text==string.Empty)
            {
                MessageBox.Show("Vehicle Registration number missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (dtpYear.Text==string.Empty)
            {
                MessageBox.Show("Year missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbMake.SelectedValue==null)
            {
                MessageBox.Show("Vehicle make missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbColor.SelectedValue==null)
            {
                MessageBox.Show("Vehicle colour missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbType.SelectedItem==null)
            {
                MessageBox.Show("Vehicle type missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbModel.SelectedValue==null)
            {
                MessageBox.Show("Vehicle model missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (cmbFuel.SelectedValue==null)
            {
                MessageBox.Show("Fuel type missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtChasis.Text==string.Empty)
            {
                MessageBox.Show("Vehicle chasis number missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtMilage.Value ==null)
            {
                MessageBox.Show("Vehicle milage missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtEngineNumber.Text == string.Empty)
            {
                MessageBox.Show("Vehicle engine number missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (txtTankCapacity.Value==null)
            {
                MessageBox.Show("Vehicle taank capacity missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                Vehicle nwVehicle = new Vehicle()
                {
                    ChasisNumber = txtChasis.Text,
                    Colour = Convert.ToByte(cmbColor.SelectedValue.ToString()),
                    EngineNumber = txtEngineNumber.Text,
                    Fuel = Convert.ToByte(cmbFuel.SelectedValue),
                    InsuranceExipry = Convert.ToDateTime(dtpInsurance.SelectedDate),
                    Make = Convert.ToByte(cmbMake.SelectedValue),
                    Milage = Convert.ToDecimal(txtMilage.Text),
                    Model = Convert.ToByte(cmbModel.SelectedValue),
                    Servicing = 5000,//Convert.ToDecimal(txtServicing.Text),
                    TankCapacity = 0, //Convert.ToDecimal(txtTankCapacity.Text),
                    VehicleRegistration = txtReg.Text,
                    VehicleType = Convert.ToByte(cmbType.SelectedValue),
                    Year = Convert.ToDateTime(dtpYear.Text)
                };

                DbInsert.InsertVehicle(nwVehicle);
                MessageBox.Show("Vehicle Added", "NEW VEHICLE", MessageBoxButton.OK, MessageBoxImage.Information);
                var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                VehicleCard nwVehicleCard = new VehicleCard();
                mainWindow.grdMain.Children.Clear();
                mainWindow.grdMain.Children.Add(nwVehicleCard);

            }
            
        }

        private void cmbCustomerSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        

    }
}
