namespace FarmerDB.DataAccess.Utils
{
	public static class Identifiers
	{
		public static HashSet<string> GetUniqueIds(int itemCount)
		{
			HashSet<string> uniqueIds = new HashSet<string>();
			var random = new Random();
			var originalGUID = Guid.NewGuid().ToString();
			for (int i = 0; i < itemCount; i++)
			{
				originalGUID = Guid.NewGuid().ToString();
				uniqueIds.Add(random.Next(200).ToString().PadLeft(3, '0') + originalGUID.Substring(originalGUID.Length - 5, 4));
			}
			return uniqueIds;
		}
	}
}
