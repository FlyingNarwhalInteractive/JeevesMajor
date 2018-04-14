using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnHighlight : MonoBehaviour
{

	private Image img;
	private GameManager GM;
	private GameDataStore dataRef;
	[SerializeField] int Num;
	[SerializeField] int cost;
	private float timeStep;
	private float count;
	private bool hover;
	private Vector3 origin;
	private Animator Ani;
	// Use this for initialization


	void Start()
	{
		//initialise stuffs
		Ani = GetComponent<Animator>();
		img = gameObject.GetComponent<Image>();
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		dataRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();

		//remember starting position
		origin = transform.position;

		//set up timers and flags
		timeStep = 5;
		count = 0;
		hover = false;

		//set cost bar for each power icon

		if (Num != 0)
		{
			if (Num == 1)
			{
				cost = (int)GM.speedUp.z;
			}
			else if (Num == 2)
			{
				cost = (int)GM.taskSpeedUp.z;
			}
			else if (Num == 3)
			{
				cost = (int)GM.power3.x;
			}
			else if (Num == 4)
			{
				cost = (int)GM.power4.x;
			}
			else
			{
				cost = 0;
			}
		}

		//run state function
	}

	void OnAwake()
	{
		hover = false;
		transform.position = origin;
			
		//run state function
	}



	public void HiLight()
	{
		if (dataRef.Stamina >= cost)
		{
			hover = false;
		}
		else
		{
			//run state function
			hover = false;
		}
	}


	private void FixedUpdate()
	{
		//run state function
		De();


	}


	public void DeHiLight()
	{
		hover = false;
	}

	private void De()
	{
		

		if (hover == false && cost != 0)
		{
			transform.localScale = new Vector3(2, 2, 1);
			if (dataRef.Stamina < cost)
			{
				img.color = Color.Lerp(img.color, Color.grey, 0.5f);
				Ani.SetBool("isWobble", false);
				Ani.SetBool("isHover", false);
			}
			else
			{
				img.color = Color.Lerp(img.color, Color.yellow, 0.5f);
				Ani.SetBool("isWobble", true);
				Ani.SetBool("isHover", false);
			}
		}
		else if (hover == true && dataRef.Stamina >= cost)
		{
			img.color = Color.Lerp(img.color, Color.green, 0.5f);
			Ani.SetBool("isWobble", true);
			Ani.SetBool("isHover", true);
		}
		else
		{
			img.color = Color.Lerp(img.color, Color.grey, 0.5f);
			Ani.SetBool("isWobble", false);
			Ani.SetBool("isHover", false);
		}
	}

	//https://forum.unity.com/threads/shake-an-object-from-script.138382/
	void Wobble()
	{

		transform.position += new Vector3(0.25f * Mathf.Sin(Time.time * Random.Range(8f, 12f)), 0.25f * Mathf.Sin(Time.time * Random.Range(8f, 12f)) + (0.25f * Mathf.Cos(Time.time * Random.Range(8f, 12f))), 0);
	}
}
