using BUL;
using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for FuelUsageReporting.xaml
    /// </summary>
    public partial class FuelUsageReporting : UserControl
    {
        int start, end ;
        public int? year;
        public int instructorID;
        public bool Isfiltered;
        public FuelUsageReporting()
        {
            InitializeComponent();
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (dtpStart.SelectedDate == null)
            {
                MessageBox.Show("Please select the start date!", "START DATE MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;
            }
            else if (dtpEnd.SelectedDate == null)
            {
                MessageBox.Show("Please select the end date!", "START DATE MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;

            }else if (dtpEnd.SelectedDate < dtpStart.SelectedDate)
            {
                MessageBox.Show("The start date cannot be greater than the end date!", "INVALID FILTER", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (cmbInstructor.SelectedValue==null&& chkAll.IsChecked == false)
            {
                MessageBox.Show("Please select the instructor!", "INSTRUCTOR MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
            }
            else
            {
               
                var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                FuelUsageReporting nwFuelUsageRprt = new FuelUsageReporting();
                mainWindow.grdMain.Children.Clear();
                nwFuelUsageRprt.instructorID = Globals.instructorID;
                nwFuelUsageRprt.start = Convert.ToDateTime(dtpStart.SelectedDate).Month;
                nwFuelUsageRprt.end = Convert.ToDateTime(dtpEnd.SelectedDate).Month;
                nwFuelUsageRprt.year = Convert.ToDateTime(dtpEnd.SelectedDate).Year;
                nwFuelUsageRprt.Isfiltered = true;
                mainWindow.grdMain.Children.Add(nwFuelUsageRprt);
            }
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Isfiltered == false)
            {
                dtpStart.SelectedDate = DateTime.Today;
                dtpEnd.SelectedDate = DateTime.Today;
                start = Convert.ToDateTime(dtpStart.SelectedDate).Month - 2;
                end = Convert.ToDateTime(dtpEnd.SelectedDate).Month;
                year = Convert.ToDateTime(dtpEnd.SelectedDate).Year;
            }

           
            ReportDataSource nwReportDataSource = new ReportDataSource();

            InstructorFuelUsage dataSet = new InstructorFuelUsage();
            dataSet.EnforceConstraints = false;
            dataSet.BeginInit();

            nwReportDataSource.Name = "DataSet1";
            nwReportDataSource.Value = dataSet.spInstructorFuelUsage;
            rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

            var reportDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var reportPath = System.IO.Path.Combine(reportDir, @"Report\Fueling.rdlc");
            rptViewer.LocalReport.ReportPath = reportPath;
            //ReportParameter p = new ReportParameter("ReportParameter1", cmbInstructor.Text);
            //ReportParameter p1 = new ReportParameter("ReportParameter2", DbAccess.GetVehicleRegInstructor(instructorID));
            //rptViewer.LocalReport.SetParameters(p);
            //rptViewer.LocalReport.SetParameters(p1);

            dataSet.EndInit();

            DataSet.InstructorFuelUsageTableAdapters.spInstructorFuelUsageTableAdapter nwAdapter = new DataSet.InstructorFuelUsageTableAdapters.spInstructorFuelUsageTableAdapter();
            nwAdapter.ClearBeforeFill = true;
            nwAdapter.Fill(dataSet.spInstructorFuelUsage, start, end, year, instructorID);//.spFuelUsage, start, end,instructorID,DbAccess.GetVehicleByInstructor(instructorID));
            rptViewer.RefreshReport();
            wfHost.Visibility = Visibility.Visible;
            Globals.instructorID = 0;
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            FuelUsageReporting nwFuelUsageRprt = new FuelUsageReporting();
            mainWindow.grdMain.Children.Clear();
            nwFuelUsageRprt.instructorID = Globals.instructorID;
            nwFuelUsageRprt.Isfiltered = false;
            mainWindow.grdMain.Children.Add(nwFuelUsageRprt);

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            popFilter.IsOpen = true;
            wndFilter.Show();
        }

        private void chkAll_Checked(object sender, RoutedEventArgs e)
        {
            cmbInstructor.Text = string.Empty;
            Globals.instructorID = 0;
            chkAll.IsChecked = true;
        }

        private void cmbInstructor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Globals.instructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
            chkAll.IsChecked = false;
        }
    }
}
