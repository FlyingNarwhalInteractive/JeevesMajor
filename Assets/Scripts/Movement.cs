using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
public class Movement : MonoBehaviour
{

	public bool m_isTravelling = false;
	public bool m_isTrapped;
	public float timeUntilTrapped = 30.0f;
	public float trappedTimer = 0;
	public float trappedRage = 5;
	public float notTrappedCalmingAmount = -5.0f;

	Vector3 m_currentDestination;
	NavManager navManager;
	NavMeshPath m_navPath;
	[SerializeField] List<Door> m_knownObstacles;
	NavMeshAgent m_navAgent;
	GameManager m_gm;
	public float m_arrivalDistanceThreshold = 1.2f;

	Door lastDoor = null;
	public float lastRenavTime = 0;

	public GameObject m_agentAni;


	// Use this for initialization
	void Start()
	{

		navManager = FindObjectOfType<NavManager>();
		m_knownObstacles = new List<Door>();
		m_navAgent = GetComponent<NavMeshAgent>();
		m_currentDestination = transform.position;
		m_gm = FindObjectOfType<GameManager>();
		GetComponent<Rigidbody>().isKinematic = true;
	}

	// Update is called once per frame
	void Update()
	{
		{

			if (m_agentAni.GetComponent<Animator>() != null)
			{
				if (m_isTravelling)
				{
					m_agentAni.GetComponent<Animator>().SetTrigger("IsTravel");
					m_agentAni.GetComponent<Animator>().SetBool("StopTravel", true);
				}
				else
				{
					m_agentAni.GetComponent<Animator>().SetBool("StopTravel", false);
				}
			}

		}



		// reset timer when we arrive
		if (m_isTravelling)
		{
			trappedTimer += Time.deltaTime;
			if (Vector3.Distance(m_currentDestination, transform.position) < m_arrivalDistanceThreshold)
			{
				m_isTravelling = false;
				trappedTimer = 0;
				m_knownObstacles.Clear();
				m_navAgent.ResetPath();
			}
		}

		if (trappedTimer > timeUntilTrapped)
			m_isTrapped = true;
		else
			m_isTrapped = false;
	}

	bool Navigate()
	{
		// get path
		m_navPath = navManager.GetPath(transform.position, m_currentDestination, m_knownObstacles.ToArray());

		if (m_navPath == m_navAgent.path)
			return true;

		// walk
		if (m_navPath != null)
		{
			m_isTravelling = true;
			//m_navAgent.SetPath(m_navPath);
			if (!m_navAgent.pathPending)
				m_navAgent.SetDestination(m_currentDestination);
			return true;
		}

		return false;
	}

	public void AddClosedDoor(Door door)
	{
		// add door to list
		if (m_knownObstacles.Exists(d => d == door))
		{
			// already in list, can presume if we are revisiting a door we are stuck.
			if (door == lastDoor && m_knownObstacles.Count > 1)
			{
				m_knownObstacles.Clear();
				m_knownObstacles.Add(door);
			}
			else
			{
				lastDoor = door;
				Navigate();
				return;
			}
		}
		else
		{
			// add to list
			m_knownObstacles.Add(door);
			lastDoor = door;
			Navigate();
		}
	}

	public bool GoTo(Vector3 destination)
	{

		if (m_currentDestination == destination)
			return true;

		m_currentDestination = destination;
		return Navigate();
	}
	/*
 void OnDrawGizmosSelected()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
    */




}



