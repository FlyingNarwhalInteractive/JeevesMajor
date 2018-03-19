using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownThingoNew : MonoBehaviour
{

    public GameObject[] distractions;
    DistractionCooldown[] cooldowns;
    float[] taskCompleteTime;
    bool[] wasEnabled;



    public void Reset(int i)
    {

        // time to re-enable
        distractions[i].SetActive(true);
        Task task = distractions[i].GetComponent<Task>();
        task.isCompleted = false;
        task.timeToComplete = taskCompleteTime[i];
       // cooldowns[i].onCooldown = false;

    }


    // Use this for initialization
    void Start()
    {
        wasEnabled = new bool[distractions.Length];
        cooldowns = new DistractionCooldown[distractions.Length];
        taskCompleteTime = new float[distractions.Length];
        for (int i = 0; i < distractions.Length; i++)
        {
            //cooldowns[i] = distractions[i].GetComponent<DistractionCooldown>();
            taskCompleteTime[i] = distractions[i].GetComponent<Task>().timeToComplete;
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*
        // do enables
        for (int i = 0; i < distractions.Length; i++)
        {
            if (Time.time > cooldowns[i].initialSpawnDelay && !cooldowns[i].spawnedInitial)
           {
                 first enable
                distractions[i].SetActive(true);
                cooldowns[i].spawnedInitial = true;
            }

            if (Time.time - cooldowns[i].timeLastUsed > cooldowns[i].taskCooldown && cooldowns[i].onCooldown)
            {
                 time to re-enable
               distractions[i].SetActive(true);
               Task task = distractions[i].GetComponent<Task>();
                task.isCompleted = false;
               task.timeToComplete = taskCompleteTime[i];
               cooldowns[i].onCooldown = false;
            }
        }*/
    }
}








