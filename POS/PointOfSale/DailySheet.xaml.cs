using BUL;
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
using System.Collections.ObjectModel;
using PointOfSale.DataSet;
using DAL;
using Microsoft.Reporting.WinForms;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for DailySheet.xaml
    /// </summary>
    public partial class DailySheet : UserControl
    {
        ObservableCollection<InstructorLearnerSchedule> sheduleReconsList = new ObservableCollection<InstructorLearnerSchedule>();
        List<InstructorLearnerSchedule> schedInstructorList = new List<InstructorLearnerSchedule>();
        List<DailyLog> log = new List<DailyLog>();
        public DailySheet()
        {
            InitializeComponent();
           // LoadGridData();
            cmbInstructorName.ItemsSource = DbAccess.GetAllInstructors();

        }

       List<DailyLog> LoadGridData()
       {
          for (int i = 0; i < 31; i++)
          {
               DailyLog dayLog = new DailyLog();
               log.Add(dayLog);
          }
          return log;
      }
       List<DailyLog> LoadTime() 
       {
           TimeSpan span = TimeSpan.FromHours(5);
           foreach (var tym in log)
           {
              
               var time=  String.Format("{0:t}", span);
               tym.Time = time.Substring(0,5);
               TimeSpan spanner = TimeSpan.FromMinutes(30);
               span = span.Add(spanner);               
           }
           return log;
       }

       private void cmbInstructorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           if (cmbInstructorName.SelectedValue!=null)
           {
               if (dtpDate.SelectedDate == null)
               {
                   MessageBox.Show("Please select the date first", "Date Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);

               }
               else if (DbAccess.CheckForUncompletedDaySheet(Convert.ToDateTime(dtpDate.SelectedDate), Convert.ToInt16(cmbInstructorName.SelectedValue)) != null)
               {
                   var dateIncompeleted = DbAccess.CheckForUncompletedDaySheet(Convert.ToDateTime(dtpDate.SelectedDate), Convert.ToInt16(cmbInstructorName.SelectedValue));
                   MessageBox.Show("Complete the schedule for the date " + dateIncompeleted.Value.ToLongDateString(), "INCOMPLETED SCHEDULE", MessageBoxButton.OK, MessageBoxImage.Warning);
                   ClearDataGrid();
               }
               else
               {
                   if ((int)cmbInstructorName.SelectedValue != -1)
                   {
                       LoadDataGrid();  
                   }

               }
           }
          
          
       }

       private void btnPrint_Click(object sender, RoutedEventArgs e)
       {
           if (dtpDate.SelectedDate==null)
           {
               MessageBox.Show("Please select the date first", "Date Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
               
           }
            else if (cmbInstructorName.SelectedValue==null)
           {
               MessageBox.Show("Please select the instructor first", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Stop);
           }
           else
           {

               wndFilter.Show();
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
               ReportParameter p = new ReportParameter("ReportParameter1", cmbInstructorName.Text);
               ReportParameter p1 = new ReportParameter("ReportParameter2", dtpDate.SelectedDate.Value.ToLongDateString());

               rptViewer.LocalReport.SetParameters(p);
               rptViewer.LocalReport.SetParameters(p1);
               dataSet.EndInit();

               //Fill data into the DataSet
               DataSet.DaySheetScheduleTableAdapters.spInstructorScheduleTableAdapter itemsTableAdapter = new DataSet.DaySheetScheduleTableAdapters.spInstructorScheduleTableAdapter();

               itemsTableAdapter.ClearBeforeFill = true;
               itemsTableAdapter.Fill(dataSet.spInstructorSchedule, Convert.ToInt32(cmbInstructorName.SelectedValue), Convert.ToDateTime(dtpDate.SelectedDate));// DateTime.Today);
               rptViewer.RefreshReport();

               
           }
          

       }

       private void btnSave_Click(object sender, RoutedEventArgs e)
       {
           if (dtpDate.SelectedDate==null)
           {
               MessageBox.Show("Please select the date first", "Date Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);

           }
           else if (cmbInstructorName.SelectedValue==null)
           {
               MessageBox.Show("Please select the instructor first", "Instructor Missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
               
           }
           else
            {
                if (dtgData.Items.OfType<InstructorLearnerSchedule>().Where(x => x.Complete == false &&x.Cancel==false && x.NoShow==false).Any())
                {
                        MessageBox.Show("The schedule is incomplete", "INCOMPLETE DATA", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                  
                }
                else
                {
                 
                    var  completedList = dtgData.Items.OfType<InstructorLearnerSchedule>().Where(x => x.Complete == true || x.NoShow == true || x.Cancel == true).ToList();
                    DbInsert.ScheduleRecons(completedList, Globals.LogInID);
                    MessageBox.Show("Schedule data saved", "DATA SAVED", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Reload window
                    var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                    DailySheet nwDailySheet = new DailySheet();
                    mainWindow.grdMain.Children.Clear();
                    mainWindow.grdMain.Children.Add(nwDailySheet);
                }

               
            }
          
          
         
       }

    

        private void chkComplete_Checked(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                {
                    var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                    variable.Complete = true;
                    variable.Cancel = false;
                    variable.NoShow = false;
                }
                else
                {
                    InstructorLearnerSchedule nwInstructorLearnerSchedule = new InstructorLearnerSchedule()
                    {
                        ScheduleInstructorID = selectedItem.ScheduleInstructorID,
                        Complete = true,

                    };
                    schedInstructorList.Add(nwInstructorLearnerSchedule);  
                }
                selectedItem.Cancel = false;
                selectedItem.NoShow = false;
               
            }
       }

        private void chkCancelled_Checked(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                {
                    var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                    variable.Cancel = true;
                    variable.Complete = false;
                    variable.NoShow = false;
                }
                else
                {
                    InstructorLearnerSchedule nwInstructorLearnerSchedule = new InstructorLearnerSchedule()
                    {
                        ScheduleInstructorID = selectedItem.ScheduleInstructorID,
                        Cancel = true,
                    };
                    schedInstructorList.Add(nwInstructorLearnerSchedule);  
                }
                var bb = schedInstructorList;
                selectedItem.Complete = false;
                selectedItem.NoShow = false;
            }
        }

        private void chkNoShow_Checked(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                {
                    var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                    variable.NoShow = true;
                    variable.Complete = false;
                    variable.Cancel = false;
                }
                else
                {
                    InstructorLearnerSchedule nwInstructorLearnerSchedule = new InstructorLearnerSchedule()
                    {
                        ScheduleInstructorID = selectedItem.ScheduleInstructorID,
                        NoShow = true
                    };
                    schedInstructorList.Add(nwInstructorLearnerSchedule);                   
                }
                selectedItem.Cancel = false;
                selectedItem.Complete = false;
                var bb = schedInstructorList;
            }
        }

        private void txtComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtSender = sender as TextBox;
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (selectedItem.ScheduleInstructorID != null)
                {
                    if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                    {
                        var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                        variable.Comment = txtSender.Text;
                    }
                    else
                    {
                        InstructorLearnerSchedule nwInstructorLearnerSchedule = new InstructorLearnerSchedule()
                        {
                            ScheduleInstructorID = selectedItem.ScheduleInstructorID,
                            Comment = txtSender.Text
                        };
                        schedInstructorList.Add(nwInstructorLearnerSchedule);
                    }
                }
            }

            
        }

        private void chkCancelled_Unchecked(object sender, RoutedEventArgs e)
        {
             var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
             if (selectedItem != null)
             {
                 if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                 {
                     var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                     schedInstructorList.Remove(variable);
                 }
             }
        }

        private void chkNoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                {
                    var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                    schedInstructorList.Remove(variable);
                }
            }
        }

        private void chkComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgData.SelectedItem as InstructorLearnerSchedule;
            if (selectedItem != null)
            {
                if (schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).Any())
                {
                    var variable = schedInstructorList.Where(s => s.ScheduleInstructorID == selectedItem.ScheduleInstructorID).SingleOrDefault();
                    schedInstructorList.Remove(variable);
                }
            }

        }

        private void dtpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInstructorName.SelectedValue != null)
            {
                var cmbVal = cmbInstructorName.SelectedValue;
                cmbInstructorName.SelectedValue = -1;
                cmbInstructorName.SelectedValue = cmbVal;
               
            }
        }

        void LoadDataGrid() 
        {
            sheduleReconsList = new ObservableCollection<InstructorLearnerSchedule>(DbAccess.GetAllInstructorScheduleRecon(Convert.ToInt32(cmbInstructorName.SelectedValue), Convert.ToDateTime(dtpDate.SelectedDate)).ToList());
            dtgData.ItemsSource = sheduleReconsList;
               
        }

        void ClearDataGrid()
        {
            dtgData.ItemsSource = null;
        }
    }
}
