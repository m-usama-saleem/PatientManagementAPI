using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class ProcedureList
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? DefaultUnit { get; set; }

    public decimal? Price { get; set; }

    public decimal? Tax { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; } = new List<InvoiceDetail>();
}
