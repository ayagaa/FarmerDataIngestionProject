using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblFarmerGroup
{
    public int EntryId { get; set; }

    public string FarmerId { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public string? GroupType { get; set; }

    public virtual TblFarmerProfile Farmer { get; set; } = null!;
}
