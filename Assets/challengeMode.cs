using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class challengeMode : MonoBehaviour {

    GameManager GM;
    GameDataStore GameStore;

	// Use this for initialization
	void Start () {
        GM = this.GetComponent<GameManager>();
        GameStore = this.GetComponent<GameDataStore>();
	}

    // Update is called once per frame
    void Update() {
        if (GameStore.GetCurrentRage() != 0) { 
            if((GameStore.GetCurrentRage() / 10) > 1)
                Time.timeScale = GameStore.GetCurrentRage() / 10;
            }
	}
}
