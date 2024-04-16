using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesInvoice.Models
{
    public class SalesInvoiceModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please add Salescode!")]

        [Display(Name = "SalesCode")]
        public string Code { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please select Date!")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Please enter Duedays!")]
        public int DueDays { get; set; }

        [Display(Name = "DueDate")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please select party!")]

        [Display(Name = "Party")]
        public string Party { get; set; }

        [Display(Name = "Item")]
        [Required(ErrorMessage = "Please select Item!")]
        public string Item { get; set; }

        [Display(Name = "Qty")]
        [Required(ErrorMessage = "Please enter Quantity!")]
        public int Qty { get; set; }

        [Display(Name = "Rate")]
        [Required(ErrorMessage = "Please enter Item Rate!")]
        public decimal Rate { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter Amount!")]
        public decimal Amount { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; }
        [Display(Name = "TotalAmount")]
        [Required(ErrorMessage = "Please enter Amount!")]
        public decimal TotalAmount { get; set; }

    }
}
