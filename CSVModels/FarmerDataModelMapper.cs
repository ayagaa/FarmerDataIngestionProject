using CsvHelper.Configuration;

namespace FarmerDB.DataAccess.CSVModels
{
    public class FarmerDataModelMapper : ClassMap<FarmerDataModel>
    {
        public FarmerDataModelMapper()
        {
            #region Old Mapping
            //Map(m => m.FarmerId).Name("Farmer_Id");
            //Map(m => m.RecordDate).Name("Record_Date");
            //Map(m => m.FirstName).Name("First_Name");
            //Map(m => m.LastName).Name("Last_Name");
            //Map(m => m.NationalId).Name("National_Id");
            //Map(m => m.PhoneNo).Name("Phone_No");
            //Map(m => m.HouseholdSize).Name("Household_Size");
            //Map(m => m.Gender).Name("Gender");
            //Map(m => m.YearOfBirth).Name("YearOfBirth");
            //Map(m => m.EducationLevel).Name("Education_Level");
            //Map(m => m.Admin1Id).Name("Admin1Id");
            //Map(m => m.Admin3Id).Name("Admin3Id");
            //Map(m => m.PreferredAdvisoryFormat).Name("Preferred_Advisory_Format");
            //Map(m => m.PreferredAdvisoryLanguage).Name("Preferred_Advisory_language");
            //Map(m => m.PreferredAdvisoryTime).Name("Preferred_Advisory_Time");
            //Map(m => m.Latitude).Name("Latitude");
            //Map(m => m.Longitude).Name("Longitude");
            //Map(m => m.Acreage).Name("Acreage");
            //Map(m => m.NumberOfLivestock).Name("Number_of_Livestock");
            //Map(m => m.ProductionSystem).Name("Production_System");
            //Map(m => m.OwnershipType).Name("Ownership_Type");
            //Map(m => m.ValueChainNamePrimary).Name("Value_Chain_Name_Primary");
            //Map(m => m.ValueChainNameAlternative1).Name("Value_Chain_Name_Alternative1");
            //Map(m => m.ValueChainNameAlternative2).Name("Value_Chain_Name_Alternative2");
            //Map(m => m.ValueChainNameAlternative3).Name("Value_Chain_Name_Alternative3");
            //Map(m => m.ValueChainType).Name("Value_Chain_Type");
            //Map(m => m.Source).Name("Source");
            //Map(m => m.KCSAPBeneficiaries).Name("KCSAP Beneficiaries");
            #endregion

            Map(m => m.FarmerId).Name("FarmerId");
            Map(m => m.RecordDate).Name("RecordDate");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
            Map(m => m.NationalId).Name("NationalId");
            Map(m => m.PhoneNo).Name("PhoneNo");
            Map(m => m.HouseholdSize).Name("HouseholdSize");
            Map(m => m.Gender).Name("Gender");
            Map(m => m.YearOfBirth).Name("YearOfBirth");
            Map(m => m.EducationLevel).Name("EducationLevel");
            Map(m => m.Admin1Id).Name("Admin1Id");
            Map(m => m.Admin3Id).Name("Admin3Id");
            Map(m => m.PreferredAdvisoryFormat).Name("PreferredAdvisoryFormat");
            Map(m => m.PreferredAdvisoryLanguage).Name("PreferredAdvisorylanguage");
            Map(m => m.PreferredAdvisoryTime).Name("PreferredAdvisoryTime");
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Longitude).Name("Longitude");
            Map(m => m.Acreage).Name("Acreage");
            Map(m => m.NumberOfLivestock).Name("NumberofLivestock");
            Map(m => m.ProductionSystem).Name("ProductionSystem");
            Map(m => m.OwnershipType).Name("OwnershipType");
            Map(m => m.ValueChainNamePrimary).Name("ValueChainNamePrimary");
            Map(m => m.ValueChainNameAlternative1).Name("ValueChainNameAlternative1");
            Map(m => m.ValueChainNameAlternative2).Name("ValueChainNameAlternative2");
            Map(m => m.ValueChainNameAlternative3).Name("ValueChainNameAlternative3");
            Map(m => m.ValueChainType).Name("ValueChainType");
            Map(m => m.Source).Name("Source");
            Map(m => m.KCSAPBeneficiaries).Name("KCSAPBeneficiaries");
            Map(m => m.GroupName).Name("GroupName");
            Map(m => m.GroupType).Name("GroupType");
            Map(m => m.LivestockProductionSystem).Name("LivestockProductionSystem");
            Map(m => m.LivestockPrimaryValueChain).Name("LivestockPrimaryValueChain");
            Map(m => m.LivestockAlternativeValueChain).Name("LivestockAlternativeValueChain");
        }
    }
}
