using BenchmarkDotNet.Attributes;

namespace WindowsEventReader.Benchmark
{
    [RankColumn]
    public class QueryCreatorBenchmark
    {
        [Benchmark]
        public void StartDateQueryBenchmark()
        {
            DateTime startDate = new DateTime(2023, 10, 1, 12, 0, 0);
            string query = QueryCreator.StartDateQuery(startDate);
        }
        [Benchmark]
        public void EventIdQueryBenchmark()
        {
            string eventId = "1000";
            string query = QueryCreator.EventIdQuery(eventId);
        }
        [Benchmark]
        public void EventIdsQueryBenchmark()
        {
            IEnumerable<string> eventIds = new List<string> { "1000", "2000", "3000" };
            string query = QueryCreator.EventIdQuery(eventIds);
        }
    }
}
