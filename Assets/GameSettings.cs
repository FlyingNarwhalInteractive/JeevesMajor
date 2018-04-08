using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public bool isTutorial;
    public bool panRightMouse;
    public bool doubleClickDoors;
    public bool cursorInverted;

    public Toggle tutorial;
    public Toggle panRight;
    public Toggle panCenter;
    public Toggle singleClick;
    public Toggle doubleClick;
    public Toggle cursorNormal;
    public Toggle cursorInvert;

    private void OnEnable()
    {
        // load settings

        SaveLoad.Load("Jeeves.cjc");
        isTutorial = SaveData.isTutorial;
        panRightMouse = SaveData.rightMousePan;
        doubleClickDoors = SaveData.doubleClickDoors;
        cursorInverted = SaveData.cursorInvert;

        UpdateControls();
        print("Settings Loaded");

    }

    private void OnDisable()
    {
        // save settings

        GetControls();

        SaveData.isTutorial = isTutorial;
        SaveData.rightMousePan = panRightMouse;
        SaveData.doubleClickDoors = doubleClickDoors;
        SaveData.cursorInvert = cursorInverted;

        SaveLoad.Save("Jeeves.cjc");
        print("Settings Saved");

    }

    void UpdateControls()
    {
        tutorial.isOn = isTutorial;
        panRight.isOn = panRightMouse;
        panCenter.isOn = !panRightMouse;
        singleClick.isOn = !doubleClickDoors;
        doubleClick.isOn = doubleClickDoors;
        cursorNormal.isOn = !cursorInverted;
        cursorInvert.isOn = cursorInverted;
    }

    void GetControls()
    {
        isTutorial = tutorial.isOn;
        panRightMouse = panRight.isOn;
        doubleClickDoors = doubleClick.isOn;
        cursorInverted = cursorInvert.isOn;
    }

}
