using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotTheRealAI : MonoBehaviour
{
    public bool isAutomated;
    public bool isPaused;
    public bool isDistracted;
    public Task distractionTask = null;
    public GameObject[] m_waypoints;
    GameObject m_lastWaypoint;
    GameObject m_beforeLastWaypoint;
    GameObject m_currentWaypoint;
    Disturbances m_disturbance;
    public float m_waitTimeBetweenWaypoints = 5;
    float m_lastWaypointTime = 0;
    bool m_wasTravelling = false;
    Movement m_movement;
    Scheduler schedule;
    [SerializeField] float timeOut;
    [SerializeField] float timeOutCap;
    public bool wander;


    // Use this for initialization
    void Start()
    {
        m_disturbance = GetComponent<Disturbances>();
        wander = false;
        m_movement = GetComponent<Movement>();
        schedule = GetComponent<Scheduler>();

        if (m_waypoints.Length < 3)
        {
            Debug.LogError("Baron needs 3 or more waypoints!");
            return;
        }
        m_lastWaypoint = m_waypoints[Random.Range(0, m_waypoints.Length)];
        do
        {
            m_beforeLastWaypoint = m_waypoints[Random.Range(0, m_waypoints.Length)];
        } while (m_lastWaypoint == m_beforeLastWaypoint);

    }

    public void DistractBaron(Task task)
    {
        isDistracted = true;
        distractionTask = task;
    }




    // Update is called once per frame
    void Update()
    {
        if (isAutomated)
        {
            if (m_movement.m_isTrapped)
            {
                // do trapped stuff!
                m_disturbance.amountRage = m_movement.trappedRage;
            }
            else
            {
                m_disturbance.amountRage = m_movement.notTrappedCalmingAmount;
            }

            if (isDistracted)
            {
                // override EVERYTHING
                if (distractionTask == null || !distractionTask.gameObject.activeSelf)
                {
                    // task is done
                    isDistracted = false;
                    return;
                }

                // otherwise, go to task - aka - do it.
                m_movement.GoTo(distractionTask.transform.position);
            }
            else
            {
                if (m_waypoints.Length < 3)
                {
                    Debug.LogError("Baron needs 3 or more waypoints!");
                    return;
                }

                // reset timer when we arrive
                if (!m_movement.m_isTravelling && m_wasTravelling)
                {
                    m_lastWaypointTime = Time.time;
                    m_wasTravelling = false;
                }

                if (Time.time - m_lastWaypointTime > m_waitTimeBetweenWaypoints && !m_movement.m_isTravelling)
                {
                    // get new waypoint
                    do
                    {
                        m_currentWaypoint = m_waypoints[Random.Range(0, m_waypoints.Length)];
                    } while (m_currentWaypoint == m_beforeLastWaypoint || m_currentWaypoint == m_lastWaypoint);

                    if (isPaused == false)
                    {
                        wander = true;
                        for (int i = 0; i < schedule.tasks.Length; i++)
                        {
                            if (schedule.tasks[i] != null)
                            {

                                if (schedule.tasks[i].activeSelf)
                                {
                                    wander = false;
                                    if (Vector3.Distance(gameObject.transform.position, schedule.tasks[i].transform.position) > 1)
                                    {
                                        m_movement.GoTo(schedule.tasks[i].transform.position);
                                    }
                                    break;

                                }

                            }
                        }
                    }

                    if (isPaused != true)
                    {
                        if (wander == true)
                        {
                            timeOut += Time.deltaTime;
                            if (timeOut > timeOutCap)
                            {
                                timeOut = 0;
                                // navigate!
                                if (m_movement.GoTo(m_currentWaypoint.transform.position))
                                {
                                    m_wasTravelling = true;
                                    // set new last waypoints
                                    m_beforeLastWaypoint = m_lastWaypoint;
                                    m_lastWaypoint = m_currentWaypoint;
                                }
                            }
                            else
                            {
                                wander = false;
                            }

                        }
                    }
                }
            }
        }
    }
}

