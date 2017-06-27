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
using WpfScheduler;
using DAL;
using System.Collections.ObjectModel;
using BUL;
using System.Globalization;
using Syncfusion.UI.Xaml.Schedule;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for InstructorSchedule.xaml
    /// </summary>
    public partial class InstructorSchedule : UserControl
    {
        //int studentID, totalLessons, stndScheduleID, totalRoadTests;
        //ObservableCollection<InstructorLearnerSchedule> studentBookings = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> roadTests = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> roadTestSelected = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> lessons = new ObservableCollection<InstructorLearnerSchedule>();
        //ObservableCollection<InstructorLearnerSchedule> lessonsSelected = new ObservableCollection<InstructorLearnerSchedule>();
      
        public InstructorSchedule()
        {
            InitializeComponent();
            cmbInstructorName.ItemsSource = DbAccess.GetAllInstructors();
           
        }


        private void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Week;
            btnWeek.IsEnabled = false;
            btnMonth.IsEnabled = true;
            btnDay.IsEnabled = true;
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Month;
            btnMonth.IsEnabled = false;
            btnWeek.IsEnabled = true;
            btnDay.IsEnabled = true;

        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Day;
            btnDay.IsEnabled = false;
            btnWeek.IsEnabled = true;
            btnMonth.IsEnabled = true;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            studentScheduler.ScheduleType = Syncfusion.UI.Xaml.Schedule.ScheduleType.Month;

        }

        private void cmbInstructorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
             try
             {
                 ScheduleAppointmentCollection appointments = new ScheduleAppointmentCollection();
                
                 var schedule = DbAccess.GetInstructorSchedule(Convert.ToInt16(cmbInstructorName.SelectedValue));
                 foreach (var sc in schedule)
                 {
                     if (sc.BookingTpye == 1)
                     {

                         appointments.Add(new ScheduleAppointment() { Subject = sc.BookingName, AppointmentBackground = Brushes.Blue, StartTime = sc.starting, EndTime = sc.ending, Notes = sc.ScheduleInstructorID.ToString(),ReadOnly=true });

                     }
                     else
                     {
                         appointments.Add(new ScheduleAppointment() { Subject = sc.BookingName, AppointmentBackground = Brushes.Green, StartTime = sc.starting, EndTime = sc.ending, Notes = sc.ScheduleInstructorID.ToString() ,ReadOnly=true});

                     }

                 }

                 studentScheduler.Appointments = appointments;
             }
             catch (Exception)
             {

                 MessageBox.Show("An error occured while trying to load the schedule", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                
             }
           



        }

        //private void modeDayBtn_Click(object sender, RoutedEventArgs e)
        //{             
        //     scheduler1.SelectedDate = DateTime.Today;
        //     scheduler1.Mode = Mode.Day;
        //     scheduler1.NextPage();
        //     scheduler1.PrevPage();
        //     modeDayBtn.Background = new SolidColorBrush(Colors.LightSkyBlue);
        //     modeWeekBtn.ClearValue(Button.BackgroundProperty);           
        //}

        //private void modeWeekBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    scheduler1.Mode = Mode.Week;
        //    scheduler1.NextPage();
        //    scheduler1.PrevPage();
        //    modeWeekBtn.Background = new SolidColorBrush(Colors.LightSkyBlue);
        //    modeDayBtn.ClearValue(Button.BackgroundProperty);
          
        //}

        //private void prevBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbInstructorName.SelectedItem!=null)
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
        //            MessageBox.Show("Please select the instructor first before you can navigate!", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            
        //        }
              

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void nextBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbInstructorName.SelectedItem!=null)
        //        {
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
        //            MessageBox.Show("Please select the instructor first before you can navigate!", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            
        //        }
              

        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("An error occured while trying to load the schedule!", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            
        //    }

        //}

        //private void cmbInstructorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    LoadScheduler();
        //}

        //private void scheduler1_OnEventDoubleClick(object sender, Event e)
        //{
        //    if ( Convert.ToInt16(e.PersonName)==2)
        //    {
        //        if (scheduler1.Mode == Mode.Week)
        //        {
        //            lstTimeSlots4.ItemsSource = null;
        //            wzdSchedule.CurrentPage = IntroPage;
        //            cmbTimes4.ItemsSource = DbAccess.GetTimeSlots(3, 31);
        //            totalLessons = 1;
        //            stndScheduleID = Convert.ToInt16(e.Description);
        //            wndScheduler.Show();
        //        }
        //    }
        //    else
        //    {
        //        if (scheduler1.Mode == Mode.Week)
        //        {
        //               cmbTimes.ItemsSource = DbAccess.GetHourTimeSlots();
        //               wzdSchedule2.CurrentPage = Page5;
        //              // selectedInstuctorID = (int)cmbInstructor.SelectedValue;
                        
        //                totalRoadTests = 1;
        //                lblRoadTest2.Content = totalRoadTests + " Road Tests to be Scheduled";
        //                stndScheduleID = Convert.ToInt16(e.Description);
        //                lstTimeSlots1.ItemsSource=null;
        //                wndScheduler2.Show();

        //        }
                
        //    }
           
           
        //}

        //private void wzdSchedule_Finish(object sender, RoutedEventArgs e)
        //{
        //    var val = lessonsSelected.FirstOrDefault();
        //    DbInsert.RescheduleLesson(stndScheduleID, val.Date, val.slottedTime);
        //    lstSelectedTimeSlots4.ItemsSource = null;
        //    totalLessons = 0;
        //    lessonsSelected.Clear();
        //    wndScheduler.Close();
        //    LoadScheduler();
        //}

        //private void wzdSchedule_Cancel(object sender, RoutedEventArgs e)
        //{
        //    roadTestSelected.Clear();
        //    lessonsSelected.Clear();
        //    wndScheduler.Close();
        //}

        //private void wzdSchedule_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        //{
        //    if (wzdSchedule.CurrentPage == IntroPage)
        //    {
        //        if (totalLessons != 0)
        //        {
        //            //totalLessons = lstPendingBookings.SelectedItems.Count;
        //            lblLesson2.Content = totalLessons + " Lessons to be rescheduled";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select lessons to reschedule", " NO LESSON SELECTED", MessageBoxButton.OK, MessageBoxImage.Hand);
        //            e.Cancel = true;
        //        }

        //    }
        //    else
        //    {
        //        if (lessonsSelected.Count() != totalLessons)
        //        {
        //            MessageBox.Show("Some lessons have not been rescheduled", "LESSON UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
        //            e.Cancel = true;
        //        }
        //    }
        //}

        //private void cmbTimes4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbTimes4.SelectedItem != null)
        //        {
        //            //get the selected time string to display together with the date
        //            var selectedItem = cmbTimes4.SelectedItem as DayTimeSlot;
        //            var timeSelected = selectedItem.TimeSlot;
        //            var slotID = selectedItem.ID;

        //            //assign selected time to each date
        //            var listOfScheduledTimes = LoadScheduleDates(Convert.ToInt16(cmbInstructorName.SelectedValue), Convert.ToInt32(cmbTimes4.SelectedValue));
        //            foreach (var ti in listOfScheduledTimes)
        //            {
        //                ti.Time = timeSelected;
        //                ti.slottedTime = slotID;
        //            }

        //            lessons = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
        //            lstTimeSlots4.ItemsSource = lessons;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }
        //}

        //private void btnWzdSchedAdd4_Click(object sender, RoutedEventArgs e)
        //{
        //    var counter = totalLessons;

        //    if (totalLessons > lessonsSelected.Count())
        //    {
        //        var toRemove = lstTimeSlots4.SelectedItems;
        //        List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();

        //        foreach (var item in toRemove)
        //        {
        //            toAdd.Add((InstructorLearnerSchedule)item);
        //            var rmv = item as InstructorLearnerSchedule;

        //        }

        //        if (totalLessons >= lstTimeSlots4.SelectedItems.Count)
        //        {
        //            foreach (var mk in toAdd)
        //            {
        //                if (totalLessons > lessonsSelected.Count())
        //                {
        //                    if (!lessonsSelected.Where(q => q.slottedTime == mk.slottedTime && q.Date == mk.Date).Any())
        //                    {
        //                        lessons.Remove(mk);
        //                        lessonsSelected.Add(mk);
        //                        counter -= 1;
        //                        lblLesson2.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";

        //                    }
        //                }

        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Too many lessons have been selected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
        //            lblLesson2.Content = totalLessons + " Lessons to be scheduled";
        //            lessonsSelected.Clear();
        //            cmbTimes4.SelectedValue = -1;
        //        }


        //        if (!lessonsSelected.Any())
        //        {
        //            lstSelectedTimeSlots4.ItemsSource = lessonsSelected;
        //            lstTimeSlots4.ItemsSource = lessonsSelected;
        //        }
        //        else
        //        {
        //            lstSelectedTimeSlots4.ItemsSource = lessonsSelected;
        //        }



        //    }
        //    else
        //    {
        //        MessageBox.Show("All lessons have been scheduled for this invoice", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}

        //private void btnWzdSchedRem4_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lstSelectedTimeSlots4.SelectedItem != null)
        //    {
        //        var removeLssn = lstSelectedTimeSlots4.SelectedItem;
        //        lessonsSelected.Remove((InstructorLearnerSchedule)removeLssn);
        //        lessons.Add((InstructorLearnerSchedule)removeLssn);
        //        var ordered = lessons.OrderBy(o => o.Date).ToList();
        //        lessons.Clear();
        //        lessons = new ObservableCollection<InstructorLearnerSchedule>(ordered);
        //        lstTimeSlots4.ItemsSource = lessons;
        //        lblLesson2.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";
        //    }
        //}

        //private void LastPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var lessonBkn = lessonsSelected.ToList();
        //    dtgLessonSchedule.ItemsSource = lessonBkn;
        //}

        //List<InstructorLearnerSchedule> LoadScheduleDates(int instructorID, int timeSlot)
        //{
        //    List<DateTime> allDates = new List<DateTime>();
        //    DateTime begin = DateTime.Today;
        //    DateTime end = DateTime.Today.AddDays(31);

        //    for (DateTime date = begin; date <= end; date = date.AddDays(1))
        //    {
        //        allDates.Add(date);
        //    }

        //    var theDates = allDates;
        //    var schedules = DbAccess.GetInstructorAvailableDate(instructorID, timeSlot);
        //    var scheduleDates = theDates.AsEnumerable().Select(x => new ScheduleDate()
        //    {
        //        Date = String.Format("{0:dd/MM/yyyy H:mm:ss}", x.Date),
        //        TheDate = Convert.ToDateTime(x.Date, new CultureInfo("en-US")),
        //        Day = x.DayOfWeek.ToString()

        //    });
        //    var schedulz = schedules.AsEnumerable().Select(y => new ScheduleInstructor()
        //    {
        //        Date = y.Date,
        //        //DateStr = y.Date.ToString(),
        //        InstructorID = y.InstructorID,
        //        StudentID = y.StudentID,
        //        TimeSlotID = y.TimeSlotID,
        //        AffectedHourSlot = y.AffectedHourSlot
        //    });


        //    var theSchedule = from o in scheduleDates
        //                      from od in schedulz
        //                      .Where(details => o.TheDate == details.Date)
        //                      .DefaultIfEmpty()
        //                      select new InstructorLearnerSchedule()
        //                      {
        //                          Date = o.TheDate,
        //                          Day = o.Day,
        //                          StudentID = od == null ? null : od.StudentID,
        //                          InstructorID = od == null ? null : od.InstructorID,
        //                      };

        //    var ii = theSchedule.ToList();
        //    return theSchedule.Where(s => s.InstructorID == null && s.StudentID == null).ToList();

        //}

        //void LoadScheduler() 
        //{
        //    try
        //    {
        //        scheduler1.SelectedDate = DateTime.Today;
        //        scheduler1.Events.Clear();
        //        var schedule = DbAccess.GetInstructorSchedule(Convert.ToInt16(cmbInstructorName.SelectedValue));
        //        foreach (var sc in schedule)
        //        {
        //            if (sc.BookingTpye == 1)
        //            {

        //                scheduler1.AddEvent(new Event() { Subject = sc.BookingName, Color = Brushes.Blue, Start = sc.starting, End = sc.ending, Description = sc.ScheduleInstructorID.ToString(),PersonName=sc.BookingTpye.ToString() });

        //            }
        //            else
        //            {
        //                scheduler1.AddEvent(new Event() { Subject = sc.BookingName, Color = Brushes.Yellow, Start = sc.starting, End = sc.ending, Description = sc.ScheduleInstructorID.ToString(),PersonName= sc.BookingTpye.ToString() });

        //            }

        //        }
        //        scheduler1.NextPage();
        //        scheduler1.PrevPage();
        //        modeDayBtn.Visibility = System.Windows.Visibility.Visible;
        //        modeWeekBtn.Visibility = System.Windows.Visibility.Visible;
        //        modeWeekBtn.Background = new SolidColorBrush(Colors.LightSkyBlue);
        //    }
        //    catch (Exception)
        //    {

        //        MessageBox.Show("An error occured while trying to load the schedule", "SYSTEM ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
        //        scheduler1.Events.Clear();
        //    }
           
        //}

        //List<InstructorLearnerSchedule> LoadScheduleDates2(int instructorID, byte timeSlot)
        //{
        //    List<DateTime> allDates = new List<DateTime>();
        //    DateTime begin = DateTime.Today;
        //    DateTime end = DateTime.Today.AddDays(31);

        //    for (DateTime date = begin; date <= end; date = date.AddDays(1))
        //    {
        //        allDates.Add(date);
        //    }

        //    var theDates = allDates;
        //    var schedules = DbAccess.GetInstructorAvailableBookingDate(instructorID, timeSlot);
        //    var scheduleDates = theDates.AsEnumerable().Select(x => new ScheduleDate()
        //    {
        //        Date = String.Format("{0:dd/MM/yyyy H:mm:ss}", x.Date),
        //        TheDate = Convert.ToDateTime(x.Date, new CultureInfo("en-US")),
        //        Day = x.DayOfWeek.ToString()

        //    });
        //    var schedulz = schedules.AsEnumerable().Select(y => new ScheduleInstructor()
        //    {
        //        Date = y.Date,
        //        //DateStr = y.Date.ToString(),
        //        InstructorID = y.InstructorID,
        //        StudentID = y.StudentID,
        //        TimeSlotID = y.TimeSlotID
        //    });
        //    var gg = schedulz.ToList();
        //    var yy = scheduleDates;


        //    var theSchedule = from o in scheduleDates
        //                      from od in schedulz
        //                      .Where(details => o.TheDate == details.Date)
        //                      .DefaultIfEmpty()
        //                      select new InstructorLearnerSchedule()
        //                      {
        //                          Date = o.TheDate,
        //                          Day = o.Day,
        //                          StudentID = od == null ? null : od.StudentID,
        //                          InstructorID = od == null ? null : od.InstructorID,
        //                      };

        //    var ii = theSchedule.ToList();

        //    return theSchedule.Where(s => s.InstructorID == null && s.StudentID == null).ToList();

        //}

        //private void wzdSchedule2_Finish(object sender, RoutedEventArgs e)
        //{
        //    var daySlot1 = DbAccess.GetDaySlotsName1(Convert.ToByte(cmbTimes.SelectedValue));
        //    var daySlot2 = DbAccess.GetDaySlotsName2(Convert.ToByte(cmbTimes.SelectedValue));
        //    InstructorLearnerSchedule scheduleItem1 = new InstructorLearnerSchedule()
        //    {
        //        InstructorID = Convert.ToInt16( cmbInstructorName.SelectedValue),
        //        StudentID = Convert.ToInt32(stndScheduleID),
        //        BookingTpye = 1,
        //        BookingName = "Road Test",
        //        HourSlotID = Convert.ToByte(cmbTimes.SelectedValue)

        //    };
        //    InstructorLearnerSchedule scheduleItem2 = new InstructorLearnerSchedule()
        //    {
        //        InstructorID = Convert.ToInt16(cmbInstructorName.SelectedValue),
        //        StudentID = Convert.ToInt32(stndScheduleID),
        //        BookingTpye = 1,
        //        BookingName = "Road Test",
        //        HourSlotID=  Convert.ToByte(cmbTimes.SelectedValue)

        //    };
        //    InstructorLearnerSchedule scheduleItem3 = new InstructorLearnerSchedule()
        //    {
        //        InstructorID = Convert.ToInt16(cmbInstructorName.SelectedValue),
        //        StudentID = Convert.ToInt32(stndScheduleID)

        //    };
        //    foreach (var item in roadTestSelected.ToList())
        //    {
        //        var roadTest = (InstructorLearnerSchedule)item;
        //        var slots = DbAccess.GetDaySlotsFromHourSlot(roadTest.slottedTime);
        //        scheduleItem1.slottedTime = (int)slots.DayTimeSlotID1;
        //        scheduleItem2.slottedTime = (int)slots.DayTimeSlotID1 + 1;
        //        scheduleItem1.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
        //        scheduleItem2.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
        //        scheduleItem1.Time = daySlot1;
        //        scheduleItem2.Time = daySlot2;
        //        studentBookings.Add(scheduleItem1);
        //        studentBookings.Add(scheduleItem2);

        //    }
        //    var test = studentBookings.FirstOrDefault();
        //    DbInsert.RescheduleRoadTest(stndScheduleID, test.Date, test.slottedTime,test.HourSlotID);            
        //    roadTestSelected.Clear();
        //    totalRoadTests = 0;
        //    wndScheduler2.Close();
        //    studentBookings.Clear();
        //    LoadScheduler();
           
        //}

        //private void wzdSchedule2_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        //{
        //    if (wzdSchedule.CurrentPage == Page5)
        //    {
        //        if (totalRoadTests != 0)
        //        {
        //          lblRoadTest2.Content = totalRoadTests + " Road Tests to be rescheduled";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select item to reschedule", " NOTHING SELECTED", MessageBoxButton.OK, MessageBoxImage.Hand);
        //            e.Cancel = true;
        //        }

        //    }
        //    else
        //    {
        //        if (roadTestSelected.Count() != totalRoadTests)
        //        {
        //            MessageBox.Show("Some road tests have not been rescheduled", "Road Test UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
        //            e.Cancel = true;
        //        }
        //    }
           
        //}

        //private void wzdSchedule2_Cancel(object sender, RoutedEventArgs e)
        //{
        //    roadTestSelected.Clear();
        //    lessonsSelected.Clear();
        //    wndScheduler2.Close();
        //}

        //private void cmbTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbTimes.SelectedItem != null)
        //        {
        //            var selectedItem = cmbTimes.SelectedItem as RoadTestTimeSlot;
        //            var timeSelected = selectedItem.Time;
        //            var slotID = selectedItem.ID;

        //            //assign selected time to each date
        //            var listOfScheduledTimes = LoadScheduleDates2(Convert.ToInt16( cmbInstructorName.SelectedValue), (byte)cmbTimes.SelectedValue);
        //            foreach (var ti in listOfScheduledTimes)
        //            {
        //                ti.HourTime = timeSelected;
        //                ti.slottedTime = slotID;
        //            }

        //            roadTests = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
        //            lstTimeSlots1.ItemsSource = roadTests;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }
        //}

        //private void btnWzdSchedAdd1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (totalRoadTests > roadTestSelected.Count())
        //    {
        //        var toRemove = lstTimeSlots1.SelectedItems;
        //        List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();
        //        foreach (var item in toRemove)
        //        {
        //            toAdd.Add((InstructorLearnerSchedule)item);
        //            var rmv = item as InstructorLearnerSchedule;

        //        }
        //        if (totalRoadTests >= lstTimeSlots1.SelectedItems.Count)
        //        {
        //            foreach (var mk in toAdd)
        //            {
        //                if (totalRoadTests > roadTestSelected.Count())
        //                {
        //                    if (!roadTestSelected.Where(r => r.HourTime == mk.HourTime && r.Date == mk.Date).Any())
        //                    {
        //                        roadTests.Remove(mk);
        //                        roadTestSelected.Add(mk);
        //                        lblRoadTest2.Content = totalRoadTests - roadTestSelected.Count() + " Road Tests to be rescheduled";

        //                    }
        //                }

        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Too many road tests have been reselected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
        //            lblRoadTest2.Content = totalRoadTests + " Road Tests to be rescheduled";
        //            roadTestSelected.Clear();
        //            cmbTimes.SelectedValue = 0;
        //        }
        //        foreach (var bkn in roadTestSelected)
        //        {
        //            if (bkn.BookingTpye == null)
        //            {
        //                bkn.BookingTpye = 1;
        //            }
        //        }

        //        if (!roadTestSelected.Any())
        //        {
        //            lstSelectedTimeSlots1.ItemsSource = roadTestSelected;
        //            lstTimeSlots1.ItemsSource = roadTestSelected;
        //        }
        //        else
        //        {
        //            lstSelectedTimeSlots1.ItemsSource = roadTestSelected;
        //        }
                              
        //    }
        //    else
        //    {
        //        MessageBox.Show("All road tests have been rescheduled", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
             
            
        //}

        //private void btnWzdSchedRem1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lstSelectedTimeSlots1.SelectedItem != null)
        //    {
        //        var removeRdTest = lstSelectedTimeSlots1.SelectedItem;
        //        roadTestSelected.Remove((InstructorLearnerSchedule)removeRdTest);
        //        roadTests.Add((InstructorLearnerSchedule)removeRdTest);
        //        var ordered = roadTests.OrderBy(o => o.Date).ToList();
        //        roadTests.Clear();
        //        roadTests = new ObservableCollection<InstructorLearnerSchedule>(ordered);
        //        lstTimeSlots1.ItemsSource = roadTests;
        //        lblRoadTest2.Content = totalRoadTests - roadTestSelected.Count + " Road Tests to be rescheduled";
        //    }

        //}

        //private void LastPage3_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var roadTestBkn = roadTestSelected.ToList();
        //    dtgRoadTest.ItemsSource = roadTestBkn;
        //}

       

    }
}
