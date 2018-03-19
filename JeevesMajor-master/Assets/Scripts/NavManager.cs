using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavManager : MonoBehaviour {

    public GameObject[] m_doors;
    public GameObject[] m_walls;
    public GameObject[] m_permaClosedDoors;
    public List<Door> m_doorsClosed = new List<Door>();
    GameDataStore m_gameDataStore;
    public Vector3 doorColliderSize = new Vector3(0.8f, 0.8f, 0.8f);

    [FMODUnity.EventRef]
    public string DoorOpen;
    [FMODUnity.EventRef]
    public string DoorClose;

    // Use this for initialization
    void Start () {
        m_gameDataStore = GameObject.FindWithTag("GM").GetComponent<GameDataStore>();
        
        // get all doors
        m_doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (var door in m_doors)
        {
            // add trigger
            BoxCollider collider = door.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            Rigidbody rigidbody = door.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            // create obstacles, add scripts
            Door script = door.AddComponent<Door>();
            script.m_obstacle = door.AddComponent<NavMeshObstacle>();
            script.m_obstacle.size = doorColliderSize;
            script.m_navManager = this;
            // open doors
            script.Open();
        }
        // get all walls
        m_walls = GameObject.FindGameObjectsWithTag("Wall");
        // Create obstacles
        foreach (var wall in m_walls)
        {
            NavMeshObstacle obstacle = wall.gameObject.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;
        }
        // get all perma closed doors
        m_permaClosedDoors = GameObject.FindGameObjectsWithTag("ClosedDoor");
        // Create obstacles
        foreach (var door in m_permaClosedDoors)
        {
            NavMeshObstacle obstacle = door.gameObject.AddComponent<NavMeshObstacle>();
            obstacle.size = doorColliderSize;
            obstacle.carving = true;
        }
    }
	

    public NavMeshPath GetPath(Vector3 origin, Vector3 destination, Door[] knownObstacles = null)
    {
        // TODO: Calc a path, return path.
        NavMeshPath newPath = new NavMeshPath();

        // turn off door carvers
        foreach (var door in m_doors)
        {
                   door.GetComponent<NavMeshObstacle>().carving = false;
        }

        // Turn on door carvers
        if (knownObstacles != null)
        {
            foreach (var door in knownObstacles)
            {
                if (door != null)
                {
                    door.gameObject.GetComponent<NavMeshObstacle>().carving = true;
                }
            }
        }
        // Make path
        
        bool success = NavMesh.CalculatePath(origin, destination, NavMesh.AllAreas, newPath);

        // did we do it?
        if (success)
            return newPath;
        else
            return null;

    }

    public bool CloseDoor(Door door)
    {
        if (m_doorsClosed.Contains(door))
            return false;  // already shut

        if (m_doorsClosed.Count < m_gameDataStore.GetMaxLocks())
        {
            m_doorsClosed.Add(door);
            FMODUnity.RuntimeManager.PlayOneShot(DoorClose);
            return true;

        }
        else
            return false;
    }

    public void OpenDoor(Door door)
    {
        if (m_doorsClosed.Contains(door))
        {
            m_doorsClosed.Remove(door);
            FMODUnity.RuntimeManager.PlayOneShot(DoorOpen);
        }
    }
}
