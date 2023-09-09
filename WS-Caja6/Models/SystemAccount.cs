using System;
using System.Collections.Generic;

namespace WS_Caja6.Models;

public partial class SystemAccount
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string RefreshToken { get; set; }

    public virtual ICollection<SystemRole> SystemRoles { get; set; } = new List<SystemRole>();
}
