using BUL;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Printing;
using System.IO;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for NewSaler.xaml
    /// </summary>
    public partial class NewSaler : UserControl
    {
        int TotalBookings, selectedInstuctorID, totalLessons, totalRoadTests;
        string invoiceNumber;
        decimal itemCost;
        ObservableCollection<InstructorLearnerSchedule> studentBookings = new ObservableCollection<InstructorLearnerSchedule>();
        ObservableCollection<InstructorLearnerSchedule> roadTests = new ObservableCollection<InstructorLearnerSchedule>();
        ObservableCollection<InstructorLearnerSchedule> roadTestSelected = new ObservableCollection<InstructorLearnerSchedule>();
        ObservableCollection<InstructorLearnerSchedule> lessons = new ObservableCollection<InstructorLearnerSchedule>();
        ObservableCollection<InstructorLearnerSchedule> lessonsSelected = new ObservableCollection<InstructorLearnerSchedule>();

        List<InstructorLearnerSchedule> learnerInstructSched = new List<InstructorLearnerSchedule>();

        ListBox dragSource = null;
        List<SaleDetails> theSales = new List<SaleDetails>();
        List<DailyLog> log = new List<DailyLog>();
        List<InstructorLog> logr = new List<InstructorLog>();

        public NewSaler()
        {
            InitializeComponent();
            cmbCustomer.ItemsSource = DbAccess.GetAllCustomers();
            cmbDescription.ItemsSource = DbAccess.GetAllSaleItems();
            dtpDate.SelectedDate = DateTime.Today;
            cmbInstructor.ItemsSource = DbAccess.GetAllInstructors();
            cmbSupervisor.ItemsSource = DbAccess.GetAllSupervisors();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dtgSales.SelectedItem != null)
            {
                var toRemove = dtgSales.SelectedItem as SaleDetails;
                var total = txtTotal.Text == null ? 0 : Convert.ToDecimal(txtTotal.Text.Substring(1));
                txtTotal.Text = (total - toRemove.Cost).ToString();
                theSales.Remove(toRemove);
                LoadDataGrid();

                //disasble the cmbInstructor combobox if there are no sales
                if (!theSales.Any())
                {
                    lblIsAnySales.Content = false;
                }
            }
            else
            {
                MessageBox.Show("Please select sale item to remove.", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }


        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            if (dtpDate.SelectedDate ==null)
            {
                MessageBox.Show("Date missing", "DATE MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Customer Name missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else if (cmbDescription.SelectedValue == null)
            {
                MessageBox.Show(" Item Description missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else if (txtQuantity.Text == null || txtQuantity.Text == string.Empty || txtQuantity.Text == "Enter the item quantity")
            {
                MessageBox.Show("Quantity missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else
            {
                var amnt = ExtractNumber(txtAmount.Text);
                var qty = ExtractNumber(txtQuantity.Text);

                SaleDetails salesDetail = new SaleDetails()
                {
                    CustomerName = cmbCustomer.Text,
                    CustomerID = Convert.ToInt32(cmbCustomer.SelectedValue),
                    Amount = Convert.ToDecimal(amnt) * (decimal).01,                                              //decimal.Parse(txtAmount.Text.Substring(1)),
                    Quantity = decimal.Parse(txtQuantity.Text),
                    CashierID = Globals.LogInID,
                    Description = cmbDescription.Text,
                    Cost = Convert.ToDecimal(amnt) * Convert.ToDecimal(qty) * (decimal).01, //decimal.Parse(txtAmount.Text.Substring(1)) * decimal.Parse(txtQuantity.Text),
                    Date =Convert.ToDateTime(dtpDate.SelectedDate),          // DateTime.Today,
                    productID = Convert.ToInt32(cmbDescription.SelectedValue),
                    productTypeID = DbAccess.GetProductTypeID(Convert.ToInt32(cmbDescription.SelectedValue)),
                    Discount = 0
                };

                theSales.Add(salesDetail);
                LoadDataGrid();

                TotalBookings += Convert.ToInt32(txtQuantity.Text);
                var total = txtTotal.Text == null ? 0 : Convert.ToDecimal(ExtractNumber(txtTotal.Text));  // Convert.ToDecimal(txtTotal.Text.Substring(1));
                txtTotal.Text = (total * (decimal).01 + Convert.ToDecimal(ExtractNumber(txtCost.Text)) * (decimal).01).ToString();     //(total + Convert.ToDecimal(txtCost.Text.Substring(1))).ToString();
                itemCost = Convert.ToDecimal(ExtractNumber(txtCost.Text));                                  // Convert.ToDecimal(txtCost.Text.Substring(1));

                txtAmount.Text = "0.00";
                cmbDescription.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtCost.Text = string.Empty;

                //enasble the cmbInstructor combobox if there are sales
                if (theSales.Any())
                {
                    lblIsAnySales.Content = true;
                }

            }
        }

        void LoadDataGrid()
        {
            dtgSales.ItemsSource = null;
            dtgSales.ItemsSource = theSales;


        }

        List<InstructorLog> LoadGridData1()
        {
            for (int i = 0; i < 365; i++)
            {
                InstructorLog dayLog = new InstructorLog();
                logr.Add(dayLog);
            }
            return logr;
        }

        private void cmbCustomer_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void txtQuantity_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtDescription_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtAmount.Text = DbAccess.GetSaleSaleItem(Convert.ToInt32(cmbDescription.SelectedValue)).ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            bool isNum = txtQuantity.Text.All(c => Char.IsNumber(c));

            if (txtAmount.Text != string.Empty)
            {
                if (txtQuantity.Text != string.Empty && isNum == true)
                {
                    var quantity = txtQuantity.Text == string.Empty ? 0 : Convert.ToDecimal(txtQuantity.Text);
                    txtCost.Text = (Convert.ToDecimal(ExtractNumber(txtAmount.Text)) * (decimal).01 * quantity).ToString(); //(Convert.ToDecimal(txtAmount.Text.TrimStart('$')) * quantity).ToString();
                }
                else
                {
                    txtCost.Text = string.Empty;
                }

            }
            else
            {
                txtCost.Text = string.Empty;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (theSales.Any())
            {
                //take the whole number part
                var wholeNum = ExtractNumber(txtTotal.Text.Split('.').First());
                var wholeNum1 = ExtractNumber(txtPaid.Text.Split('.').First());
                //take the decimal part
                var decimalPart = ExtractNumber(txtTotal.Text.Split('.').Last());
                var decimalPart1 = ExtractNumber(txtPaid.Text.Split('.').Last());

                var totalCost = Convert.ToDecimal(wholeNum) + (Convert.ToDecimal(decimalPart));
                var paid = Convert.ToDecimal(wholeNum1) + (Convert.ToDecimal(decimalPart1));

                if (paid < totalCost)
                {
                    MessageBox.Show("Amount paid cannot be less than the total amount", "SHORT FALL", MessageBoxButton.OK, MessageBoxImage.Hand);

                }
                else if (txtPaid.Text == null || txtPaid.Text == string.Empty)
                {
                    MessageBox.Show("Amount Paid", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

                }
                else if (!studentBookings.Any())
                {
                    MessageBox.Show("Invoice items have not been scheduled", "SCHEDULE MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {

                    //  DbInsert.InsertInvoice(theSales, studentBookings.ToList());
                    var bookings = studentBookings.OrderBy(o => o.Date).ToList();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
                        {
                            //save sale data
                            Invoice invo = new Invoice();
                            InvoiceLineItem lineItem = new InvoiceLineItem();

                            //add invoice data
                            SaleDetails saleInvoiceData = theSales.FirstOrDefault();
                            invo.CashierID = saleInvoiceData.CashierID;
                            invo.CustomerID = saleInvoiceData.CustomerID;
                            invo.Date = saleInvoiceData.Date;
                            invo.Cancelled = false;
                            invo.CancelledBy = 0;
                            invo.CancellationReason = string.Empty;
                            context.Invoices.Add(invo);
                            context.SaveChanges();
                            var saleDataID = context.Invoices.OrderByDescending(o => o.ID).Select(i => i.ID).First();
                            invoiceNumber = "R" + DateTime.Now.Year.ToString() + saleDataID;
                            var updateInvo = context.Invoices.Where(s => s.ID == saleDataID).Single();
                            updateInvo.InvoiceNumber = invoiceNumber;
                            context.SaveChanges();

                            foreach (var sale in theSales)
                            {

                                //add InvoiceLineItems
                                lineItem.InvoiceID = invo.ID;
                                lineItem.ProductID = sale.productID;
                                lineItem.Quantity = (int)sale.Quantity;
                                lineItem.Description = sale.Description;
                                lineItem.Amount = sale.Amount;
                                lineItem.Discount = sale.Discount;
                                context.InvoiceLineItems.Add(lineItem);
                                context.SaveChanges();
                            }


                            // save schedule data
                            ScheduleInstructor instructorLearnerSched = new ScheduleInstructor();
                            foreach (var shd in bookings)
                            {
                                instructorLearnerSched.BookingType = shd.BookingTpye;
                                instructorLearnerSched.Date = shd.Date;
                                instructorLearnerSched.InstructorID = shd.InstructorID;
                                instructorLearnerSched.TimeSlotID = shd.slottedTime;
                                instructorLearnerSched.StudentID = shd.StudentID;
                                instructorLearnerSched.AffectedHourSlot = shd.HourSlotID;
                                instructorLearnerSched.Cancelled = false;
                                instructorLearnerSched.Completed = false;
                                instructorLearnerSched.Pending = false;
                                instructorLearnerSched.NoShow = false;
                                instructorLearnerSched.UserID = Globals.LogInID;
                                instructorLearnerSched.InvoiceID = invo.ID;
                                context.ScheduleInstructors.Add(instructorLearnerSched);
                                context.SaveChanges();
                            }

                            PrintInvoice();
                        }
                        scope.Complete();

                    }
                    var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                    NewSaler newSale = new NewSaler();
                    mainWindow.grdMain.Children.Clear();
                    mainWindow.grdMain.Children.Add(newSale);

                }


            }
            else
            {
                MessageBox.Show("Cannot save data because no sale has been made.", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }



        }

        private void txtQuantity_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (txtAmount.Text != string.Empty)
            {
                if (txtQuantity.Text != string.Empty)
                {
                    var quantity = txtQuantity.Text == string.Empty ? 0 : Convert.ToDecimal(txtQuantity.Text);
                    txtCost.Text = (Convert.ToDecimal(txtAmount.Text.TrimStart('$')) * quantity).ToString();
                }
                else
                {
                    txtCost.Text = string.Empty;
                }

            }
            else
            {
                txtCost.Text = string.Empty;
            }
        }

        private void txtQuantity_KeyUp_1(object sender, KeyEventArgs e)
        {

        }

        private void txtAmount_KeyUp(object sender, KeyEventArgs e)
        {
            bool isNum = txtAmount.Text.All(c => Char.IsNumber(c));

            if (txtAmount.Text != string.Empty)
            {
                if (txtAmount.Text != string.Empty && isNum == true)
                {
                    var quantity = txtQuantity.Text == string.Empty ? 0 : Convert.ToDecimal(txtQuantity.Text);
                    txtCost.Text = (Convert.ToDecimal(txtAmount.Text.TrimStart('$')) * quantity).ToString();
                }
                else
                {
                    txtCost.Text = string.Empty;
                }

            }
            else
            {
                txtCost.Text = string.Empty;
            }
        }

        private void txtPaid_KeyUp(object sender, KeyEventArgs e)
        {

            decimal payable = 0;
            bool isNum = txtAmount.Text.All(c => Char.IsNumber(c));

            var amntPayable = ExtractNumber(txtTotal.Text);
            if (txtPaid.Text != string.Empty)
            {
                payable = (decimal)txtPaid2.Value;

                if (txtPaid.Text != string.Empty && isNum == true)
                {
                    var totalPayable = txtTotal.Text == null ? 0 : Convert.ToDecimal(amntPayable) * (decimal).01; //Convert.ToDecimal(txtTotal.Text.Substring(1));
                    if (txtPaid.Text != string.Empty)
                    {

                        if (payable >= totalPayable)                         //(txtPaid.Text.TrimStart('$'))>= totalPayable)
                        {
                            txtChange.Text = (payable - totalPayable).ToString();

                        }
                        else
                        {
                            txtChange.Text = string.Empty;
                        }
                    }

                }

            }
            else
            {
                txtChange.Text = string.Empty;
            }

            txtPaid1.Visibility = System.Windows.Visibility.Visible;
        }

        private void txtChange1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPaid1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void WatermarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadGridData();
            //dtgInstructorData.ItemsSource = LoadTime();
            //popInstructor.IsOpen = true;
            //  LoadGridData1();

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

                var time = String.Format("{0:t}", span);
                tym.Time = time.Substring(0, 5);
                TimeSpan spanner = TimeSpan.FromMinutes(30);
                span = span.Add(spanner);
            }
            return log;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bthnSSX_Click(object sender, RoutedEventArgs e)
        {
            //Invoice invo = new Invoice()
            //{
            //     Amount=100,
            //     Date=DateTime.Today.AddDays(-20),
            //     Description= "blah blah blah"
            //};
            //  DbInsert.InsertInvoice(invo);
        }

        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            popNewCustomer.IsOpen = true;

        }

        private void cmbInstructor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (theSales.Any())
            {
                if (cmbInstructor.SelectedValue != null)
                {
                    if (theSales.Where(c => c.productTypeID == 1).Any() && theSales.Where(d => d.productTypeID == 2).Any())
                    {
                        cmbTimes3.ItemsSource = DbAccess.GetTimeSlots(3, 31);
                        cmbTimes2.ItemsSource = DbAccess.GetHourTimeSlots();
                        selectedInstuctorID = (int)cmbInstructor.SelectedValue;
                        //get all lessons
                        totalLessons = (int)theSales.Where(p => p.productTypeID == 2).Select(q => q.Quantity).Sum();
                        lblLesson.Content = totalLessons + " Lessons to be Scheduled";
                        //get all road tests
                        totalRoadTests = (int)theSales.Where(p => p.productTypeID == 1).Select(q => q.Quantity).Sum();
                        lblRoadTest.Content = totalRoadTests + " Road tests to be Scheduled";
                        lstTimeSlots3.ItemsSource = null;
                        lstTimeSlots2.ItemsSource = null;
                        wzdSchedule1.CurrentPage = IntroPage1;
                        wndScheduler1.Show();
                    }
                    else if (theSales.Where(f => f.productTypeID == 1).Any())
                    {
                        cmbTimes.ItemsSource = DbAccess.GetHourTimeSlots();
                        selectedInstuctorID = (int)cmbInstructor.SelectedValue;
                        //get all road tests
                        totalRoadTests = (int)theSales.Where(p => p.productTypeID == 1).Select(q => q.Quantity).Sum();
                        lblRoadTest2.Content = totalRoadTests + " Road Tests to be Scheduled";
                        lstTimeSlots1.ItemsSource = null;
                        wndScheduler2.Show();
                    }
                    else
                    {
                        cmbTimes4.ItemsSource = DbAccess.GetTimeSlots(3, 31);
                        selectedInstuctorID = (int)cmbInstructor.SelectedValue;
                        //get all lessons
                        totalLessons = (int)theSales.Where(p => p.productTypeID == 2).Select(q => q.Quantity).Sum();
                        lblLesson2.Content = totalLessons + " Lessons to be Scheduled";
                        wndScheduler.Show();
                    }

                }
            }
            else
            {
                MessageBox.Show("There are no items to schedule", "NO SALES", MessageBoxButton.OK, MessageBoxImage.Hand);
                cmbInstructor.SelectedValue = -1;

            }



        }

        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            //    popSchedule.IsOpen = true;
            //    lstMorn.ItemsSource = DbAccess.GetTimeSlots(1,6);
            //    lstMorning.ItemsSource = LoadTime1(Convert.ToInt32(cmbInstructor.SelectedValue));
        }

        private void lstMorning_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private void lstSelectedMorning_Drop(object sender, DragEventArgs e)
        {
            //drag the selected date
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(InstructorLearnerSchedule));
            var draggedItem = (InstructorLearnerSchedule)data;

            // popSchedule.IsOpen = false;
            if (draggedItem.slottedTime <= 14)
            {
                string draggedText = (string)e.Data.GetData(DataFormats.StringFormat);
                try
                {

                    //assign instructor, time and student to the schedule item
                    draggedItem.InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
                    draggedItem.slottedTime = Convert.ToInt32(lstMorn.SelectedValue);
                    draggedItem.StudentID = Convert.ToInt32(cmbCustomer.SelectedValue);
                    draggedItem.ending = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').Last()) + ":00.000");
                    draggedItem.starting = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').First()) + ":00.000");
                    ((IList)dragSource.ItemsSource).Remove(data);

                    //check if date has been dragged before

                    if (learnerInstructSched == null)
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);

                    }
                    else if (learnerInstructSched.Where(d => d.Date == draggedItem.Date && d.slottedTime == draggedItem.slottedTime).Any())
                    {
                        // MessageBox.Show("Already Added");
                    }
                    else if (learnerInstructSched.Count > TotalBookings)
                    {
                        MessageBox.Show("Number of selected time slots exceed the total number of bookings or lessons");
                    }
                    else
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // MessageBox
            }



        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void lstMorn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // popSchedule.IsOpen = false;

            try
            {
                //get the selected time string to display together with the date
                var selectedItem = lstMorn.SelectedItem as DayTimeSlot;
                var timeSelected = selectedItem.TimeSlot;
                var slotID = selectedItem.ID;

                //assign selected time to each date
                var listOfScheduledTimes = LoadScheduleDates(selectedInstuctorID, (int)lstMorn.SelectedValue);
                foreach (var ti in listOfScheduledTimes)
                {
                    ti.Time = timeSelected;
                    ti.slottedTime = slotID;
                }
                lstMorning.ItemsSource = listOfScheduledTimes;

            }
            catch (Exception)
            {


            }

            // popSchedule.IsOpen = true;
        }

        List<InstructorLearnerSchedule> LoadScheduleDates(int instructorID, int timeSlot)
        {
            List<DateTime> allDates = new List<DateTime>();
            DateTime begin = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(31);

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            var theDates = allDates;
            var schedules = DbAccess.GetInstructorAvailableDate(instructorID, timeSlot);
            var scheduleDates = theDates.AsEnumerable().Select(x => new ScheduleDate()
            {
                Date = String.Format("{0:dd/MM/yyyy H:mm:ss}", x.Date),
                TheDate = Convert.ToDateTime(x.Date, new CultureInfo("en-US")),
                Day = x.DayOfWeek.ToString()

            });
            var schedulz = schedules.AsEnumerable().Select(y => new ScheduleInstructor()
            {
                Date = y.Date,
                //DateStr = y.Date.ToString(),
                InstructorID = y.InstructorID,
                StudentID = y.StudentID,
                TimeSlotID = y.TimeSlotID,
                AffectedHourSlot = y.AffectedHourSlot
            });
            ////var gg = schedulz.ToList();
            ////var yy = scheduleDates;


            var theSchedule = from o in scheduleDates
                              from od in schedulz
                              .Where(details => o.TheDate == details.Date)
                              .DefaultIfEmpty()
                              select new InstructorLearnerSchedule()
                              {
                                  Date = o.TheDate,
                                  Day = o.Day,
                                  StudentID = od == null ? null : od.StudentID,
                                  InstructorID = od == null ? null : od.InstructorID,
                              };

            var ii = theSchedule.ToList();
            return theSchedule.Where(s => s.InstructorID == null && s.StudentID == null).ToList();

        }

        private void btnSaveSchedule_Click(object sender, RoutedEventArgs e)
        {
            popSchedule.IsOpen = false;


            InstructorLearnerSchedule instructorLearnerSchedule = new InstructorLearnerSchedule();
            //get all morning schedules
            foreach (var item in lstSelectedMorning.Items)
            {
                var morningItem = (InstructorLearnerSchedule)item;
                studentBookings.Add(morningItem);

            }

            foreach (var item in lstSelectedAfternoon.Items)
            {
                var afterNoonItem = (InstructorLearnerSchedule)item;
                studentBookings.Add(afterNoonItem);

            }

            foreach (var item in lstSelectedEvening.Items)
            {
                var eveningItem = (InstructorLearnerSchedule)item;
                studentBookings.Add(eveningItem);

            }

            //check if there is a booking in the sale items
            if (theSales.Where(s => s.productTypeID == 1).Any())
            {

                //assign the last scheduled item as a booking
                var oo = studentBookings.OrderByDescending(o => o.Date).ThenByDescending(q => q.slottedTime).FirstOrDefault();

                //check if time is after 8 and it is not a weekend 
                if (oo.Date.Day == 1 || oo.Date.Day == 7)
                {
                    MessageBox.Show("Booking cant be on a weekend");
                }
                else if (oo.slottedTime < 7 || oo.slottedTime > 21)
                {
                    MessageBox.Show("Booking cant be on the selected time");
                }
                else
                {//create another road test and add it
                    var rdTest = oo;
                    rdTest.ending = rdTest.ending.AddMinutes(30);
                    studentBookings.Add(rdTest);
                    //oo.ending = oo.ending.AddMinutes(30);
                    oo.BookingTpye = 1;
                }

            }

            //assign the remaing schedule items as lessons
            foreach (var bkn in studentBookings)
            {
                if (bkn.BookingTpye == null)
                {
                    bkn.BookingTpye = 2;
                }
            }

            var r = studentBookings;

            //foreach (var sc in studentBookings)
            //{  //check to see if booking is a lesson or not
            //    if (sc.BookingTpye==1)
            //    {
            //        scheduler1.AddEvent(new Event() { Subject = "Road Test " + "-" + cmbInstructor.Text, Color = Brushes.Blue, Start = sc.starting, End = sc.ending });
            //    }
            //    else
            //    {
            //        scheduler1.AddEvent(new Event() { Subject = "Lesson" + "-" + cmbInstructor.Text, Color = Brushes.Yellow, Start = sc.starting, End = sc.ending });
            //    }


            //}


        }

        List<bool> CheckIfItemWasAdded()
        {
            List<bool> schudalaData = new List<bool>();
            foreach (var item in lstSelectedMorning.Items)
            {
                var schdItem = (InstructorLearnerSchedule)item;
                var schedulaz = schdItem.slottedTime == Convert.ToInt32(lstMorn.SelectedValue) ? true : false;
                schudalaData.Add(schedulaz);
            }
            return schudalaData;
        }

        private void popScheduleClose_Click(object sender, RoutedEventArgs e)
        {
            popSchedule.IsOpen = false;
        }

        private void btnScheduleNext_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    DateTime startn = DateTime.Today;
            //    DateTime endn = DateTime.Today.AddDays(31);
            //    DateTime curr = scheduler1.GetSheduledDate();
            //    if (startn >= curr || curr < endn)
            //    {
            //       scheduler1.NextPage();
            //    }

            //}
            //catch (Exception ex)
            //{
            //   MessageBox.Show(ex.Message);
            //}


        }

        private void btnScheduleNext_Click_1(object sender, RoutedEventArgs e)
        {


        }

        private void scheduler1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (theSales.Any())
            {
                popDiscount.IsOpen = true;
            }
            else
            {
                MessageBox.Show("There are no sales to discount ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }

        }

        private void btnSaveDiscount_Click(object sender, RoutedEventArgs e)
        {
            var hashedPassword = DbAccess.GetUserPasswordHashByID(Convert.ToByte(cmbSupervisor.SelectedValue));
            if (txtDiscountAmount.Text == string.Empty)
            {
                MessageBox.Show("Discount Amount missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (cmbSupervisor.SelectedValue == null)
            {
                MessageBox.Show("Supervisor missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else if (pbxPassword.Password == string.Empty)
            {
                MessageBox.Show("Password missing", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else
            {
                if (PasswordHashing.ValidatePassword(pbxPassword.Password, hashedPassword))
                {

                    //take the whole number part
                    var wholeNum = ExtractNumber(txtTotal.Text.Split('.').First());
                    var wholeNum1 = ExtractNumber(txtDiscountAmount.Text.Split('.').First());
                    //take the decimal part
                    var decimalPart = ExtractNumber(txtTotal.Text.Split('.').Last());
                    var decimalPart1 = ExtractNumber(txtDiscountAmount.Text.Split('.').Last());

                    //convert to decimal
                    var totalCost = Convert.ToDecimal(wholeNum) + (Convert.ToDecimal(decimalPart) * Convert.ToDecimal(.01));
                    var discount = Convert.ToDecimal(wholeNum1) + (Convert.ToDecimal(decimalPart1) * Convert.ToDecimal(.01));
                    txtTotal.Text = (totalCost - discount).ToString();

                    //get discount as percentage
                    var discountPercentage = (discount / totalCost) * 100;

                    //change sales data 
                    foreach (var sale in theSales)
                    {
                        //  sale.Amount = sale.Amount * discountPercentage;
                        sale.Discount = discountPercentage;
                    }


                    txtDscount.Text = Math.Round(discountPercentage, MidpointRounding.AwayFromZero).ToString() + "%";
                    pbxPassword.Password = string.Empty;   //  var salesData 
                    cmbSupervisor.Text = string.Empty;
                    popDiscount.IsOpen = false;

                }
                else
                {
                    MessageBox.Show("Password does not match", "PASSWORD MISMATCH", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
            }


        }

        public static string ExtractNumber(string original)
        {
            return new string(original.Where(c => Char.IsDigit(c)).ToArray());
        }

        private void txtPaid_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPaid.Text = string.Empty;
        }

        private void txtDiscountAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDscount.Text = string.Empty;
        }

        private void txtQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQuantity.Text = string.Empty;
        }

        private void lstAftrn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // popSchedule.IsOpen = false;
            try
            {
                //get the selected time string to display together with the date
                var selectedItem = lstAftrn.SelectedItem as DayTimeSlot;
                var timeSelected = selectedItem.TimeSlot;
                var slotID = selectedItem.ID;

                //assign selected time to each date
                var listOfScheduledTimes1 = LoadScheduleDates(selectedInstuctorID, (int)lstAftrn.SelectedValue);
                foreach (var ti in listOfScheduledTimes1)
                {
                    ti.Time = timeSelected;
                    ti.slottedTime = slotID;
                }
                lstAfternoon.ItemsSource = listOfScheduledTimes1;
            }
            catch (Exception)
            {

                // throw;
            }



        }

        private void lstEven_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //get the selected time string to display together with the date
                var selectedItem = lstEven.SelectedItem as DayTimeSlot;
                var timeSelected = selectedItem.TimeSlot;
                var slotID = selectedItem.ID;

                //assign selected time to each date
                var listOfScheduledTimes2 = LoadScheduleDates(selectedInstuctorID, (int)lstEven.SelectedValue);
                foreach (var ti in listOfScheduledTimes2)
                {
                    ti.Time = timeSelected;
                    ti.slottedTime = slotID;
                }
                lstEvening.ItemsSource = listOfScheduledTimes2;

            }
            catch (Exception)
            {

                //throw;
            }


        }

        private void lstSelectedEvening_Drop(object sender, DragEventArgs e)
        {
            // popSchedule.IsOpen = false;

            //drag the selected date
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(InstructorLearnerSchedule));
            var draggedItem = (InstructorLearnerSchedule)data;


            if (draggedItem.slottedTime > 24 && draggedItem.slottedTime <= 31)
            {
                string draggedText = (string)e.Data.GetData(DataFormats.StringFormat);
                try
                {
                    //assign instructor, time and student to the schedule item
                    draggedItem.InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
                    draggedItem.slottedTime = Convert.ToInt32(lstEven.SelectedValue);
                    draggedItem.StudentID = Convert.ToInt32(cmbCustomer.SelectedValue);
                    draggedItem.ending = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').Last()) + ":00.000");
                    draggedItem.starting = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').First()) + ":00.000");

                    ((IList)dragSource.ItemsSource).Remove(data);

                    //check if date has been dragged before

                    if (learnerInstructSched == null)
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);

                    }
                    else if (learnerInstructSched.Where(d => d.Date == draggedItem.Date && d.slottedTime == draggedItem.slottedTime).Any())
                    {
                        // MessageBox.Show("Already Added");
                    }
                    else if (learnerInstructSched.Count > TotalBookings)
                    {
                        MessageBox.Show("Number of selected time slots exceed the total number of bookings or lessons");
                    }
                    else
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // MessageBox
            }


        }

        private void lstSelectedAfternoon_Drop(object sender, DragEventArgs e)
        {

            //drag the selected date
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(InstructorLearnerSchedule));
            var draggedItem = (InstructorLearnerSchedule)data;


            if (draggedItem.slottedTime > 14 && draggedItem.slottedTime <= 24)
            {
                string draggedText = (string)e.Data.GetData(DataFormats.StringFormat);
                try
                {
                    //assign instructor, time and student to the schedule item
                    draggedItem.InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
                    draggedItem.slottedTime = Convert.ToInt32(lstAftrn.SelectedValue);
                    draggedItem.StudentID = Convert.ToInt32(cmbCustomer.SelectedValue);
                    draggedItem.ending = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').Last()) + ":00.000");
                    draggedItem.starting = Convert.ToDateTime(draggedItem.Date.ToString().Split(' ').First() + " " + (draggedItem.Time.Split('-').First()) + ":00.000");

                    ((IList)dragSource.ItemsSource).Remove(data);

                    //check if date has been dragged before

                    if (learnerInstructSched == null)
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);

                    }
                    else if (learnerInstructSched.Where(d => d.Date == draggedItem.Date && d.slottedTime == draggedItem.slottedTime).Any())
                    {
                        MessageBox.Show("Already Added");
                    }
                    else if (learnerInstructSched.Count > TotalBookings)
                    {
                        MessageBox.Show("Number of selected time slots exceed the total number of bookings or lessons");
                    }
                    else
                    {
                        learnerInstructSched.Add(draggedItem);
                        parent.Items.Add(draggedItem);
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // MessageBox
            }


        }

        private void lstSelectedAfternoon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private void lstSelectedEvening_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        List<InstructorLearnerSchedule> LoadRoadTestScheduleDates(int instructorID, int timeSlot)
        {
            List<DateTime> allDates = new List<DateTime>();
            DateTime begin = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(31);

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            var theDates = allDates;
            var schedules = DbAccess.GetInstructorAvailableDate(instructorID, timeSlot);
            var scheduleDates = theDates.AsEnumerable().Select(x => new ScheduleDate()
            {
                Date = String.Format("{0:dd/MM/yyyy H:mm:ss}", x.Date),
                TheDate = Convert.ToDateTime(x.Date, new CultureInfo("en-US")),
                Day = x.DayOfWeek.ToString()

            });
            var schedulz = schedules.AsEnumerable().Select(y => new ScheduleInstructor()
            {
                Date = y.Date,
                //DateStr = y.Date.ToString(),
                InstructorID = y.InstructorID,
                StudentID = y.StudentID,
                TimeSlotID = y.TimeSlotID
            });
            //var gg = schedulz.ToList();
            //var yy = scheduleDates;


            var theSchedule = from o in scheduleDates
                              from od in schedulz
                              .Where(details => o.TheDate == details.Date)
                              .DefaultIfEmpty()
                              select new InstructorLearnerSchedule()
                              {
                                  Date = o.TheDate,
                                  Day = o.Day,
                                  StudentID = od == null ? null : od.StudentID,
                                  InstructorID = od == null ? null : od.InstructorID,
                              };

            var ii = theSchedule.ToList();
            return theSchedule.Where(s => s.InstructorID == null && s.StudentID == null).ToList();

        }

        private void wzdSchedule_Finish(object sender, RoutedEventArgs e)
        {
            foreach (var lss in lessonsSelected.ToList())
            {
                var lesson = (InstructorLearnerSchedule)lss;
                lesson.InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
                lesson.StudentID = Convert.ToInt32(cmbCustomer.SelectedValue);
                lesson.BookingTpye = 2;
                lesson.BookingName = "Lesson";
                studentBookings.Add(lesson);
            }
            wndScheduler.Close();
            DisableWindowPart();
        }

        private void wzdSchedule2_Finish(object sender, RoutedEventArgs e)
        {
            var daySlot1 = DbAccess.GetDaySlotsName1(Convert.ToByte(cmbTimes.SelectedValue));
            var daySlot2 = DbAccess.GetDaySlotsName2(Convert.ToByte(cmbTimes.SelectedValue));
            InstructorLearnerSchedule scheduleItem1 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue),
                BookingTpye = 1,
                BookingName = "Road Test",
                HourSlotID = Convert.ToByte(cmbTimes.SelectedValue)


            };
            InstructorLearnerSchedule scheduleItem2 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue),
                BookingTpye = 1,
                BookingName = "Road Test",
                HourSlotID = Convert.ToByte(cmbTimes.SelectedValue)
            };
            InstructorLearnerSchedule scheduleItem3 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue)

            };
            foreach (var item in roadTestSelected.ToList())
            {
                var roadTest = (InstructorLearnerSchedule)item;
                var slots = DbAccess.GetDaySlotsFromHourSlot(roadTest.slottedTime);
                scheduleItem1.slottedTime = (int)slots.DayTimeSlotID1;
                scheduleItem2.slottedTime = (int)slots.DayTimeSlotID1 + 1;
                scheduleItem1.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
                scheduleItem2.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
                scheduleItem1.Time = daySlot1;
                scheduleItem2.Time = daySlot2;
                studentBookings.Add(scheduleItem1);
                studentBookings.Add(scheduleItem2);

            }

            wndScheduler2.Close();
            DisableWindowPart();
        }

        private void wzdSchedule1_Finish(object sender, RoutedEventArgs e)
        {
            var daySlot1 = DbAccess.GetDaySlotsName1(Convert.ToByte(cmbTimes2.SelectedValue));
            var daySlot2 = DbAccess.GetDaySlotsName2(Convert.ToByte(cmbTimes2.SelectedValue));
            InstructorLearnerSchedule scheduleItem1 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue),
                BookingTpye = 1,
                BookingName = "Road Test",
                HourSlotID = Convert.ToByte(cmbTimes2.SelectedValue)

            };
            InstructorLearnerSchedule scheduleItem2 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue),
                BookingTpye = 1,
                BookingName = "Road Test",
                HourSlotID = Convert.ToByte(cmbTimes2.SelectedValue)

            };
            InstructorLearnerSchedule scheduleItem3 = new InstructorLearnerSchedule()
            {
                InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue),
                StudentID = Convert.ToInt32(cmbCustomer.SelectedValue),
                BookingTpye = 1,
                BookingName = "Road Test",
                HourSlotID = Convert.ToByte(cmbTimes2.SelectedValue)

            };

            foreach (var lss in lessonsSelected.ToList())
            {
                var lesson = (InstructorLearnerSchedule)lss;
                lesson.InstructorID = Convert.ToInt16(cmbInstructor.SelectedValue);
                lesson.StudentID = Convert.ToInt32(cmbCustomer.SelectedValue);
                lesson.BookingTpye = 2;
                lesson.BookingName = "Lesson";
                studentBookings.Add(lesson);
            }

            foreach (var item in roadTestSelected.ToList())
            {
                var roadTest = (InstructorLearnerSchedule)item;
                var slots = DbAccess.GetDaySlotsFromHourSlot(roadTest.slottedTime);
                scheduleItem1.slottedTime = (int)slots.DayTimeSlotID1;
                scheduleItem2.slottedTime = (int)slots.DayTimeSlotID1 + 1;
                scheduleItem1.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
                scheduleItem2.Date = Convert.ToDateTime(item.Date.ToString().Split(' ').First());
                scheduleItem1.Time = daySlot1;
                scheduleItem2.Time = daySlot2;
                studentBookings.Add(scheduleItem1);
                studentBookings.Add(scheduleItem2);

            }
            wndScheduler1.Close();
            DisableWindowPart();
        }

        private void cmbTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTimes.SelectedItem != null)
                {
                    var selectedItem = cmbTimes.SelectedItem as RoadTestTimeSlot;
                    var timeSelected = selectedItem.Time;
                    var slotID = selectedItem.ID;

                    //assign selected time to each date
                    var listOfScheduledTimes = LoadScheduleDates2(selectedInstuctorID, (byte)cmbTimes.SelectedValue);
                    foreach (var ti in listOfScheduledTimes)
                    {
                        ti.HourTime = timeSelected;
                        ti.slottedTime = slotID;
                    }

                    roadTests = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
                    lstTimeSlots1.ItemsSource = roadTests;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        List<InstructorLearnerSchedule> LoadScheduleDates2(int instructorID, byte timeSlot)
        {
            List<DateTime> allDates = new List<DateTime>();
            DateTime begin = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(31);

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            var theDates = allDates;
            var schedules = DbAccess.GetInstructorAvailableBookingDate(instructorID, timeSlot);
            var scheduleDates = theDates.AsEnumerable().Select(x => new ScheduleDate()
            {
                Date = String.Format("{0:dd/MM/yyyy H:mm:ss}", x.Date),
                TheDate = Convert.ToDateTime(x.Date, new CultureInfo("en-US")),
                Day = x.DayOfWeek.ToString()

            });
            var schedulz = schedules.AsEnumerable().Select(y => new ScheduleInstructor()
            {
                Date = y.Date,
                //DateStr = y.Date.ToString(),
                InstructorID = y.InstructorID,
                StudentID = y.StudentID,
                TimeSlotID = y.TimeSlotID
            });
            var gg = schedulz.ToList();
            var yy = scheduleDates;


            var theSchedule = from o in scheduleDates
                              from od in schedulz
                              .Where(details => o.TheDate == details.Date)
                              .DefaultIfEmpty()
                              select new InstructorLearnerSchedule()
                              {
                                  Date = o.TheDate,
                                  Day = o.Day,
                                  StudentID = od == null ? null : od.StudentID,
                                  InstructorID = od == null ? null : od.InstructorID,
                              };

            var ii = theSchedule.ToList();

            return theSchedule.Where(s => s.InstructorID == null && s.StudentID == null).ToList();

        }

        private void btnWzdSchedAdd1_Click(object sender, RoutedEventArgs e)
        {
            if (totalRoadTests > roadTestSelected.Count())
            {
                var toRemove = lstTimeSlots1.SelectedItems;
                List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();
                foreach (var item in toRemove)
                {
                    toAdd.Add((InstructorLearnerSchedule)item);
                    var rmv = item as InstructorLearnerSchedule;

                }
                if (totalRoadTests >= lstTimeSlots1.SelectedItems.Count)
                {
                    foreach (var mk in toAdd)
                    {
                        if (totalRoadTests > roadTestSelected.Count())
                        {
                            if (!roadTestSelected.Where(r => r.HourTime == mk.HourTime && r.Date == mk.Date).Any())
                            {
                                roadTests.Remove(mk);
                                roadTestSelected.Add(mk);
                                lblRoadTest2.Content = totalRoadTests - roadTestSelected.Count() + " Road Tests to be scheduled";

                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Too many road tests have been selected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
                    lblRoadTest2.Content = totalRoadTests + " Road Tests to be scheduled";
                    roadTestSelected.Clear();
                    cmbTimes.SelectedValue = 0;
                }
                foreach (var bkn in roadTestSelected)
                {
                    if (bkn.BookingTpye == null)
                    {
                        bkn.BookingTpye = 1;
                    }
                }

                if (!roadTestSelected.Any())
                {
                    lstSelectedTimeSlots1.ItemsSource = roadTestSelected;
                    lstTimeSlots1.ItemsSource = roadTestSelected;
                }
                else
                {
                    lstSelectedTimeSlots1.ItemsSource = roadTestSelected;
                }


                //lstSelectedTimeSlots1.ItemsSource = roadTestSelected;
            }
            else
            {
                MessageBox.Show("All road tests have been scheduled for this invoice", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        private void btnWzdSchedRem1_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedTimeSlots1.SelectedItem != null)
            {
                var removeRdTest = lstSelectedTimeSlots1.SelectedItem;
                roadTestSelected.Remove((InstructorLearnerSchedule)removeRdTest);
                roadTests.Add((InstructorLearnerSchedule)removeRdTest);
                var ordered = roadTests.OrderBy(o => o.Date).ToList();
                roadTests.Clear();
                roadTests = new ObservableCollection<InstructorLearnerSchedule>(ordered);
                lstTimeSlots1.ItemsSource = roadTests;
                lblRoadTest2.Content = totalRoadTests - roadTestSelected.Count + " Road Tests to be scheduled";
            }

        }

        private void LastPage3_Loaded(object sender, RoutedEventArgs e)
        {
            var roadTestBkn = roadTestSelected.ToList();
            dtgRoadTest.ItemsSource = roadTestBkn;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #region PrintInvoice
        void PrintInvoice()
        {
            var printList = theSales;
            //Create flow document
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(25, 0, 0, 0);
            doc.FontSize = 12;
            //add invoice number


            //add an image
            Image img = new Image();
            img.Height = 100;
            img.Width = 100;
            var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var imagePath = System.IO.Path.Combine(startupPath, @"Icons\Rosepet.jpg");
            img.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            RichTextBox richText = new RichTextBox();
            InlineUIContainer container = new InlineUIContainer(img);
            Paragraph paragraph = new Paragraph(container);
            paragraph.TextAlignment = TextAlignment.Center;
            richText.Document.Blocks.Add(paragraph);
            doc.Blocks.Add(paragraph);

            Paragraph parago = new Paragraph(new Run("DRIVING SCHOOL\n317 Parkway Drive Victoria Falls\n   Mobile:0776625777 / 0715412222\nTel:(013)45088 \nemail:sales@rosepet.co.zw\nskype:rosepetzim"));
            parago.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(parago);

            Paragraph para = new Paragraph(new Run("Receipt #:" + invoiceNumber + "\n" + DateTime.Now.ToShortDateString() + "@" + DateTime.Now.ToShortTimeString() + "\nCustomer : " + cmbCustomer.Text));
            para.TextAlignment = TextAlignment.Left;
            doc.Blocks.Add(para);

            Table docTable = new Table();
            docTable.FontSize = 12;
            for (int i = 0; i < 3; i++)
            {
                docTable.Columns.Add(new TableColumn());
            }

            docTable.RowGroups.Add(new TableRowGroup());
            docTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow current = new TableRow();
            current = docTable.RowGroups[0].Rows[0];
            current.Cells.Add(new TableCell(new Paragraph(new Run("Product"))));
            current.Cells[0].ColumnSpan = 2;
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("Cost"))));

            int count = printList.Count;
            int counter = count - 1;
            foreach (var item in printList)
            {
                docTable.RowGroups[0].Rows.Add(new TableRow());
                current = docTable.RowGroups[0].Rows[count - (counter)];
                //Add the 
                current.Cells.Add(new TableCell(new Paragraph(new Run(item.Description.ToString()))));
                current.Cells[0].ColumnSpan = 2;
                current.Cells.Add(new TableCell(new Paragraph(new Run(item.Quantity.ToString() + "X" + " $" + item.Amount.ToString()))));
                //Add the 
                current.Cells.Add(new TableCell(new Paragraph(new Run("$" + item.Cost.ToString()))));
                counter = counter - 1;
            }

            docTable.RowGroups[0].Rows.Add(new TableRow());
            current = new TableRow();
            current = docTable.RowGroups[0].Rows[count + 1];

            RichTextBox richText2 = new RichTextBox();
            Line ruler2 = new Line { X1 = 0, X2 = 10, Y1 = 0, Y2 = 0, Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 0.5 };
            ruler2.Stretch = Stretch.Fill;

            InlineUIContainer contain1 = new InlineUIContainer(ruler2);
            Paragraph parag1 = new Paragraph(contain1);
            current.Cells.Add(new TableCell(new Paragraph(contain1)));
            current.Cells[0].ColumnSpan = 4;

            docTable.RowGroups[0].Rows.Add(new TableRow());
            current = new TableRow();
            current = docTable.RowGroups[0].Rows[count + 2];
            current.Cells.Add(new TableCell(new Paragraph(new Run("CASH"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(txtPaid.Text))));

            docTable.RowGroups[0].Rows.Add(new TableRow());
            current = new TableRow();
            current = docTable.RowGroups[0].Rows[count + 3];
            current.Cells.Add(new TableCell(new Paragraph(new Run("TOTAL"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(txtTotal.Text))));

            docTable.RowGroups[0].Rows.Add(new TableRow());
            current = new TableRow();
            current = docTable.RowGroups[0].Rows[count + 4];
            current.Cells.Add(new TableCell(new Paragraph(new Run("Discount"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(txtDscount.Text))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("(" + txtDiscountAmount.Text + ")"))));

            docTable.RowGroups[0].Rows.Add(new TableRow());
            current = new TableRow();
            current = docTable.RowGroups[0].Rows[count + 5];
            current.Cells.Add(new TableCell(new Paragraph(new Run("CHANGE"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            current.Cells.Add(new TableCell(new Paragraph(new Run(txtChange.Text))));
            doc.Blocks.Add(docTable);

            Paragraph para2 = new Paragraph(new Run("Cashier: " + DbAccess.GetUserFullNameByID(Globals.LogInID) + "\n   THANK YOU FOR CHOOSING ROSEPET"));
            para2.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(para2);

            RichTextBox richText1 = new RichTextBox();
            Line ruler1 = new Line { X1 = 0, X2 = 10, Y1 = 0, Y2 = 0, Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 2 };
            ruler1.Stretch = Stretch.Fill;

            InlineUIContainer contain = new InlineUIContainer(ruler1);
            Paragraph parag = new Paragraph(contain);
            richText1.Document.Blocks.Add(parag);
            doc.Blocks.Add(parag);

            Paragraph para1 = new Paragraph(new Run("Customer Schedule"));
            para1.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(para1);
            Table docTable1 = new Table();
            docTable1.FontSize = 12;
            for (int i = 0; i < 3; i++)
            {
                docTable1.Columns.Add(new TableColumn());
            }

            docTable1.RowGroups.Add(new TableRowGroup());
            docTable1.RowGroups[0].Rows.Add(new TableRow());
            TableRow tblRow = new TableRow();
            tblRow = docTable1.RowGroups[0].Rows[0];
            tblRow.Cells.Add(new TableCell(new Paragraph(new Run("Date"))));
            tblRow.Cells.Add(new TableCell(new Paragraph(new Run("Time"))));
            tblRow.Cells.Add(new TableCell(new Paragraph(new Run(" "))));

            int kount = studentBookings.Count;
            int kounter = kount - 1;



            var theBookingList = studentBookings.OrderBy(o => o.Date).ThenBy(o => o.slottedTime).ToList();
            foreach (var item in theBookingList.ToList())
            {
                var bookedTime = item.Time == string.Empty ? item.HourTime : item.Time;
                docTable1.RowGroups[0].Rows.Add(new TableRow());
                tblRow = docTable1.RowGroups[0].Rows[kount - (kounter)];
                //Add the 
                tblRow.Cells.Add(new TableCell(new Paragraph(new Run(item.Date.ToString().Split(' ').First().ToString()))));
                //Add the 
                tblRow.Cells.Add(new TableCell(new Paragraph(new Run(bookedTime))));
                //Add the 
                tblRow.Cells.Add(new TableCell(new Paragraph(new Run(item.BookingName.ToString()))));
                kounter = kounter - 1;
            }

            doc.Blocks.Add(docTable1);

            Paragraph para3 = new Paragraph(new Run("\nInstructor: " + cmbInstructor.Text));
            para3.TextAlignment = TextAlignment.Justify;
            doc.Blocks.Add(para3);

            IDocumentPaginatorSource idocum = doc;


            // System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            // settings.Copies = 2;        

            PrintDialog printDlg = new PrintDialog();
           // printDlg.PrintQueue.DefaultPrintTicket.CopyCount = 2;
            printDlg.PrintDocument(idocum.DocumentPaginator, "Print Receipt");
            printDlg.PrintDocument(idocum.DocumentPaginator, "Print Receipt");
            //var source = doc;
            //var range = new TextRange(source.ContentStart, source.ContentEnd);
            //var invoNumSrting = @"C:\Users\TENDAI\Desktop\pos"+invoiceNumber+".rtf";
            //using (var stream = File.Create("invoNumSrting"))
            //{
            //    range.Save(stream,DataFormats.Rtf);
            //}


            // printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), "EPSON TM-T88V Receipt5");


        }

        #endregion

        private void cmbTimes2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTimes2.SelectedItem != null)
                {
                    var selectedItem = cmbTimes2.SelectedItem as RoadTestTimeSlot;
                    var timeSelected = selectedItem.Time;
                    var slotID = selectedItem.ID;

                    //assign selected time to each date
                    var listOfScheduledTimes = LoadScheduleDates2(selectedInstuctorID, (byte)cmbTimes2.SelectedValue);
                    foreach (var ti in listOfScheduledTimes)
                    {
                        ti.HourTime = timeSelected;
                        ti.slottedTime = slotID;
                    }

                    roadTests = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
                    lstTimeSlots2.ItemsSource = roadTests;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnWzdSchedAdd2_Click(object sender, RoutedEventArgs e)
        {
            if (totalRoadTests > roadTestSelected.Count())
            {
                var toRemove = lstTimeSlots2.SelectedItems;
                List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();
                foreach (var item in toRemove)
                {
                    toAdd.Add((InstructorLearnerSchedule)item);
                    var rmv = item as InstructorLearnerSchedule;

                }

                if (totalRoadTests >= lstTimeSlots2.SelectedItems.Count)
                {
                    foreach (var mk in toAdd.ToList())
                    {
                        if (lessonsSelected.Where(t => t.slottedTime == DbAccess.GetDayTimeSlotFromHourSlot(Convert.ToByte(cmbTimes2.SelectedValue)).FirstOrDefault() && t.Date == mk.Date).Any())
                        {
                            MessageBox.Show("You have already allocated this time for a lesson", "SLOT TAKEN", MessageBoxButton.OK, MessageBoxImage.Information);
                            roadTests.Remove(mk);
                        }
                        else
                        {
                            if (totalRoadTests > roadTestSelected.Count())
                            {
                                if (!roadTestSelected.Where(r => r.HourTime == mk.HourTime && r.Date == mk.Date).Any())
                                {
                                    roadTests.Remove(mk);
                                    roadTestSelected.Add(mk);
                                    lblRoadTest.Content = totalRoadTests - roadTestSelected.Count + " Road tests to be scheduled";

                                }
                            }

                        }


                    }
                }
                else
                {
                    MessageBox.Show("Too many road tests have been selected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
                    lblRoadTest.Content = totalRoadTests + " Road tests to be scheduled";
                    roadTestSelected.Clear();
                    cmbTimes2.SelectedValue = 0;
                }

                if (!roadTestSelected.Any())
                {
                    lstSelectedTimeSlots2.ItemsSource = roadTestSelected;
                    lstTimeSlots2.ItemsSource = roadTestSelected;
                }
                else
                {
                    lstSelectedTimeSlots2.ItemsSource = roadTestSelected;
                }


                //var pp = roadTestSelected;
                //lstSelectedTimeSlots2.ItemsSource = roadTestSelected;

            }
            else
            {
                MessageBox.Show("All road tests have been scheduled.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnWzdSchedRem2_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedTimeSlots4.SelectedItem != null)
            {
                var removeRdTest = lstSelectedTimeSlots2.SelectedItem;
                roadTestSelected.Remove((InstructorLearnerSchedule)removeRdTest);
                roadTests.Add((InstructorLearnerSchedule)removeRdTest);
                var ordered = roadTests.OrderBy(o => o.Date).ToList();
                roadTests.Clear();
                roadTests = new ObservableCollection<InstructorLearnerSchedule>(ordered);
                lstTimeSlots4.ItemsSource = roadTests;
                lblRoadTest.Content = totalRoadTests - roadTestSelected.Count + " Road Tests to be scheduled";
            }

        }

        private void cmbTimes3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (cmbTimes3.SelectedItem != null)
                {
                    //get the selected time string to display together with the date
                    var selectedItem = cmbTimes3.SelectedItem as DayTimeSlot;
                    var timeSelected = selectedItem.TimeSlot;
                    var slotID = selectedItem.ID;

                    //assign selected time to each date
                    var listOfScheduledTimes = LoadScheduleDates(selectedInstuctorID, Convert.ToInt32(cmbTimes3.SelectedValue));
                    foreach (var ti in listOfScheduledTimes)
                    {
                        ti.Time = timeSelected;
                        ti.slottedTime = slotID;
                    }

                    lessons = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
                    lstTimeSlots3.ItemsSource = lessons;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnWzdSchedAdd3_Click(object sender, RoutedEventArgs e)
        {

            if (totalLessons > lessonsSelected.Count())
            {
                var toRemove = lstTimeSlots3.SelectedItems;
                List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();

                foreach (var item in toRemove)
                {
                    toAdd.Add((InstructorLearnerSchedule)item);
                    var rmv = item as InstructorLearnerSchedule;

                }

                if (totalLessons >= lstTimeSlots3.SelectedItems.Count)
                {
                    foreach (var mk in toAdd)
                    {
                        if (totalLessons > lessonsSelected.Count())
                        {
                            if (!lessonsSelected.Where(q => q.slottedTime == mk.slottedTime && q.Date == mk.Date).Any())
                            {
                                lessons.Remove(mk);
                                lessonsSelected.Add(mk);
                                lblLesson.Content = totalLessons - lessonsSelected.Count + "Lessons to be scheduled";

                            }
                        }

                    }
                }

                else
                {
                    MessageBox.Show("Too many lessons have been selected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
                    lblLesson.Content = totalLessons + " Lessons to be scheduled";
                    lessonsSelected.Clear();
                    cmbTimes3.SelectedValue = -1;
                }

                if (!lessonsSelected.Any())
                {
                    lstSelectedTimeSlots3.ItemsSource = lessonsSelected;
                    lstTimeSlots3.ItemsSource = lessonsSelected;
                }
                else
                {
                    lstSelectedTimeSlots3.ItemsSource = lessonsSelected;
                }



                // lstSelectedTimeSlots3.ItemsSource = lessonsSelected;

            }
            else
            {
                MessageBox.Show("All lessons have been scheduled for this invoice", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnWzdSchedRem3_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedTimeSlots3.SelectedItem != null)
            {
                var removeLssn = lstSelectedTimeSlots3.SelectedItem;
                lessonsSelected.Remove((InstructorLearnerSchedule)removeLssn);
                lessons.Add((InstructorLearnerSchedule)removeLssn);
                var ordered = lessons.OrderBy(o => o.Date).ToList();
                lessons.Clear();
                lessons = new ObservableCollection<InstructorLearnerSchedule>(ordered);
                lstTimeSlots3.ItemsSource = lessons;
                lblLesson.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";
            }
        }

        private void LastPage1_Loaded(object sender, RoutedEventArgs e)
        {
            var kk = roadTestSelected.ToList();
            foreach (var ts in roadTestSelected.ToList())
            {
                if (!lessonsSelected.Where(t => t.Time == ts.Time && t.Date == ts.Date).Any())
                {
                    ts.BookingTpye = 1;
                    ts.Time = ts.HourTime;
                    lessonsSelected.Add(ts);
                }

            }
            var ordered = lessonsSelected.OrderBy(o => o.Date).ThenBy(o => o.slottedTime);
            dtgFullSchedule.ItemsSource = ordered;

        }

        private void btnScheduleNext3_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    DateTime startn = DateTime.Today;
            //    DateTime endn = DateTime.Today.AddDays(31);
            //    DateTime curr = scheduler3.GetSheduledDate();
            //    if (startn < curr)
            //    {
            //        scheduler1.PrevPage();
            //    }

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}

        }

        private void btnSchedulePrev_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedTimeSlots4.SelectedItem != null)
            {
                var removeLssn = lstSelectedTimeSlots4.SelectedItem;
                lessonsSelected.Remove((InstructorLearnerSchedule)removeLssn);
                lessons.Add((InstructorLearnerSchedule)removeLssn);
                var ordered = lessons.OrderBy(o => o.Date).ToList();
                lessons.Clear();
                lessons = new ObservableCollection<InstructorLearnerSchedule>(ordered);
                lstTimeSlots4.ItemsSource = lessons;
                lblLesson2.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";
            }
        }

        private void wzdSchedule2_Cancel(object sender, RoutedEventArgs e)
        {
            roadTestSelected.Clear();
            lessonsSelected.Clear();
            ResetInstructorCombobox();
            lstTimeSlots2.ItemsSource = null;
            wndScheduler2.Close();
        }

        private void wzdSchedule1_Cancel(object sender, RoutedEventArgs e)
        {
            roadTestSelected.Clear();
            lessonsSelected.Clear();
            ResetInstructorCombobox();
            wndScheduler1.Close();
        }

        private void cmbTimes4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTimes4.SelectedItem != null)
                {
                    //get the selected time string to display together with the date
                    var selectedItem = cmbTimes4.SelectedItem as DayTimeSlot;
                    var timeSelected = selectedItem.TimeSlot;
                    var slotID = selectedItem.ID;

                    //assign selected time to each date
                    var listOfScheduledTimes = LoadScheduleDates(selectedInstuctorID, Convert.ToInt32(cmbTimes4.SelectedValue));
                    foreach (var ti in listOfScheduledTimes)
                    {
                        ti.Time = timeSelected;
                        ti.slottedTime = slotID;
                    }

                    lessons = new ObservableCollection<InstructorLearnerSchedule>(listOfScheduledTimes);
                    lstTimeSlots4.ItemsSource = lessons;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnWzdSchedAdd4_Click(object sender, RoutedEventArgs e)
        {
            var counter = totalLessons;

            if (totalLessons > lessonsSelected.Count())
            {
                var toRemove = lstTimeSlots4.SelectedItems;
                List<InstructorLearnerSchedule> toAdd = new List<InstructorLearnerSchedule>();

                foreach (var item in toRemove)
                {
                    toAdd.Add((InstructorLearnerSchedule)item);
                    var rmv = item as InstructorLearnerSchedule;

                }

                if (totalLessons >= lstTimeSlots4.SelectedItems.Count)
                {
                    foreach (var mk in toAdd)
                    {
                        if (totalLessons > lessonsSelected.Count())
                        {
                            if (!lessonsSelected.Where(q => q.slottedTime == mk.slottedTime && q.Date == mk.Date).Any())
                            {
                                lessons.Remove(mk);
                                lessonsSelected.Add(mk);
                                counter -= 1;
                                lblLesson2.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";

                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Too many lessons have been selected.", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
                    lblLesson2.Content = totalLessons + " Lessons to be scheduled";
                    lessonsSelected.Clear();
                    cmbTimes4.SelectedValue = -1;
                }


                if (!lessonsSelected.Any())
                {
                    lstSelectedTimeSlots4.ItemsSource = lessonsSelected;
                    lstTimeSlots4.ItemsSource = lessonsSelected;
                }
                else
                {
                    lstSelectedTimeSlots4.ItemsSource = lessonsSelected;
                }



            }
            else
            {
                MessageBox.Show("All lessons have been scheduled for this invoice", "SCHEDULED", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnWzdSchedRem4_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedTimeSlots4.SelectedItem != null)
            {
                var removeLssn = lstSelectedTimeSlots4.SelectedItem;
                lessonsSelected.Remove((InstructorLearnerSchedule)removeLssn);
                lessons.Add((InstructorLearnerSchedule)removeLssn);
                var ordered = lessons.OrderBy(o => o.Date).ToList();
                lessons.Clear();
                lessons = new ObservableCollection<InstructorLearnerSchedule>(ordered);
                lstTimeSlots4.ItemsSource = lessons;
                lblLesson2.Content = totalLessons - lessonsSelected.Count + " Lessons to be scheduled";
            }

        }

        private void LastPage_Loaded(object sender, RoutedEventArgs e)
        {
            var lessonBkn = lessonsSelected.ToList();
            dtgLessonSchedule.ItemsSource = lessonBkn;
        }

        private void wzdSchedule_Cancel(object sender, RoutedEventArgs e)
        {
            roadTestSelected.Clear();
            lessonsSelected.Clear();
            ResetInstructorCombobox();
            wndScheduler.Close();
        }

        void ResetInstructorCombobox()
        {
            cmbInstructor.SelectedIndex = -1;
        }

        void DisableWindowPart()
        {
            grpSalesDetail.IsEnabled = false;
            dtgSales.IsEnabled = false;
        }

        private void wzdSchedule_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            if (lessonsSelected.Count() != (int)theSales.Where(p => p.productTypeID == 2).Select(q => q.Quantity).Sum())
            {
                MessageBox.Show("Some lessons have not been scheduled", "LESSON UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
                e.Cancel = true;
            }
        }

        private void wzdSchedule2_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            if (roadTestSelected.Count() != (int)theSales.Where(p => p.productTypeID == 1).Select(q => q.Quantity).Sum())
            {
                MessageBox.Show("Some road tests have not been scheduled", "ROAD TEST UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
                e.Cancel = true;
            }
        }

        private void wzdSchedule1_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            if (wzdSchedule1.CurrentPage.Name == "IntroPage1" && lessonsSelected.Count() != (int)theSales.Where(p => p.productTypeID == 2).Select(q => q.Quantity).Sum())
            {
                MessageBox.Show("Some lessons have not been scheduled", "LESSON UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
                e.Cancel = true;
            }
            else if (wzdSchedule1.CurrentPage.Name == "ScheduleRoadTest" && roadTestSelected.Count() != (int)theSales.Where(p => p.productTypeID == 1).Select(q => q.Quantity).Sum())
            {
                MessageBox.Show("Some road tests have not been scheduled", "ROAD TEST UNSCHEDULED", MessageBoxButton.OK, MessageBoxImage.Hand);
                e.Cancel = true;
            }
        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustName.Text == string.Empty)
            {
                MessageBox.Show("The customer's name is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;

            }
            else if (txtCustSurname.Text == string.Empty)
            {
                MessageBox.Show("The customer's surname is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (dtpCustDOB.SelectedDate == null)
            {
                MessageBox.Show("The customer's Date Of Birth is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtCustId.Text == string.Empty)
            {
                MessageBox.Show("The customer's National ID Number is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtContact1.Text == string.Empty)
            {
                MessageBox.Show("The customer's Contact Number1 is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtContact2.Text == string.Empty)
            {
                MessageBox.Show("The customer's Contact Number2 is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtAdress.Text == string.Empty)
            {
                MessageBox.Show("The customer's Adress is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (IsValidEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Invalid email Address", "INVALID DATA", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtPDLNum.Text == string.Empty)
            {
                MessageBox.Show("The customer's Provisional Drivers Licence Number is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (dtpExipry.SelectedDate == null)
            {
                MessageBox.Show("The customer's Provisional Drivers Licence Exipry date is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtNextOfKin.Text == string.Empty)
            {
                MessageBox.Show("The customer's Next Of Kin is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else if (txtNextOfKinContact.Text == string.Empty)
            {
                MessageBox.Show("The customer's Next Of Kin Contant detail is missing. ", "DATA MISSING", MessageBoxButton.OK, MessageBoxImage.Hand);
                popNewCustomer.IsOpen = true;
            }
            else
            {
                Customer nwCust = new Customer()
                {
                    Address = txtAdress.Text,
                    // CustomerNumber="001",
                    DateOfBirth = Convert.ToDateTime(dtpCustDOB.SelectedDate),
                    ExpiryDate = Convert.ToDateTime(dtpExipry.SelectedDate),
                    FirstName = txtCustName.Text,
                    IdentityNumber = txtCustId.Text,
                    NextOfKin = txtNextOfKin.Text,
                    NextOfKinPhoneNumber = txtNextOfKinContact.Text,
                    PDLNumber = txtPDLNum.Text,
                    PhoneNumber1 = txtContact1.Text,
                    PhoneNumber2 = txtContact2.Text,
                    Surname = txtCustSurname.Text,
                    Valid = Convert.ToDateTime(dtpValid.SelectedDate)
                };
                DbInsert.InsertCustomer(nwCust);
                cmbCustomer.ItemsSource = DbAccess.GetAllCustomers();
                Binding myBinding = new Binding();
                myBinding.Source = DbAccess.GetCustomerLastCustomer();
                myBinding.Path = new PropertyPath("ID");
                myBinding.Mode = BindingMode.OneWay;
                myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(cmbCustomer, ComboBox.SelectedValueProperty, myBinding);
                //  popNewCustomer.IsOpen = false;
                MessageBox.Show("Customer " + txtCustName.Text + " " + txtCustSurname.Text + " was added", "NEW CUSTOMER ADDED", MessageBoxButton.OK, MessageBoxImage.Hand);

            }

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        bool IsValidEmail(string email)
        {
            if (txtEmail.Text != string.Empty)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        private void txtPaid_TextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void txtPaid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //if (txtPaid.Value!=0)
            //{
            //    var hh = txtPaid.Value;
            //    var uu = txtTotal.Text;
            //    decimal payable = 0;
            //    bool isNum = txtAmount.Text.All(c => Char.IsNumber(c));

            //    var amntPayable = ExtractNumber(txtTotal.Text);
            //    if (txtPaid.Text != string.Empty)
            //    {
            //        payable = (decimal)txtPaid.Value;

            //        if (txtPaid.Text != string.Empty && isNum == true)
            //        {
            //            var totalPayable = txtTotal.Text == null ? 0 : Convert.ToDecimal(amntPayable) * (decimal).01; //Convert.ToDecimal(txtTotal.Text.Substring(1));
            //            if (txtPaid.Text != string.Empty)
            //            {

            //                if (payable >= totalPayable)                         //(txtPaid.Text.TrimStart('$'))>= totalPayable)
            //                {
            //                    txtChange.Text = (payable - totalPayable).ToString();

            //                }
            //                else
            //                {
            //                    txtChange.Text = string.Empty;
            //                }
            //            }

            //        }

            //    }
            //    else
            //    {
            //        txtChange.Text = string.Empty;
            //    }
            //    txtPaid1.Visibility = System.Windows.Visibility.Visible;

            // }




        }

        private void txtPaid1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

    }
}
