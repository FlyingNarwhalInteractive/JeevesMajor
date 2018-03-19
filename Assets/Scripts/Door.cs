using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour {

    public bool m_isOpen = false;
    bool wasOpen;
    public NavMeshObstacle m_obstacle;
    public GameObject[] m_doors;
    public GameObject m_fullDoorway;
    BoxCollider m_doorCollider;
    public NavManager m_navManager;
    private GameDataStore dataRef;
    //[SerializeField] int cost = 10;
    // Use this for initialization
    void Awake () {
        //create datastore ref
        dataRef =  GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();
        // get door objects THIS IS A HORRIBLE HORRIBLE BUG PRONE HACK
        m_doors = new GameObject[2];

        m_doors[0] = transform.Find("G_DoubleDoorFull_01").Find("G_door_L").gameObject;
        m_doors[1] = transform.Find("G_DoubleDoorFull_01").Find("G_door_R").gameObject;

        // create dynamic collider
        m_fullDoorway = transform.Find("G_DoubleDoorFull_01").gameObject;
        m_doorCollider = m_fullDoorway.AddComponent<BoxCollider>();
        m_doorCollider.center = new Vector3(0, 0.75f, 0);
        m_doorCollider.size = new Vector3(2, 1.5f, 0.1f);


        // check if door should be open, if so, open.
        wasOpen = !m_isOpen;
    }

	
	// Update is called once per frame
	void Update () {
        if (wasOpen != m_isOpen)
        {
            SetState(m_isOpen);
            wasOpen = m_isOpen;
        }
	}

    public void Action()
    {
        if (m_isOpen)
        {
            //check cost
            if (dataRef.Stamina >= dataRef.DoorCost) 
            {
            Close();
            dataRef.SetUsedLocks(1);
 
            }
        }
           
        else
            Open();
    }

    public void Open()
    {
        if(m_isOpen == false)
            SetState(true);
    }

    public void Close()
    {
        if (m_isOpen == true)
            SetState(false);
    }

    void SetDoorObjectsVisible(bool enabled)
    {
        if (m_doors == null)
            return;

        foreach (var door in m_doors)
        {
            door.SetActive(!enabled);
        }
    }

    void SetState(bool isOpen)
    {
        // check with navmanager if we are allowed
        if (isOpen)
        {
            // open door
            m_navManager.OpenDoor(this);
        }
        else
        {
            if (!m_navManager.CloseDoor(this))
                return;     // computer says no.
        }
        // animate door opening/closing
        // TODO
        m_isOpen = isOpen;
        if (isOpen)
        {
            // disable collider
            m_doorCollider.enabled = false;
            // lift door out of way
            m_obstacle.center = new Vector3(0, 3, 0);
            // TODO remove from locked door list.
        }
        else
        {
            //enable collider
            m_doorCollider.enabled = true;
            //drop door into player path
            m_obstacle.center = new Vector3(0, 0, 0);
            //pay toll
            dataRef.Stamina = -dataRef.DoorCost;
            // tODO checks and shizzle
        }
        SetDoorObjectsVisible(isOpen);
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_isOpen)
            return;

        Movement movement = other.gameObject.GetComponent<Movement>();
        if (movement == null)
            return;

        if (Time.time - movement.lastRenavTime > 1.0f)
        {
            movement.AddClosedDoor(this);
            movement.lastRenavTime = Time.time;
        }
    }




}
