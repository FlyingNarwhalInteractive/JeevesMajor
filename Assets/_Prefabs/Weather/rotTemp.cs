using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotTemp : MonoBehaviour
{

	public Vector3 speed;
	[SerializeField] ParticleSystem pa;
	private Gradient gradient = new Gradient();
	private Gradient grad1 = new Gradient();
	private Gradient grad2 = new Gradient();
	private Gradient grad3 = new Gradient();
	private Gradient grad4 = new Gradient();
	private Gradient grad5 = new Gradient();
	private float alpha = 1.0f;
	public float height;
	public float weight;
	public bool pulse;

	//custom colours
	[SerializeField] Color greeen = new Color(0.0f, 1.0f, 0.09f);
	[SerializeField] Color lightGreeen = new Color(0.94f, 1.0f, 0.0f);
	[SerializeField] Color bluue = new Color(0.098f, 0.1098f, 1.0f);
	[SerializeField] Color lightBluue = new Color(0.0f, 1.0f, 0.874f);
	[SerializeField] Color lightOrannge = new Color(1.0f, 0.9294f, 0.0f);
	[SerializeField] Color Orannge = new Color(1.0f, 0.725f, 0.0f);

	// Use this for initialization
	void Start()
	{

		pulse = false;
		height = 1.5f;
		weight = 10;
		pa = GetComponent<ParticleSystem>();
		
		var main = pa.main;
		var trails = pa.trails;
		trails.enabled = true;


		grad1.SetKeys(new GradientColorKey[] { new GradientColorKey(bluue, 0.0f), new GradientColorKey(lightBluue, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

		grad2.SetKeys(new GradientColorKey[] { new GradientColorKey(greeen, 0.0f), new GradientColorKey(lightGreeen, 1.0f) },
		new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

		grad3.SetKeys(new GradientColorKey[] { new GradientColorKey(Orannge, 0.0f), new GradientColorKey(lightOrannge, 1.0f) },
		new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

		grad4.SetKeys(new GradientColorKey[] { new GradientColorKey(Orannge, 0.0f), new GradientColorKey(lightOrannge, 0.45f), new GradientColorKey(lightGreeen, 0.46f), new GradientColorKey(greeen, 1.0f) },
        new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

		grad5.SetKeys(new GradientColorKey[] { new GradientColorKey(Orannge, 0.0f), new GradientColorKey(lightOrannge, 0.45f), new GradientColorKey(lightBluue, 0.46f), new GradientColorKey(bluue, 1.0f) },
        new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

	}

	public void Gradiant1(int i)
	{
		var trails = pa.trails;


		//hide debug commands


		//1 & 2 & 3
		if (i == 0)
		{
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(grad4, grad5);
		}
		//1 & 2
		else if (i == 1)
		{
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(grad1, grad2);
		}
		//1 & 3
		else if (i == 2)
		{
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(grad1, grad3);
		}
		//2 & 3
		else if (i == 3)
		{
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(grad2, grad3);
		}
		//1
		else if (i == 4)
		{
			gradient = grad1;
			print("CHANGE COLOURS");
			trails.colorOverTrail = gradient;
		}
		//2
		else if (i == 5)
		{
			gradient = grad2;
			print("CHANGE COLOURS");
			trails.colorOverTrail = gradient;
		}
		//3
		else if (i == 6)
		{
			gradient = grad3;
			print("CHANGE COLOURS");
			trails.colorOverTrail = gradient;
		}
	}

	// Update is called once per frame
	void Update()
	{
		/*
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			Gradiant1(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			Gradiant1(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			Gradiant1(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			Gradiant1(3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Gradiant1(4);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Gradiant1(5);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Gradiant1(6);
		}
		*/


		transform.Rotate(speed * Time.deltaTime);

		var t = pa.main;
		var e = pa.emission;
		if (pulse)
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
