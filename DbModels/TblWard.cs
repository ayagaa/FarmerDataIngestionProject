using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblWard
{
    public int WardId { get; set; }

    public string WardName { get; set; } = null!;

    public int CountyId { get; set; }

    public int? SubcountyId { get; set; }

    public virtual TblCounty County { get; set; } = null!;

    public virtual TblSubCounty? Subcounty { get; set; }

    public virtual ICollection<TblFarmerProfile> TblFarmerProfiles { get; } = new List<TblFarmerProfile>();
}
