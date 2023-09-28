using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class Patient
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
}
