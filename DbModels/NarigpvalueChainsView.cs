using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class NarigpvalueChainsView
{
    public int ValueChainId { get; set; }

    public string ValueChainName { get; set; } = null!;

    public string? DataSource { get; set; }
}
