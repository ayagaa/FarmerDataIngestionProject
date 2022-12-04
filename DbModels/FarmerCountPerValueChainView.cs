using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class FarmerCountPerValueChainView
{
    public int CountyId { get; set; }

    public string CountyName { get; set; } = null!;

    public int? TotalFarmers { get; set; }

    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public int? MaleCount { get; set; }

    public int? FemaleCount { get; set; }

    public int? OtherGenderCount { get; set; }
}
