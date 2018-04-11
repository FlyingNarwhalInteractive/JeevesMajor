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

	private GameManager GMRef;

	// Use this for initialization
	void Start ()
	{
		GMRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		spawn = false;
		ani = gameObject.GetComponent<Animation>();
		c = Camera.main;
	}



	// Update is called once per frame
	void Update ()
	{
	}

	public void PlayMulti(int multi)
	{
		ani.wrapMode = WrapMode.Once;
		ani.Play();
		Spawn(multi);
	}

	public void Spawn(int multi)
	{
		ParticleSystem t = Instantiate(pa);
		Destroy(t.transform.gameObject, 2.0f);
		count();

	}

	void count()
	{
		gameObject.GetComponent<Text>().text = "X" + GMRef.multiplyer;
		gameObject.GetComponent<Text>().fontSize = 100 + (GMRef.multiplyer * 2);
	}

	IEnumerator Wait()
	{
		yield return new WaitForSecondsRealtime(1.0f);
	}
}
