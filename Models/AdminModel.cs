namespace FarmerDB.DataAccess.Models
{
	public class AdminModel
	{
		public int CountyId { get; set; }

		public string CountyName { get; set; }

		public int SubcountyId { get; set; }

		public string SubcountyName { get; set; }

		public int WardId { get; set; }

		public string WardName { get; set; }

		public float Score { get; set; }

		public override string ToString()
		{
			return string.Format("CountyId: {0}, CountyName: {1}, SubcountyId: {2}, " +
								 "SubcountyName: {3}, WardId: {4}, WardName: {5}",
				CountyId, CountyName, SubcountyId, SubcountyName, WardId, WardName);
		}
	}
}
