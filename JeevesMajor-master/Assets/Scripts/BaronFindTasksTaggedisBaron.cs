using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaronFindTasksTaggedisBaron : MonoBehaviour {

    public Task currentTask = null;
    NotTheRealAI ai;

	// Use this for initialization
	void Start () {

        ai = GetComponent<NotTheRealAI>();
	}
	
	// Update is called once per frame
	void Update () {

        if (currentTask != null)
            return; // leave if we are busy

        // OTHERWISE look for new tasks tagged as isBaron
        Task[] tasks = FindObjectsOfType<Task>();

        foreach (Task task in tasks)
        {
            if (task.isBaron == true)
            {
                // we got one! Do it.
                ai.DistractBaron(task);
                break;
            }
        }
	}
}
