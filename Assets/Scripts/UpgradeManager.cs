using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour {





    public bool isToggleChecked;
    public GameObject upgradeCheckPanel;





    // Use this for initialization
    void Start()
    {
        isToggleChecked = false;
        upgradeCheckPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void doUpgrade()
    {
        // if (bulterCoins > cost)
        //{

        //        }
        if (isToggleChecked == false) //if true, upgrade completes without confirming, jump to confirm code
        {
            if (upgradeCheckPanel.activeSelf == false)
            {
               // upgradeCheckPanel.SetActive(true);
            }
        }
    }

    public void toggleChecked()
    {
        isToggleChecked = true;
    }

    public void closePanel()
    {
        upgradeCheckPanel.SetActive(false);
    }

}
