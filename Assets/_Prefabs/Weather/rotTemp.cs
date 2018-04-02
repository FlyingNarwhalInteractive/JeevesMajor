using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotTemp : MonoBehaviour {

	public Vector3 speed;
	private ParticleSystem pa;
	public float height;
	public float weight;
	public bool pulse;
	// Use this for initialization
	void Start ()
	{
		pulse = false;
		height = 1.5f;
		weight = 10;
		pa = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(speed * Time.deltaTime);

		var t = pa.main;
		var e = pa.emission;
		if(pulse)
		{
        t.startLifetime = 0.25f;
		e.rateOverTime = 100;
			speed = new Vector3(0, 0, 1000);
		}
		else
		{
			t.startLifetime = 1;
			e.rateOverTime = 30;
			speed = new Vector3(0, 0, 200);
		}
		
	}
}
