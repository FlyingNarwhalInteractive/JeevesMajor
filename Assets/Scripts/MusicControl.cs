using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour {

    [FMODUnity.EventRef]
    public string music = "event:/BGM_Game";

    FMOD.Studio.EventInstance musicEv;
    FMOD.Studio.Bus musicBus;

    void Start ()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        musicEv.start();
    }

    void Update()
    {
        // Check Mute status for music
        bool musicMuted;
        musicBus.getMute(out musicMuted);

        // If current scene is Main Menu, unmute music.
        if(SceneManager.GetActiveScene().buildIndex == 0 && musicMuted)
        {
            musicBus.setMute(false);
        }

        // If current scene is Upgrades Menu, unmute music.
        if (SceneManager.GetActiveScene().buildIndex == 2 && musicMuted)
        {
            musicBus.setMute(false);
        }
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

    //Mute Music only
    public void MuteMusic()
    {
        Debug.Log("Muted Music");
        musicBus.setMute(true);
    }

    //Unmute Music only
    public void UnmuteMusic()
    {
        Debug.Log("Unmuted Music");
        musicBus.setMute(false);
    }
}
