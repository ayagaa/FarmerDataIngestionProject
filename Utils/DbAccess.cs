using FarmerDB.DataAccess.DbModels;

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
    }
}
