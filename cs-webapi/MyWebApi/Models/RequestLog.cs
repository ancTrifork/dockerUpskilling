using System;

namespace MyWebApi.Models
{
    public class RequestLog
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Endpoint { get; set; } = default!;
    }
}