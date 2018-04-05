using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUpgrade : MonoBehaviour {

	public GameObject[] Panels;
	[SerializeField] ToggleTut toggleManagerRef;
	
	private int current;
	[SerializeField] bool isOn;

	// Use this for initialization
	void Start()
	{

			isOn = toggleManagerRef.GetIsTut();
			print("TutStatus" + isOn);
	    	
			if (isOn)
			{

				current = 0;
				Panels[current].SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}

		
	}

	public void DisableTut()
	{
		
			toggleManagerRef.OffTut();
			isOn = false;
			print("TUTOFF" + toggleManagerRef.GetIsTut());
			gameObject.SetActive(false);
		
	}

	public void OK()
	{
		Panels[current].SetActive(false);
		current++;
		if (current != Panels.Length)
		{
			Panels[current].SetActive(true);
		}


	}

	public int GetCurrent()
	{
		return current;
	}

	void FixedUpdate()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
