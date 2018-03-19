using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbances : MonoBehaviour
{

	public float sightRange = 2;
	public float frequancyRage = 5;
	public float amountRage;
    public int maxRageOverLifetime = 0;   // if set to zero, rage is not capped.
    public int totalRageDealt = 0;
	private GameObject baronRef;
	private GameDataStore dataRef;
	private float time;
	private float timer;
	public float wait;
	private new AudioSource audio;
	public AudioClip[] clips;
	public GameObject Ring;



	// Use this for initialization
	void Start()
	{
		dataRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();
		baronRef = GameObject.FindGameObjectWithTag("Baron");
		time = frequancyRage;
		if (Ring != null)
		{
			Ring.gameObject.transform.localScale = Ring.transform.localScale * (sightRange * 2);
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (audio != null)
		{
			audio.clip = clips[0];
			audio.Play();
		}

		if (wait > 0)
		{
			wait -= Time.deltaTime;

		}
		else
		{
			if (Vector3.Distance(gameObject.transform.position, baronRef.gameObject.transform.position) < sightRange)
			{

				if (time < 0 && (maxRageOverLifetime == 0 || maxRageOverLifetime > totalRageDealt))
				{
					dataRef.SetCurrentRage((int)amountRage);
					print("inrange");
					time = frequancyRage;
                    totalRageDealt += (int)amountRage;
				}

				time -= Time.deltaTime;

			}

		}


	}
}
