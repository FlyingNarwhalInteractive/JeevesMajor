using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour {

	int x = 500;
	int y = 10;
	int multi = 0;
	bool speedUp = false;
	bool slowDown = false;
	float timer = 0.0f;
	public ParticleSystem pa;
	public bool spawn;
	Animation ani;
	Camera c;
	// Use this for initialization
	void Start ()
	{
		spawn = false;
		ani = gameObject.GetComponent<Animation>();
		c = Camera.main;
	}



	// Update is called once per frame
	void Update ()
	{
	


		if (Input.GetKeyDown(KeyCode.Q))
		{
			speedUp = true;
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			speedUp = false;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			slowDown = true;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			slowDown = false;
		}

		

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ani.wrapMode = WrapMode.Once;
			ani.Play();
			Spawn();
		}
	}

	public void Spawn()
	{
		

		ParticleSystem t = Instantiate(pa);
		Destroy(t.transform.gameObject, 2.0f);
		multi++;
		Invoke("count", 0.25f);

	}

	void count()
	{
		gameObject.GetComponent<Text>().text = "X" + multi;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSecondsRealtime(1.0f);
	}
}
