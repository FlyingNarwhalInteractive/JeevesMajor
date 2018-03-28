using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaronAI : MonoBehaviour
{
    // Public Variables
    public float detectionRange = 2;
    public float jeevesDetectionCooldown = 30;
    public int jeevesRage = 0;
    public int maxRage;
    public int currentRage;
    public bool turnOnRaycasts = false; // Flag this as true to see the Raycasts
    public List<GameObject> currentAnnoyances;
    List<GameObject> tempAnnoyances = new List<GameObject>();
    public GameObject Ring;
    public GameObject Line;
    GameObject[] annoyanceLines;


    //protect jeeves after handing in fetch
    public bool protect;

    // Private Variables
    GameObject[] taskObjects;
    GameObject jeevesObject;
    [SerializeField] bool jeevesSpotted;
    [SerializeField] bool isJeevesVisibleRightNow = false;
    float jeevesTimer;
    GameObject gameManager;

    private Jeeves jeevesRef;
    private Tutorial tutRef;
    //  [FMODUnity.EventRef]
    // public string BaronSigh;

    // Use this for initialization
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Tut") != null)
        {
        tutRef = GameObject.FindGameObjectWithTag("Tut").GetComponent<Tutorial>();
        }
        if (Ring != null)
        {
            Ring.gameObject.transform.localScale = Ring.transform.localScale * (detectionRange * 2);
        }
        protect = false;
        jeevesTimer = jeevesDetectionCooldown;
        gameManager = GameObject.FindGameObjectWithTag("GM");
        maxRage = gameManager.GetComponent<GameDataStore>().GetMaxRage();
        currentRage = gameManager.GetComponent<GameDataStore>().GetCurrentRage();
        jeevesRef = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();

        annoyanceLines = new GameObject[1];
        annoyanceLines[0] = new GameObject();
    }

    // Update is called once per frame
    void Update ()
    {
        // Cooldown Timer for Jeeves being spotted.
        if(jeevesSpotted) 
        {
            jeevesTimer -= Time.deltaTime;
        }

        // Cooldown Timer being reset.
        if(jeevesTimer < 0)
        {
            jeevesSpotted = false;
            jeevesTimer = jeevesDetectionCooldown;
            Ring.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
        }
        
        // Check if Baron has reached max Rage.
        currentRage = gameManager.GetComponent<GameDataStore>().GetCurrentRage();
        if (currentRage >= maxRage)
            BaronRages();
    }
	
	void FixedUpdate ()
    {
        // Get the Game Objects
        taskObjects = GameObject.FindGameObjectsWithTag("Task");
        jeevesObject = GameObject.FindGameObjectWithTag("Jeeves");
        Vector3 rayOrigin = transform.position;
        rayOrigin.y += 1;

        tempAnnoyances.Clear();

        // Checking all tasks in Task Objects array
        foreach (GameObject task in taskObjects)
        {
			if(!task.GetComponent<Task>().isAI)
			{ 

            RaycastHit hit; // Object hit by Raycast
            Vector3 objBearing = Vector3.Normalize(task.GetComponent<BoxCollider>().ClosestPoint(transform.position) - rayOrigin); // Calculate bearing of current task

            // Check if Raycast has hit the object.
            bool spottedObject = Physics.Raycast(rayOrigin, objBearing, out hit, detectionRange);
            if(turnOnRaycasts)
                Debug.DrawRay(rayOrigin, objBearing * detectionRange, Color.green);

            // If the object hit is the task, handle task detection.
            if (spottedObject && hit.transform == task.transform)
            {

                Debug.Log("Baron has detected a Task");
                //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/BaronSigh");
                task.GetComponent<Task>().Detect();
                if (task.GetComponent<Task>().ragedOnce)
                    tempAnnoyances.Add(task);
            }
            else
            {
                task.GetComponent<Task>().isVisibleToBaron = false;
                Disturbances dist = task.GetComponent<Disturbances>();

                if (dist != null)
                    if (Vector3.Distance(transform.position, task.transform.position) < dist.sightRange)
                        if(dist.wait <= 0)
                            tempAnnoyances.Add(task);
            }
			}

		}


        // Checks if Jeeves has been detected.
        if (jeevesObject != null && jeevesRef.hasFetch == false && protect == false)
        {
            RaycastHit hit; // Object hit by Raycast
            Vector3 objBearing = Vector3.Normalize(jeevesObject.transform.position - rayOrigin); // Calculate bearing of Jeeves

            // Check if Raycast has hit Jeeves.
            bool spottedObject = Physics.Raycast(rayOrigin, objBearing, out hit, detectionRange);
            if (turnOnRaycasts)
                Debug.DrawRay(rayOrigin, objBearing * detectionRange, Color.red);

            
            // If the object hit is Jeeves, handle the detection.
            if (spottedObject && hit.transform == jeevesObject.transform)
            {
                Debug.Log("Baron has detected Jeeves");
                Ring.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                //tutorial link
                if(tutRef != null)
                {
                if (tutRef.GetCurrent() == 3)
                {
                    tutRef.OK();
                }
                }


                if (!jeevesSpotted)
                    gameManager.GetComponent<GameDataStore>().SetCurrentRage(jeevesRage);
                jeevesSpotted = true;
                isJeevesVisibleRightNow = true;
                tempAnnoyances.Add(jeevesObject);
            }
            else
                isJeevesVisibleRightNow = false;
        }

        // put a line on it
        // remove exisiting lines
        for (int i = 0; i < annoyanceLines.Length; i++)
        {
            Destroy(annoyanceLines[i]);
        }

        annoyanceLines = new GameObject[tempAnnoyances.Count];

        for (int i = 0; i < tempAnnoyances.Count; i++)
        {
            annoyanceLines[i] = Instantiate(Line);
            annoyanceLines[i].name = "AngerLine";
            annoyanceLines[i].transform.parent = tempAnnoyances[i].transform;
            annoyanceLines[i].transform.localPosition = Vector3.zero;
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = tempAnnoyances[i].transform.position;
            annoyanceLines[i].GetComponent<LineRenderer>().SetPositions(positions);
        }

        currentAnnoyances = tempAnnoyances;
    }

    // When Baron Rages, this code executes.
    void BaronRages ()
    {
        //Debug.Log("Baron Rages");
        // Insert Code where Baron Rages.
    }

    public bool isJeevesSpotted()
    {
        return isJeevesVisibleRightNow;
    }

}
