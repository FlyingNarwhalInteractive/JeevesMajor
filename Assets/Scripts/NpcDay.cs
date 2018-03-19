using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDay : MonoBehaviour
{
    //Grab Npc variables
    [SerializeField] GameObject[] npcTypesList;

    [SerializeField] int npcSpawnChance;
    private int npcSpawnBase;
    [SerializeField] int npcSpawnIncrement;
    [SerializeField] Transform npcSpwanLocation;
    private bool npcSpawned;


    public GameObject[] aiActors;
    private GameObject gameManagerRef;

    private GameObject[] tasks;
    // Use this for initialization 
    //task list
    public GameObject[] RandomTaskList;
    //weights
    public int[] chanceNumbers;


    private int total = 0;
    private int tempTotal = 0;
    private int chosenTask = 0;
    private int lastTask;

    private Scheduler tasksRef;

    void Start()
    {
        //grab references
        gameManagerRef = GameObject.FindGameObjectWithTag("GM");
        gameManagerRef.GetComponent<GameDataStore>().GetTime();

        npcSpawnBase = npcSpawnChance;
       // npcSpawned = false;

        SpawnNpcRoll();

        //Setup Objects
        foreach (GameObject o in aiActors)
        {
           
            
                if (o != null)
                {
                tasks = o.GetComponent<Scheduler>().tasks;
                
             
                SetUpNPC();
                o.GetComponent<Scheduler>().tasks = tasks;
                }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewDay()
    {

        npcSpawnChance += npcSpawnIncrement;

        SpawnNpcRoll();

        //Setup the Rest
        foreach (GameObject o in aiActors)
        {
            if (o != null)
            {
            tasks = o.GetComponent<Scheduler>().tasks;
            SetUpNPC();
            o.GetComponent<Scheduler>().tasks = tasks;
            }

        }
    }

    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
    //                                             //
    //             SETUP SCHEDULER                 //
    //                                             //
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//


    //Re-Roll spawn Ranges

    void SetUpNPC()
    {


        for (int i = 0; i < tasks.Length; i++)
        {

            if (tasks[i] != null)
            {

                if (tasks[i].GetComponent<Task>().spawnRangeStart != Vector2.zero && tasks[i].GetComponent<Task>().spawnRangeEnd != Vector2.zero)
                {
                    Vector2 Start = tasks[i].GetComponent<Task>().spawnRangeStart;
                    Vector2 End = tasks[i].GetComponent<Task>().spawnRangeEnd;


                    int x = (int)Random.Range(Start.x, End.x);
                    int y = (int)Random.Range(Start.y, End.y);
                    tasks[i].GetComponent<Task>().spawnTime = new Vector2(x, y);
                }

            }

            //fill in the gaps
            else
            {
                FindRandTasks();


                tasks[i] = Instantiate(RandomTaskList[chosenTask], RandomTaskList[chosenTask].transform.position, Quaternion.identity);//, positions[chosenTask]);


            }
            tasks[i].GetComponent<Task>().isCompleted = false;


            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
            //                                             //
            //             FIX TO DYNAMIC TASK TIME        //
            //                                             //
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
            tasks[i].GetComponent<Task>().timeToComplete = tasks[i].GetComponent<Task>().timeToCompleteStore;

            tasks[i].SetActive(false);

        }
    }


    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
    //                                             //
    //             Find Random Task                //
    //                                             //
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//

    //alter chanceNum array to add progression huristics


    void FindRandTasks()
    {



        if (RandomTaskList.Length != 0 && chanceNumbers.Length != 0)
        {

            total = 0;
            for (int i = 0; i < chanceNumbers.Length; i++)
            {

                total += chanceNumbers[i];
            }

            int roll = Random.Range(0, total);
            tempTotal = 0;

            for (int i = 0; i < chanceNumbers.Length; i++)
            {
                tempTotal += chanceNumbers[i];
                Debug.Log(tempTotal);
                if (i == 0)
                {
                    if (roll - 1 < tempTotal)
                    {
                        chosenTask = i;
                        if (lastTask == chosenTask)
                        {
                            FindRandTasks();
                        }
                        else
                        {
                            lastTask = i;
                        }
                        break;
                    }
                }
                else if (i != 0)
                {
                    if (roll - 1 < tempTotal)
                    {
                        chosenTask = i;
                        if (lastTask == chosenTask)
                        {
                            FindRandTasks();
                        }
                        else
                        {
                        lastTask = i;
                        }
                       
                        break;
                    }
                }
                else
                {
                    Debug.Log("NOT FOUND ERROR");
                }

            }
            total = 0;
        }


    }


    void SpawnNpcRoll()
    {

       
        
        if (npcTypesList != null)
        {
            int typeRoll = Random.Range(0, npcTypesList.Length - 1);
            int spawnRoll = Random.Range(0, 100);
            print("SPAWN ROLL" + spawnRoll);


            if (npcSpawnChance >= spawnRoll)
            {
                Debug.Log("NPC Spawned");

                for (int i = 0; i < aiActors.Length ; i++)
                {
                    if (aiActors[i] == null)
                    {
                    print("SPAWNING NPC");
                    GameObject NPC = Instantiate(npcTypesList[typeRoll], npcSpwanLocation.position, Quaternion.identity);
                        NPC.SetActive(true);
                        aiActors[i] = NPC;
                        //Setup the Rest
  
                        npcSpawnChance = npcSpawnBase;
                        break;
                    }
                }


            }

        }
    }



}