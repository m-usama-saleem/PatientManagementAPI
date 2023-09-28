using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class PaymentHistory
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? TotalPaid { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
