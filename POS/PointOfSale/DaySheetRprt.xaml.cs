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
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for DaySheetRprt.xaml
    /// </summary>
    public partial class DaySheetRprt : Window
    {
       public static int insrtuctorID;
       public static string instruktaName;
        public DaySheetRprt()
        {
            InitializeComponent();
        }

        private void wfhMain_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDataSource nwReportDataSource = new ReportDataSource();
            DaySheetSchedule dataSet = new DaySheetSchedule();

            dataSet.BeginInit();

            nwReportDataSource.Name = "DataSet1";
            nwReportDataSource.Value = dataSet.spInstructorSchedule;
            rptViewer.LocalReport.DataSources.Add(nwReportDataSource);
         
            var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var reportPath = System.IO.Path.Combine(startupPath, @"Report\InstructorDaySheet.rdlc");
            this.rptViewer.LocalReport.ReportPath = reportPath;

            //Add report parameters
            ReportParameter p = new ReportParameter("ReportParameter1", instruktaName);
            rptViewer.LocalReport.SetParameters(p);
            dataSet.EndInit();

            //Fill data into the DataSet
            DataSet.DaySheetScheduleTableAdapters.spInstructorScheduleTableAdapter itemsTableAdapter = new DataSet.DaySheetScheduleTableAdapters.spInstructorScheduleTableAdapter();

            itemsTableAdapter.ClearBeforeFill = true;
            itemsTableAdapter.Fill(dataSet.spInstructorSchedule, insrtuctorID,DateTime.Today);
            rptViewer.RefreshReport();


        }
    }
}
