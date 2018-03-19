using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionCooldown : MonoBehaviour
{

    public float initialSpawnDelay = 30;
    public float taskCooldown = 30;
    public float timeLastUsed;
    public bool onCooldown = false;
    public bool spawnedInitial = false;

    Task myTask;

    private void Start()
    {
        myTask = GetComponent<Task>(); 
    }
    private void OnDisable()
    {
        if (myTask.isCompleted && !onCooldown)
        {
            onCooldown = true;
            timeLastUsed = Time.time;
        }
    }
}