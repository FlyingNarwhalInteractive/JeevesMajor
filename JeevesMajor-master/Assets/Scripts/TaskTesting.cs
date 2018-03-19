using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTesting : MonoBehaviour
{
    bool taskDetected = false;
    public float cooldownTimer = 5;
    float timerCount;

    // Use this for initialization
    void Start ()
    {
        timerCount = cooldownTimer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (taskDetected)
        {
            timerCount -= Time.deltaTime;
            if(timerCount < 0)
            {
                Debug.Log("Detect() cooldown elapsed");
                timerCount = cooldownTimer;
                taskDetected = false;
            }
        }
	}

    public void Detect()
    {
        Debug.Log("Detect() called upon");
        taskDetected = true;
    }
}
