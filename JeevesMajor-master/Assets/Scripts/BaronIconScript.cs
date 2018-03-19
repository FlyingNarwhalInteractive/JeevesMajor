using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaronIconScript : MonoBehaviour {

    public GameObject annoyedQuad;
    Material quadMaterial;
    public Texture jeevesAnnoyed;
    public Texture trappedAnnoyed;
    public Texture defaultAngerIcon;
    public Texture defaultTaskingIcon;
    public Texture doorTask;
    public Texture fetchTask;
    public List<Texture> m_currentAnnoyances;

    BaronAI m_baronAI;
    NotTheRealAI m_notTheRealAI;
    Movement m_movement;
    GameObject[] taskObjects;
    Jeeves jeevesRef;
    Task taskInQuestion;



    int currentIcon = 0;
    public float iconSwapSpeed = 1.0f;
    float timeLastSwapped;

	// Use this for initialization
	void Start ()
    {

        m_baronAI = GetComponent<BaronAI>();
        m_notTheRealAI = GetComponent<NotTheRealAI>();
        m_movement = GetComponent<Movement>();
        m_currentAnnoyances = new List<Texture>();
        jeevesRef = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();
        quadMaterial = annoyedQuad.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //check for new annoyances
        m_currentAnnoyances.Clear();
        //jeeves
        if (m_baronAI.isJeevesSpotted())
            AddAnnoyance(jeevesAnnoyed);
        // trapped
        if (m_movement.m_isTrapped)
            AddAnnoyance(trappedAnnoyed);
        //if Jeeves has fetch
        if (jeevesRef.isFetchActive == true)
            AddAnnoyance(fetchTask);

/*
        //if doorTask is active
        GameObject[] t = GameObject.FindGameObjectsWithTag("Task");
        foreach(GameObject g in t)
        {
            if(g.name.Contains("DoorTask"))
            {
            AddAnnoyance(doorTask);
                break;
            }
        }
            */
            

        // overdue tasks
        taskObjects = GameObject.FindGameObjectsWithTag("Task");
        foreach (var task in taskObjects)
        {
            //if doorTask is active
            if (task.name.Contains("DoorTask"))
            {
                AddAnnoyance(doorTask);
                continue;
            }

            taskInQuestion = task.GetComponent<Task>();
            if (taskInQuestion.isVisibleToBaron && taskInQuestion.isDetect && !taskInQuestion.isBaron && taskInQuestion.ragedOnce)
            {
                if (taskInQuestion.GetComponent<DistractionCooldown>() == null) // ignore task if is distraction
                    AddAnnoyance(taskInQuestion.baronAngerIcon != null ? taskInQuestion.baronAngerIcon : defaultAngerIcon);
            }
        }
        // baron working
        if (m_notTheRealAI.isDistracted)
        {
            AddAnnoyance(m_notTheRealAI.distractionTask.workingIcon != null ? m_notTheRealAI.distractionTask.workingIcon : defaultTaskingIcon);
        }

        if (m_currentAnnoyances.Count != 0)
        {
            annoyedQuad.SetActive(true);

            // cycle through annoyances (icons)
            if (Time.time - timeLastSwapped > iconSwapSpeed)
            {
                currentIcon++;
                timeLastSwapped = Time.time;
            }
            if (currentIcon >= m_currentAnnoyances.Count)
                currentIcon = 0;

            // display annoyances
            quadMaterial.SetTexture("_MainTex", m_currentAnnoyances[currentIcon]);
        }
        else
            annoyedQuad.SetActive(false);
    }

    void AddAnnoyance(Texture texture)
    {
        if (texture == null)
            texture = defaultAngerIcon;
        if (!m_currentAnnoyances.Contains(texture))
            m_currentAnnoyances.Add(texture);
    }
}
