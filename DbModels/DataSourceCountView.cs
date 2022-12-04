using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class DataSourceCountView
{
    public string? DataSource { get; set; }

    public int? RecordCount { get; set; }
}
