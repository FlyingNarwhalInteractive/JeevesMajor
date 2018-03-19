using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTut : MonoBehaviour {

    private Toggle tog;

    public bool GetIsTut()
    {
        SaveLoad.Load("Jeeves.cjc");
        return SaveData.isTutorial;
    }

    public void OffTut()
    {
        SaveLoad.Load("Jeeves.cjc");
        SaveData.isTutorial = false;
        print("TTTTTUUUUUUUTTTTTTT" + SaveData.isTutorial);
        SaveLoad.Save();
    }
    public void OnTut()
    {

        SaveLoad.Load("Jeeves.cjc");
        SaveData.isTutorial = true;
        print("TTTTTUUUUUUUTTTTTTT" + SaveData.isTutorial);
        SaveLoad.Save();
    }
    void Awake()
    {
        tog = GetComponent<Toggle>();
        tog.isOn = GetIsTut();
    }



    public void TutToggle()
    {
        if(!tog.isOn)
        {
            OffTut();
        }
        else
        {
            OnTut();
        }
    }
}
