using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblValueChain
{
    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public int? ValueChainTypeId { get; set; }

    public int CategoryId { get; set; }

    public virtual TblValueChainCategory Category { get; set; } = null!;

    public virtual ICollection<TblFarmerParcel> TblFarmerParcels { get; } = new List<TblFarmerParcel>();
}
