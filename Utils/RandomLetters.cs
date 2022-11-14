namespace FarmerDB.DataAccess.Utils
{
	public static class RandomLetters
	{
		public static string GenerateRandomLetters(int stringLength)
		{
			var alphabet = "abcdefghijklmnopqrstuvwxyz";
			var random = new Random();

			string resultString = string.Empty;

			for (int i = 0; i < stringLength; i++)
			{
				int index = random.Next(alphabet.Length);
				resultString = resultString + alphabet.ElementAt(index);
			}

			return resultString;
		}
	}
}
