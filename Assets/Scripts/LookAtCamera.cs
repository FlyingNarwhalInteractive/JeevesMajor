using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () 
    {
        Vector3 current = transform.rotation.eulerAngles;
        transform.LookAt(Camera.main.transform.position * -1.0f);

        current.y = transform.eulerAngles.y - 37.0f;
        current.x = 45.0f;

        transform.rotation = Quaternion.Euler(current);

	}
}
