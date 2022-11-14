using CsvHelper;

namespace FarmerDB.DataAccess.Utils
{
    public static class ReadWriteCSVFiles
    {
        public static List<RootObject> ReadRootObjectFiles<RootObject>(string directory)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                if (directoryInfo.Exists)
                {
                    var resultRootObjects = new List<RootObject>();

                    var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;
                    CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(cultureInfo);
                    config.MissingFieldFound = null;
                    config.IgnoreBlankLines = true;
                    FileInfo[] files = directoryInfo.GetFiles("*.csv");

                    foreach (var file in files)
                    {
                        Console.WriteLine("Reading data from: {0}", file.Name);
                        using (TextReader reader = file.OpenText())
                        using (var csv = new CsvReader(reader, config))
                        {
                            csv.Read();
                            csv.ReadHeader();
                            var csvData = csv.GetRecords<RootObject>().ToList();
                            resultRootObjects.AddRange(csvData);
                        }
                    }
                    return resultRootObjects;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<RootObject>();
        }

        public static bool WriteRootObjectFile<RootObject>(string filePath, List<RootObject> rootObjects)
        {
            try
            {
                if (rootObjects.Count > 0)
                {
                    var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;
                    CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(cultureInfo);
                    config.MissingFieldFound = null;
                    using TextWriter writer = File.CreateText(filePath);
                    using var csv = new CsvWriter(writer, config);
                    csv.WriteRecords(rootObjects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
