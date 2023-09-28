using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Models;
using static Azure.Core.HttpHeader;

namespace WebAPI.ViewModels.ViewModels
{
    public class InvoiceViewModel
    {
        public long Id { get; set; }

        public long PatientId { get; set; }

        public long DoctorId { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal NetPayable { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public string InvoiceStatus { get; set; }

        public string Notes { get; set; }

        public string AdditionalNotes { get; set; }

        public DoctorViewModel? Doctor { get; set; }
        public PatientViewModel? Patient { get; set; }

        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; } = new List<InvoiceDetailViewModel>();
        public List<InvoiceHistoryViewModel> InvoiceHistory { get; set; } = new List<InvoiceHistoryViewModel>();

        public InvoiceViewModel()
        {

        }
        public InvoiceViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            PatientId = invoice.PatientId;
            DoctorId = invoice.DoctorId;
            CreatedDate = invoice.CreatedDate;
            TotalAmount = Convert.ToDecimal(invoice.TotalAmount);
            Discount = Convert.ToDecimal(invoice.Discount);
            NetPayable = Convert.ToDecimal(invoice.NetPayable);
            PaidAmount = Convert.ToDecimal(invoice.PaidAmount);
            DueAmount = Convert.ToDecimal(invoice.DueAmount);
            InvoiceStatus = invoice.InvoiceStatus;
            Notes = invoice.Notes;
            AdditionalNotes = invoice.AdditionalNotes;
            foreach (var inv in invoice.InvoiceDetails)
            {
                InvoiceDetails.Add(new InvoiceDetailViewModel(inv));
            }
            if (invoice.Doctor != null)
            {
                Doctor = new DoctorViewModel
                {
                    Firstname = invoice.Doctor.FirstName,
                    Lastname = invoice.Doctor.LastName,
                    Address = invoice.Doctor.Address,
                    City = invoice.Doctor.City,
                    Country = invoice.Doctor.Country,
                    Designation = invoice.Doctor.Designation,
                    Contact = invoice.Doctor.Contact,
                    Email = invoice.Doctor.Email,

                };
            }
            if (invoice.Patient != null)
            {
                Patient = new PatientViewModel
                {
                    Firstname = invoice.Patient.FirstName,
                    Lastname = invoice.Patient.LastName,
                    Address = invoice.Patient.Address,
                    City = invoice.Patient.City,
                    Country = invoice.Patient.Country,
                    Contact = invoice.Patient.Contact,
                    Email = invoice.Patient.Email,
                    Dob = Convert.ToDateTime(invoice.Patient.Dob)
                };
            }
            foreach (var inv in invoice.PaymentHistories)
            {
                InvoiceHistory.Add(new InvoiceHistoryViewModel(inv));
            }
        }
    }

    public class InvoiceDetailViewModel
    {
        public long Id { get; set; }

        public long InvoiceId { get; set; }

        public long ProcedureId { get; set; }

        public decimal Qty { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public bool IsPercent { get; set; }
        public string ProcedureNotes { get; set; }
        public ProcedureList? Procedure { get; set; }

        public InvoiceDetailViewModel()
        {

        }
        public InvoiceDetailViewModel(InvoiceDetail invoiceDetail)
        {
            Id = invoiceDetail.Id;
            InvoiceId = invoiceDetail.InvoiceId;
            ProcedureId = invoiceDetail.ProcedureId;
            ProcedureNotes = invoiceDetail.ProceduralNotes;
            Qty = Convert.ToDecimal(invoiceDetail.Qty);
            Price = Convert.ToDecimal(invoiceDetail.Price);
            Discount = Convert.ToDecimal(invoiceDetail.Discount);
            IsPercent = Convert.ToBoolean(invoiceDetail.IsPercent);
            if (invoiceDetail.Procedure != null)
            {
                Procedure = new ProcedureList
                {
                    Id = invoiceDetail.Procedure.Id,
                    Name = invoiceDetail.Procedure.Name,
                    Description = invoiceDetail.Procedure.Description,
                    Price = invoiceDetail.Procedure.Price,
                };
            }
        }
    }

    public class InvoicePaymentViewModel
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal NetPayable { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public string InvoiceStatus { get; set; }

        public string Notes { get; set; }

        public string AdditionalNotes { get; set; }

        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; } = new List<InvoiceDetailViewModel>();
        public List<InvoiceHistoryViewModel> InvoiceHistory { get; set; } = new List<InvoiceHistoryViewModel>();

        public InvoicePaymentViewModel()
        {

        }
        public InvoicePaymentViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            CreatedDate = invoice.CreatedDate;
            TotalAmount = Convert.ToDecimal(invoice.TotalAmount);
            Discount = Convert.ToDecimal(invoice.Discount);
            NetPayable = Convert.ToDecimal(invoice.NetPayable);
            PaidAmount = Convert.ToDecimal(invoice.PaidAmount);
            DueAmount = Convert.ToDecimal(invoice.DueAmount);
            InvoiceStatus = invoice.InvoiceStatus;
            Notes = invoice.Notes;
            AdditionalNotes = invoice.AdditionalNotes;
            foreach (var inv in invoice.InvoiceDetails)
            {
                InvoiceDetails.Add(new InvoiceDetailViewModel(inv));
            }

        }
    }
}
