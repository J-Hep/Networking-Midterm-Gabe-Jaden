using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{

  public GameObject textToChange, cube;

    // Start is called before the first frame update
    void Start()
    {
        textToChange.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    textToChange.GetComponent<Text>().text = "X: "+cube.transform.position.x + "\n Y: "+cube.transform.position.y + "\n Z: "+cube.transform.position.z; 



    }
}
