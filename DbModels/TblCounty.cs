using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblCounty
{
    public int CountyId { get; set; }

    public string CountyName { get; set; } = null!;

    public virtual ICollection<TblSubCounty> TblSubCounties { get; } = new List<TblSubCounty>();

    public virtual ICollection<TblWard> TblWards { get; } = new List<TblWard>();
}
