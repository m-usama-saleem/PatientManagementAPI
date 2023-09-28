using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class UserGroup
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<GroupPermission> GroupPermissions { get; } = new List<GroupPermission>();

    public virtual ICollection<GroupRoute> GroupRoutes { get; } = new List<GroupRoute>();

    public virtual ICollection<UsersInGroup> UsersInGroups { get; } = new List<UsersInGroup>();
}
