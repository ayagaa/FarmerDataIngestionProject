using System;
using System.Collections.Generic;

namespace FarmerDB.DataAccess.DbModels;

public partial class TblFarmerProfile
{
    public string FarmerId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? NationalId { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string? HouseholdSize { get; set; }

    public string? Gender { get; set; }

    public int? Yob { get; set; }

    public string? EducationLevel { get; set; }

    public string? RecordDate { get; set; }

    public int CountyId { get; set; }

    public int WardId { get; set; }

    public string? PreferredAdvisoryFormat { get; set; }

    public string? PreferredAdvisoryLanguage { get; set; }

    public string? PreferredAdvisoryTime { get; set; }

    public string? DataSource { get; set; }

    public string? KcsapBeneficiary { get; set; }

    public virtual TblCounty County { get; set; } = null!;

    public virtual ICollection<TblFarmerGroup> TblFarmerGroups { get; } = new List<TblFarmerGroup>();

    public virtual ICollection<TblFarmerParcel> TblFarmerParcels { get; } = new List<TblFarmerParcel>();

    public virtual TblWard Ward { get; set; } = null!;
}
