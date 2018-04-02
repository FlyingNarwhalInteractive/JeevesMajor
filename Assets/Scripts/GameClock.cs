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

    // FMOD Audio Events
  //  [FMODUnity.EventRef]
  //  public string DayBreak;
    [FMODUnity.EventRef]
    public string NightFall;

    // Use this for initialization
    void Start ()
    {
        gameManagerRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        dayBuilder = GameObject.FindGameObjectWithTag("GM").GetComponent<DayBuilder>();
        npcDay = GameObject.FindGameObjectWithTag("GM").GetComponent<NpcDay>();
        DirectionLight = 0;
        LightGroup1 = 1;
        LightGroup2 = 3;
    
        timeCounter = 0;
        timeOfDay = startOfDay;
    }
	
	// Update is called once per frame
	void Update ()
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
        timeDayCheck   = (int)(timeOfDay.x * 100) + (int)timeOfDay.y;
        endDayCheck    = (int)(endOfDay.x  * 100)  + (int)endOfDay.y;
        daybreakCheck  = (int)(daybreak.x  * 100)  + (int)daybreak.y;
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
            GameObject.FindGameObjectWithTag("DayWonPanel").GetComponent<DayWonPanel>().Reveal
                ("Day " + gameObject.GetComponent<GameDataStore>().GetDaysSurvived().ToString() + " Survived!",
                currentChallenge, Color.black, didWin ? new Color(0,0.55f,0,1) : Color.red, 5.0f);


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
        if(DirectionLight < 1)
        {
            
            DirectionLight += Time.deltaTime/ fadeDuration;
            LightGroup1 -= Time.deltaTime / fadeDuration;
            LightGroup2 -= Time.deltaTime / fadeDuration * 3;
        }

        GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>().intensity = DirectionLight;
        GameObject[] light =  GameObject.FindGameObjectsWithTag("Light");
        GameObject[] slight = GameObject.FindGameObjectsWithTag("SLight");

        for (int i = 0; i < light.Length; i++)
        {
            light[i].GetComponent<Light>().intensity = LightGroup1;
        }
        for (int i = 0; i < slight.Length; i++)
        {
            light[i].GetComponent<Light>().intensity = LightGroup2;
        }

    }

     void nightLight()
    {

        if (DirectionLight > 0)
        {
           
            LightGroup1 += Time.deltaTime / fadeDuration;
            LightGroup2 += Time.deltaTime / fadeDuration * 3;
            DirectionLight -= Time.deltaTime / fadeDuration;
        }

        GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>().intensity = DirectionLight;
        GameObject[] light = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] slight = GameObject.FindGameObjectsWithTag("SLight");

        for (int i = 0; i < light.Length; i++)
        {
            light[i].GetComponent<Light>().intensity = LightGroup1;
        }
        for (int i = 0; i < slight.Length; i++)
        {
            light[i].GetComponent<Light>().intensity = LightGroup2;
        }

    }

    
    
}
