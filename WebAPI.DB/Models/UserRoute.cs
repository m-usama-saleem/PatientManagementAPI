using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class UserRoute
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<GroupRoute> GroupRoutes { get; } = new List<GroupRoute>();
}
