using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPServer: MonoBehaviour
{
    public static byte[] buffer;
    public static IPHostEntry hostInfo ;
    public static IPAddress ip;
    public static IPEndPoint localEP;
    public static Socket server;
    public static EndPoint remoteClient;

    public GameObject cube;
    public float posX, posZ;

    public static void StartServer()
    {
        buffer = new byte[512];
        hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        ip = IPAddress.Parse("127.0.0.1");//hostInfo.AddressList[1]; //IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}  IP: {1}", hostInfo.HostName, ip);
        Debug.Log("Server name: {0}  IP: {1} "+ hostInfo.HostName+ ip);
        localEP = new IPEndPoint(ip, 8888);
        server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        remoteClient = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            server.Bind(localEP);
            Console.WriteLine("Waiting for data....");
            Debug.Log("Waiting for data....");

            //server shutdown
        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Debug.Log(e.ToString());
        }
    }
    
    private void Start() {
        StartServer();

    }

    private void Update(){
        
     int recv = server.ReceiveFrom(buffer, ref remoteClient);

    //Who knew a single and a float were the same thing. So much trial n error to get this its not even funny.
     Debug.Log( BitConverter.ToSingle(buffer,0) +" "+  BitConverter.ToSingle(buffer,1*4)+" "+  BitConverter.ToSingle(buffer,2*4));
            
     cube.transform.position = new Vector3(BitConverter.ToSingle(buffer,0),BitConverter.ToSingle(buffer,1*4),BitConverter.ToSingle(buffer,2*4));

    }

}