using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

	//Reference
	private GameDataStore dataRef;
	private GameObject HUDRef;
	private Upgrades upStats;
	[SerializeField] Tutorial tutRef;

	[SerializeField] Slider staminaBar;
    [SerializeField] Image staminaBarBackground;
    [SerializeField] UIParticle staminaParticleSystem;
    public Color stamBarFlashColour = Color.white;
    public float stamBarFlashSpeed = 0.5f;
    float stamBarLastFlash = 0;
    Color stamBarOriginalColour;

	//jeeves
	private GameObject jeevesRef;

	//Dirty
	private bool spawned;
	private float delay;

	//HUD data
	public Vector2 currentTime;
	public int currentDay;
	public string AmPm;

	public Sprite angerFace1;
	public Sprite angerFace2;
	public Sprite angerFace3;
	public Sprite angerFace4;
	public Image AngerFace;

	public Text hudTime;
	public Text hudDay;
	public Text hudRage;
	public GameObject hudAngerRadial;
	[SerializeField]
	Image CircleImage;
	[SerializeField]
	Color start;
	[SerializeField]
	Color end;

	public GameObject mainUI;

	//Save Data
	private int cumulativeScore;
	public int currentScore;

	public int ButlerCoins;
	public int doorUpgrade;
	public int power1Upgrade;
	public int power2Upgrade;
	public int power3Upgrade;
	public int power4Upgrade;
	public int power5Upgrade;

	//Scoring
	public int dayScore;
	public int multiplyer;


	//Meta Currency


	//PowerUps
	[SerializeField] GameObject p1;
	[SerializeField] GameObject p2;
	[SerializeField] GameObject p3;
	[SerializeField] GameObject p4;
	[SerializeField] GameObject p5;

	//(speed/duration/cost/coolDown)
	public Vector4 speedUp;
	public Vector4 taskSpeedUp;

	private float speedUpCD;
	private float taskSpeedUpCD;

	private float counter1 = 0;
	private float counter2 = 0;
	private float counter3 = 0;
	private float counter4 = 0;

	private bool active1;
	private bool active2;

	private bool run1;
	private bool run2;
	private bool run3;
	private bool run4;

	//(cost, coolDown)
    public Vector2 power3;
	public Vector2 power4;



	//End Screen Stats
	public GameObject endPanel;

	public Text multi;
	public Text currentScoreText;
	public Text days;
	public Text chores;
	public Text NPC;
	public Text interruption;
	public Text door;
	public Text anger;
	public Text Score;
	public Text HighScore;
	public Text challangeText;

	public int maxLocksAvailable;
	public int startingRage;

	public MusicControl musicSystem;  //MusicControl script on the "MusicSystem" GameObject


	//Challanges
	private int lastChallange;
	enum Challanges { Task1, Task2, Task3, Lock1, Lock2, Lock3, Rage1, Rage2, Rage3 };
	private Challanges challanges;
	private string currentChallangeStr;
	public int currentChallange;
	public int[] rewards = new int[9];
	public int[] coins = new int[9];

	// Challanges stats reset every day;
	public int chaTaskComplete;
	public int chaLockComplete;
	public int chaRageMax;

	void setChallangeStats(int num)
	{
		print("CHALLANGE REWARD");
		setChallangeScore(rewards[num]);
		ButlerCoins += coins[num];
	}

	public void Check(int cha)
	{
		//Challanges


		if (cha == 0)
		{
			if (chaTaskComplete > 10)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 1)
		{
			if (chaTaskComplete > 20)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 2)
		{
			if (chaTaskComplete > 30)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 3)
		{
			if (chaLockComplete == 0)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 4)
		{
			if (chaLockComplete < 3)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 5)
		{
			if (chaLockComplete < 10)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 6)
		{
			if (chaRageMax < 25)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 7)
		{
			if (chaRageMax < 50)
			{
				setChallangeStats(cha);
			}
		}
		else if (cha == 8)
		{
			if (chaRageMax < 75)
			{
				setChallangeStats(cha);
			}
		}



		currentChallange = Random.Range(0, rewards.Length);
		while (currentChallange == lastChallange)
		{
			currentChallange = Random.Range(0, rewards.Length);
		}
		lastChallange = currentChallange;
		SetChallangeUI(currentChallange);
		print("challange NUMBERRR  " + currentChallange);

	}
	void SetChallangeUI(int cha)
	{
		if (cha == 0)
		{
			currentChallangeStr = "Complete more than 10 tasks\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 1)
		{
			currentChallangeStr = "Complete more than 20 tasks\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 2)
		{
			currentChallangeStr = "Complete more than 30 tasks\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 3)
		{
			currentChallangeStr = "Use no locks\n" + "Reward:  " + rewards[cha].ToString();
		}
		else if (cha == 4)
		{
			currentChallangeStr = "Use less than 3 locks\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 5)
		{
			currentChallangeStr = "Use less than 10 locks\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 6)
		{
			currentChallangeStr = "Keep baron under 25 rage\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 7)
		{
			currentChallangeStr = "Keep baron under 50 rage\n" + "Reward: " + rewards[cha].ToString();
		}
		else if (cha == 8)
		{
			currentChallangeStr = "Keep baron under 75 rage\n" + "Reward: " + rewards[cha].ToString();
		}
	}

	public int CumulativeScore
	{
		get
		{
			return cumulativeScore;
		}

		set
		{
			cumulativeScore += value;
		}
	}

	public int CurrentScore
	{
		get
		{
			return currentScore;
		}

		set
		{
			currentScore += value * multiplyer;
			dataRef.Stamina = value * multiplyer;
			print("STAMINA  " + dataRef.Stamina);
			print(currentScore);
		}
	}

	public void setChallangeScore(int value)
	{
		currentScore += value;
	}

	// Use this for initialization
	void Start()
	{

        stamBarOriginalColour = staminaBarBackground.color;

		active1 = false;
		active2 = false;

		run1 = false;
		run2 = false;
		run3 = false;
		run4 = false;

		upStats = GetComponent<Upgrades>();
		jeevesRef = GameObject.FindGameObjectWithTag("Jeeves");
		speedUpCD = 0;
		taskSpeedUpCD = 0;
		//roll challange

		currentChallange = Random.Range(0, rewards.Length);
		lastChallange = currentChallange;
		SetChallangeUI(currentChallange);
		print("challange NUMBERRR  " + currentChallange);

		//scoring
		multiplyer = 1;

		//Dirty
		spawned = true;
		delay = 0;


		//get references
		// HUDRef = GameObject.FindGameObjectWithTag("HUD");
		if (endPanel.activeSelf == true)
		{
			endPanel.SetActive(false);
		}
		if (mainUI.activeSelf == false)
		{
			mainUI.SetActive(true);
		}

		dataRef = gameObject.GetComponent<GameDataStore>();
		dataRef.SetMaxLocks(maxLocksAvailable);
		dataRef.SetCurrentRage(startingRage);

		// hudAngerRadial.GetComponent<Image>().type = Image.Type.Filled;
		// hudAngerRadial.GetComponent<Image>().fillMethod = Image.FillMethod.Radial360;
		// hudAngerRadial.GetComponent<Image>().fillOrigin = 0;

		SaveLoad.Load("Jeeves.cjc");
		ButlerCoins = SaveData.currencyMeta;
		doorUpgrade = SaveData.DoorUpgradeLevel;
		power1Upgrade = SaveData.Power1UpgradeLevel;
		power2Upgrade = SaveData.Power2UpgradeLevel;
		power3Upgrade = SaveData.Power3UpgradeLevel;
		power4Upgrade = SaveData.Power4UpgradeLevel;
		power5Upgrade = SaveData.Power5UpgradeLevel;
		print("High " + SaveData.Score);


		//tutRef.isOn = SaveData.isTutorial;
		// print("TTTTTUUUUUUUTTTTTTT" + SaveData.isTutorial);

		//set powerUP stats
		dataRef.DoorCost = upStats.SetDoorUpgrade(doorUpgrade);
		speedUp = upStats.SetPower1Upgrade(power1Upgrade);
		taskSpeedUp = upStats.SetPower2Upgrade(power2Upgrade);
		power3 = upStats.SetPower3Upgrade(power3Upgrade);
		power4 = upStats.SetPower4Upgrade(power4Upgrade);

	}


	public bool GetIsTut()
	{
		SaveLoad.Load("Jeeves.cjc");
		return SaveData.isTutorial;
	}

	public void OffTut()
	{
		SaveLoad.Load("Jeeves.cjc");
		SaveData.isTutorial = false;
		print("TTTTTUUUUUUUTTTTTTT" + SaveData.isTutorial);
		SaveLoad.Save();
	}
	public void OnTut()
	{

		SaveLoad.Load("Jeeves.cjc");
		SaveData.isTutorial = true;
		print("TTTTTUUUUUUUTTTTTTT" + SaveData.isTutorial);
		SaveLoad.Save();
	}

	// Update is called once per frame
	void Update()
	{
        float currentFlashTime = Time.time - stamBarLastFlash;
        // flash bar stuff
        if (currentFlashTime < stamBarFlashSpeed)
        {
            // do flash
            if (currentFlashTime < (stamBarFlashSpeed / 2.0f))
            {
                // go towards flash colour
                staminaBarBackground.color = Color.Lerp(stamBarOriginalColour, stamBarFlashColour, currentFlashTime / (stamBarFlashSpeed / 2.0f));
            }
            else
            {
                // go towards original colour
                staminaBarBackground.color = Color.Lerp(stamBarFlashColour, stamBarOriginalColour, (currentFlashTime - (stamBarFlashSpeed / 2.0f)) / (stamBarFlashSpeed / 2.0f));
            }
        }

		if (Input.GetKeyDown(KeyCode.Insert))
		{
			OnTut();
		}


		//Clear Save Data
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (dataRef.GetUsedLocks() < 10)
			{
				CurrentScore = rewards[0];
			}
			print(dataRef.GetUsedLocks());

		}


		SetChallangeUI(currentChallange);
		challangeText.text = currentChallangeStr;


		if (multiplyer > 5)
		{
			multiplyer = 5;
		}

		multi.text = "X" + multiplyer.ToString();


		//Stamina
		ManageStamina();


		//Update HUD

		currentTime = dataRef.GetTime();
		currentDay = dataRef.GetDaysSurvived() + 1;

		if ((dataRef.GetTime().x >= 0) && (dataRef.GetTime().x < 6))
		{
			currentTime.x = currentTime.x + 6;
			AmPm = "AM";
		}
		else if ((dataRef.GetTime().x >= 6) && (dataRef.GetTime().x < 7))
		{
			currentTime.x = currentTime.x + 6;
			AmPm = "PM";
		}
		else if ((dataRef.GetTime().x >= 7) && (dataRef.GetTime().x < 18))
		{
			currentTime.x = currentTime.x - 6;
			AmPm = "PM";
		}
		else if ((dataRef.GetTime().x >= 18) && (dataRef.GetTime().x < 20))
		{
			currentTime.x = currentTime.x - 18;
			AmPm = "AM";
		}

		if (dataRef.GetTime().y < 10)
		{
			hudTime.text = (currentTime.x.ToString()) + ":" + "0" + currentTime.y.ToString() + AmPm;
			hudDay.text = currentDay.ToString();
		}
		else
		{
			hudTime.text = (currentTime.x.ToString()) + ":" + currentTime.y.ToString() + AmPm;
			hudDay.text = currentDay.ToString();
		}

		hudRage.text = dataRef.GetCurrentRage().ToString();
		hudAngerRadial.GetComponent<Image>().fillAmount = Mathf.Lerp(hudAngerRadial.GetComponent<Image>().fillAmount, (dataRef.GetCurrentRage() / 100f), 4 * Time.deltaTime);

		if (dataRef.GetCurrentRage() < 25)
		{
			AngerFace.sprite = angerFace1;
		} else if (dataRef.GetCurrentRage() >= 25 && dataRef.GetCurrentRage() < 50)
		{
			AngerFace.sprite = angerFace2;
		}
		else if (dataRef.GetCurrentRage() >= 50 && dataRef.GetCurrentRage() < 75)
		{
			AngerFace.sprite = angerFace3;
		}
		else if (dataRef.GetCurrentRage() >= 75 && dataRef.GetCurrentRage() <= 100)
		{
			AngerFace.sprite = angerFace4;
		}

		currentScoreText.text = currentScore.ToString();

		// hudAngerRadial.GetComponent<Image>().color = Color.Lerp(start, end, dataRef.GetCurrentRage() / 100f);

		//Display save file data to console
		if (Input.GetKeyDown(KeyCode.L))
		{

			print(SaveData.Score);

		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			print(ButlerCoins);
			print(doorUpgrade);
			print(power1Upgrade);
			print(power2Upgrade);
			print(power3Upgrade);
			print(power4Upgrade);
			print(power5Upgrade);
		}

		if (Input.GetKeyDown(KeyCode.Alpha9))
		{

			print(SaveData.Power2Stats);
			print(SaveData.Power3Stats);
			print(SaveData.Power4Stats);
			print(SaveData.DoorCost1);
		}


		//Clear Save Data
		if (Input.GetKeyDown(KeyCode.M))
		{

			print("CLEAR DATA");

			//edit data
			SaveData.Score = 0;
			SaveData.currencyMeta = 0;
			SaveData.DoorUpgradeLevel = 0;
			SaveData.Power1UpgradeLevel = 0;
			SaveData.Power2UpgradeLevel = 0;
			SaveData.Power3UpgradeLevel = 0;
			SaveData.Power4UpgradeLevel = 0;
			SaveData.Power5UpgradeLevel = 0;
			//save data
			SaveLoad.Save();

		}


		if (dataRef.GetCurrentRage() > 100)
		{
			//reload current level
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			//set data
			print("CURRENT SCORE:" + currentScore + "  HighScore: " + SaveData.Score);
			if (currentScore > SaveData.Score)
			{
				SaveData.Score = currentScore;
			}

			//setUpgrades
			SaveData.currencyMeta = ButlerCoins;
			SaveData.DoorUpgradeLevel = doorUpgrade;
			SaveData.Power1UpgradeLevel = power1Upgrade;
			SaveData.Power2UpgradeLevel = power2Upgrade;
			SaveData.Power3UpgradeLevel = power3Upgrade;
			SaveData.Power4UpgradeLevel = power4Upgrade;
			SaveData.Power5UpgradeLevel = power5Upgrade;




			//save data
			SaveLoad.Save();

			dataRef.SetTime(dataRef.GetTime() - dataRef.GetTime());

			endPanel.SetActive(true);
			Time.timeScale = 0;
			setEndScreenStats();

			mainUI.SetActive(false);


			musicSystem.EndScreen();
			//load menu
			// SceneManager.LoadScene("Menu");

		}

		/* if (dataRef.GetCurrentRage() >= 50)
        {
        
            musicSystem.Anger();
        }
        */

		//test
		/*
        if(Input.GetKeyDown(KeyCode.A))
        {
            dataRef.SetCurrentRage(1000);
        }
        */
	}


	void setEndScreenStats()
	{
		////load new saved data
		/*  SaveLoad.Load("Jeeves.cjc");


          */

		days.text = dataRef.GetDaysSurvived().ToString();
		chores.text = dataRef.GetCompletedTasks().ToString(); ;
		NPC.text = dataRef.GetNpcsEncountered().ToString();
		interruption.text = dataRef.GetInterruptionsUsed().ToString();
		door.text = dataRef.GetUsedLocks().ToString();
		anger.text = dataRef.GetAccumulatedRage().ToString();
		Score.text = currentScore.ToString();
		HighScore.text = SaveData.Score.ToString();

	}




	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
	//                                             //
	//                  Stamina                    //
	//                                             //
	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

	void ManageStamina()
	{
		//Link stamina bar to value
		staminaBar.value = Mathf.Lerp(staminaBar.value, dataRef.Stamina, 4.0f * Time.deltaTime);//dataRef.Stamina;


		//PowerUps


		//Trigger
		RunPower1();
		RunPower2();
		RunPower3();
		RunPower4();



	}

    public void FlashStamBar()
    {
        stamBarLastFlash = Time.time;
        staminaParticleSystem.Fire();
    }



	public void Run1()
	{
		run1 = true;
	}
	public void Run2()
	{
		run2 = true;
	}
	public void Run3()
	{
		run3 = true;
	}
	public void Run4()
	{
		run4 = true;
	}



	private void RunPower1()
	{
		//1) (SpeedUp //speed/duration/cost/coolDown)

		//Trigger 
		//Run Once
		if (Input.GetKeyDown(KeyCode.Alpha1) || run1)
		{
			//tutorial link
			if (tutRef != null && tutRef.GetCurrent() == 6)
			{
				tutRef.OK();
			}
			if (speedUpCD > speedUp.w)
			{
				if (dataRef.Stamina >= speedUp.z)
				{
					//pay toll
					dataRef.Stamina = -(int)speedUp.z;

					jeevesRef.GetComponent<NavMeshAgent>().speed = speedUp.x;

					counter1 = 0;
					speedUpCD = 0;

					active1 = true;
				}

			}
			run1 = false;
		}
		//Timers
		{

			if (speedUpCD < speedUp.w)
			{
				speedUpCD += Time.deltaTime;
				p1.SetActive(false);

			}
			else
			{
				p1.SetActive(true);
			}


			if (active1)
			{
				if (counter1 < speedUp.y)
				{
					counter1 += Time.deltaTime;

				}
				else
				{
					jeevesRef.GetComponent<NavMeshAgent>().speed = 3.5f;
					active1 = false;
				}
			}
		}
	}

	private void RunPower2()
	{


		//2) (TaskSpeedUp //speed/duration/cost/coolDown)

		// Trigger
		if (Input.GetKeyDown(KeyCode.Alpha2) || run2)
		{
			//tutorial link
			if (tutRef != null && tutRef.GetCurrent() == 6)
			{
				tutRef.OK();
			}

			if (taskSpeedUpCD > taskSpeedUp.w)
			{
				if (dataRef.Stamina >= taskSpeedUp.z)
				{
					//pay toll
					dataRef.Stamina = -(int)taskSpeedUp.z;


					dataRef.TaskSpeed = taskSpeedUp.x;

					counter2 = 0;
					taskSpeedUpCD = 0;
					active2 = true;

				}

			}
			run2 = false;

		}
		//Timers
		{
			if (taskSpeedUpCD < taskSpeedUp.w)
			{
				taskSpeedUpCD += Time.deltaTime;
				p2.SetActive(false);

			}
			else
			{
				p2.SetActive(true);
			}


			if (active2)
			{
				if (counter2 < taskSpeedUp.y)
				{
					counter2 += Time.deltaTime;

				}
				else
				{
					dataRef.TaskSpeed = 1;
					active2 = false;
				}
			}
		}
	}

	private void RunPower3()
	{
		//(cost, coolDown)
		//3 Interruptiion1
		//trigger
		if (Input.GetKeyDown(KeyCode.Alpha3) || run3)
		{
			//tutorial link
			if (tutRef != null && tutRef.GetCurrent() == 6)
			{
				tutRef.OK();
			}

			if (counter3 > power3.y)
			{
				if (dataRef.Stamina >= power3.x)
				{
					//pay toll
					dataRef.Stamina = -(int)power3.x;
					GameObject.FindGameObjectWithTag("Inter").GetComponent<CooldownThingoNew>().Reset(0);

					counter3 = 0;
				}
			}
			run3 = false;
		}

		//Timer
		if (counter3 < power3.y)
		{
			counter3 += Time.deltaTime;
			p3.SetActive(false);
		}
		else
		{
			p3.SetActive(true);
		}
	}

	private void RunPower4()
	{
		//4, Interruption2
		//(cost, coolDown)
		if (Input.GetKeyDown(KeyCode.Alpha4) || run4)
		{
			//tutorial link
			if (tutRef != null && tutRef.GetCurrent() == 6)
			{
				tutRef.OK();
			}
			if (counter4 > power4.y)
			{
				if (dataRef.Stamina >= power4.x)
				{
					//pay toll
					dataRef.Stamina = -(int)power4.x;

					GameObject.FindGameObjectWithTag("Inter").GetComponent<CooldownThingoNew>().Reset(1);
					counter4 = 0;
				}
			}
			run4 = false;
		}
		//Timer
		if (counter4 < power4.y)
		{
			counter4 += Time.deltaTime;
			p4.SetActive(false);
		}
		else
		{
			p4.SetActive(true);
		}
	}
}

