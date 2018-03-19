using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {


    [SerializeField] int[] DoorCost;// = new int[5];
    [SerializeField] Vector4[] power1Stats;// = new Vector4[4];
    [SerializeField] Vector4[] power2Stats;// = new Vector4[4];
    [SerializeField] Vector2[] power3Stats;// = new Vector2[4];
    [SerializeField] Vector2[] power4Stats;// = new Vector2[4];

    // Use this for initialization
    void Start ()
    {
        SaveLoad.Load("Jeeves.cjc");

        float[] p1 = (float[])SaveData.Vector4ToInt(power1Stats);
        float[] p2 = (float[])SaveData.Vector4ToInt(power2Stats);
        float[] p3 = (float[])SaveData.Vector2ToInt(power3Stats);
        float[] p4 = (float[])SaveData.Vector2ToInt(power4Stats);




        SaveData.Power1Stats = p1;
        SaveData.Power2Stats = p2;
        SaveData.Power3Stats = p3;
        SaveData.Power4Stats = p4;
        SaveData.DoorCost1 = DoorCost;

        foreach (float f in p1)
        {
            print("ARRAY: " + f);
        }

        SaveLoad.Save();

    }
    //(speed/duration/cost/coolDown)
    public Vector4 SetPower1Upgrade(int pow)
    {
        Vector4 stats = new Vector4();

        if(pow == 0)
        {
            stats = power1Stats[0];
        }
        else if(pow == 1)
        {
            stats = power1Stats[1];
        }
        else if (pow == 2)
        {
            stats = power1Stats[2];
        }
        else if (pow ==3)
        {
            stats = power1Stats[3];
        }

        return stats;
    }

    //(speed/duration/cost/coolDown)
    public Vector4 SetPower2Upgrade(int pow)
    {
        Vector4 stats = new Vector4();

        if (pow == 0)
        {
            stats = power2Stats[0];
        }
        else if (pow == 1)
        {
            stats = power2Stats[1];
        }
        else if (pow == 2)
        {
            stats = power2Stats[2];
        }
        else if (pow == 3)
        {
            stats = power2Stats[3];
        }

        return stats;
    }

    //(cost/coolDown)
    public Vector2 SetPower3Upgrade(int pow)
    {
        Vector2 stats = new Vector2();

        if (pow == 0)
        {
            stats = power3Stats[0];
        }
        else if (pow == 1)
        {
            stats = power3Stats[1];
        }
        else if (pow == 2)
        {
            stats = power3Stats[2];
        }
        else if (pow == 3)
        {
            stats = power3Stats[3];
        }

        return stats;
    }

    //(cost/coolDown)
    public Vector2 SetPower4Upgrade(int pow)
    {
        Vector2 stats = new Vector2();

        if (pow == 0)
        {
            stats = power4Stats[0];
        }
        else if (pow == 1)
        {
            stats = power4Stats[1];
        }
        else if (pow == 2)
        {
            stats = power4Stats[2];
        }
        else if (pow == 3)
        {
            stats = power4Stats[3];
        }

        return stats;
    }


    public int SetDoorUpgrade(int doorlvl)
    {
        int cost = 0;

        if(doorlvl == 0)
        {
            cost = DoorCost[0];
        }
        else if(doorlvl == 1)
        {
            cost =  DoorCost[1];
        }
        else if (doorlvl == 2)
        {
            cost = DoorCost[2];
        }
        else if (doorlvl == 3)
        {
            cost =  DoorCost[3];
        }
        else if (doorlvl == 4)
        {
            cost = DoorCost[4];
        }
        else if (doorlvl == 5)
        {
            cost =  DoorCost[5];
        }

        return cost;
    }


	// Update is called once per frame
	void Update ()

    {
		
	}
}
