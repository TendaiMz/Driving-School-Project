using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;
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
    /// Interaction logic for DetailedMonthlyReport.xaml
    /// </summary>
    public partial class DetailedMonthlyReport : UserControl
    {
        DateTime now = DateTime.Today;
        DateTime start = DateTime.Today, end = DateTime.Today;
        public DetailedMonthlyReport()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        { 
            var start = new DateTime(now.Year, now.Month, 1);
            end = DateTime.Now;//start.AddMonths(1).AddDays(-1);
            ReportDataSource nwReportDataSource = new ReportDataSource();
           DetailedMonthlyData  dataSet = new DetailedMonthlyData();
            dataSet.BeginInit();

            nwReportDataSource.Name = "DataSet1";
            nwReportDataSource.Value = dataSet.spDetailedMonthlyReporting;
            rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

            var reportDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var reportPath = System.IO.Path.Combine(reportDir, @"Report\DetailedMonthly.rdlc");
            rptViewer.LocalReport.ReportPath = reportPath;

            //Add report parameters
            //cashier = DbAccess.GetUserFullNameByID(Globals.LogInID);
            //ReportParameter p = new ReportParameter("CashierName", cashier);
            //rptViewer.LocalReport.SetParameters(p);

            dataSet.EndInit();
            DataSet.DetailedMonthlyDataTableAdapters.spDetailedMonthlyReportingTableAdapter nwAdapter = new DataSet.DetailedMonthlyDataTableAdapters.spDetailedMonthlyReportingTableAdapter();
            nwAdapter.ClearBeforeFill = true;
            nwAdapter.Fill(dataSet.spDetailedMonthlyReporting, start,end);//, Globals.LogInID);
            rptViewer.RefreshReport();


        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
