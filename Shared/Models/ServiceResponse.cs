namespace Shared.Models
{
    public class ServiceResponse
    {
        public TimeSpan ProcessingTime { get; set; }
        public TimeSpan Latency { get; set; }
        public string ServiceName { get; set; }
    }
}
