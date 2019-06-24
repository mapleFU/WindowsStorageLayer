using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WindowsStorageLayer.Tools;
using Google.Protobuf;
using NLog;
using org.apache.zookeeper;

namespace WindowsStorageLayer
{
    
    public class RouteConfig
    {
//    // TODO: add checker for this
//        public static bool Runned = false;
//                
//        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
//                
//                            
//        private static System.Timers.Timer aTimer;
//        private static string zkNode;
//        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
//        {
//            var zkConn = ZkConnection.ZkClient;
//                            
//            var server = new Server();
//            // TODO: modify this host
//            server.Host = "localhost";
//            server.HttpPort = "8000";
//            server.TotalSpace = 0;
//            server.AvailableSpace = 0;
//            //            MemoryStream mstream = new MemoryStream();
//            //            CodedOutputStream cstream = new CodedOutputStream(mstream);
//            //            server.WriteTo(cstream);
//            zkConn.setDataAsync(zkNode, server.ToByteArray()).GetAwaiter().GetResult();
//                            
//            //            zkConn.setDataAsync();
//            //            throw new NotImplementedException();
//            Logger.Info("定时任务执行");
//        }
        public static void RegisterRoutes(RouteCollection routes)
        {
//            if (Runned)
//            {
//                return;
//            }
//            Logger.Info("SetTimer");
//            
//            // Create a timer with a two second interval.
//            aTimer = new System.Timers.Timer(2000);
//            // Hook up the Elapsed event for the timer. 
//            aTimer.Elapsed += OnTimedEvent;
//            aTimer.AutoReset = true;
//            aTimer.Enabled = true;
//            var server = new Server();
//            // TODO: modify this host
//            server.Host = "localhost";
//            server.HttpPort = "8000";
//            server.TotalSpace = 0;
//            server.AvailableSpace = 0;
//            zkNode = ZkConnection.ZkClient.createAsync("/fs/Node", server.ToByteArray(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).GetAwaiter().GetResult();
//            using (StreamWriter outputFile = new StreamWriter("WriteLines.txt", true))
//            {
//                outputFile.WriteLine(zkNode);
//            }
//            Runned = true;
//            return;
        }
    }
}
