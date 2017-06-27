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
using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;
using BUL;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for CashUp.xaml
    /// </summary>
    public partial class CashUp : UserControl
    {
        string cashier = " ";
        DateTime start = DateTime.Today, end=DateTime.Today;
        public CashUp()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDataSource nwReportDataSource = new ReportDataSource();
            CashUpData dataSet = new CashUpData();
            dataSet.BeginInit();

            nwReportDataSource.Name = "DataSet1";
            nwReportDataSource.Value = dataSet.spCasUp;
            rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

            var reportDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var reportPath = System.IO.Path.Combine(reportDir, @"Report\CashUpRprt.rdlc");
            rptViewer.LocalReport.ReportPath=reportPath;

            //Add report parameters
            cashier = DbAccess.GetUserFullNameByID(Globals.LogInID);
            ReportParameter p = new ReportParameter("CashierName", cashier);
            rptViewer.LocalReport.SetParameters(p);

            dataSet.EndInit();

            DataSet.CashUpDataTableAdapters.spCasUpTableAdapter nwAdapter = new DataSet.CashUpDataTableAdapters.spCasUpTableAdapter();
            nwAdapter.ClearBeforeFill=true;
            nwAdapter.Fill(dataSet.spCasUp, start, end, Globals.LogInID);
            rptViewer.RefreshReport();            
           
            
        }
    }
}
