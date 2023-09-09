using System;
using System.Collections.Generic;

namespace WS_Caja6.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? BusinessName { get; set; }

    public string? TaxId { get; set; }

    public int? CompanyType { get; set; }

    public string? AccountNumber { get; set; }

    public string? Cbu { get; set; }
}
