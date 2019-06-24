using System;

namespace WindowsStorageLayer.Models
{
    public class RedisUuidMessage
    {
        public Int64 Size { get; set; }
        public string Uid { get; set; }
        public string FileHash { get; set; }
    }
}