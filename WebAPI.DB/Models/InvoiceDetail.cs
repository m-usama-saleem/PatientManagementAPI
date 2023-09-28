using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class InvoiceDetail
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public long ProcedureId { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public bool? IsPercent { get; set; }

    public string? ProceduralNotes { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual ProcedureList Procedure { get; set; } = null!;
}
