using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{

   

    public int timeSpeed;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = timeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timeSpeed++;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            timeSpeed--;
        }

        Time.timeScale = timeSpeed;
        //if (Input.GetKeyDown("SpeedUp"))
        //{
        //testSpeed++;
        //}
    }

   
}
