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
    
    public partial class spDetailedSales_Result
    {
        public Nullable<System.DateTime> SaleDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> ProductQty { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public string CashierName { get; set; }
    }
}
