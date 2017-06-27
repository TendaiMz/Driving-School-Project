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
    /// Interaction logic for InstructorDetails.xaml
    /// </summary>
    public partial class InstructorDetails : UserControl
    {
        public InstructorDetails()
        {
            InitializeComponent();
            LoadInstructors();
            cmbVehicle.ItemsSource = DbAccess.GetAllVehicles();
            cmbVehicle1.ItemsSource = DbAccess.GetAllVehicles();
          
        }

        private void dtgInstructors_CurrentCellChanged(object sender, EventArgs e)
        {
           var selectedRow= dtgInstructors.SelectedItem as Instructor;
           if (selectedRow!=null)
           {
               DbInsert.UpdateInstructorrData(selectedRow);
           }
        }

        private void dtgInstructors_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("Value Changed", "USER DATA", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Instructor nwInstructor = new Instructor() 
            {
                Address=txtAddress.Text,
                ContactNumber=txtPhone1.Text,
                DefensiveExpiry=Convert.ToDateTime(dtpDefensiveExp.SelectedDate),
                DefensiveNo = txtDefensive.Text,
                Email = txtEmail.Text,
                IDNumber= txtIDNumber.Text,
                LicenceNumber=txtLicence.Text,
                Name=txtName.Text,
                Resigned=false,
                Surname=txtSurname.Text
            };
            DbInsert.InsertInstructor(nwInstructor);
            MessageBox.Show("Instructor Added", "NEW INSTRUCTOR", MessageBoxButton.OK, MessageBoxImage.Information);
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            InstructorDetails newInstructor = new InstructorDetails();
            mainWindow.grdMain.Children.Clear();
            mainWindow.grdMain.Children.Add(newInstructor);
            

        }
       void  LoadInstructors()
        {
            dtgInstructors.ItemsSource = DbAccess.GetAllInstructors();
       }
    }
}
