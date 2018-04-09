using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    //public GameObject invCursor;
   // public GameObject normalCursor;
    public Text speedText; 
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale -= 1;
        }

        speedText.text = Time.timeScale.ToString();
        

        
    }

   
}
