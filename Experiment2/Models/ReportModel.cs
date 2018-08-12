using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Experiment2.Models
{
    public class ReportModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public string SoldBy { get; set; }
        public string SoldTo { get; set; }
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        [Key]
        public int SalesOrderID { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalDue { get; set; }
        public int ProductNumber { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal UnitPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal LineTotal { get; set; }

    }
}