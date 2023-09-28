using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class UsersInGroup
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GroupId { get; set; }

    public string AssignBy { get; set; } = null!;

    public DateTime AssignDate { get; set; }

    public virtual UserGroup Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
