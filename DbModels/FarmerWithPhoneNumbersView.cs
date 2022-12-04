using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class FarmerWithPhoneNumbersView
{
    public string FarmerId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? PhoneNo { get; set; }

    public int WardId { get; set; }

    public string WardName { get; set; } = null!;

    public int SubcountyId { get; set; }

    public string SubcountyName { get; set; } = null!;

    public int CountyId { get; set; }

    public string CountyName { get; set; } = null!;

    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public string? ValueChainPriority { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? DataSource { get; set; }
}
