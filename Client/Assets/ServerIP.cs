using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ServerIP : MonoBehaviour
{

    public string serverIP = "127.0.0.1";
    public string serverPort = "8888";
    public GameObject inputTextIP,inputTextPort, connectButton;

    public void SetServerIP(){
        serverIP = inputTextIP.GetComponent<InputField>().text;
        serverPort = inputTextPort.GetComponent<InputField>().text;
        connectButton.GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = "Connected to: " + serverIP + ":"+serverPort;


    

        Client.StartClient(serverIP,int.Parse(serverPort));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
