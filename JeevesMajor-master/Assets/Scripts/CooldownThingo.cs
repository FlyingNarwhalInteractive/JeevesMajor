using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownThingo : MonoBehaviour {

    public Button[] distractionButtons;
    bool[] wasEnabled;
    public float distractionCooldown = 10;
    float lastDistractionTime;
    bool onCooldown = false;

	// Use this for initialization
	void Start () {
        wasEnabled = new bool[distractionButtons.Length];
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - lastDistractionTime > distractionCooldown && onCooldown)
        {
            // time to re-enable
            ReEnableButtons();
        }

	}

    public void StartCooldown()
    {
        if (onCooldown)
            return;

        onCooldown = true;
        lastDistractionTime = Time.time;
        for (int i = 0; i < distractionButtons.Length; i++)
        {
            //get current state and disable all buttons
            wasEnabled[i] = distractionButtons[i].interactable;
            distractionButtons[i].interactable = false;
        }
    }

    void ReEnableButtons()
    {
        for (int i = 0; i < distractionButtons.Length; i++)
        {
            //get current state and disable all buttons
            distractionButtons[i].interactable = wasEnabled[i];
        }

        onCooldown = false;
    }





}
