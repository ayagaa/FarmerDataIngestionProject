using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class ValueChainFarmerCountView
{
    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public int? FarmerCount { get; set; }
}
