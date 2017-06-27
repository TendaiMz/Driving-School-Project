using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAL;
using System.Data.Entity;

namespace BUL
{
  public static class DbInsert
  {
      #region RescheduleLesson
      public static void RescheduleLesson(int scheduleID,DateTime theDate, int slot)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {

              if (context.ScheduleInstructors.Where(t => t.ID == scheduleID).Any())
              {
                  var reScheduledbooking = context.ScheduleInstructors.Where(t => t.ID == scheduleID).Single();
                  reScheduledbooking.TimeSlotID = slot;
                  reScheduledbooking.Date = theDate;
                  context.SaveChanges();
              }     
         
             
          }
      }
      #endregion

      #region RescheduleRoadTest
      public static void RescheduleRoadTest(int scheduleID, DateTime theDate, int slot,byte hourSlot)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
             
              if (context.ScheduleInstructors.Where(t => t.ID == scheduleID).Any())
              {
                  var reScheduleTest = context.ScheduleInstructors.Where(t => t.ID == scheduleID).Single();
                  var reScheduledest1 = context.ScheduleInstructors.Where(s => s.AffectedHourSlot == reScheduleTest.AffectedHourSlot && s.Date == reScheduleTest.Date && s.ID!=scheduleID).Single();

                  reScheduledest1.TimeSlotID = slot+1;
                  reScheduledest1.Date = theDate;
                  reScheduledest1.AffectedHourSlot = hourSlot;
                  
                  reScheduleTest.TimeSlotID = slot;
                  reScheduleTest.Date = theDate;
                  reScheduleTest.AffectedHourSlot = hourSlot;
                  context.SaveChanges();
                
              }


          }
      }
      #endregion


      #region ScheduleRecons
      public static void ScheduleRecons(List<InstructorLearnerSchedule> scheduleList,byte UsrId)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              foreach (var sched in scheduleList)
              {
                  var pending = sched.Complete == true || sched.Cancel == true ? false : true;
                  var selectedSchedule = context.ScheduleInstructors.Where(i => i.ID==sched.ScheduleInstructorID).SingleOrDefault();
                  selectedSchedule.Cancelled = sched.Cancel;
                  selectedSchedule.Completed = sched.Complete;
                  selectedSchedule.NoShow = sched.NoShow;
                  selectedSchedule.Pending = pending;
                  selectedSchedule.ReconsComment = sched.Comment;
                  selectedSchedule.ScheduleReconsDoneBy = UsrId;
                  context.SaveChanges();
              }
              
          }
      }
      #endregion

      #region InsertUser
      public static void InsertUser(User usr)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Users.Add(usr);
              context.SaveChanges();
          }
      }
      #endregion

      #region InsertCustomer
      public static void InsertCustomer(Customer cust)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Customers.Add(cust);
              context.SaveChanges();
          }
      }
      #endregion

      #region ResetPassword
      public static void ResetPassword(PaswordReset passReset, string userPassHash)
      {
          
          using (var scope = new TransactionScope())
          {
              using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
              {
                      var sysUser = context.Users.Where(u => u.ID == passReset.UserID).First();
                      sysUser.PasswordHash = userPassHash;
                      context.PaswordResets.Add(passReset);
                      context.SaveChanges();                    
                      scope.Complete();
                            
              }             
             
          }
        
      }
      #endregion

      #region UpdateFuelingeData
      public static void UpdateFuelingeData(VehicleFueling updateFueling)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(updateFueling).State = updateFueling.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }

      }
      #endregion

      #region InsertFuelingData
      public static void InsertFuelingData(VehicleFueling fuelingData)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.VehicleFuelings.Add(fuelingData);
              context.SaveChanges();
          }
      }
      #endregion

      #region UpdateMaintenaceData
      public static void UpdateMaintenaceData(Maintenace updateMaintanance)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(updateMaintanance).State = updateMaintanance.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }

      }
      #endregion

      #region InsertMaintenaceData
      public static void InsertMaintenaceData(Maintenace maintananceData)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Maintenaces.Add(maintananceData);
              context.SaveChanges();
          }
      }
      #endregion

      #region UpdateInstructorrData
      public static void UpdateInstructorrData(Instructor updateInstructor)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(updateInstructor).State = updateInstructor.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }

      }
      #endregion

      #region InsertInstructor
      public static void InsertInstructor(Instructor instructorData)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Instructors.Add(instructorData);
              context.SaveChanges();
          }
      }
      #endregion

      #region UpdateUserData
      public static void UpdateUserData(User updateUsr)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(updateUsr).State = updateUsr.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }
      
      }
      #endregion

      #region UpdateVehicleData
      public static void UpdateVehicleData(Vehicle updateVehic)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(updateVehic).State = updateVehic.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }
      }
      #endregion

      #region InsertVehicle
      public static void InsertVehicle(Vehicle vehicleData)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Vehicles.Add(vehicleData);
              context.SaveChanges();
          }
      }
      #endregion

      #region UpdateCustomerData
      public static void UpdateCustomerData(Customer customerData)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              context.Entry(customerData).State = customerData.ID== 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }
      }
      #endregion

      #region InsertInvoice
      public static void InsertInvoice(List<SaleDetails> saleData, List<InstructorLearnerSchedule> schedule)
      {

          using (TransactionScope scope = new TransactionScope())
          {
              using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
              {
                  //save sale data
                  Invoice invo = new Invoice();
                  InvoiceLineItem lineItem = new InvoiceLineItem();

                  //add invoice data
                  SaleDetails saleInvoiceData = saleData.FirstOrDefault();
                  invo.CashierID = saleInvoiceData.CashierID;
                  invo.CustomerID = saleInvoiceData.CustomerID;
                  invo.Date = saleInvoiceData.Date;
                  invo.InvoiceNumber = saleInvoiceData.InvoiceNumber;
                  context.Invoices.Add(invo);
                  context.SaveChanges();

                  foreach (var sale in saleData)
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
                  foreach (var shd in schedule)
                  {
                      instructorLearnerSched.BookingType = shd.BookingTpye;
                      instructorLearnerSched.Date = shd.Date;
                      instructorLearnerSched.InstructorID = shd.InstructorID;
                      instructorLearnerSched.TimeSlotID = shd.slottedTime;
                      instructorLearnerSched.StudentID = shd.StudentID;
                      instructorLearnerSched.Pending = true;
                      context.ScheduleInstructors.Add(instructorLearnerSched);
                      context.SaveChanges();
                  }


              }
              scope.Complete();
          }

      }
      #endregion

   

     // public void InsertScheduleData


      public static void CancelSale(InvoiceData selectedItem)
      {
          using (drivingSchoolDBEntities context = new drivingSchoolDBEntities())
          {
              Invoice invoice = new Invoice { 
                                         CancellationReason=selectedItem.CancellationReason,
                                         Cancelled=selectedItem.Cancelled,
                                         CancelledBy=selectedItem.CancelledBy,
                                         Date=selectedItem.Date,
                                         CashierID=selectedItem.CashierID,
                                         CustomerID=selectedItem.CustomerID,
                                         ID=selectedItem.ID,
                                         InvoiceNumber=selectedItem.InvoiceNumber
                                         };
              context.Entry(invoice).State = invoice.ID == 0 ? EntityState.Added : EntityState.Modified;
              context.SaveChanges();
          }
      }
  }
}
