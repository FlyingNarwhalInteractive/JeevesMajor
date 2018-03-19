using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

    [FMODUnity.EventRef]
    public string music = "event:/BGM_Game";

    FMOD.Studio.EventInstance musicEv;

    void Start ()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);

        musicEv.start();
	}
	
    //Game started, main music loop
    public void MainLoop()
    {
        musicEv.setParameterValue("Anger", 1f);
    }

    //Baron's anger is over 50
    public void Anger()
    {
        musicEv.setParameterValue("Anger", 50f);
    }

    //Baron Anger >= 100: Game over, end screen showing
    public void EndScreen()
    {
        musicEv.setParameterValue("Anger", 100f);
    }

}
