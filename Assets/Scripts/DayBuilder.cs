using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayBuilder : MonoBehaviour
{

	public GameObject[] aiActors;
	private GameObject gameManagerRef;
	private Jeeves jeevesRef;
	private GameObject baronRef;

	private GameObject[] tasks;
	// Use this for initialization 
	//task list
	public GameObject[] baronRandomTaskList;
	//weights
	public int[] chanceNum;
	[SerializeField] int[] increment;

	//public Transform[] positions;

	private int total = 0;
	private int tempTotal = 0;
	private int chosenTask = 0;
	private int lastTask;
	private Scheduler tasksRef;

	void Start()
	{
		//grab reference
		jeevesRef = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();
		gameManagerRef = GameObject.FindGameObjectWithTag("GM");
		gameManagerRef.GetComponent<GameDataStore>().GetTime();
		// tempClock = 0;
		baronRef = GameObject.FindGameObjectWithTag("Baron");

		//resize jeeves fetch array
		//' jeevesRef.fetches.Clear();

		//Setup Baron
		tasks = baronRef.GetComponent<Scheduler>().tasks;
		SetUp();
		baronRef.GetComponent<Scheduler>().tasks = tasks;

		foreach (GameObject o in aiActors)
		{
			tasks = o.GetComponent<Scheduler>().tasks;
			SetUp();
			o.GetComponent<Scheduler>().tasks = tasks;

		}


	}

	// Update is called once per frame
	void Update()
	{



	}

	private void Increment()
	{
		if (increment.Length == chanceNum.Length)
		{
			for (int i = 0; i < increment.Length; i++)
			{
				chanceNum[i] += increment[i];

				if (chanceNum[i] < 1)
				{
					chanceNum[i] = 1;
				}
			}

		}

	}

	//Note tasks need to be destroyed for daybuilder to refill them.

	public void NewDay()
	{
		//resize jeeves fetch array
		//jeevesRef.fetches.Clear();

		//increment spawn chances
		Increment();

		//Set up baron
		tasks = baronRef.GetComponent<Scheduler>().tasks; ;
		SetUp();
		baronRef.GetComponent<Scheduler>().tasks = tasks;

		//Setup the Rest
		foreach (GameObject o in aiActors)
		{
			tasks = o.GetComponent<Scheduler>().tasks;
			SetUp();
			o.GetComponent<Scheduler>().tasks = tasks;

		}

	}



	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//
	//                                             //
	//             SETUP SCHEDULER                 //
	//                                             //
	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>//


	//Re-Roll spawn Ranges

	void SetUp()
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
				FindRandTask();


				tasks[i] = Instantiate(baronRandomTaskList[chosenTask]);//, positions[chosenTask]);


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


	void FindRandTask()
	{

		if (baronRandomTaskList.Length != 0 && chanceNum.Length != 0)
		{

			total = 0;
			for (int i = 0; i < chanceNum.Length; i++)
			{

				total += chanceNum[i];
			}

			int roll = Random.Range(0, total);
			tempTotal = 0;

			for (int i = 0; i < chanceNum.Length; i++)
			{
				tempTotal += chanceNum[i];
				Debug.Log(tempTotal);
				if (i == 0)
				{
					if (roll - 1 < tempTotal)
					{
						chosenTask = i;
						if (lastTask == chosenTask)
						{
							FindRandTask();
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
							FindRandTask();
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

}
