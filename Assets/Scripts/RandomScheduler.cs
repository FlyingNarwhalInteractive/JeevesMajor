using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomScheduler : MonoBehaviour
{
	//spawn ratio
	private float frequancy;
	public float startRange;
	public float endRange;
	//task list
	public GameObject[] randomTaskList;
	//spawn locations
	public Transform[] spawnLocations;
	//weights
	public int[] chanceNum;

	private int total = 0;
	private int tempTotal = 0;
	private int chosenTask = 0;
	private float localClock;




	// Use this for initialization
	void Start()
	{
		frequancy = Random.Range(startRange, endRange);
	}


	void findRandTask()
	{

		if (randomTaskList.Length != 0 && spawnLocations.Length != 0 && chanceNum.Length != 0)
		{

			total = 0;
			for (int i = 0; i < chanceNum.Length; i++)
			{

				total += chanceNum[i];
			}

			int roll = Random.Range(0, total);
			// Debug.Log("ROLL " + total);
			// Debug.Log("ROLL " + roll);
			tempTotal = 0;

			for (int i = 0; i < chanceNum.Length; i++)
			{
				//   Debug.Log("LENGTH: " + chanceNum.Length);
				tempTotal += chanceNum[i];
				Debug.Log(tempTotal);
				if (i == 0)
				{
					if (roll - 1 < tempTotal)
					{
						// Debug.Log("FOUND!!  " + "ROLL  " + roll + "NUMBER  " + tempTotal + "Total  " + total);
						chosenTask = i;
						// Debug.Log("TASK:  " + chosenTask);
						break;
					}
				}
				else if (i != 0)
				{
					if (roll - 1 < tempTotal)
					{
						// Debug.Log("FOUND!!  " + "ROLL  " + roll + "NUMBER  " + tempTotal + "Total" + total);
						chosenTask = i;
						// Debug.Log("TASK:  " + chosenTask);
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


	// Update is called once per frame
	void Update()
	{


		//TEMP CLOCK
		localClock += Time.deltaTime;



        //if spawn time
        if (frequancy < localClock)
        {
            findRandTask();

            GameObject[] tasks = GameObject.FindGameObjectsWithTag("Task");

            if (randomTaskList[chosenTask].name == "DoorTask")
            {
                if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameClock>().timeOfDay.x > 16)
                    return;

                foreach (GameObject t in tasks)
                {
                    if (t.name == "DoorTask(Clone)")
                    {
                        return;
                    }
                }
            }




			int rand = Random.Range(0, (int)spawnLocations.Length);
			Instantiate(randomTaskList[chosenTask], spawnLocations[chosenTask].position, Quaternion.identity).SetActive(true);
			localClock = 0;

			Debug.Log("Task spawned at: " + frequancy);
			frequancy = Random.Range(startRange, endRange);
		}


	}


}