using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
	// Public Variables
	public Vector2 timeOfDay;
	public Vector2 startOfDay;
	public Vector2 endOfDay;
	public Vector2 daybreak;
	public Vector2 nightfall;
	public float hourLengthInMinutes = 1;
	public bool isDaytime;
	public bool clockPaused;
	public float fadeDuration;

	//reset scheduler
	private GameManager gameManagerRef;
	private DayBuilder dayBuilder;
	private NpcDay npcDay;
	// Private Variables
	float timeCounter;
	int timeDayCheck;
	int endDayCheck;
	int daybreakCheck;
	int nightfallCheck;
	private float DirectionLight;
	private float LightGroup1;
	private float LightGroup2;
	private float LightGroup3;
	private float LightGroup4;
	private float DirLightOrigin;
	[SerializeField] float DirLightMax;
	private GameObject[] individualLights;
	private float[] individualLightsOrigin;
	[SerializeField] float lightGroupIntensity1;
	[SerializeField] float lightGroupIntensity2;
	[SerializeField] float lightGroupIntensity3;
	[SerializeField] float lightGroupIntensity4;

	[SerializeField] Animator sun;
	// FMOD Audio Events
	//  [FMODUnity.EventRef]
	//  public string DayBreak;
	[FMODUnity.EventRef]
	public string NightFall;

	// Use this for initialization
	void Start()
	{

		gameManagerRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		dayBuilder = GameObject.FindGameObjectWithTag("GM").GetComponent<DayBuilder>();
		npcDay = GameObject.FindGameObjectWithTag("GM").GetComponent<NpcDay>();
		DirLightOrigin = GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>().intensity;
		DirectionLight = 0;


		LightGroup1 = lightGroupIntensity1;
		LightGroup2 = lightGroupIntensity2;
		LightGroup3 = lightGroupIntensity3;
		LightGroup4 = lightGroupIntensity4;

		timeCounter = 0;
		timeOfDay = startOfDay;
		individualLights = GameObject.FindGameObjectsWithTag("Light");
		individualLightsOrigin = new float[individualLights.Length];

		foreach(GameObject g in individualLights)
		{
			print(g.name);
		}

		for (int i = 0; i < individualLights.Length; i++)
		{
			individualLightsOrigin[i] = individualLights[i].GetComponent<Light>().intensity;
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Increment clock timer if clock is not paused
		if (!clockPaused)
			timeCounter += Time.deltaTime;

		//Check if hour has passed and increment
		float hourCheck = hourLengthInMinutes * 60;
		if (timeCounter > hourCheck)
		{
			timeOfDay.x++;
			timeCounter = 0;
		}
		timeOfDay.y = (int)(timeCounter / hourLengthInMinutes);

		//Set Timecheck variables
		timeDayCheck = (int)(timeOfDay.x * 100) + (int)timeOfDay.y;
		endDayCheck = (int)(endOfDay.x * 100) + (int)endOfDay.y;
		daybreakCheck = (int)(daybreak.x * 100) + (int)daybreak.y;
		nightfallCheck = (int)(nightfall.x * 100) + (int)nightfall.y;

		//Check if End of Day has been reached to reset day.
		if (timeDayCheck >= endDayCheck)
		{
			string currentChallenge = gameManagerRef.GetCurrentChallenge(); // for the alert - SP
			gameObject.GetComponent<GameDataStore>().SetDaysSurvived(1);
			print("DAY REWARD");
			gameManagerRef.setChallangeScore(gameManagerRef.dayScore);
			timeOfDay = startOfDay;
			timeDayCheck = (int)(timeOfDay.x * 100) + (int)timeOfDay.y;
			timeCounter = 0;
			dayBuilder.NewDay();
			npcDay.NewDay();
			bool didWin = gameManagerRef.Check(gameManagerRef.currentChallange);
			//reset challange counters
			gameManagerRef.chaLockComplete = 0;
			gameManagerRef.chaRageMax = 0;
			gameManagerRef.chaTaskComplete = 0;

            // alert player - A SCRIPT by Scott Purcival :D
            print("DayWonPanel Calling");
			GameObject.FindGameObjectWithTag("DayWonPanel").GetComponent<DayWonPanel>().Reveal
				("Day " + gameObject.GetComponent<GameDataStore>().GetDaysSurvived().ToString() + " Survived!",
				currentChallenge, Color.black, didWin ? new Color(0, 0.55f, 0, 1) : Color.red, 5.0f);
            print("DayWonPanel called.");

		}



		//Check if time is in daylight hours.
		if (timeDayCheck >= daybreakCheck && timeDayCheck <= nightfallCheck)
		{
			isDaytime = true;
			dayLight();
		}

		else
		{
			isDaytime = false;
			nightLight();
		}

	}



	void SetTime(Vector2 newTime)
	{
		timeOfDay = newTime;
	}

	void SetDayStart(Vector2 newTime)
	{
		startOfDay = newTime;
	}

	void SetDayEnd(Vector2 newTime)
	{
		endOfDay = newTime;
	}

	void SetDaybreak(Vector2 newTime)
	{
		daybreak = newTime;
	}

	void SetNightfall(Vector2 newTime)
	{
		nightfall = newTime;
	}

	void PauseClock(bool paused)
	{
		clockPaused = paused;
	}

	void dayLight()
	{
		if(sun != null)
		{
			sun.SetBool("isDay", true);
		}

		

		for(int i = 0; i < individualLights.Length; i++ )
		{
			individualLights[i].GetComponent<Light>().intensity = Mathf.Lerp(individualLights[i].GetComponent<Light>().intensity, individualLightsOrigin[i], fadeDuration * Time.deltaTime);
		}

		/*
		if (DirectionLight < DirLightMax)
		{
			DirectionLight += Time.deltaTime / fadeDuration;
		}
		*/

		if (LightGroup1 > 0)
		{
			LightGroup1 -= Time.deltaTime / fadeDuration * lightGroupIntensity1;
		}

		if (LightGroup2 > 0)
		{
			LightGroup2 -= Time.deltaTime / fadeDuration * lightGroupIntensity2;
		}

		if (LightGroup3 > 0)
		{
			LightGroup3 -= Time.deltaTime / fadeDuration * lightGroupIntensity3;
		}

		if (LightGroup4 > 0 )
		{
			LightGroup4 -= Time.deltaTime / fadeDuration * lightGroupIntensity4;
		}


		//GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>().intensity = DirectionLight;

		GameObject[] candelLight = GameObject.FindGameObjectsWithTag("CandelLight");
		GameObject[] smallLamp = GameObject.FindGameObjectsWithTag("SmallLamp");
		GameObject[] largeLamp = GameObject.FindGameObjectsWithTag("LargeLamp");
		GameObject[] screens = GameObject.FindGameObjectsWithTag("Screen");


		for (int i = 0; i < candelLight.Length; i++)
		{
			candelLight[i].GetComponent<Light>().intensity = LightGroup1;
		}
		for (int i = 0; i < smallLamp.Length; i++)
		{
			smallLamp[i].GetComponent<Light>().intensity = LightGroup2;
		}
		for (int i = 0; i < largeLamp.Length; i++)
		{
			largeLamp[i].GetComponent<Light>().intensity = LightGroup3;
		}
		for (int i = 0; i < screens.Length; i++)
		{
			screens[i].GetComponent<Light>().intensity = LightGroup4;
		}

	}

	void nightLight()
	{
		if (sun != null)
		{
			sun.SetBool("isDay", false);
		}

		for (int i = 0; i < individualLights.Length; i++)
		{
			individualLights[i].GetComponent<Light>().intensity = Mathf.Lerp(individualLights[i].GetComponent<Light>().intensity, 0, fadeDuration * Time.deltaTime);
		}


		/*
		if (DirectionLight > DirLightOrigin)
		{
			DirectionLight -= Time.deltaTime / fadeDuration;
		}
		*/

		if (LightGroup1 < lightGroupIntensity1)
		{
			LightGroup1 += Time.deltaTime / fadeDuration * lightGroupIntensity1;
		}

		if (LightGroup2 < lightGroupIntensity2)
		{
			LightGroup2 += Time.deltaTime / fadeDuration * lightGroupIntensity2;
		}

		if (LightGroup3 < lightGroupIntensity3)
		{
			LightGroup3 += Time.deltaTime / fadeDuration * lightGroupIntensity3;
		}

		if (LightGroup4 < lightGroupIntensity4)
		{
			LightGroup4 += Time.deltaTime / fadeDuration * lightGroupIntensity4;
		}

		//GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>().intensity = DirectionLight;

		GameObject[] candelLight = GameObject.FindGameObjectsWithTag("CandelLight");
		GameObject[] smallLamp = GameObject.FindGameObjectsWithTag("SmallLamp");
		GameObject[] largeLamp = GameObject.FindGameObjectsWithTag("LargeLamp");
		GameObject[] screens = GameObject.FindGameObjectsWithTag("Screen");

		//4 catagories

		for (int i = 0; i < candelLight.Length; i++)
		{
			candelLight[i].GetComponent<Light>().intensity = LightGroup1;
		}
		for (int i = 0; i < smallLamp.Length; i++)
		{
			smallLamp[i].GetComponent<Light>().intensity = LightGroup2;
		}
		for (int i = 0; i < largeLamp.Length; i++)
		{
			largeLamp[i].GetComponent<Light>().intensity = LightGroup3;
		}
		for (int i = 0; i < screens.Length; i++)
		{
			screens[i].GetComponent<Light>().intensity = LightGroup4;
		}
	}



}
