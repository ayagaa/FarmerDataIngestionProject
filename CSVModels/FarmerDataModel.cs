namespace FarmerDB.DataAccess.CSVModels
{
    public class FarmerDataModel
    {
        public string FarmerId { get; set; }
        public string RecordDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string PhoneNo { get; set; }
        public string HouseholdSize { get; set; }
        public string Gender { get; set; }
        public int YearOfBirth { get; set; } = 1000;
        public string EducationLevel { get; set; }
        public string Admin1Id { get; set; }
        public string Admin3Id { get; set; }
        public string PreferredAdvisoryFormat { get; set; }
        public string PreferredAdvisoryLanguage { get; set; }
        public string PreferredAdvisoryTime { get; set; }
        public double? Latitude { get; set; } = 0;
        public double? Longitude { get; set; } = 0;
        public double? Acreage { get; set; } = 0;
        public double NumberOfLivestock { get; set; } = 0;
        public string ProductionSystem { get; set; }
        public string OwnershipType { get; set; }
        public string ValueChainNamePrimary { get; set; }
        public string ValueChainNameAlternative1 { get; set; }
        public string ValueChainNameAlternative2 { get; set; }
        public string ValueChainNameAlternative3 { get; set; }
        public string ValueChainType { get; set; }
        public string Source { get; set; }
        public string KCSAPBeneficiaries { get; set; }

        public string CountyId { get; set; }
        public string CountyName { get; set; }

        public string SubcountyId { get; set; }

        public string SubcountyName { get; set; }

        public string WardId { get; set; }

        public string WardName { get; set; }

        public override string ToString()
        {
            return string.Format("FirstName: {0}, LastName: {1}, PrimaryValueChain: {2}", FirstName, LastName, ValueChainNamePrimary);
        }
    }


}
