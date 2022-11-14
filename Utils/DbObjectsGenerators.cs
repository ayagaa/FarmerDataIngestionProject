using FarmerDB.DataAccess.CSVModels;
using FarmerDB.DataAccess.DbModels;

namespace FarmerDB.DataAccess.Utils
{
    public static class DbObjectsGenerators
    {
        private static float alphanumCheck;
        private static float latitude = 0;
        private static float longitude = 0;
        public static List<TblFarmerProfile> FarmerProfiles = new List<TblFarmerProfile>();
        public static List<TblFarmerParcel> FarmerParcels = new List<TblFarmerParcel>();
        public static void GenerateDbObjects(List<FarmerDataModel> farmerData, string adminIndexFolder)
        {
            var identifiers = Identifiers.GetUniqueIds(farmerData.Count + (farmerData.Count * 2)).ToList();
            var valueChains = DbAccess.GetValueChains();
            for (int index = 0; index < farmerData.Count; index++)
            {
                if (!string.IsNullOrEmpty(farmerData[index].Admin1Id) &&
                    !string.IsNullOrEmpty(farmerData[index].Admin3Id))
                {
                    var county =
                        AdminMatcher.GetCounty(farmerData[index].Admin1Id, adminIndexFolder).MaxBy(r => r.Score);

                    if (county != null)
                    {

                        var admins = AdminMatcher.GetWard(farmerData[index].Admin3Id + " " + county.CountyName + " " + county.CountyId, adminIndexFolder);

                        if (admins?.Count() > 0)
                        {

                            try
                            {
                                latitude = 0;
                                longitude = 0;
                                var ward = admins?.Where(r => r.CountyId == county.CountyId)?.ToList().MaxBy(s => s.Score);
                                //Console.WriteLine(ward);
                                if (ward != null && !string.IsNullOrEmpty(ward.WardName))
                                {
                                    var farmerId = county.CountyId.ToString().PadLeft(2, '0') +
                                                   ward.SubcountyId.ToString().PadLeft(3, '0') +
                                                   ward.WardId.ToString().PadLeft(4, '0') +
                                                   identifiers[index];

                                    if (float.TryParse(farmerId, out alphanumCheck))
                                    {
                                        var characters = RandomLetters.GenerateRandomLetters(3);
                                        farmerId = farmerId.Substring(0, farmerId.Length - 3) + characters;
                                    }

                                    farmerData[index].FarmerId = farmerId;
                                    farmerData[index].CountyId = county.CountyId.ToString();
                                    farmerData[index].CountyName = county.CountyName;
                                    farmerData[index].WardId = ward.WardId.ToString();
                                    farmerData[index].WardName = ward.WardName;

                                    FarmerProfiles.Add(new TblFarmerProfile()
                                    {
                                        FarmerId = farmerId,
                                        FirstName = farmerData[index].FirstName?.Trim(),
                                        LastName = farmerData[index].LastName?.Trim(),
                                        NationalId = farmerData[index].NationalId?.Trim(),
                                        PhoneNo = farmerData[index].PhoneNo?.Trim(),
                                        HouseholdSize = farmerData[index]?.HouseholdSize,
                                        Gender = farmerData[index].Gender?.Trim(),
                                        Yob = farmerData[index].YearOfBirth,
                                        EducationLevel = farmerData[index].EducationLevel?.Trim(),
                                        RecordDate = farmerData[index].RecordDate?.Trim(),
                                        CountyId = county.CountyId,
                                        WardId = ward.WardId,
                                        PreferredAdvisoryFormat = farmerData[index].PreferredAdvisoryFormat?.Trim(),
                                        PreferredAdvisoryLanguage = farmerData[index].PreferredAdvisoryLanguage?.Trim(),
                                        PreferredAdvisoryTime = farmerData[index].PreferredAdvisoryTime?.Trim(),
                                        DataSource = farmerData[index].Source?.Trim(),
                                        KcsapBeneficiary = farmerData[index].KCSAPBeneficiaries?.Trim(),
                                    });

                                    var valueChainPrimary = valueChains?.FirstOrDefault(v =>
                                        v.ValueChainName.ToLower().Trim() ==
                                        farmerData[index]?.ValueChainNamePrimary?.ToLower()?.Trim());
                                    var valueChainAlternative1 = valueChains?.FirstOrDefault(v =>
                                        v.ValueChainName.ToLower().Trim() ==
                                        farmerData[index]?.ValueChainNameAlternative1?.ToLower()?.Trim());
                                    var valueChainAlternative2 = valueChains?.FirstOrDefault(v =>
                                        v.ValueChainName.ToLower().Trim() ==
                                        farmerData[index]?.ValueChainNameAlternative2?.ToLower()?.Trim());
                                    var valueChainAlternative3 = valueChains?.FirstOrDefault(v =>
                                        v.ValueChainName.ToLower().Trim() ==
                                        farmerData[index]?.ValueChainNameAlternative3?.ToLower()?.Trim());

                                    if (valueChainPrimary != null)
                                        FarmerParcels.Add(new TblFarmerParcel()
                                        {
                                            FarmerId = farmerId,
                                            Latitude = farmerData[index].Latitude,
                                            Longitude = farmerData[index].Longitude,
                                            ValueChainId = valueChainPrimary.ValueChainId
                                        });

                                    if (valueChainAlternative1 != null)
                                        FarmerParcels.Add(new TblFarmerParcel()
                                        {
                                            FarmerId = farmerId,
                                            Latitude = farmerData[index].Latitude,
                                            Longitude = farmerData[index].Longitude,
                                            ValueChainId = valueChainAlternative1.ValueChainId
                                        });

                                    if (valueChainAlternative2 != null)
                                        FarmerParcels.Add(new TblFarmerParcel()
                                        {
                                            FarmerId = farmerId,
                                            Latitude = farmerData[index].Latitude,
                                            Longitude = farmerData[index].Longitude,
                                            ValueChainId = valueChainAlternative2.ValueChainId
                                        });

                                    if (valueChainAlternative3 != null)
                                        FarmerParcels.Add(new TblFarmerParcel()
                                        {
                                            FarmerId = farmerId,
                                            Latitude = farmerData[index].Latitude,
                                            Longitude = farmerData[index].Longitude,
                                            ValueChainId = valueChainAlternative3.ValueChainId
                                        });
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                }
            }
        }
    }
}
