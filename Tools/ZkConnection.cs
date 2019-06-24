using System;
using org.apache.zookeeper;
using StackExchange.Redis;

namespace WindowsStorageLayer.Tools
{
    public class ZkConnection
    {
//        public static readonly Lazy<ZooKeeper> ZkClient =
//            new Lazy<ZooKeeper>(() => new ZooKeeper("maplewish.cn:2181", 1500, null, true));
        
        public static readonly ZooKeeper ZkClient = new ZooKeeper("maplewish.cn:2181", 1500, null, true);

//        private static readonly int Counter = PeriodicTaskConfig.SetTimer();
        // TODO: specific machine on this
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("maplewish.cn:6400");
        
        public static IDatabaseAsync Db
        {
            get { return redis.GetDatabase(); }
        }

//        public static  async void Main(string[] args)
//        {
//           
//            var s = await ZkConnection.ZkClient.getDataAsync("/fs");
//            if (s == null)
//            {
//                Console.WriteLine("NIMASILE");
//            }
//        }
    }
}