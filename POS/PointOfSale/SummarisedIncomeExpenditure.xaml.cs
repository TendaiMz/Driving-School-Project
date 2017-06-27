using BUL;
using DAL;
using Microsoft.Reporting.WinForms;
using PointOfSale.DataSet;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for SummarisedIncomeExpenditure.xaml
    /// </summary>
    public partial class SummarisedIncomeExpenditure : UserControl
    {
        public  DateTime start, end,now = DateTime.Today;
        public int instructorID;
        public bool Isfiltered;

        public SummarisedIncomeExpenditure()
        {
            InitializeComponent();
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();
           

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
     
            if (dtpStart.SelectedDate==null)
            {
                MessageBox.Show("Please select the start date!", "START DATE MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;
            }
            else  if (dtpEnd.SelectedDate==null)
            {
                MessageBox.Show("Please select the end date!", "START DATE MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;

            }
            else   if (dtpEnd.SelectedDate < dtpStart.SelectedDate)
            {
                MessageBox.Show("The start date cannot be greater than the end date!", "INVALID FILTER", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;

            }
            else if (cmbInstructor.SelectedValue == null&& chkAll.IsChecked==false)
            {
                MessageBox.Show("Please select the instructor!", "INSTRUCTOR MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                popFilter.IsOpen = true;


            }
            else
           {
                var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                SummarisedIncomeExpenditure nwIncomeExpenditure = new SummarisedIncomeExpenditure();
                mainWindow.grdMain.Children.Clear();
                nwIncomeExpenditure.instructorID =Globals.instructorID;
                nwIncomeExpenditure.start = Convert.ToDateTime(dtpStart.SelectedDate) ;
                nwIncomeExpenditure.end = Convert.ToDateTime(dtpEnd.SelectedDate);
                nwIncomeExpenditure.Isfiltered = true;
                mainWindow.grdMain.Children.Add(nwIncomeExpenditure);
                

            }

    }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            popFilter.IsOpen = true;
            wndFilter.Show();
           
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
           
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            SummarisedIncomeExpenditure nwIncomeExpenditure = new SummarisedIncomeExpenditure();
            mainWindow.grdMain.Children.Clear();
            nwIncomeExpenditure.instructorID =Globals.instructorID;           
            nwIncomeExpenditure.Isfiltered = false;
            mainWindow.grdMain.Children.Add(nwIncomeExpenditure);
        }

        private void cmbInstructor_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
           
        }

        private void chkAll_Checked(object sender, RoutedEventArgs e)
        {
            cmbInstructor.Text = string.Empty;
            Globals.instructorID = 0;
            chkAll.IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (Isfiltered == false)
            {
                start = new DateTime(now.Year, now.Month, 1);
                end = DateTime.Now;
            }


            ReportDataSource nwReportDataSource = new ReportDataSource();

            IncomeExpenditureSummarised dataSet = new IncomeExpenditureSummarised();
            dataSet.BeginInit();

            nwReportDataSource.Name = "DataSet1";
            nwReportDataSource.Value = dataSet.spCompletedLessons_Test_Income;
            rptViewer.LocalReport.DataSources.Add(nwReportDataSource);

            var reportDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var reportPath = System.IO.Path.Combine(reportDir, @"Report\DetailedIncomeFlow.rdlc");
            rptViewer.LocalReport.ReportPath = reportPath;
            ReportParameter p = new ReportParameter("ReportParameter1", start.ToShortDateString());
            ReportParameter p1 = new ReportParameter("ReportParameter2", end.ToShortDateString());
            rptViewer.LocalReport.SetParameters(p);
            rptViewer.LocalReport.SetParameters(p1);

            dataSet.EndInit();
            DataSet.IncomeExpenditureSummarisedTableAdapters.spCompletedLessons_Test_IncomeTableAdapter nwAdapter = new DataSet.IncomeExpenditureSummarisedTableAdapters.spCompletedLessons_Test_IncomeTableAdapter();
            nwAdapter.ClearBeforeFill = true;
            nwAdapter.Fill(dataSet.spCompletedLessons_Test_Income, start, end,Globals.instructorID);
            rptViewer.RefreshReport();
            wfHost.Visibility = Visibility.Visible;
            wndFilter.Close();
            Globals.instructorID = 0;
        }

        private void cmbInstructor_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       Globals.instructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
       chkAll.IsChecked = false;
    }


}
}
