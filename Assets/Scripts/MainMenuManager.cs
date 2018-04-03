using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject gameSettingsPanel;
    public GameObject audioSettingsPanel;
    public GameObject mainPanel;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
		if(pausePanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                if (pausePanel.activeSelf == false)
                {
                    pausePanel.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pausePanel.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(pausePanel.activeSelf == true || audioSettingsPanel.activeSelf == true || gameSettingsPanel.activeSelf == true)
            {
                mainPanel.SetActive(false);
            }
            else
            {
                mainPanel.SetActive(true);
            }
        }
    }

    public void resume()
    {
        if(pausePanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void restartLevel()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void openPausePanel(bool open)
    {
        if (open)
        {
            pausePanel.SetActive(true);
            gameSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            //Time.timeScale = 0;
        }
        
        if (!open)
        {
            pausePanel.SetActive(false);
            gameSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

    }

    public void GameSettings(bool Open)
    {
        if (Open)
        {
            gameSettingsPanel.SetActive(true);
            audioSettingsPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
        if (!Open)
        {
            gameSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
    }

    public void AudioSettings(bool Open)
    {
        if (Open)
        {
            audioSettingsPanel.SetActive(true);
            gameSettingsPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
        if (!Open)
        {
            audioSettingsPanel.SetActive(false);
            gameSettingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
    }


    public void secretLevel()
    {
        SceneManager.LoadScene(3);
    }
}
