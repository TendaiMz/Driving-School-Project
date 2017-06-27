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
using System.Windows.Controls.Ribbon;
using BUL;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnNewSale_Click(object sender, RoutedEventArgs e)
        {
            NewSaler newSale = new NewSaler();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(newSale);
        }

        private void btnDailyLog_Click(object sender, RoutedEventArgs e)
        {
            DailySheet daySheet = new DailySheet();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(daySheet);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbxResult = MessageBox.Show("Are you sure you want to close?", "SYSTEM CLOSING", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
           

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            VehicleCard vehic = new VehicleCard();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(vehic);

        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            NewCustomer nwCustoner = new NewCustomer();
            //nwCustoner.wrpNav.Visibility = System.Windows.Visibility.Collapsed;
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwCustoner);
        }

        private void btnViewCustomer_Click(object sender, RoutedEventArgs e)
        {
            ViewCustomer vwCust = new ViewCustomer();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(vwCust);  
        }

        private void btnSaleData_Click(object sender, RoutedEventArgs e)
        {
            DetailedSales nwDetaledSales = new DetailedSales();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwDetaledSales);
        }

        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            EditVehicle edtVehicle = new EditVehicle();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(edtVehicle);  
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            SystemUser nwUser = new SystemUser();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwUser);  
        }

        private void btnManageInstructor_Click(object sender, RoutedEventArgs e)
        {
            InstructorDetails nwInstructr = new InstructorDetails();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwInstructr);
        }

        private void btnMaintenace_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceCard nwMaintenanceCrd = new MaintenanceCard();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwMaintenanceCrd);
        }

        private void btnFueling_Click(object sender, RoutedEventArgs e)
        {
            Fueling nwFueling = new Fueling();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwFueling);
        }

        private void btnInstructorSchedule_Click(object sender, RoutedEventArgs e)
        {
            InstructorSchedule nwInstructorSched = new InstructorSchedule();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwInstructorSched);
        }

        private void btnStudentSchedule_Click(object sender, RoutedEventArgs e)
        {
            StudentSchedule nwStudentSchedule = new StudentSchedule();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwStudentSchedule);
        }

        private void btnScheduleRec_Click(object sender, RoutedEventArgs e)
        {
            DailySheet daySheet = new DailySheet();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(daySheet);
        }

        private void btnCashUp_Click(object sender, RoutedEventArgs e)
        {
            CashUp nwCashUp = new CashUp();
           // nwCashUp.popCashUp.IsOpen = true;
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwCashUp);
        }

        private void btnReporting_Click(object sender, RoutedEventArgs e)
        {
            FuelUsageReporting nwReport = new FuelUsageReporting();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwReport);
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         MessageBoxResult mbxResult=MessageBox.Show("Are you sure you want to close?","SYSTEM CLOSING",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (mbxResult==MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnCashFlow_Click(object sender, RoutedEventArgs e)
        {
            InstructorCashFlow nwReport = new InstructorCashFlow();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwReport);

        }

        private void btnIncomeExpenditure_Click(object sender, RoutedEventArgs e)
        {
            SummarisedIncomeExpenditure nwReport = new SummarisedIncomeExpenditure();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwReport);
        }

        private void btnDetailedMonthly_Click(object sender, RoutedEventArgs e)
        {
            DetailedMonthlyReport nwReport = new DetailedMonthlyReport();
            this.grdMain.Children.Clear();
            this.grdMain.Children.Add(nwReport);
        }

        private void btnCancelSale_Click(object sender, RoutedEventArgs e)
        {
            if (DbAccess.IsSysAdmin(Globals.LogInID))
            {
                CancelSale nwCancelSale = new CancelSale();
                this.grdMain.Children.Clear();
                this.grdMain.Children.Add(nwCancelSale);
            }
         
        
        }
    }
}
