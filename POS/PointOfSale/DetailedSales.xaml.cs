using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;
using System;
using System.Windows;
using System.Windows.Controls;
using BUL;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for DetailedSales.xaml
    /// </summary>
    public partial class DetailedSales : UserControl
    {
        public static int cashierID;
        public static DateTime startDate;
        public static DateTime endDate;

        public DetailedSales()
        {
          
            InitializeComponent();
            cmbCashierID.ItemsSource = DbAccess.GetAllUsers();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtpStart.SelectedDate = DateTime.Today;
            dtpEnd.SelectedDate = DateTime.Today;
            if (DbAccess.GetUserTypeByUserID(Globals.LogInID)!=1)
            {
                cmbCashierID.Visibility = System.Windows.Visibility.Collapsed;
                lblCashier.Visibility = System.Windows.Visibility.Collapsed;
                cashierID = Globals.LogInID;
            }
            
            wndFilter.Show();

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (dtpEnd.SelectedDate<dtpStart.SelectedDate)
            {
                MessageBox.Show("The start date cannot be greater than the end date!","INVALID FILTER",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
            else if (DbAccess.GetUserTypeByUserID(Globals.LogInID)==1&& cmbCashierID.SelectedValue==null)
	        {
                MessageBox.Show("Please select the cashier!","CASHIER MISSING",MessageBoxButton.OK,MessageBoxImage.Exclamation);
		 
	        }
            else
            {
                startDate = Convert.ToDateTime(dtpStart.SelectedDate);
                endDate = Convert.ToDateTime(dtpEnd.SelectedDate);

                ReportDataSource nwReportDataSource = new ReportDataSource();
                DetailedSalesData dataSet = new DetailedSalesData();

                dataSet.BeginInit();
                nwReportDataSource.Name = "DataSet1";
                nwReportDataSource.Value = dataSet.spDetailedSales;
                rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

                var reportDirectoryName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var reportPath = System.IO.Path.Combine(reportDirectoryName, @"Report\DetailedSalesRpt.rdlc");
                rptViewer.LocalReport.ReportPath = reportPath;

                DataSet.DetailedSalesDataTableAdapters.spDetailedSalesTableAdapter tableAdapter = new DataSet.DetailedSalesDataTableAdapters.spDetailedSalesTableAdapter();
                tableAdapter.ClearBeforeFill = true;
                tableAdapter.Fill(dataSet.spDetailedSales, dtpStart.SelectedDate, dtpEnd.SelectedDate,cashierID );

                rptViewer.RefreshReport();
                frmHost.Visibility = System.Windows.Visibility.Visible;
                wndFilter.Close();
            }
          
        
        }

        private void cmbCashierID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cashierID =Convert.ToInt16(cmbCashierID.SelectedValue);
        }

    }
}
