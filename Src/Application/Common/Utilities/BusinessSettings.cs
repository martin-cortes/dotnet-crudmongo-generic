namespace Application.Common.Utilities
{
    public class BusinessSettings
    {
        public string HealthChecksEndPoint { get; set; }
        public string LogLevelSink { get; set; }
        public string SinkCollectionName { get; set; }
        public int DocumentExpiration { get; set; }
    }
}