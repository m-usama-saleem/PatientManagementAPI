using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class UserPermission
{
    public string Id { get; set; } = null!;

    public string? Description { get; set; }

    public string? Module { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<GroupPermission> GroupPermissions { get; } = new List<GroupPermission>();
}
