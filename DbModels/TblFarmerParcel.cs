using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblFarmerParcel
{
    public int ParcelId { get; set; }

    public string FarmerId { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int ValueChainId { get; set; }

    public virtual TblFarmerProfile Farmer { get; set; } = null!;

    public virtual TblValueChain ValueChain { get; set; } = null!;
}
