using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class Invoice
{
    public long Id { get; set; }

    public long PatientId { get; set; }

    public long DoctorId { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? NetPayable { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? DueAmount { get; set; }

    public string? InvoiceStatus { get; set; }

    public string? Notes { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; } = new List<InvoiceDetail>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PaymentHistory> PaymentHistories { get; } = new List<PaymentHistory>();
}
