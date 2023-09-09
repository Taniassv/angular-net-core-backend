using System;
using System.Collections.Generic;

namespace WS_Caja6.Models;

public partial class SystemRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsBackOfficeRol { get; set; }

    public virtual ICollection<SystemAccount> SystemAccounts { get; set; } = new List<SystemAccount>();
}
