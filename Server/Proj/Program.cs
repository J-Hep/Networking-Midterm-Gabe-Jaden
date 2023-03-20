//using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Net.Sockets;
using System.Text;
using System.Net;


public class UDPServer
{


    public static byte[] UDPbufferOne, UDPbufferTwo;
    public static IPHostEntry UDPhostInfoOne, UDPhostInfoTwo;
    public static IPAddress ipOne, ipTwo;
    public static IPEndPoint UDPlocalEPOne, UDPlocalEPTwo;
    public static Socket UDPserverOne, UDPserverTwo;
    public static EndPoint UDPremoteClientOne, UDPremoteClientTwo;

    public float posX, posZ;
    public static bool clientOneConnected = false, clientTwoConnected = false;

    public static void StartUDPServerOne()
    {
        UDPbufferOne = new byte[512];
        UDPhostInfoOne = Dns.GetHostEntry(Dns.GetHostName());
        ipOne = IPAddress.Parse("127.0.0.1");//hostInfo.AddressList[1]; //IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}  IP: {1}", UDPhostInfoOne.HostName, ipOne);

        UDPlocalEPOne = new IPEndPoint(ipOne, 8888);
        UDPserverOne = new Socket(ipOne.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        UDPremoteClientOne = new IPEndPoint(IPAddress.Any, 0);

        /////////////////////////////////////////////////////////////////////////////////


        try
        {
            UDPserverOne.Bind(UDPlocalEPOne);
            Console.WriteLine("Waiting for client one data....");
            clientOneConnected = true;

            //server shutdown
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());

        }



    }

    public static void StartUDPServerTwo()
    {
        /*
        bufferOne = new byte[512];
        hostInfoOne = Dns.GetHostEntry(Dns.GetHostName());
        ipOne = IPAddress.Parse("127.0.0.1");//hostInfo.AddressList[1]; //IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}  IP: {1}", hostInfoOne.HostName, ipOne);
 
        localEPOne = new IPEndPoint(ipOne, 8888);
        serverOne = new Socket(ipOne.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        remoteClientOne = new IPEndPoint(IPAddress.Any, 0);
        */
        /////////////////////////////////////////////////////////////////////////////////

        UDPbufferTwo = new byte[512];
        UDPhostInfoTwo = Dns.GetHostEntry(Dns.GetHostName());
        ipTwo = IPAddress.Parse("127.0.0.1");//hostInfo.AddressList[1]; //IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}  IP: {1}", UDPhostInfoTwo.HostName, ipTwo);

        UDPlocalEPTwo = new IPEndPoint(ipTwo, 8889);
        UDPserverTwo = new Socket(ipTwo.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        UDPremoteClientTwo = new IPEndPoint(IPAddress.Any, 0);


        try
        {
            // serverOne.Bind(localEPOne);
            //Console.WriteLine("Waiting for client one data....");

            UDPserverTwo.Bind(UDPlocalEPTwo);
            Console.WriteLine("Waiting for client Two data....");
            clientTwoConnected = true;


            //server shutdown
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());

        }


    }

    public static void recvUpdate()
    {
        if (clientOneConnected)
        {
            int recvFromClientOne = UDPserverOne.ReceiveFrom(UDPbufferOne, ref UDPremoteClientOne);
            //  Console.WriteLine(UDPremoteClientOne.ToString()+ " ClientOne: " + BitConverter.ToSingle(UDPbufferOne,0) +" "+  BitConverter.ToSingle(UDPbufferOne,1*4)+" "+  BitConverter.ToSingle(UDPbufferOne,2*4));
            Console.WriteLine("Received " + BitConverter.ToSingle(UDPbufferOne, 0) + ", " + BitConverter.ToSingle(UDPbufferOne, 1 * 4) + ", " + BitConverter.ToSingle(UDPbufferOne, 2 * 4) + " from [C1 " + UDPremoteClientOne.ToString() + "]");

        }

        if (clientTwoConnected)
        {
            int recvFromClientTwo = UDPserverTwo.ReceiveFrom(UDPbufferTwo, ref UDPremoteClientTwo);
            // Console.WriteLine(UDPremoteClientTwo.ToString()+" ClientTwo: " + BitConverter.ToSingle(UDPbufferTwo,0) +" "+  BitConverter.ToSingle(UDPbufferTwo,1*4)+" "+  BitConverter.ToSingle(UDPbufferTwo,2*4));
            Console.WriteLine("Received " + BitConverter.ToSingle(UDPbufferTwo, 0) + ", " + BitConverter.ToSingle(UDPbufferTwo, 1 * 4) + ", " + BitConverter.ToSingle(UDPbufferTwo, 2 * 4) + " from [C2 " + UDPremoteClientTwo.ToString() + "]");
        }

        if (clientOneConnected && clientTwoConnected)
        {

        }



        Console.WriteLine("████████████████████████████████████████████████████████████████");

        //cube.transform.position = new Vector3(BitConverter.ToSingle(buffer,0),BitConverter.ToSingle(buffer,1*4),BitConverter.ToSingle(buffer,2*4));

    }

    static void Main(string[] args)
    {

        TCPServer.StartServer();


        StartUDPServerOne();
        StartUDPServerTwo();
    


        while (clientOneConnected && clientTwoConnected)
        {
            recvUpdate();
        }

    }

}

public class TCPServer
{
    public static void StartServer()
    {
        byte[] buffer = new byte[512];
        //Setup server

        Console.WriteLine("Gamer");
        //local host
        IPAddress ip = IPAddress.Parse("172.31.87.75");
        Console.WriteLine("Server IP: " + ip);

        IPEndPoint serverEP = new IPEndPoint(ip, 8888);

        Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            server.Bind(serverEP);
            server.Listen(1);

            //Accept Connections
            Console.WriteLine("Waiting for connections...");
            Socket client = server.Accept();

            Console.WriteLine("Client Connected.");

            IPEndPoint clientEp = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine("Client: {0} Port: {1} ", clientEp.Address, clientEp.Port);

            //Sending message to client
            byte[] msg = Encoding.ASCII.GetBytes("Hello Server.");
            client.Send(msg);

            ////////////////////////////////////////////
            bool sendEnabled = true;
            while (sendEnabled)
            {

                int recv = client.Receive(buffer);
                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, recv));
            }

            ////////////////////////////////////////////

            //releasing resources
            client.Shutdown(SocketShutdown.Both);
            client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    }

}