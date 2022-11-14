using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblSubCounty
{
    public int SubcountyId { get; set; }

    public string SubcountyName { get; set; } = null!;

    public int CountyId { get; set; }

    public virtual TblCounty County { get; set; } = null!;

    public virtual ICollection<TblWard> TblWards { get; } = new List<TblWard>();
}
