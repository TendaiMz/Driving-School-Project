using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUL
{
	public class DailyCashUp
	{
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public int TotalSales { get; set; }
		public decimal Cash { get; set; }

	}

	public class InstructorLearnerScheduleComparer : IEqualityComparer<InstructorLearnerSchedule>
	{
		public bool Equals(InstructorLearnerSchedule x,InstructorLearnerSchedule y) 
		{
			if (x.Date==y.Date)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public int GetHashCode(InstructorLearnerSchedule obj)
		{
			return obj.GetHashCode();
		}


	}

	public class InvoiceData:INotifyPropertyChanged
	{

	   
		public Nullable<int> CustomerID { get; set; }
		public Nullable<int> CashierID { get; set; }       
		public string InvoiceNumber { get; set; }       
		public Nullable<int> CancelledBy { get; set; }
		public Nullable<System.DateTime> Date { get; set; }		
		public int ID { get; set; }
		private string cancellationReason;
		private bool? cancelled;

		public string CancellationReason
		{
			get { return this.cancellationReason; }
			set
			{
				cancellationReason = value;
				OnPropertyChanged(new PropertyChangedEventArgs("CancellationReason"));
			}
		}


		public bool? Cancelled
		{
			get { return this.cancelled; }
			set
			{
				cancelled = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Cancelled"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}
	}
   

	public class InstructorLearnerSchedule:INotifyPropertyChanged
	{
		public string Day { get; set; }
		public DateTime Date { get; set; }
		public string Time { get; set; }
		public string HourTime { get; set; }
		public byte HourSlotID { get; set; }
		public int slottedTime { get; set; }
		public Nullable<int> InstructorID { get; set; }
		public Nullable<int> StudentID { get; set; }
		public short? BookingTpye { get; set; }
		public string BookingName { get; set; }
		public string StudentName { get; set; }
		public string InstuctorName { get; set; }
		public DateTime starting { get; set; }
		public DateTime ending { get; set; }
		public string  Comment { get; set; }
		public int ReconciledBy { get; set; }
		public int? ScheduleInstructorID { get; set; }

		private bool? complete { get; set; }
		private bool? cancel { get; set; }
		private bool? noShow { get; set; }

		public bool? Complete
		{
			get { return this.complete; }
			set
			{
				 complete = value;
				 OnPropertyChanged(new PropertyChangedEventArgs("Complete"));
			}
		}

		 public bool? Cancel
		{
			get { return this.cancel; }
			set
			{
				 cancel = value;
				 OnPropertyChanged(new PropertyChangedEventArgs("Cancel"));
			}
		}

		 public bool? NoShow
		{
			get { return this.noShow; }
			set
			{
				 noShow = value;
				 OnPropertyChanged(new PropertyChangedEventArgs("NoShow"));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}
	}

	public class ScheduleDate
	{
		public string Day { get; set; }
		public string Date { get; set; }
		public DateTime TheDate { get; set; }

	}

	public class SaleDetails
	{
		public DateTime Date { get; set; }
		public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal Amount { get; set; }
		public decimal Quantity { get; set; }
		public string Units { get; set; }
		public int UnitID { get; set; }
		public string Description { get; set; }
		public decimal Cost { get; set; }
		public int productID { get; set; }
		public int productTypeID { get; set; }
		public int CashierID { get; set; }
		public string InvoiceNumber { get; set; }
		public decimal Discount { get; set; }

	}

   public class DailyLog
	{
		public string Time { get; set; }
		public string Name { get; set; }
		public int Lessons { get; set; }
		public string Comment { get; set; }

	}

   public class InstructorLog
   {
	   public string Date { get; set; }
	   public string Name { get; set; }
	   public int Lessons { get; set; }
	   public string Comment { get; set; }

   }

   public  class VehicleDisplayData
   {
	   public short ID { get; set; }
	   public string VehicleRegistration { get; set; }
	   public string Make { get; set; }
	   public string Model { get; set; }
	   public string ChasisNumber { get; set; }
	   public string EngineNumber { get; set; }
	   public Nullable<System.DateTime> Year { get; set; }
	   public Nullable<byte> Colour { get; set; }
	   public Nullable<byte> Fuel { get; set; }
	   public Nullable<decimal> Milage { get; set; }
	   public Nullable<decimal> Servicing { get; set; }
	   public Nullable<System.DateTime> InsuranceExipry { get; set; }
	   public Nullable<decimal> TankCapacity { get; set; }
	   public Nullable<byte> VehicleType { get; set; }
   }
}
