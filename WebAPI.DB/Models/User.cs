using System;
using System.Collections.Generic;

namespace WebAPI.DB.Models;

public partial class User
{
    public int Id { get; set; }

    public string LoginId { get; set; } = null!;

    public byte[]? Password { get; set; }

    public string? OauthKey { get; set; }

    public bool IsDeleted { get; set; }

    public string? ActivationCode { get; set; }

    public bool? IsEmailVerified { get; set; }

    public virtual ICollection<UsersInGroup> UsersInGroups { get; } = new List<UsersInGroup>();
}
