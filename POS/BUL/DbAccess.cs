using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUL
{
    public class DbAccess
    {

        #region GetInstructorIdByStudentID
        public static int? GetInstructorIdByStudentID(int studentID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.ScheduleInstructors.Where(s=>s.StudentID==studentID).Select(i=>i.InstructorID).FirstOrDefault();
            }
        }
        #endregion

        #region CheckConnection
        public static bool CheckConnection()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Any();
            }
        }
        #endregion

        #region GetAllSupervisors
        public static IEnumerable<User> GetAllSupervisors()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.UserType==3).ToList();
            }
        }
        #endregion

        #region GetInvoices
        public static IEnumerable<InvoiceData> GetInvoices()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Invoices
                    .AsEnumerable()
                    .Select(x => new InvoiceData { 
                                                   ID=x.ID,
                                                   Date=x.Date,
                                                   CancellationReason=x.CancellationReason,
                                                   Cancelled=x.Cancelled,
                                                   CancelledBy=x.CancelledBy,
                                                   InvoiceNumber=x.InvoiceNumber,
                                                   CashierID=x.CustomerID,
                                                   CustomerID=x.CustomerID
                                                                                        
                                                     }).ToList();
            }
        }
        #endregion

        #region GetUserIDByUserName
        public static byte GetUserIDByUserName(string userName)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.UserName == userName).Select(i=>i.ID).First();
            }
        }
        #endregion

        #region GetUserNameByID
        public static string GetUserNameByID(int supervisorID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.ID==supervisorID).Select(s=>s.UserName).First();
            }
        }
        #endregion

        #region GetUserFullNameByID
        public static string GetUserFullNameByID(int userID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.ID == userID).Select(s => s.Name+" "+ s.Surname).First();
            }
        }
        #endregion

        #region GetAllInstructorScheduleRecon
        public static IEnumerable<InstructorLearnerSchedule> GetAllInstructorScheduleRecon(int instuctorID,DateTime date)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                var theSchedule = (from ts in context.ScheduleInstructors.Where(i => i.InstructorID == instuctorID)
                                   join cu in context.Customers on ts.StudentID equals cu.ID
                                   join ni in context.Instructors on ts.InstructorID equals ni.ID
                                   join bk in context.BookingTypes on ts.BookingType equals bk.ID
                                   join st in context.DayTimeSlots on ts.TimeSlotID equals st.ID
                                   join inv in context.Invoices on ts.InvoiceID equals inv.ID
                                   where inv.Cancelled==false
                                   select new InstructorLearnerSchedule()
                                   { 
                                       ScheduleInstructorID=ts.ID,
                                       Date = (DateTime)ts.Date,
                                       slottedTime=(int)ts.TimeSlotID,
                                       StudentID = ts.StudentID == null ? null : ts.StudentID,
                                       InstructorID = ts.InstructorID == null ? null : ts.InstructorID,
                                       BookingTpye = ts.BookingType == null ? null : ts.BookingType,
                                       BookingName = bk.BookingName == null ? null : bk.BookingName,
                                       Time = st.TimeSlot == null ? null : st.TimeSlot,
                                       StudentName = cu.FirstName + " " + cu.Surname == null ? null : cu.FirstName + " " + cu.Surname,
                                       InstuctorName = ni.Name + " " + ni.Surname == null ? null : ni.Name + " " + ni.Surname,
                                       Complete = ts.Completed == null ? null : ts.Completed, 
                                       Cancel = ts.Cancelled == null ? null : ts.Cancelled,
                                       NoShow = ts.NoShow == null ? null : ts.NoShow
                                   }).ToList();

                var odt = context.DayTimeSlots.ToList();
                var theSchedule1 = (from sc in odt
                                    join  tr in theSchedule.Where(i => i.InstructorID == instuctorID && i.Date==date) on sc.ID equals tr.slottedTime into td
                                    from ts in td.DefaultIfEmpty()
                                    select new InstructorLearnerSchedule()
                                    {
                                        Time = sc.TimeSlot,
                                        slottedTime=sc.ID,
                                        StudentID = ts == null ? null : ts.StudentID,
                                        InstructorID = ts == null ? null : ts.InstructorID,
                                        BookingName = ts == null ? null : ts.BookingName,
                                        StudentName = ts== null ? null : ts.StudentName,
                                        InstuctorName = ts == null ? null : ts.InstuctorName,
                                        ScheduleInstructorID = ts==null?0:ts.ScheduleInstructorID,
                                        Complete=ts==null?null:ts.Complete,
                                        Cancel = ts == null ? null : ts.Cancel,
                                        NoShow = ts == null ? null : ts.NoShow
                                    }).ToList();
                return theSchedule1.ToList();    
            }
        }
        #endregion

        #region GetStudentSchedule
        public static IEnumerable<InstructorLearnerSchedule> GetStudentSchedule(int studentID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                var instructorBookings = (from sc in context.ScheduleInstructors
                                          join iv in context.Invoices on sc.InvoiceID equals iv.ID
                                          join cu in context.Customers on sc.StudentID equals cu.ID
                                          join ins in context.Instructors on sc.InstructorID equals ins.ID
                                          join dt in context.DayTimeSlots on sc.TimeSlotID equals dt.ID
                                          join bo in context.BookingTypes on sc.BookingType equals bo.ID
                                          where sc.StudentID==studentID && iv.Cancelled==false
                                          select new InstructorLearnerSchedule()
                                          { 
                                              ScheduleInstructorID=sc.ID,
                                              BookingName = bo.BookingName,
                                              Time = dt.TimeSlot,
                                              InstuctorName = ins.Name + " " + ins.Surname,
                                              Date = (DateTime)sc.Date,
                                              StudentName = cu.FirstName + " " + cu.Surname,
                                              InstructorID=sc.InstructorID
                                          }).AsEnumerable().Select(x => new InstructorLearnerSchedule
                                          { 
                                              InstructorID=x.InstructorID,
                                              ScheduleInstructorID=x.ScheduleInstructorID,
                                              BookingName = x.BookingName + " -" + x.InstuctorName,
                                              ending = Convert.ToDateTime(x.Date.ToString().Split(' ').First() + " " + (x.Time.Split('-').Last()) + ":00.000"),
                                              InstuctorName = x.InstuctorName,
                                              starting = Convert.ToDateTime(x.Date.ToString().Split(' ').First() + " " + (x.Time.Split('-').First()) + ":00.000"),
                                              Date = x.Date,
                                              Time=x.Time
                                          });
                var uu = instructorBookings.ToList();//.Where(d => d.Date >= DateTime.Today);
                return uu;
             
            }
        }
        #endregion

        #region GetStudentIncompletedLessons
        public static IEnumerable<InstructorLearnerSchedule> GetStudentIncompletedLessons(int studentID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                var instructorBookings = (from sc in context.ScheduleInstructors.Where(i => i.StudentID == studentID&&i.Pending==true)
                                          join cu in context.Customers on sc.StudentID equals cu.ID
                                          join ins in context.Instructors on sc.InstructorID equals ins.ID
                                          join dt in context.DayTimeSlots on sc.TimeSlotID equals dt.ID
                                          join bo in context.BookingTypes on sc.BookingType equals bo.ID
                                          select new InstructorLearnerSchedule()
                                          {
                                              BookingName = bo.BookingName,
                                              Time = dt.TimeSlot,
                                              InstuctorName = ins.Name + " " + ins.Surname,
                                              Date = (DateTime)sc.Date,
                                              StudentName = cu.FirstName + " " + cu.Surname
                                          }).AsEnumerable().Select(x => new InstructorLearnerSchedule
                                          {
                                              BookingName = x.BookingName + " -" + x.InstuctorName,
                                              ending = Convert.ToDateTime(x.Date.ToString().Split(' ').First() + " " + (x.Time.Split('-').Last()) + ":00.000"),
                                              InstuctorName = x.InstuctorName,
                                              starting = Convert.ToDateTime(x.Date.ToString().Split(' ').First() + " " + (x.Time.Split('-').First()) + ":00.000"),
                                              Date = x.Date,
                                              Time = x.Time,
                                          });
                var uu = instructorBookings.ToList().Where(d => d.Date >= DateTime.Today);
                return uu;
                //  return instructorBookings.ToList();//.OrderBy(o=>o.starting);
            }
        }
        #endregion

        #region GetInstructorSchedule
        public static IEnumerable<InstructorLearnerSchedule> GetInstructorSchedule(int instructorID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                var instructorBookings = (from sc in context.ScheduleInstructors
                                         join iv in context.Invoices on sc.InvoiceID equals iv.ID
                                         join cu in context.Customers on sc.StudentID equals cu.ID
                                         join ins in context.Instructors on sc.InstructorID equals ins.ID
                                         join dt in context.DayTimeSlots on sc.TimeSlotID equals dt.ID
                                         join bo in context.BookingTypes on sc.BookingType equals bo.ID
                                        where sc.InstructorID == instructorID && iv.Cancelled==false
                                         select new InstructorLearnerSchedule() 
                                         {
                                             ScheduleInstructorID=sc.ID,
                                             BookingName=bo.BookingName,
                                             Time = dt.TimeSlot,
                                             InstuctorName= ins.Name + " " + ins.Surname,
                                             Date = (DateTime)sc.Date,
                                             StudentName=cu.FirstName +" "+ cu.Surname, 
                                             BookingTpye=sc.BookingType,
                                             
                                         }).AsEnumerable().Select(x=> new  InstructorLearnerSchedule
                                         {  
                                             ScheduleInstructorID=x.ScheduleInstructorID,
                                             BookingName=x.BookingName + " -"+x.StudentName,
                                             ending= Convert.ToDateTime(x.Date.ToString().Split(' ').First()+" "+(x.Time.Split('-').Last())+":00.000"),
                                             InstuctorName=x.InstuctorName,
                                             starting = Convert.ToDateTime(x.Date.ToString().Split(' ').First() + " " + (x.Time.Split('-').First()) + ":00.000"),
                                             BookingTpye=x.BookingTpye,
                                             Date = x.Date
                                         });

               
                return instructorBookings.ToList();//.Where(d=>d.Date>=DateTime.Today).ToList();
            }
        }
        #endregion

        #region IsUserNameIsAcivated
        public static bool IsUserNameIsAcivated(string userName)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && u.Activated==true).Any() ? true : false;
            }
        }
        #endregion

        #region CheckIfUserNameIsInUse
        public static bool CheckIfUserNameIsInUse(string userName)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).Any()? true:false;
            }
        }
        #endregion

        #region GetUserPasswordHashByUserName
        public static string GetUserPasswordHashByUserName(string userName)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.UserName.Equals(userName,StringComparison.OrdinalIgnoreCase)).Select(h => h.PasswordHash).First();
            }
        }
        #endregion

        #region GetUserPasswordHashByID
        public static string GetUserPasswordHashByID(Byte userID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u=>u.ID==userID).Select(h=>h.PasswordHash).First();
            }
        }
        #endregion

        #region GetAllFuelingData
        public static IEnumerable<VehicleFueling> GetAllFuelingData()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.VehicleFuelings.ToList();
            }
        }
        #endregion

        #region GetAllMaintananceType
        public static IEnumerable<MaintenaceType> GetAllMaintananceType()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.MaintenaceTypes.ToList();
            }
        }
        #endregion

        #region GetAllMaintenanceDetails
        public static IEnumerable<Maintenace> GetAllMaintenanceDetails()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Maintenaces.ToList();
            }
        }
        #endregion

        #region GetAllInstructors
        public static IEnumerable<Instructor> GetAllInstructors()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Instructors.ToList();
            }
        }
        #endregion

        #region GetUserType
        public static IEnumerable<UserType> GetUserType()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.UserTypes.ToList();
            }
        }
        #endregion

        #region GetAllUsers
        public static IEnumerable<User> GetAllUsers()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.ToList();
            }
        }
        #endregion

        #region GetAllCustomers
        public static IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Customers.OrderBy(c=>c.FirstName).ToList();
            }
        }
        #endregion

        #region GetCustomersByID
        public static IEnumerable<Customer> GetCustomersByID(int customerID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Customers.Where(i=>i.ID==customerID).ToList();
            }
        }
        #endregion

        #region GetCustomerLastCustomer
        public static Customer GetCustomerLastCustomer()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Customers.OrderByDescending(o=>o.ID).FirstOrDefault();
            }
        }
        #endregion

        #region GetCustomersByCustomerNumber
        public static Customer GetCustomersByCustomerNumber(string customerNo)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Customers.Where(i => i.CustomerNumber.Equals(customerNo,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
        }
        #endregion

        #region GetCarModel
        public static IEnumerable<Model> GetCarModel()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Models.ToList();
            }
        }
        #endregion

        #region GetCarMake
        public static IEnumerable<Make> GetCarMake()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Makes.ToList();
            }
        }
        #endregion

        #region GetCarColor
        public static IEnumerable<Colour> GetCarColor()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Colours.ToList();
            }
        }
        #endregion

        #region GetCarType
        public static IEnumerable<VehicleType> GetCarType()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.VehicleTypes.ToList();
            }
        }
        #endregion

        #region GetCarFuel
        public static IEnumerable<Fuel> GetCarFuel()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Fuels.ToList();
            }
        }
        #endregion

        #region GetAllVehicles
        public static IEnumerable<Vehicle> GetAllVehicles()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Vehicles.ToList();
            }
        }
        #endregion

        public static IEnumerable<Unit> GetAllUnits()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Units.ToList();
            }
        }

        public static IEnumerable<SaleItem> GetAllSaleItems()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.SaleItems.ToList();
            }
        }

        public static decimal? GetSaleSaleItem(int itemID)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.SaleItems.Where(i=>i.ID==itemID).Select(p=>p.Price).FirstOrDefault();
            }
        }

        //public static IEnumerable<Instructor> GetAllInstructors() 
        //{
        //    using (var context = new drivingSchoolDBEntities())
        //    {
        //        return context.Instructors.ToList();
        //    }
        //}

        public static IEnumerable<ScheduleInstructor> GetAllDriverSchedules()
        {
            using (var context = new drivingSchoolDBEntities())
            {

                var schedule = from c in context.ScheduleInstructors
                               join x in context.Invoices on c.InvoiceID equals x.ID
                               where x.Cancelled==false
                               select c;
                return schedule.ToList();
            }
        }

        #region GetInstructorAvailableDate
        //Get instructor free date 
        public static IEnumerable<ScheduleInstructor> GetInstructorAvailableDate(int instruktah, int dateTimeSlot)
        {
            using (var context = new drivingSchoolDBEntities())
            {
               
                return context.ScheduleInstructors.Where(i => i.InstructorID == instruktah && i.TimeSlotID == dateTimeSlot).ToList();
            }
        }
        #endregion

        #region GetInstructorAvailableBookingDate
        //Get instructor free date for road test
        public static IEnumerable<ScheduleInstructor> GetInstructorAvailableBookingDate(int instruktah, int dateTimeSlot)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                //Get all road test booked hrs time
                var lessonSlot1 =  context.RoadTest_DayTimeSlot.Where(c=>c.RoadTestID==dateTimeSlot).Select(s=>s.DayTimeSlotID1).SingleOrDefault();
                var list1 = context.ScheduleInstructors.Where(i => i.InstructorID == instruktah && i.TimeSlotID == lessonSlot1).ToList();
                var list2 = context.ScheduleInstructors.Where(i => i.InstructorID == instruktah && i.TimeSlotID == lessonSlot1+1).ToList();                                      
                var list3= list1.Union(list2);
                return list3;
            }
        }
        #endregion

        #region GetProductTypeID
        public static int GetProductTypeID(int productID)
        {
            using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
            {
                return Convert.ToInt32(context.SaleItems.Where(i => i.ID == productID).Select(x => x.SaleItemTypeID).FirstOrDefault());
            }
        }
        #endregion

        #region GetTimeSlots
        public static IEnumerable<DayTimeSlot> GetTimeSlots()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.DayTimeSlots.ToList();
            }
        }
        #endregion

        #region GetTimeSlots
        public static IEnumerable<DayTimeSlot> GetTimeSlots(int start,int end)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                var oo = context.DayTimeSlots.Where(x => x.ID >= start && x.ID <= end).ToList();
                return context.DayTimeSlots.Where(x=>x.ID>=start && x.ID<=end).ToList();
               
            }
        }
        #endregion

        #region GetTimeSlotID
        public static int GetTimeSlotID(string slotTime)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.DayTimeSlots.Where(x => x.TimeSlot==slotTime).Select(i=>i.ID).FirstOrDefault();

            }
        }
        #endregion

        #region GetHourTimeSlots
        public static IEnumerable<RoadTestTimeSlot> GetHourTimeSlots()
        {
            using (var context = new drivingSchoolDBEntities())
            {
                   return context.RoadTestTimeSlots.ToList();

            }
        }
        #endregion

        #region GetDaySlotsFromHourSlot
         public static RoadTest_DayTimeSlot GetDaySlotsFromHourSlot(int hourSlot)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.RoadTest_DayTimeSlot.Where(x=>x.RoadTestID==hourSlot).FirstOrDefault();
               
            }
        }
        #endregion

        #region GetDaySlotsName1
         public static string GetDaySlotsName1(byte hourSlot)
         {
             using (var context = new drivingSchoolDBEntities())
             {
                var slot = (from d in context.RoadTest_DayTimeSlot.Where(x => x.RoadTestID == hourSlot)
                           join c in context.DayTimeSlots on d.DayTimeSlotID1 equals c.ID
                           select c.TimeSlot).FirstOrDefault();
                return slot;
             }
         }
         #endregion

        #region GetDaySlotsName2
         public static string GetDaySlotsName2(byte hourSlot)
         {
             using (var context = new drivingSchoolDBEntities())
             {
                 var slot = (from d in context.RoadTest_DayTimeSlot.Where(x => x.RoadTestID == hourSlot)
                             join c in context.DayTimeSlots on d.DayTimeSlotID2 equals c.ID
                             select c.TimeSlot).FirstOrDefault();
                 return slot;
             }
         }
         #endregion

        #region GetDayTimeSlotFromHourSlot
         public static List<int> GetDayTimeSlotFromHourSlot(byte dateTimeSlot)
         {
             List<int> timeSlots = new List<int>();
             using (var context = new  drivingSchoolDBEntities())
             {
                 var slot1 = context.RoadTest_DayTimeSlot.Where(c => c.RoadTestID == dateTimeSlot).Select(s => s.DayTimeSlotID1).SingleOrDefault();
                 timeSlots.Add((int)slot1);
                 var slot2 = context.RoadTest_DayTimeSlot.Where(c => c.RoadTestID == dateTimeSlot).Select(s => s.DayTimeSlotID1+1).SingleOrDefault();
                 timeSlots.Add((int)slot2);
                 

               return timeSlots;    
             }
         }
        #endregion

        #region CashUp
        // public static DailyCashUp GetCashUpData(int cashierID)
        //{
        //    using (var context = new drivingSchoolDBEntities())
        //    {
        //        var cashUpData = from nv in context.Invoices
        //                         join nvl in context.InvoiceLineItems on nv.ID equals nvl.InvoiceID
        //                         join us in context.Users on nv.CashierID equals us.ID
        //                         where nv.Date == DateTime.Today
        //                         select new DailyCashUp
        //                         {
        //                           Name= us.FullName,                                   
        //                           Cash=nv.
        //                         };
               
        //    }
        //}
        #endregion

        #region GetCheckMilage
         //Get instructor free date 
         public static bool GetCheckMilage(decimal milage,int vehicleID)
         {
             using (var context = new drivingSchoolDBEntities())
             {
                 var milages= context.VehicleFuelings.Where(c=>c.VehicleID==vehicleID).OrderByDescending(o=>o.ID).Select(m=>m.Milage).FirstOrDefault();
                 if (milages< milage )
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
         }
         #endregion

        #region CheckForUncompletedDaySheet
         //Get instructor free date 
         public static DateTime? CheckForUncompletedDaySheet(DateTime today,int instructorID)
         {
             using (var context = new drivingSchoolDBEntities())
             {
                 var uncancelledIncompleteData =from nr in context.ScheduleInstructors
                                                join nv in context.Invoices on nr.InvoiceID equals nv.ID
                                                where nv.Cancelled==false
                                                select nr;

                 var incompletedDate = uncancelledIncompleteData.Where(c => c.Cancelled == false && c.Completed == false && c.NoShow == false && c.Pending == false && c.Date < today && c.InstructorID == instructorID).Select(d => d.Date).FirstOrDefault();
                 if (incompletedDate != null)
                 {
                     return incompletedDate;
                 }
                 else
                 {
                     return null;
                 }
             }
         }
         #endregion

         public static int GetInstructorByCar(int reg)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                 return Convert.ToInt16(context.Instructors.Where(v=>v.Vehicle==reg).Select(i=>i.ID).FirstOrDefault());
             };
         }

         public static int? GetVehicleByInstructor(int instructor)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                 return Convert.ToInt16(context.Instructors.Where(i=>i.ID==instructor).Select(v => v.Vehicle).FirstOrDefault());
             };
         }

         public static string GetVehicleRegInstructor(int instructor)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                 var vehicID = Convert.ToInt16(context.Instructors.Where(i => i.ID == instructor).Select(v => v.Vehicle).FirstOrDefault());
                 return context.Vehicles. Where(i => i.ID == vehicID).Select(v => v.VehicleRegistration).FirstOrDefault();
             }
         }

       
         public static int GetUserTypeByUserID(int userID)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                 
                 return Convert.ToInt16(context.Users.Where(u=>u.ID==userID).Select(v => v.UserType).FirstOrDefault());
             }
         }

         public static int GetStudentCompletedLessons(int studentID)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                var completed = (from c in context.ScheduleInstructors
                                 join x in context.Invoices on c.InvoiceID equals x.ID
                                 where c.StudentID == studentID && c.Completed == true && x.Cancelled == false
                                 select c).ToList();
                 return Convert.ToInt16(completed.Count());
             }
         }

         public static int GetStudentPendingLessons(int studentID)
         {
             using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
             {
                var pending = (from c in context.ScheduleInstructors
                                 join x in context.Invoices on c.InvoiceID equals x.ID
                                 where c.StudentID == studentID && c.Completed == false && x.Cancelled == false
                                 select c).ToList();
                return Convert.ToInt16(pending.Count());
             }
         }

        public static bool IsSysAdmin(int logIn)
        {
            using (var context = new drivingSchoolDBEntities())
            {
                return context.Users.Where(u => u.ID==logIn).Select(s=>s.UserType).FirstOrDefault()==1?true:false;
            }
        }
    }
}
