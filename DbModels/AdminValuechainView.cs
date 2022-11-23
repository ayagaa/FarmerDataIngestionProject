using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class AdminValuechainView
{
    public int CountyId { get; set; }

    public string CountyName { get; set; } = null!;

    public string FarmerId { get; set; } = null!;

    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public string? Gender { get; set; }
}
