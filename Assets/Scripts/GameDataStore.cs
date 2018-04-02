using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameDataStore : MonoBehaviour
{
    // Public Variables
    [SerializeField] int maximumRage;  //active
    [SerializeField] int currentRage;  //active
    [SerializeField] int accumulatedRage;
    [SerializeField] int stamina;
    public Vector2 time;
    private int maximumLocks; 
    [SerializeField] int usedLocks; //active
    [SerializeField] int completedTasks; //active
    private int spawnedTasks;
    [SerializeField] int daysSurvived;   //active
    public int npcsEncountered;
    public int interrupsUsed;
    private GameManager gameManagerRef;
    private float wait = 3;
    private float timer = 0;

    public float maxRageToTakePerSecond = 5.0f; // max rage that baron can gain per second, zero disables feature
    public float lastRageLimitTick = 0;
    public float rageTakenThisTick = 0;

    public float angerArtificialCeilingPercent = 80.0f; // artificial anger ceiling, above which anger will be modified 
    public float angerArtificialCeilingModifier = 2.0f; // number by which anger will be divided above ceiling
    
    private int doorCost;

    public UIParticle angerParticles;

    public GameObject up;
    public GameObject up1;
    public GameObject up2;
    public GameObject down;
   // public GameObject down1;
   // public GameObject down2;

    public int rage;
    public int rage1;
    public int rage2;


    private float taskSpeed;



    public int Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            if(stamina > 100)
            {
                stamina = 100;
            }
            else
            {
            stamina += value;
            }

            gameManagerRef.FlashStamBar();
        }
    }


    public float TaskSpeed
    {
        get
        {
            return taskSpeed;
        }

        set
        {
            taskSpeed = value;
        }
    }

    public int DoorCost
    {
        get
        {
            return doorCost;
        }

        set
        {
            doorCost = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        taskSpeed = 1;
        gameManagerRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        maximumRage = 0;
        completedTasks = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = gameObject.GetComponent<GameClock>().timeOfDay;

        if(currentRage > maximumRage)
        {
            maximumRage = currentRage;
        }
        if (currentRage > gameManagerRef.chaRageMax)
        {
           gameManagerRef.chaRageMax = currentRage;
        }
    }

    //Get and Set functions for Current Rage
    public int GetCurrentRage()
    {
        return currentRage;
    }

    public int GetAccumulatedRage()
    {
        return accumulatedRage;
    }

    public void SetCurrentRage(int newCurrentRage)
    {
        // do tick stuff;
        if (((time.x*60) + time.y) - lastRageLimitTick > 1.0f )
        {
            lastRageLimitTick = ((time.x * 60) + time.y);
            rageTakenThisTick = 0;
        }

        if (rageTakenThisTick > maxRageToTakePerSecond && maxRageToTakePerSecond != 0)
            return;     // no more rage this tick

        // deal with rubberbanding rage
        if (currentRage > angerArtificialCeilingPercent)
        {
            newCurrentRage = newCurrentRage > 0 ? Mathf.RoundToInt(newCurrentRage / angerArtificialCeilingModifier) : newCurrentRage;
        }

        currentRage += newCurrentRage;
        if (currentRage < 0)
            currentRage = 0;

        if(newCurrentRage > 0)
        {

            angerParticles.Fire();
        
            gameManagerRef.SetMultiplier(-1);
            accumulatedRage += newCurrentRage;

            if(newCurrentRage <= rage)
            {
            up.SetActive(true);
            up1.SetActive(false);
            up2.SetActive(false);
            down.SetActive(false);
            }
            else if(newCurrentRage <= rage1)
            {
                up.SetActive(false);
                up1.SetActive(true);
                up2.SetActive(false);
                down.SetActive(false);
            }
            else if (newCurrentRage <= rage2)
            {
                up.SetActive(false);
                up1.SetActive(false);
                up2.SetActive(true);
                down.SetActive(false);
            }

        }
        else if(newCurrentRage < 0)
        {
            up.SetActive(false);
            up1.SetActive(false);
            up2.SetActive(false);
            down.SetActive(true);

        }
        else
        {
            up.SetActive(false);
            up1.SetActive(false);
            up2.SetActive(false);
            down.SetActive(false);
        }
    }

    public int GetMaxRage()
    {

        return maximumRage;

    }
    //Get and Set functions for Time
    public Vector2 GetTime()
    {
        return time;
    }

    public void SetTime(Vector2 newTime)
    {
        time = newTime;
    }

    //set max locks for level

    public void SetMaxLocks(int maxLocks)
    {
        maximumLocks = maxLocks;
    }


    public int GetMaxLocks()
    {
        return maximumLocks;
    }
    //Get and Set functions for Used Locks
    public int GetUsedLocks()
    {
        return usedLocks;
    }

    public void SetUsedLocks(int newUsedLocks)
    {
        usedLocks += newUsedLocks;
        gameManagerRef.chaLockComplete += newUsedLocks;
    }

    //Get and Set functions for Spawned Tasks
    public int GetCompletedTasks()
    {
        return completedTasks;
    }

    public void SetCompletedTasks(int newSpawnedTasks)
    {
        completedTasks += newSpawnedTasks;
        gameManagerRef.chaTaskComplete += newSpawnedTasks;
    }

    public void GetSpawnedTasks(int newtask)
    {
        spawnedTasks = newtask;
    }

    public int GetSpawnedTasks()
    {
        return spawnedTasks;
    }
    //Get and Set functions for Days Survived
    public int GetDaysSurvived()
    {
        return daysSurvived;
    }

    public void SetDaysSurvived(int newDaysSurvived)
    {
        daysSurvived += newDaysSurvived;
    }

    //Get and Set functions for NPCs Encountered
    public int GetNpcsEncountered()
    {
        return npcsEncountered;
    }

    public void SetNpcsEncountered(int newNpcsEncountered)
    {
        npcsEncountered += newNpcsEncountered;
    }

    public int GetInterruptionsUsed()
    {
        return interrupsUsed;
    }

    
}
