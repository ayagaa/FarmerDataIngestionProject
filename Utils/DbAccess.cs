using FarmerDB.DataAccess.DbModels;
using Microsoft.Data.SqlClient;

namespace FarmerDB.DataAccess.Utils
{
    public static class DbAccess
    {
        public static List<TblValueChain> GetValueChains()
        {
            var valueChains = new List<TblValueChain>();
            try
            {
                using var dbContext = new FarmerDatabaseContext();
                var data = dbContext.TblValueChains;
                if (data != null && data.Any())
                    valueChains.AddRange(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while fetching Value Chains");
                Console.WriteLine(e);
            }
            return valueChains;
        }

        public static bool InsertFarmerProfile(TblFarmerProfile farmerProfile)
        {
            try
            {
                using var dbContext = new FarmerDatabaseContext();
                //while (dbContext.TblFarmerProfiles.Any(p => p.FarmerId == farmerProfile.FarmerId))
                //{
                //    var characters = RandomLetters.GenerateRandomLetters(3);
                //    farmerProfile.FarmerId = farmerProfile.FarmerId.Substring(0, farmerProfile.FarmerId.Length - 3) + characters;
                //}
                dbContext.Add(farmerProfile);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while inserting farmer profile");
                Console.WriteLine(e);
            }
            return false;
        }

        public static bool InsertFarmerParcel(TblFarmerParcel farmerParcel)
        {
            try
            {
                using var dbContext = new FarmerDatabaseContext();
                dbContext.Add(farmerParcel);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while inserting farmer parcel");
                Console.WriteLine(e);
            }
            return false;
        }

        public static bool InsertFarmerWithGroup(TblFarmerGroup farmerGroup)
        {
            try
            {
                using var dbContext = new FarmerDatabaseContext();
                dbContext.Add(farmerGroup);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while inserting farmer with group");
                Console.WriteLine(e);
            }

            return false;
        }

        public static bool InsertFarmerRecord(TblFarmerProfile farmerProfile,
            List<TblFarmerParcel> farmerParcels, List<TblFarmerGroup> farmerGroups)
        {
            try
            {
                int attemptsCount = 0;
                var parcels = farmerParcels.ToList();
                var groups = farmerGroups.ToList();
                while (attemptsCount < 5)
                {
                    try
                    {
                        using var dbContext = new FarmerDatabaseContext();
                        dbContext.TblFarmerProfiles.Add(farmerProfile);
                        if (parcels != null && parcels.Any())
                            dbContext.AddRange(parcels);
                        if (groups != null && groups.Any())
                            dbContext.AddRange(groups);

                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        if (e.Number == 2627)
                        {
                            var characters = RandomLetters.GenerateRandomLetters(4);
                            farmerProfile.FarmerId = farmerProfile.FarmerId.Substring(0, farmerProfile.FarmerId.Length - 4) + characters;

                            parcels?.Clear();
                            groups?.Clear();

                            if (farmerParcels != null && farmerParcels.Any())
                                parcels?.AddRange(farmerParcels.Select(p => new TblFarmerParcel()
                                {
                                    FarmerId = farmerProfile.FarmerId,
                                    Latitude = p.Latitude,
                                    Longitude = p.Longitude,
                                    NumberOfLivestock = p.NumberOfLivestock,
                                    OwnershipType = p.OwnershipType,
                                    ProductionSystem = p.ProductionSystem,
                                    ValueChainId = p.ValueChainId,
                                    ValueChainPriority = p.ValueChainPriority
                                }).ToArray());
                            if (farmerGroups != null && farmerGroups.Any())
                                groups?.AddRange(farmerGroups.Select(g => new TblFarmerGroup()
                                {
                                    FarmerId = farmerProfile.FarmerId,
                                    GroupName = g.GroupName,
                                    GroupType = g.GroupType
                                }).ToArray());
                        }
                        else
                        {
                            Console.WriteLine("Error while inserting farmer profile: {0}", farmerProfile.FarmerId);
                            Console.WriteLine(e);
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    attemptsCount++;
                }

                return true;
            }
            catch (Exception e)
            {

            }

            return false;
        }
    }
}
