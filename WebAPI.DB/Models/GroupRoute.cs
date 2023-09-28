using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class GroupRoute
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public string RouteId { get; set; } = null!;

    public string AssignBy { get; set; } = null!;

    public DateTime AssignDate { get; set; }

    public virtual UserGroup Group { get; set; } = null!;

    public virtual UserRoute Route { get; set; } = null!;
}
