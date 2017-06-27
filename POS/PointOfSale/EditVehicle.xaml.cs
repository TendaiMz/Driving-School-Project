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
    /// Interaction logic for EditVehicle.xaml
    /// </summary>
    public partial class EditVehicle : UserControl
    {
        private ListCollectionView view;
        private ICollection<Vehicle> vehicles;
        public EditVehicle()
        {
            InitializeComponent();

            cmbColor.ItemsSource = DbAccess.GetCarColor();
            cmbFuel.ItemsSource = DbAccess.GetCarFuel();
            cmbMake.ItemsSource = DbAccess.GetCarMake();
            cmbModel.ItemsSource = DbAccess.GetCarModel();
            cmbType.ItemsSource = DbAccess.GetCarType();

            vehicles = (ICollection<Vehicle>)DbAccess.GetAllVehicles();
            this.DataContext = vehicles;
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
            view.CurrentChanged += new EventHandler(view_CurrentChanged);

            //Search for vehicle
            cmbVehicleSearch.ItemsSource = vehicles;
            
        }

        private void view_CurrentChanged(object sender, EventArgs e)
        {
            lblPosition.Text = "Record " + (view.CurrentPosition + 1).ToString() + " of " + view.Count.ToString();
            btnPrev.IsEnabled = view.CurrentPosition > 0;
            btnNext.IsEnabled = view.CurrentPosition < view.Count - 1;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Vehicle nwVehicle = new Vehicle()
            {
                ChasisNumber = txtChasis.Text,
                Colour = Convert.ToByte(cmbColor.SelectedValue.ToString()),
                EngineNumber = txtEngineNumber.Text,
                Fuel = Convert.ToByte(cmbFuel.SelectedValue),
                ID = Convert.ToInt16(txtID.Text),
                InsuranceExipry = Convert.ToDateTime(dtpInsurance.SelectedDate),
                Make = Convert.ToByte(cmbMake.SelectedValue),
                Milage = Convert.ToDecimal(txtMilage.Text),
                Model = Convert.ToByte(cmbModel.SelectedValue),
                Servicing = Convert.ToDecimal(txtServicing.Text),
                TankCapacity = Convert.ToDecimal(txtTankCapacity.Text),
                VehicleRegistration = txtReg.Text,
                VehicleType = Convert.ToByte(cmbType.SelectedValue),
                Year = Convert.ToDateTime(dtpYear.Text)
            };

            DbInsert.UpdateVehicleData(nwVehicle);
            MessageBox.Show("Vehicle Data Updated", "DATA UPDATE", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToNext();
        }

        private void cmbVehicleSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.MoveCurrentTo(cmbVehicleSearch.SelectedItem);
        }
    }
}
