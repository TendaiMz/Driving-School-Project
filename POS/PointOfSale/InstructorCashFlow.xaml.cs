using BUL;
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
    /// Interaction logic for InstructorCashFlow.xaml
    /// </summary>
    public partial class InstructorCashFlow : UserControl
    {

        int month, year, month1, year1;
        int instructorID;
        public InstructorCashFlow()
        {
            InitializeComponent();
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (dtpEnd.SelectedDate < dtpStart.SelectedDate)
            {
                MessageBox.Show("The start date cannot be greater than the end date!", "INVALID FILTER", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (cmbInstructor.SelectedValue == null)
            {
                MessageBox.Show("Please select the instructor!", "INSTRUCTOR MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            else
            {
                month = Convert.ToDateTime(dtpStart.SelectedDate).Month;
                year = Convert.ToDateTime(dtpStart.SelectedDate).Year;
                month1 = Convert.ToDateTime(dtpEnd.SelectedDate).Month;
                year1 = Convert.ToDateTime(dtpEnd.SelectedDate).Year;

                ReportDataSource nwReportDataSource = new ReportDataSource();
                InstructorCashFlowData dataSet = new InstructorCashFlowData();
                dataSet.BeginInit();

                nwReportDataSource.Name = "DataSet1";
                nwReportDataSource.Value = dataSet.spInstructorIncomeExpenditure;
                rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

                var reportDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var reportPath = System.IO.Path.Combine(reportDir, @"Report\InstructorCashFlowRprt.rdlc");
                rptViewer.LocalReport.ReportPath = reportPath;
                //ReportParameter p = new ReportParameter("ReportParameter1", cmbInstructor.Text);
                //ReportParameter p1 = new ReportParameter("ReportParameter2", DbAccess.GetVehicleRegInstructor(instructorID));
                //rptViewer.LocalReport.SetParameters(p);
                //rptViewer.LocalReport.SetParameters(p1);

                dataSet.EndInit();

                DataSet.InstructorCashFlowDataTableAdapters.spInstructorIncomeExpenditureTableAdapter nwAdapter = new DataSet.InstructorCashFlowDataTableAdapters.spInstructorIncomeExpenditureTableAdapter();
                nwAdapter.ClearBeforeFill = true;
                nwAdapter.Fill(dataSet.spInstructorIncomeExpenditure, instructorID, dtpStart.SelectedDate, dtpEnd.SelectedDate);
                rptViewer.RefreshReport();
                wfHost.Visibility = Visibility.Visible;
                wndFilter.Close();
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wndFilter.Show();
        }

        private void cmbInstructor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
        }
    }
}
