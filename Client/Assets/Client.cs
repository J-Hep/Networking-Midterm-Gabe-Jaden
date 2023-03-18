using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lec04
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;


public class Client : MonoBehaviour
{
    public GameObject myCube;
     byte[] arrPos;
     public float culminativeTime, desiredTime = 0.25f;

    private static byte[] outBuffer = new byte[512], inBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket clientSoc;

    public static bool clientStarted = false;

    public static void StartClient(string userInputIP)
    {
        try
        {
            
            IPAddress ip = IPAddress.Parse(userInputIP); //IPv4 address from IPconfig
            remoteEP = new IPEndPoint(ip, 8888);

            Debug.Log("Connecting to: "+ ip);

            clientSoc = new Socket(AddressFamily.InterNetwork, 
                SocketType.Dgram, ProtocolType.Udp);



        } catch (Exception e)
        {
            Debug.Log("Exception: " + e.ToString());
        }

        clientStarted = true;

    }

    // Start is called before the first frame update
    void Start()
    {
       // StartClient();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(clientStarted)
        {

            culminativeTime += Time.deltaTime;

            float[] arrPos = {myCube.transform.position.x, myCube.transform.position.y,myCube.transform.position.z};

            //outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString()+ " " + myCube.transform.position.z.ToString());

            List<byte> byteList = new List<byte>();

            foreach (float f in arrPos)
            {
                byte[] t = BitConverter.GetBytes(f);
                byteList.AddRange(t);
            }
            byte[] byteArr = byteList.ToArray();

            outBuffer = byteArr;

            Debug.Log("Locally provided poss: " + myCube.transform.position);

        
            //Reversing

            //Local byte list convert
            Debug.Log("Local byte List Convert: " + BitConverter.ToSingle(byteArr,0) +" "+  BitConverter.ToSingle(byteArr,1*4)+" "+  BitConverter.ToSingle(byteArr,2*4));

            //Bonus portion for interval
            /*
            if(culminativeTime >= desiredTime){
                culminativeTime = 0;

                //Only sending when moving, unintentionally freezes server when not moving.
                if(myCube.GetComponent<Rigidbody>().velocity.x != 0 || myCube.GetComponent<Rigidbody>().velocity.z != 0 )
                {
                    clientSoc.SendTo(outBuffer, remoteEP);
                }
            }
            */
        }   

    }
}
