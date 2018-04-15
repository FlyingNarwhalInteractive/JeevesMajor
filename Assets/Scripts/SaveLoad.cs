using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;


//REFERANCE

///////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                           //
//   http://answers.unity3d.com/questions/8480/how-to-scrip-a-saveload-game-option.html      //
//   6/02/2018                                                                               //
//   Answer by CJCurrie · Apr 05, 2011 at 05:35 PM                                           //
//   Original by Answer by TowerOfBricks · Dec 01, 2009 at 07:15 AM                          //
//                                                                                           //
///////////////////////////////////////////////////////////////////////////////////////////////

[Serializable()]
public class SaveData : ISerializable
{

    static private int score = 0;



    //Store  options - SP :P
    static public bool isTutorial;
    static public bool rightMousePan;
    static public bool doubleClickDoors;
    static public bool cursorInvert;

    //store upgrade levels
    static public int currencyMeta;// = 0;
    static private int doorUpgradeLevel;// = 0;
    static private int power1UpgradeLevel;// = 0;
    static private int power2UpgradeLevel;// = 0;
    static private int power3UpgradeLevel;// = 0;
    static private int power4UpgradeLevel;// = 0;
    static private int power5UpgradeLevel;// = 0;

    static private int[] DoorCost;// = new int[5];

    static private float[] power1Stats;// = new float[16];
    static private float[] power2Stats;// = new float[16];
    static private float[] power3Stats;// = new float[8];
    static private float[] power4Stats;// = new float[8];

    static public float[] Vector2ToInt(Vector2[] vec)
    {
        float[] intAr = new float[8];

        int count = 0;

        foreach (Vector2 v in vec)
        {

            intAr[count] = v.x;
            count++;
            intAr[count] = v.y;
            count++;

        }
        count = 0;
        return intAr;
    }

    //converts vector array into float array
    static public  float[] Vector4ToInt(Vector4[] vec)
    {
        float[] intAr = new float[16];

        int count = 0;

        foreach (Vector4 v in vec )
        {
            
            intAr[count] = v.x;
            count++;
            intAr[count] = v.y;
            count++;
            intAr[count] = v.z;
            count++;
            intAr[count] = v.w;
            count++;
        }
 
        return intAr;
    }

    //Upgrade Levels + score

    public static int DoorUpgradeLevel
    {
        get
        {
            return doorUpgradeLevel;
        }

        set
        {
            doorUpgradeLevel = value;
        }
    }

    public static int Power1UpgradeLevel
    {
        get
        {
            return power1UpgradeLevel;
        }

        set
        {
            power1UpgradeLevel = value;
        }
    }

    public static int Power2UpgradeLevel
    {
        get
        {
            return power2UpgradeLevel;
        }

        set
        {
            power2UpgradeLevel = value;
        }
    }

    public static int Power3UpgradeLevel
    {
        get
        {
            return power3UpgradeLevel;
        }

        set
        {
            power3UpgradeLevel = value;
        }
    }

    public static int Power4UpgradeLevel
    {
        get
        {
            return power4UpgradeLevel;
        }

        set
        {
            power4UpgradeLevel = value;
        }
    }

    public static int Power5UpgradeLevel
    {
        get
        {
            return power5UpgradeLevel;
        }

        set
        {
            power5UpgradeLevel = value;
        }
    }

    public static int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    //PowerUp Stats



    public static int[] DoorCost1
    {
        get
        {
            return DoorCost;
        }

        set
        {
            DoorCost = value;
        }
    }

    public static float[] Power1Stats
    {
        get
        {
            return power1Stats;
        }

        set
        {
            power1Stats = value;
        }
    }

    public static float[] Power2Stats
    {
        get
        {
            return power2Stats;
        }

        set
        {
            power2Stats = value;
        }
    }

    public static float[] Power3Stats
    {
        get
        {
            return power3Stats;
        }

        set
        {
            power3Stats = value;
        }
    }

    public static float[] Power4Stats
    {
        get
        {
            return power4Stats;
        }

        set
        {
            power4Stats = value;
        }
    }



    public SaveData() { }



    public SaveData(SerializationInfo info, StreamingContext ctxt)
    {
        // saveTime = (int[])info.GetValue("saveTime", typeof(int[]));
        Score = (int)info.GetValue("Score", typeof(int));
        currencyMeta = (int)info.GetValue("currencyMeta", typeof(int));
        doorUpgradeLevel = (int)info.GetValue("doorUpgradeLevel", typeof(int));
        power1UpgradeLevel = (int)info.GetValue("power1UpgradeLevel", typeof(int));
        power2UpgradeLevel = (int)info.GetValue("power2UpgradeLevel", typeof(int));
        power3UpgradeLevel = (int)info.GetValue("power3UpgradeLevel", typeof(int));
        power4UpgradeLevel = (int)info.GetValue("power4UpgradeLevel", typeof(int));
        power5UpgradeLevel = (int)info.GetValue("power5UpgradeLevel", typeof(int));

        // save option;
        isTutorial = (bool)info.GetValue("isTutorial", typeof(bool));
        rightMousePan = (bool)info.GetValue("rightMousePan", typeof(bool));
        doubleClickDoors = (bool)info.GetValue("doubleClickDoors", typeof(bool));
        cursorInvert = (bool)info.GetValue("cursorInvert", typeof(bool));

        //Stats


        Power1Stats = (float[])info.GetValue("Power1Stats", typeof(float[]));
        

            Power2Stats = (float[])info.GetValue("Power2Stats", typeof(float[]));
        


            Power3Stats = (float[])info.GetValue("Power3Stats", typeof(float[]));
        


            Power4Stats = (float[])info.GetValue("Power4Stats", typeof(float[]));
     

  
            DoorCost = (int[])info.GetValue("DoorCost", typeof(int[]));
        
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {


        info.AddValue("Score", Score);
        info.AddValue("currencyMeta", currencyMeta);
        info.AddValue("doorUpgradeLevel", doorUpgradeLevel);
        info.AddValue("power1UpgradeLevel", power1UpgradeLevel);
        info.AddValue("power2UpgradeLevel", power2UpgradeLevel);
        info.AddValue("power3UpgradeLevel", power3UpgradeLevel);
        info.AddValue("power4UpgradeLevel", power4UpgradeLevel);
        info.AddValue("power5UpgradeLevel", power5UpgradeLevel);

        //get option
        info.AddValue("isTutorial", isTutorial);
        info.AddValue("rightMousePan", rightMousePan);
        info.AddValue("doubleClickDoors", doubleClickDoors);
        info.AddValue("cursorInvert", cursorInvert);


        //Stats
        info.AddValue("Power1Stats", Power1Stats);
        info.AddValue("Power2Stats", Power2Stats);
        info.AddValue("Power3Stats", Power3Stats);
        info.AddValue("Power4Stats", Power4Stats);
        info.AddValue("DoorCost", DoorCost);


      
    }




    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


   
}

public class SaveLoad
{
    public static string currentFilePath = "Jeeves.cjc";

    public static void Save()
    {
        Save(currentFilePath);
    }

    public static void Save(string filePath)
    {
        SaveData data = new SaveData();

        Stream stream = File.Open(filePath, FileMode.OpenOrCreate);
        BinaryFormatter binformatter = new BinaryFormatter();
        binformatter.Binder = new VersionDesirialisationBinder();
        binformatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load() { Load(currentFilePath); }
    public static void Load(string filePath)
    {
        SaveData data = new SaveData();

        try
        {
            // loading save data
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter binformatter = new BinaryFormatter();
                binformatter.Binder = new VersionDesirialisationBinder();
                data = (SaveData)binformatter.Deserialize(stream);
                stream.Close();
        }
        catch
        {
            //  oh noes, we failed, load defaults
            // oh wait! new SaveData totally already did this for us! muah ha ah hahahahahaa
            SaveData.cursorInvert = true;
            SaveData.doubleClickDoors = true;
            SaveData.rightMousePan = true;
            // save file with defaults.
            Save(filePath);
        }
    }
}


    //Dont change
    public sealed class VersionDesirialisationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                Type typeToDeserialise = null;
                assemblyName = Assembly.GetExecutingAssembly().FullName;

                typeToDeserialise = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

                return typeToDeserialise;
            }
            return null;
        }
    }