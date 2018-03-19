using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBase : MonoBehaviour
{
    private Movement move;
    private Scheduler schedule;
    

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Movement>();
        schedule = GetComponent<Scheduler>();

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < schedule.tasks.Length; i++)
        {
            if(schedule.tasks[i] != null)
            {
                move.GoTo(schedule.tasks[i].transform.position);
                continue;
            }

        }


    }
}

