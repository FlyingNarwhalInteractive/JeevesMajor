using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBaron : MonoBehaviour {

    public int rage;

	// Use this for initialization
	void Start ()
    {
        rage = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
       // print(rage);
	}

   public  void setRage(int trage)
    {
        rage += trage;
    }
  public   int getRage()
    {
        return rage;
    }
}
