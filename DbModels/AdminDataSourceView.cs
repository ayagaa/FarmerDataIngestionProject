using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class AdminDataSourceView
{
    public int CountyId { get; set; }

    public string CountyName { get; set; } = null!;

    public int? RecordCount { get; set; }

    public string? DataSource { get; set; }
}
