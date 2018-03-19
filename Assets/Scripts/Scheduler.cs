using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{
    private Jeeves jeevesRef;
    private GameObject gameManagerRef;
    private Vector2 worldClock;
    private Vector2 spawn;
    //  private float tempClock;
    public GameObject[] tasks;
    public Vector2[] activateTime;

    // Use this for initialization
    void Start()
    {
        //grab reference
        jeevesRef = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();
        gameManagerRef = GameObject.FindGameObjectWithTag("GM");
        gameManagerRef.GetComponent<GameDataStore>().GetTime();
        // tempClock = 0;

        print(tasks.Length);

        for (int i = 0; i < tasks.Length; i++)
        {

            if (tasks[i] != null)
            {

                tasks[i].GetComponent<Task>().spawnTime = activateTime[i];

                if (activateTime[i] == Vector2.zero)
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


                tasks[i].SetActive(false);
            }


        }

    }

    void Update()
    {
        worldClock = gameManagerRef.GetComponent<GameDataStore>().GetTime();


        //loop task time array
        for (int i = 0; i < tasks.Length; i++)
        {


            if (tasks[i] != null)
            {



                //if not completed
                if (tasks[i].GetComponent<Task>().isCompleted == false)
                {

                    //if spawn time not set

                    if (tasks[i].GetComponent<Task>().spawnTime == new Vector2(0, 0))
                    {
                        spawn = activateTime[i];
                    }
                    else
                    {
                        spawn = tasks[i].GetComponent<Task>().spawnTime;
                    }

                    if (spawn == worldClock)

                    {
                        print("Spawn" + spawn + tasks[i].gameObject.name.ToString());

                        //activate task in world
                        if (tasks[i].tag == "Dummy")
                        {
                            //if dummy is a fetch task
                            if (tasks[i].GetComponent<Task>().isFetch || tasks[i].GetComponent<Task>().messSpawnMode != 0)
                            {
                                Task temp = tasks[i].GetComponent<Task>();

                                if (temp.messSpawnMode != 0)
                                {
                                    temp.spawnMess();
                                    Destroy(tasks[i]);
                                }

                                //if schedule belongs to baron
                                if (gameObject.tag == "Baron")
                                {
                                    //if spawned object exists, spawn/pause baron/delete baron task
                                    if (temp.FetchObj != null)
                                    {
                                        GameObject fobj;
                                        fobj = Instantiate(temp.FetchObj, temp.transform.position, Quaternion.identity);
                                        fobj.SetActive(true);
                                        GetComponent<Movement>().GoTo(temp.baronFetchLocation.position);
                                        GetComponent<NotTheRealAI>().isPaused = true;


                                        // jeevesRef.fetches.Add(fobj);




                                        Destroy(tasks[i]);
                                    }
                              
                                    else
                                    {
                                        Destroy(tasks[i]);
                                    }
                                }
                            }
                            else
                            {

                                Destroy(tasks[i]);
                            }

                        }
                        else
                        {
                            tasks[i].gameObject.SetActive(true);
                        }

                        //Debug.Log("ACTIVATED");

                    }
                }
            }



        }
    }
}




