using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task : MonoBehaviour
{
    //Owner;
    public bool isDestroy;
    public bool isBaron;
    public bool isAI;
    //public bool isNPC;

    //Fetch quest Variables
    public bool isDummy;
    public bool isRandDummy;
    public bool isFetch;
    public GameObject FetchObj;
    public Transform baronFetchLocation;
    

    //added because of compiler errors
    public bool isCompleted;
    
    //Rand
    public bool isRandom = false;
    public GameObject[] randomTaskList;
    public Transform[] spawnLocations;
    public int[] chanceNum;
    private int total = 0;
    private int tempTotal = 0;
    private int chosenTask = 0;
    private Scheduler sched;

    //world links
    private GameObject gameManagerRef;
    private NotTheRealAI baronRef;
    private GameDataStore dataRef;
    //scoring 
    public int points;
    public Vector2 spawnTime;
    public Vector2 spawnRangeStart, spawnRangeEnd;


    //Visual feedback
    public GameObject Ring;
    public GameObject Blink;
	public ParticleSystem spawnFX;
	public ParticleSystem completeFX;
    //assets
    public Mesh taskObject;
    public Texture baronAngerIcon;
    public Texture workingIcon;

    //subTasks
    //list of subtasks
    public GameObject[] subTasks;
    //is a subTask
    private bool isSub;
    //number counters
    private int chain;
    private int current;

    //Info
    public Transform spawnLocation;
    public float timeToComplete;
    public float timeToCompleteStore;
    public int priority;
    public bool isDisturbance;
    public int unfinishedRage;
    public int completedRageReduction;
    public int detectCooldown;
    public float cdCounter;
    public bool isDetect;
    public bool isVisibleToBaron;
    public bool baronHasSeenBefore = false;
    bool        baronFirstSighting = false;
    public bool ragedOnce;

	//enable/disable objects

	//On Spawn
[SerializeField] GameObject	ObjectEnable;
[SerializeField] GameObject ObjectDisable;


	//task creating task
	public GameObject mess;
    public int messSpawnMode;
    public Transform messPos;
    [SerializeField] float messRadius;
    [SerializeField] int messAmmount;

    //sound
    private new AudioSource audio;
    //public AudioClip[] clips;

    //tell + texture
   // public int tellXSize;
   // public int tellYSize;
   // public Texture tex1;
   // public Texture tex2;
   // public float tellXOffSet;
   // public float tellYOffSet;
    private Vector3 otherPos;
    private Camera m_camera;


    //FX
    TaskFX FX;

    // jeeves
    Jeeves m_jeeves;
    private Tutorial tutRef;


    public void Detect()
    {
        if (baronHasSeenBefore)     // so if baron has seen it before, act as normal
        {
            isDetect = true;
            isVisibleToBaron = true;
        }
        else                        // however - if baron has not seen it before, identify that its his first sighting
        {
            isVisibleToBaron = true;
            baronFirstSighting = true;
        }
    }



    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
    //                                             //
    //          GENERAL INITIALISATION             //
    //                                             //
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//


    
    void Start()
    {
      //  tutRef = GameObject.FindGameObjectWithTag("Tut").GetComponent<Tutorial>();
        if(GameObject.FindGameObjectWithTag("Tut") != null)
        {
        tutRef = GameObject.FindGameObjectWithTag("Tut").GetComponent<Tutorial>();
        }

		//disable/enable object on spawn
		if(ObjectDisable != null && ObjectEnable != null)
		{
			ObjectEnable.SetActive(true);
			ObjectDisable.SetActive(false);
		}
  

        isDetect = false;
        isCompleted = false;
        ragedOnce = false;


		//Spawn effect

		if(spawnFX != null)
		{
			ParticleSystem pa =  Instantiate(spawnFX, gameObject.transform.position, spawnFX.transform.rotation);
			Destroy(pa, 5.0f);
		}

        //tutorial link
        if(tutRef != null)
        { 
        if (tutRef.GetCurrent() == 1)
        {
            tutRef.OK();
        }
        }
        //grab references
        if (GetComponent<TaskFX>() != null)
        {
            FX = GetComponent<TaskFX>();
            FX.SpawnFX();
        }

        baronRef = GameObject.FindGameObjectWithTag("Baron").GetComponent<NotTheRealAI>();
        m_jeeves = GameObject.Find("Jeeves").GetComponent<Jeeves>();
        gameManagerRef = GameObject.FindGameObjectWithTag("GM");
        dataRef = gameManagerRef.GetComponent<GameDataStore>();
        if (isAI)
        {
            sched = GameObject.FindGameObjectWithTag("Baron").GetComponent<Scheduler>();
            gameObject.SetActive(false);
        }

        //setup chain
        chain = subTasks.Length;
        current = 0;
        isSub = false;

        //camra ref for reason
        m_camera = Camera.main;

        //check and manage for mising components
        if (GetComponent<MeshFilter>() != null)
        {
        GetComponent<MeshFilter>().mesh = taskObject;
        }
        if(spawnLocation != null)
        {
        gameObject.transform.position = spawnLocation.position;
        }
        else
        {

        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //                 RAND DUMMY                  //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

        if(isRandDummy == true)
        {
            if(messSpawnMode != 0)
            {
                spawnMess();
            }
            Destroy(gameObject);
        }




        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //          SUBTASK INITIALISATION             //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

        //if there are subtasks in the array
        if (subTasks != null && subTasks.Length > 0)
        {
            //turn on subtask flag
            isSub = true;

            //turn off all subtasks in the array
            for (int i = 0; i < subTasks.Length; i++)
            {
                subTasks[i].SetActive(false);
            }

            //set the current subtask to active
            subTasks[current].SetActive(true);

        }






        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //          RANDOM TASK CALCULATOR             //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

          ////////////////////////////////////////////////////////
         // Adds chances together, roll a dice and pick a task //
        ////////////////////////////////////////////////////////

        //verify paramaters
        if (randomTaskList.Length != 0 && spawnLocations.Length != 0   && chanceNum.Length != 0)
        {
        //set total to 0 (tracking variable)
        total = 0;

            //add all of the chances together
        for(int i = 0; i < chanceNum.Length; i++)
        {
            total += chanceNum[i];
        }
            //roll a dice between 0 and total ammount of chance
            int roll = Random.Range(0, total);
            tempTotal = 0;

            //manage for 0, add chances until roll found, grab [i] and select that task index
            for(int i = 0; i < chanceNum.Length; i++)
            {
                tempTotal += chanceNum[i];
                Debug.Log(tempTotal);
                if(i == 0)
                {
                   if ( roll -1 < tempTotal)
                    {
                        chosenTask = i;
                        break;
                    }
                }
                else if( i != 0)
                {
                    if (roll -1 < tempTotal)
                     {
                        chosenTask = i;
                        break;
                     } 
                }
                else
                {
                    Debug.Log("NOT FOUND ERROR");
                }

            }
            total = 0;



            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
            //                                             //
            //          RANDOM TASK CREATION               //
            //                                             //
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//


        if (isRandom == true)
        {
            //select a random location index
            int rand = Random.Range(0, (int)spawnLocations.Length-1);

               
                //Spawn randomised task at selected location
                if(isAI)
                {
                    //grab instantiated reference, set initial flags/variables 
                GameObject cre = Instantiate(randomTaskList[chosenTask], spawnLocations[rand].position, Quaternion.identity) as GameObject;
                    if (isBaron)
                    {
                        cre.gameObject.SetActive(false);
                        cre.gameObject.GetComponent<Task>().isDestroy = true;
                        cre.gameObject.GetComponent<Task>().isBaron = true;
                    }
                    else
                    {
                       cre.gameObject.SetActive(true);
                       cre.gameObject.GetComponent<Task>().isDestroy = true;
                    }
               //find a null place within schedule and assign created random task to it
                for(int i = sched.tasks.Length -1; i > 0; i--)
                    {
                        if(sched.tasks[i] == null)
                        {
                            sched.tasks[i] = cre;
                            break;
                        }
                    }
                }
                else
                {
                    //just create it in level
                   if(isBaron)
                    {
                    Instantiate(randomTaskList[chosenTask], spawnLocations[rand].position, Quaternion.identity).SetActive(false);
                    }
                    else
                    {
                    Instantiate(randomTaskList[chosenTask], spawnLocations[rand].position, Quaternion.identity).SetActive(true);
                    }
                }
            //destroy host task
            Destroy(gameObject);
        }

        }



    }

    void OnEnable()
    {
        if(FX != null)
        {
            if(FX.spawnFX != null)
            {
            FX.SpawnFX();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(isDummy)
        {
            return;
        }


        // Manage rage at first sight - is it his first sighting?
        if (baronFirstSighting)
            if (isVisibleToBaron == false)      // has he now gone out of range?
            {
                baronHasSeenBefore = true;      // set the has seen before trigger to yes - anger now applies
                baronFirstSighting = false;     // set this to false to prevent this code executing again.
            }



        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //          TASK DETECT FUNCTION               //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

        if (isDetect)
        {
      

            if (cdCounter < detectCooldown)
            {
                if(Ring != null)
                {
                Ring.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                }


                if (Blink != null)
                {
                    Blink.GetComponent<Light>().intensity = 5;
                }

                cdCounter += Time.deltaTime;
            }
            else
            {
                //tutorial link
                if(tutRef != null)
                {
                if (tutRef.GetCurrent() == 4)
                {
                    tutRef.OK();
                }
                }

                if (Ring != null)
                {
                Ring.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
                }
                if (Blink != null)
                {
                    Blink.GetComponent<Light>().intensity = 0;
                }
                //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/BaronSigh");
                gameManagerRef.GetComponent<GameDataStore>().SetCurrentRage(unfinishedRage);
                cdCounter = 0.0f;
                isDetect = false;
                ragedOnce = true;
            }
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //             SUBTASK FUNCTION                //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

        //////////////////////////////////////////////////////////////////////////////      
        //   Fix chain complete                                                    //
        //   Change isNull check to isCompleted & turn completed setActive(false) //
        ///////////////////////////////////////////////////////////////////////////

        // Daniel Delgado Subtask Function Rebuild.
        // Only checks iterator if isSub is still true
        if (isSub == true)
        {
            // If the current subtask is completed, increment current to the next one.
            if (subTasks[current] == null)
                current++;

            // If current has incremented past the last subtask, then isSub should be false
            if (current == chain)
                isSub = false;
            else  // Otherwise, there is still a subtask that needs to be activated.
                subTasks[current].SetActive(true);
        }

        // Original Code before Daniel Delgado's changes.
        //if chain is completed turn off isSub
        // if (current == chain - 1)
        // {
        //     isSub = false;
        // }
        // 
        // //if subtask is completed set current to the nest in array and enable it
        // if (isSub == true)
        // {
        // 
        //     if (subTasks[current] != null)
        //     {
        //         current++;
        //         subTasks[current].SetActive(true);
        //     }
        // }

        //sounds
        if (audio != null)
        {
            //audio.clip = clips[0];
           // audio.Play();
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
        //                                             //
        //             DISTURBANCES TOGGLE             //
        //                                             //
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

        //Toggle disturbance
        if (isDisturbance)
        {
            if (GetComponent<Disturbances>() != null)
            {
                GetComponent<Disturbances>().enabled = true;
            }
        }
        else
        {
            if (GetComponent<Disturbances>() != null)
            {
                GetComponent<Disturbances>().enabled = false;
            }
        }
 
        
      

        //manage subtask running in the schedular
    }

  

    void OnTriggerEnter(Collider other)
    {

    }


    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
    //                                             //
    //             TASK INTERACT FUNCTION          //
    //                                             //
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

    //Complete task
    void OnTriggerStay(Collider other)
    {
        //check if task belongs to baron
        if(isBaron == true)
        {
            //check which agent is colliding
        if(other.tag == "Baron")
            {
                //if its not a sub task continue
                if (isSub == false)
                {
                    //if task not completed start complete countdown / else run complete code
                    if (timeToComplete > 0)
                    {
                        timeToComplete -= Time.deltaTime;

                        //play animation while task mis being completed
                        //other.GetComponent<Animator>().Play("dance", 1);
                    }
                    else if (timeToComplete < 0)
                    {
                        //Stop animation
                        //other.GetComponent<Animator>().StopPlayback();
                        isCompleted = true;

						//reset objects that changed during tasks
						if (ObjectDisable != null && ObjectEnable != null)
						{
							ObjectEnable.SetActive(false);
							ObjectDisable.SetActive(true);
						}

						//spawn mess on completion
						if (messSpawnMode != 0)
                        {
                            print("START OBJECT SPAWN");
                            spawnMess();
                        }

                        //wander after complete
                        baronRef.wander = true;


                        //clean up
                        if(isDestroy)
                        {
                        Destroy(gameObject);
                        }
                        else
                        {
                        gameObject.SetActive(false);
                        }
                    }
                }
            }

        }
        //check which agent is colliding
        if (isBaron == false)
        {
            //verify jeeves paramaters
            if (other.tag == "Jeeves"  && m_jeeves.currentTask == this.gameObject)
            {   //if its not a subtask continue
                if (isSub == false)
                {
                    //task complete countdown
                    if (timeToComplete > 0)
                    {
                        


                        // timeToComplete -= worldClock;
                        
                        timeToComplete -= (Time.deltaTime * dataRef.TaskSpeed);

                    }
                    else if (timeToComplete < 0)
                    {
                        if( FX != null)
                        {
                            if(FX.completedFX != null)
                            {
                            FX.CompletedFX();
                            }
                      
                        }

                        //tutorial link
                        if(tutRef != null)
                        {
                        if (tutRef.GetCurrent() == 2)
                        {
                            tutRef.OK();
                        }
                        }


                        //run task complete code, send stats to gameDataStore.cs / GameManager.cs
                        isCompleted = true;
						if (completeFX != null)
						{
							ParticleSystem pa = Instantiate(completeFX, gameObject.transform.position, completeFX.transform.rotation);
							Destroy(pa, 3.0f);
						}
						//reset objects that changed during tasks
						if (ObjectDisable != null && ObjectEnable != null)
						{
							ObjectEnable.SetActive(false);
							ObjectDisable.SetActive(true);
						}

						//reduce barons rage
						gameManagerRef.GetComponent<GameDataStore>().SetCurrentRage(-completedRageReduction);
                        gameManagerRef.GetComponent<GameDataStore>().SetCompletedTasks(1);
                        gameManagerRef.GetComponent<GameDataStore>().Stamina = points;

                        gameManagerRef.GetComponent<GameManager>().CurrentScore = points;
						gameManagerRef.GetComponent<GameManager>().SetMultiplier(1);



                        //spawn mess on completion
                        if (messSpawnMode != 0)
                        {
                            spawnMess();
                        }

                       //clean up
                        if(isDestroy)
                        {
                        Destroy(gameObject);
                        }
                        else
                        {
                        gameObject.SetActive(false);
                        }
                    }
                }
            }

            if (other.tag == "NPCGuest" && isAI == true)
            {
                //if its not a sub task continue
                if (isSub == false)
                {
                    //if task not completed start complete countdown / else run complete code
                    if (timeToComplete > 0)
                    {
                        timeToComplete -= Time.deltaTime;

                        //play animation while task mis being completed
                        //other.GetComponent<Animator>().Play("dance", 1);
                    }
                    else if (timeToComplete < 0)
                    {
                        //Stop animation
                        //other.GetComponent<Animator>().StopPlayback();
                        isCompleted = true;
                        //spawn mess on completion
                        if (messSpawnMode != 0)
                        {
                            print("START OBJECT SPAWN");
                            spawnMess();
                        }

                        //wander after task is completed
                        other.gameObject.GetComponent<NotTheRealAI>().wander = true;

                        //clean up
                        if (isDestroy)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }


    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
    //                                             //
    //             SPAWN MESS FUNCTION             //
    //                                             //
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

    //Mess Spawner
    public void spawnMess()
    {
		if (messSpawnMode == 1 && mess != null)
		{
			//Instantiate(messObject, gameObject.transform);

			GameObject obj = Instantiate(mess, messPos.position, Quaternion.identity) as GameObject;
			obj.SetActive(true);
			obj.GetComponent<Task>().isDestroy = true;
			print("CREATED");
		}
		else if (messSpawnMode == 2 && mess != null)
        {
            for(int i = 0; i < messAmmount; i++)
            {
                float Xoffset = Random.Range(-messRadius, messRadius);
                float Yoffset = Random.Range(-messRadius, messRadius);
                Vector3 offset = new Vector3(Xoffset, 0, Yoffset);

                //create mess
                GameObject obj = Instantiate(mess, messPos.position + offset, Quaternion.identity) as GameObject;
                obj.SetActive(true);
                obj.GetComponent<Task>().isDestroy = true;
                print("CREATED");
            }

            }
        }
    }





