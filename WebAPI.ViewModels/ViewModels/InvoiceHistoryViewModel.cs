using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Models;

namespace WebAPI.ViewModels.ViewModels
{
    public class InvoiceHistoryViewModel
    {
        public long Id { get; set; }

        public long InvoiceId { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }

        public InvoiceHistoryViewModel()
        {

        }
        public InvoiceHistoryViewModel(PaymentHistory payment)
        {
            Id = payment.Id;
            InvoiceId = payment.InvoiceId;
            PaidAmount = Convert.ToDecimal(payment.PaidAmount);
            TotalPaid = Convert.ToDecimal(payment.TotalPaid);
            Balance = Convert.ToDecimal(payment.Balance);
            CreatedDate = Convert.ToDateTime(payment.CreatedDate);
        }
    }
}
