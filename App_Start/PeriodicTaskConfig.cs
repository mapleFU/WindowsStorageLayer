using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using WindowsStorageLayer.Tools;
using Google.Protobuf;
using Microsoft.Ajax.Utilities;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
using Quartz;
using Quartz.Impl;

namespace WindowsStorageLayer
{
    public class PeriodicTaskConfig
    {
        // TODO: add checker for this
        public static bool Runned = false;
        
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        
                    
        private static System.Timers.Timer aTimer;
        private static string zkNode;
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var zkConn = ZkConnection.ZkClient;
            
            var server = new Server();
            // TODO: modify this host
            server.Host = "localhost";
            server.HttpPort = "8000";
            ulong s1, s2, s3;
            ZkConnection.GetDiskFreeSpaceEx("C:", out s1, out s2, out s3);

            server.TotalSpace = s2;
            server.AvailableSpace = s3;
            //            MemoryStream mstream = new MemoryStream();
            //            CodedOutputStream cstream = new CodedOutputStream(mstream);
            //            server.WriteTo(cstream);
            zkConn.setDataAsync(zkNode, server.ToByteArray()).GetAwaiter().GetResult();
            
//            zkConn.setDataAsync();
//            throw new NotImplementedException();
            Logger.Info("定时任务执行");
        }
        public static int SetTimer()
        {
            if (Runned)
            {
                return 0;
            }
            Logger.Info("SetTimer");
            
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            var server = new Server();
            // TODO: modify this host
            server.Host = "localhost";
            server.HttpPort = "8000";
            ulong s1, s2, s3;
            ZkConnection.GetDiskFreeSpaceEx("C:", out s1, out s2, out s3);
            
            server.TotalSpace = s2;
            server.AvailableSpace = s3;
            zkNode = ZkConnection.ZkClient.createAsync("/fs/Node", server.ToByteArray(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL).GetAwaiter().GetResult();
//            using (StreamWriter outputFile = new StreamWriter("WriteLines.txt", true))
//            {
//                outputFile.WriteLine(zkNode);
//            }
            Runned = true;
            return 0;
        }
        
    }
}