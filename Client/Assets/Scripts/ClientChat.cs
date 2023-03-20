using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

public class ClientChat : MonoBehaviour
{

    //Create TCP Client
    TcpClient client;

    public string input;


    // Start is called before the first frame update
    void Start()
    {
        //Creating Tcp Socket
        client = new TcpClient();
        client.Connect("127.0.0.1", 7777);
    }

    // Update is called once per frame
    void Update()
    {
        // Encode chat
        byte[] chatBytes = Encoding.UTF8.GetBytes(input);

        //Send chat over socket
        NetworkStream stream = client.GetStream();
        stream.Write(chatBytes, 0, chatBytes.Length);
    }



    public void InputText(string input)
    {
        //Check for quit command
        if (input == "quit")
        {
            //Close Socket and exit
            client.Close();
            Application.Quit();
        }

        Debug.Log(input);
    }
}

