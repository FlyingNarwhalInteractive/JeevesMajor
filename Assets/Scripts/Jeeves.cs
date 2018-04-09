using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeeves : MonoBehaviour {
    public Vector3 clickPoint;
    public GameObject clickTarget;
    public GameObject m_mouseTarget;
    public Vector3 m_mousePosition;
    public bool m_taskStarted = false;
    Movement move;
    private GameDataStore dataRef;
    private GameObject baronRef;
    private GameManager GMRef;
    public GameObject currentTask = null;
    private JeevesFX FX;
    public bool isFetchActive;
    public bool hasFetch;
    [SerializeField] float fetchDist;
    public int fetchReward;
    public int fetchRage;
    public float  jeevesFetchTimeout;
    private Tutorial tutRef;
    public LineRenderer line;
    //protection timeout;
    [SerializeField] float protectTimer;
    [SerializeField] float protectTime;
    NavManager navManager;

    float doubleClickTimer;
    float doubleClickTime = 0.5f;

    public GameObject BLight;
	public GameObject CompleteFX;
   // [SerializeField] int fetchRage;
   //[SerializeField] float wait;
   // public bool fetchStarted;
   // public List<GameObject> fetches;
   // [SerializeField] float  fcount;

    public bool m_jeevesIsMagicAndCanOpenDoorsFromAfar = false;

    public GameObject[] m_distractions;

    // Use this for initialization
    void Start()
    {
        navManager = FindObjectOfType<NavManager>();
        //confirm if tutorial is enabled
        if (GameObject.FindGameObjectWithTag("Tut") != null)
        {
        tutRef = GameObject.FindGameObjectWithTag("Tut").GetComponent<Tutorial>();
        }
        //reference draw line
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        protectTimer = protectTime;
        hasFetch = false;
      //  fetchStarted = false;
      //  fcount = 0;
        //grab references
        if (GetComponent<JeevesFX>() != null)
        {
            FX = GetComponent<JeevesFX>();
        }
        dataRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();
        baronRef = GameObject.FindGameObjectWithTag("Baron");
        GMRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        move = GetComponent<Movement>();
        clickPoint = gameObject.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
         if(hasFetch)
        {
           
            

            if (jeevesFetchTimeout > 0)
            {
                jeevesFetchTimeout -= Time.deltaTime;

                Vector3[] newLine = navManager.GetPath(gameObject.transform.position, baronRef.transform.position).corners;
                line.positionCount = newLine.Length;
                line.SetPositions(newLine);
                line.enabled = true;
                
                //draw line
                //line.SetPosition(0, gameObject.transform.position);
                //line.SetPosition(1, baronRef.transform.position);

                // baron light
                BLight.SetActive(true);
            }
            else
            {
                // baron light
                BLight.SetActive(false);
                line.enabled = false;
                dataRef.SetCurrentRage(fetchRage);
                hasFetch = false;
                isFetchActive = false;
            }
        }
 

         if (baronRef.GetComponent<BaronAI>().protect == true)
        {
            if(protectTimer > 0)
            {
            protectTimer -= Time.deltaTime;
            }
            else
            {
                baronRef.GetComponent<BaronAI>().protect = false;
                protectTimer = protectTime;
            }
        }



        //check baron distance
        if(Vector3.Distance(gameObject.transform.position, baronRef.transform.position) < fetchDist)
        {
            if(hasFetch == true)
            {
                //disable lights and lines
                // baron light
                isFetchActive = false;
                BLight.SetActive(false);
                line.enabled = false;
                baronRef.GetComponent<NotTheRealAI>().isPaused = false ;
                hasFetch = false;
                baronRef.GetComponent<BaronAI>().protect = true;
                //reward player
                GMRef.CurrentScore = fetchReward;

				GameObject g = Instantiate(CompleteFX, baronRef.gameObject.transform.position, CompleteFX.transform.rotation);
				Destroy(g, 2.0f);
                    
            }
        }



        if (Fire())
        {
            if(tutRef != null)
            {
            if (tutRef.GetCurrent() == 0)
            {
                tutRef.OK();
            }
            }

            clickPoint = ClickToWorld();
            clickTarget = m_mouseTarget;
            m_taskStarted = false;

            if (FX != null)
            {
                if (FX.clickObjectFX != null)
                {
                    FX.ClickedObject(clickPoint);
                }
            }
        }
        else
            m_mousePosition = ClickToWorld();


        if (Vector3.Distance(gameObject.transform.position, clickPoint) > 1)
        {
            if (  clickTarget != null && clickTarget.tag == "Door")
            {
                // this is as per bobbos request, can close and open doors magically from afar - like some sort of sophisticated butlery WIZARD!
                if (m_jeevesIsMagicAndCanOpenDoorsFromAfar && (Time.time - doubleClickTimer < doubleClickTime || !SaveData.doubleClickDoors))
                {
                    StartTask();
                    move.GoTo(transform.position);
                }
                else
                {
                    doubleClickTimer = Time.time;
                    move.GoTo(clickPoint);
                    clickTarget = null;
                }
            }
            else
                move.GoTo(clickPoint);
        }
        else
        {
            clickPoint = gameObject.transform.position;
            StartTask();
        }

    }

    void StartTask()
    {
        if (m_taskStarted)
            return;
        if(clickTarget != null)
        {
        switch (clickTarget.tag)
        {
            case "Door":
                    if (Time.time - doubleClickTimer < doubleClickTime || !SaveData.doubleClickDoors)
                    {
                        clickTarget.GetComponent<Door>().Action();
                        m_taskStarted = true;
                        clickPoint = transform.position;
                    }
                    else
                        doubleClickTimer = Time.time;
                break;
            case "Task":
                currentTask = clickTarget.gameObject;
                break;
            default:
                break;
        }
        }

     
    }

	private bool IsClickValid()
	{
		//moyse screen position
		Vector3 mouse_pos = Input.mousePosition;
		//mouse screen position to ray mased on camera position and angle
		Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_pos);
		//create a plane to intersect
		Plane player_plane = new Plane(Vector3.up, transform.position);
		//temp
		float ray_distance = 0;
		//get distance 
		player_plane.Raycast(mouse_ray, out ray_distance);
		// SP: Get thing that we hit
		RaycastHit hit;
		Physics.Raycast(mouse_ray, out hit);

		if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
		{
			return false;
		}
		else
		{
			return true;
		}

	}

    public Vector3 ClickToWorld()

    {

		bool found = false;
		//moyse screen position
		Vector3 mouse_pos = Input.mousePosition;
		//mouse screen position to ray mased on camera position and angle
		Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_pos);
		//create a plane to intersect
		Plane player_plane = new Plane(Vector3.up, transform.position);
		//temp
		float ray_distance = 0;
		//get distance 
		player_plane.Raycast(mouse_ray, out ray_distance);
		// SP: Get thing that we hit
		RaycastHit[] hits;
		hits = Physics.RaycastAll(mouse_ray);


		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];

			if (hit.collider.tag == "Task" || hit.collider.tag == "Door" || hit.collider.tag == "Fetch" || hit.collider.tag == "Baron")
			{
				m_mouseTarget = hit.transform.gameObject;
				found = true;
			}
		}

		if (found == false)
		{
			m_mouseTarget = null;
		}

		//get intersect point
		Vector3 cast_point = mouse_ray.GetPoint(ray_distance);

		//print plane intersect point to screen
		// print(cast_point);



		//return intersect value
		return cast_point;
	}

    bool Fire()
    {
        //is left click pressed
		if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
		{
        return Input.GetMouseButtonDown(0);
		}
		else
		{
			return false;
		}
	

     
      

    }

    public void PerformDistraction(int distraction)
    {
        
        // check if already done?
        if (m_distractions[distraction] != null)
        {
            dataRef.interrupsUsed += 1;
            // distraction doth exist, do it.
            m_distractions[distraction].SetActive(true);
            clickPoint = m_distractions[distraction].transform.position;
            clickTarget = m_distractions[distraction];
        }

        return;
    }

}
