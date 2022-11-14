using FarmerDB.DataAccess.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace FarmerDB.DataAccess.Utils
{
    public static class AdminMatcher
    {
        private static string searchIndexDirectory = string.Empty;

        //private static string searchIndexDirectory =
        //    @"D:\PROJECTS\NARIGP\CSV-File-Generation\NARIGP.CSV.Generator\NARIGP.CSV.Generator\AdminIndex";

        private static FSDirectory fsDirectoryTemp;
        private static FSDirectory fsDirectory
        {
            get
            {
                if (fsDirectoryTemp == null) fsDirectoryTemp = FSDirectory.Open(new DirectoryInfo(searchIndexDirectory));
                if (IndexWriter.IsLocked(fsDirectoryTemp)) IndexWriter.Unlock(fsDirectoryTemp);
                var lockFilePath = Path.Combine(searchIndexDirectory, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return fsDirectoryTemp;
            }
        }

        public static void AddUpdateLuceneIndex(AdminModel adminDetail)
        {
            AddUpdateLuceneIndex(new List<AdminModel> { adminDetail }, searchIndexDirectory);
        }

        public static void AddUpdateLuceneIndex(List<AdminModel> adminDetails, string searchIndexDirectory)
        {
            // init lucene
            AdminMatcher.searchIndexDirectory = searchIndexDirectory;
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(fsDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entries if any)
                foreach (var adminDetail in adminDetails) AddToLuceneIndex(adminDetail, writer);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public static void ClearLuceneIndexRecord(int record_id)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(fsDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Id", record_id.ToString()));
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(fsDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(fsDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        public static IEnumerable<AdminModel> GetCounty(string adminQuery, string searchIndexDirectory)
        {
            AdminMatcher.searchIndexDirectory = searchIndexDirectory;
            return string.IsNullOrEmpty(adminQuery) ? new List<AdminModel>() : SearchCounty(adminQuery);
        }

        public static IEnumerable<AdminModel> GetWard(string adminQuery, string searchIndexDirectory)
        {
            AdminMatcher.searchIndexDirectory = searchIndexDirectory;
            return string.IsNullOrEmpty(adminQuery) ? new List<AdminModel>() : SearchWard(adminQuery);
        }

        private static void AddToLuceneIndex(AdminModel adminDetail, IndexWriter writer)
        {
            // remove older index entry
            //var wardName = new TermQuery(new Term("Email", adminDetail.Email.ToString()));
            //writer.DeleteDocuments(wardName);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("CountyId", adminDetail.CountyId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("CountyName", adminDetail.CountyName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("SubcountyId", adminDetail.SubcountyId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("SubcountyName", adminDetail.SubcountyName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("WardId", adminDetail.WardId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("WardName", adminDetail.WardName, Field.Store.YES, Field.Index.ANALYZED));
            //doc.Add(new Field("Description", sampleData.Description, Field.Store.YES, Field.Index.ANALYZED));

            // add entry to index
            writer.AddDocument(doc);
        }

        private static AdminModel MapLuceneDocumentToAdminModel(Document doc, float score)
        {
            try
            {
                if (doc != null && !string.IsNullOrEmpty(doc.Get("WardName")))
                {
                    return new AdminModel
                    {
                        CountyId = int.Parse(doc.Get("CountyId")),
                        CountyName = doc.Get("CountyName"),
                        SubcountyId = int.Parse(doc.Get("SubcountyId")),
                        SubcountyName = doc.Get("SubcountyName"),
                        WardId = int.Parse(doc.Get("WardId")),
                        WardName = doc.Get("WardName"),
                        Score = score,
                    };
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
            }

            return new AdminModel();
        }

        private static IEnumerable<AdminModel> MapLuceneToAdminModels(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            var scoreDocs = hits.ToList();
            var docScore = scoreDocs.Select(doc => searcher.Doc(doc.Doc));
            return scoreDocs.Select(hit => MapLuceneDocumentToAdminModel(searcher.Doc(hit.Doc), hit.Score))
                .Where(hit => !string.IsNullOrEmpty(hit.CountyName)).ToList();
        }

        //private static Query ParseQuery(string searchQuery, QueryParser parser)
        //{
        //    Query query;
        //    try
        //    {
        //        query = parser.Parse(searchQuery.Trim());

        //    }
        //    catch (ParseException)
        //    {
        //        query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
        //    }
        //    return query;
        //}

        private static Query CountyParseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                parser.FuzzyMinSim = 0.2f;
                query = parser.Parse(searchQuery.Trim());

            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static Query WardParseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                parser.FuzzyMinSim = 0.1f;
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static IEnumerable<AdminModel> SearchCounty(string adminQuery)
        {
            // validation
            if (string.IsNullOrEmpty(adminQuery.Replace("*", "").Replace("?", ""))) return new List<AdminModel>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(fsDirectory, false))
            {
                var hitsLimit = 100;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                // search by single field
                var parser = new QueryParser(Version.LUCENE_30, "CountyName", analyzer);
                //var parser = new MultiFieldQueryParser
                //    (Version.LUCENE_30, new[] { "CountyName" }, analyzer);
                //var parser = new MultiFieldQueryParser
                //    (Version.LUCENE_30, new[] { "WardName", "SubcountyName", "CountyName" }, analyzer);
                var query = CountyParseQuery(adminQuery, parser);
                var hits = searcher.Search(query, null, hitsLimit, Sort.INDEXORDER).ScoreDocs;
                var results = MapLuceneToAdminModels(hits, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }




        private static IEnumerable<AdminModel> SearchWard(string adminQuery)
        {
            // validation
            if (string.IsNullOrEmpty(adminQuery.Replace("*", "").Replace("?", ""))) return new List<AdminModel>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(fsDirectory, false))
            {
                var hitsLimit = 500;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                // search by single field
                //var parser = new MultiFieldQueryParser
                //    (Version.LUCENE_30, new[] { "WardName" }, analyzer);
                //var parser = new QueryParser(Version.LUCENE_30, "WardName", analyzer);
                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, new[] { "WardName", "CountyName", "CountyId" }, analyzer);
                var query = WardParseQuery(adminQuery, parser);
                var hits = searcher.Search(query, null, hitsLimit, Sort.INDEXORDER).ScoreDocs;
                var results = MapLuceneToAdminModels(hits, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }

    }
}
