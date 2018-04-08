using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    public GameObject invCursor;
    public GameObject normalCursor;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(invCursor.activeSelf == true)
            {
                invCursor.SetActive(false);
                normalCursor.SetActive(true);
            }
            else
            {
                normalCursor.SetActive(false);
                invCursor.SetActive(true);
            }
        }

        

        
    }

   
}
