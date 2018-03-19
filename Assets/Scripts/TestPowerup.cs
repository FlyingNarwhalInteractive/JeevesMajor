using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPowerup : MonoBehaviour
{

    public float jeevesSpeed;

    public GameObject jeeves;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        jeeves.GetComponent<NavMeshAgent>().speed = jeevesSpeed;
    }


    public void OnTriggerEnter(Collider jeeves)
    {
        jeevesSpeed++;
        Destroy(gameObject);
    }

}
