//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    
    public partial class spInstructorSchedule_Result
    {
        public byte ID { get; set; }
        public string TimeSlot { get; set; }
        public Nullable<byte> AffectedHourSlot1 { get; set; }
        public Nullable<byte> AffectedHourSlot2 { get; set; }
        public Nullable<int> TimeSlotID { get; set; }
        public Nullable<System.DateTime> TheDate { get; set; }
        public string Student { get; set; }
        public string Instuctor { get; set; }
        public string Booking { get; set; }
    }
}