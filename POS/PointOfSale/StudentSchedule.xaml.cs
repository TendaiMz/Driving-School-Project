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
using WpfScheduler;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.UI.Xaml.Schedule;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for StudentSchedule.xaml
    /// </summary>
    public partial class StudentSchedule : UserControl
    {
      //  int instuctorID,totalLessons, stndScheduleID;
        //ObservableCollection<InstructorLearnerSchedule> studentBookings = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> roadTests = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> roadTestSelected = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> lessons = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> lessonsSelected = new ObservableCollection<InstructorLearnerSchedule>();
       

        public StudentSchedule()
        {
            InitializeComponent();
            cmbStudentName.ItemsSource = DbAccess.GetAllCustomers();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Week;
            btnWeek.IsEnabled = false;
            btnMonth.IsEnabled = true;
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Month;
            btnMonth.IsEnabled = false;
            btnWeek.IsEnabled = true;


        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Day;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Month;
            
        }

        private void cmbStudentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Get uncompleted and pending lessons
                txbCompleted.Text = DbAccess.GetStudentCompletedLessons(Convert.ToInt16(cmbStudentName.SelectedValue)).ToString();
                txbPending.Text = DbAccess.GetStudentPendingLessons(Convert.ToInt16(cmbStudentName.SelectedValue)).ToString();

                ScheduleAppointmentCollection appointments = new ScheduleAppointmentCollection();
              
                var schedule = DbAccess.GetStudentSchedule(Convert.ToInt16(cmbStudentName.SelectedValue));
                foreach (var sc in schedule)
                {
                    appointments.Add(new ScheduleAppointment() { Subject = sc.BookingName, AppointmentBackground = Brushes.Green, StartTime = sc.starting, EndTime = sc.ending, Notes = sc.ScheduleInstructorID.ToString(),ReadOnly=true });
                }
            
                studentScheduler.Appointments = appointments;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while trying to load the schedule", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                
            }

        }

         //private void prevBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbStudentName.SelectedItem!=null)
        //        {
        //            DateTime startn = DateTime.Today;
        //            DateTime endn = DateTime.Today.AddDays(31);
        //            DateTime curr = scheduler1.GetSheduledDate();
        //            if (startn < curr)
        //            {
        //                scheduler1.PrevPage();
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select the student first before you can navigate!", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);

        //        }

        //    }
        //    catch (Exception )
        //    {

        //        MessageBox.Show("An error occured while trying to load the schedule", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);

        //    }
        //}

        //private void nextBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbStudentName.SelectedItem!=null)
        //        {
        //            var weekStart = scheduler1.GetLastDate();
        //            DateTime startn = DateTime.Today;
        //            DateTime endn = DateTime.Today.AddDays(31);
        //            DateTime curr = scheduler1.GetSheduledDate();
        //            if (startn >= curr || curr < endn)
        //            {
        //                scheduler1.NextPage();
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select the student first before you can navigate!", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            
        //        }
              

        //    }
        //    catch (Exception )
        //    {
        //        MessageBox.Show("An error occured while trying to load the schedule", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
 
        //    }
           
        //}

        //private void cmbStudentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        //Get uncompleted and pending lessons
        //        txbCompleted.Text = DbAccess.GetStudentCompletedLessons(Convert.ToInt16(cmbStudentName.SelectedValue)).ToString();
        //        txbPending.Text = DbAccess.GetStudentPendingLessons(Convert.ToInt16(cmbStudentName.SelectedValue)).ToString();
        //        scheduler1.Events.Clear();
        //        scheduler1.SelectedDate = DateTime.Today;
        //        var schedule = DbAccess.GetStudentSchedule(Convert.ToInt16(cmbStudentName.SelectedValue));
        //        foreach (var sc in schedule)
        //        {
        //            scheduler1.AddEvent(new Event() { Subject = sc.BookingName, Color = Brushes.Yellow, Start = sc.starting, End = sc.ending, Description=sc.ScheduleInstructorID.ToString(),PersonName=sc.InstructorID.ToString()});
        //        }

        //        scheduler1.NextPage();
        //        scheduler1.PrevPage();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("An error occured while trying to load the schedule","SYSTEM ERROR",MessageBoxButton.OK,MessageBoxImage.Stop);
        //        scheduler1.Events.Clear();
        //    }
           
        //}

      
    }
}