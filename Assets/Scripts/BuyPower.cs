using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPower : MonoBehaviour
{

    //DEBUG VARS
    [SerializeField] Button add;
    [SerializeField] Button spend;
    [SerializeField] Button reset;
    [SerializeField] GameObject debugPannel;
    //STORE FRONT


    //button links
    [SerializeField] Button btn;
    [SerializeField] Button btn1;
    [SerializeField] Button btn2;
    [SerializeField] Button btn3;
    [SerializeField] Button btn4;

    //cost text feilds
    [SerializeField] Text butlerCoins;
    [SerializeField] Text txt;
    [SerializeField] Text txt1;
    [SerializeField] Text txt2;
    [SerializeField] Text txt3;
    [SerializeField] Text txt4;


    //currency costs
    [SerializeField] Vector4 doorCost;
    [SerializeField] Vector4 cost;
    [SerializeField] Vector4 cost1;
    [SerializeField] Vector4 cost2;
    [SerializeField] Vector4 cost3;
    [SerializeField] Vector4 cost4;


    //stat display
    [SerializeField] Text p1s1;
    [SerializeField] Text p1s2;
    [SerializeField] Text p1s3;
    [SerializeField] Text p1s4;

    [SerializeField] Text p2s1;
    [SerializeField] Text p2s2;
    [SerializeField] Text p2s3;
    [SerializeField] Text p2s4;

    [SerializeField] Text DoorStaminaCost;

    [SerializeField] Text p3s1;
    [SerializeField] Text p3s2;

    [SerializeField] Text p4s1;
    [SerializeField] Text p4s2;


    private float[] power1stats;
    private float[] power2stats;
    private float[] power3stats;
    private float[] power4stats;
    private int[] doorCost1;



    [SerializeField] GameObject[] starArray;

    public void OnClick()
    {
        if (SaveData.Power1UpgradeLevel != 3)
        {
            if (SaveData.currencyMeta >= cost[SaveData.Power1UpgradeLevel])
            {
                SaveData.currencyMeta -= (int)cost[SaveData.Power1UpgradeLevel];
                SaveData.Power1UpgradeLevel += 1;


                
                SaveLoad.Save();
                UpPower1();


                butlerCoins.text = SaveData.currencyMeta.ToString();
                print(SaveData.Power1UpgradeLevel);
                print("Currency: " + SaveData.currencyMeta);
            }
            else
            {
                print("NOT ENOUGH MONEY");
            }
        }
        else
        {
            print("MaxRank");
        }
    }

    public void OnClick1()
    {
        if (SaveData.Power2UpgradeLevel != 3)
        {
            if (SaveData.currencyMeta >= cost1[SaveData.Power2UpgradeLevel])
            {
                SaveData.currencyMeta -= (int)cost1[SaveData.Power2UpgradeLevel];
                SaveData.Power2UpgradeLevel += 1;



                SaveLoad.Save();
                UpPower2();


                butlerCoins.text = SaveData.currencyMeta.ToString();

                print(SaveData.Power2UpgradeLevel);
                print("Currency: " + SaveData.currencyMeta);
            }
            else
            {
                print("NOT ENOUGH MONEY");
            }
        }
        else
        {
            print("MaxRank");
        }
    }

    public void OnDoorBuy()
    {
        if (SaveData.DoorUpgradeLevel != 3)
        {
            if (SaveData.currencyMeta >= cost2[SaveData.DoorUpgradeLevel])
            {
                SaveData.currencyMeta -= (int)cost2[SaveData.DoorUpgradeLevel];
                SaveData.DoorUpgradeLevel += 1;




                SaveLoad.Save();
                UpPower3();

                butlerCoins.text = SaveData.currencyMeta.ToString();
                print(SaveData.DoorUpgradeLevel);
                print("Currency: " + SaveData.currencyMeta);
            }
            else
            {
                print("NOT ENOUGH MONEY");
            }
        }
        else
        {
            print("MaxRank");
        }
    }
    public void OnClick2()
    {
        if (SaveData.Power3UpgradeLevel != 3)
        {
            if (SaveData.currencyMeta >= cost3[SaveData.Power3UpgradeLevel])
            {
                SaveData.currencyMeta -= (int)cost3[SaveData.Power3UpgradeLevel];
                SaveData.Power3UpgradeLevel += 1;



                SaveLoad.Save();
                UpPower4();


                butlerCoins.text = SaveData.currencyMeta.ToString();
                print(SaveData.Power3UpgradeLevel);
                print("Currency: " + SaveData.currencyMeta);
            }
            else
            {
                print("NOT ENOUGH MONEY");
            }
        }
        else
        {
            print("MaxRank");
        }
    }
   public  void OnClick3()
    {
        if (SaveData.Power4UpgradeLevel != 3)
        {
            if (SaveData.currencyMeta >= cost4[SaveData.Power4UpgradeLevel])
            {
                SaveData.currencyMeta -= (int)cost4[SaveData.Power4UpgradeLevel];
                SaveData.Power4UpgradeLevel += 1;




                SaveLoad.Save();
                UpPower5();

                butlerCoins.text = SaveData.currencyMeta.ToString();
                print(SaveData.Power4UpgradeLevel);
                print("Currency: " + SaveData.currencyMeta);
            }
            else
            {
                print("NOT ENOUGH MONEY");
            }
        }
        else
        {
            print("MaxRank");
        }
    }


    // Use this for initialization
    void Start()
    {

        SaveLoad.Load("Jeeves.cjc");

        print(SaveData.currencyMeta);

        

        power1stats = SaveData.Power1Stats;
        power2stats = SaveData.Power2Stats;
        power3stats = SaveData.Power3Stats;
        power4stats = SaveData.Power4Stats;
        doorCost1 = SaveData.DoorCost1;

        for(int i = 0; i < power1stats.Length; i++)
        {
            print("Save: " + power1stats[i] + "  " + i);
        }

        btn.onClick.AddListener(OnClick);
        btn1.onClick.AddListener(OnClick1);
        btn2.onClick.AddListener(OnDoorBuy);
        btn3.onClick.AddListener(OnClick2);
        btn4.onClick.AddListener(OnClick3);
        add.onClick.AddListener(AddCurrency);
        spend.onClick.AddListener(ReduceCurrency);
        reset.onClick.AddListener(Reset);

        butlerCoins.text = SaveData.currencyMeta.ToString();

        SetCosts();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (debugPannel.activeSelf == true)
            {
                debugPannel.SetActive(false);
            }
            else
            {
                debugPannel.SetActive(true);
            }
        }
    }


    void AddCurrency()
    {
        SaveData.currencyMeta += 10;
        SaveLoad.Save();
       

        butlerCoins.text = SaveData.currencyMeta.ToString();
    }
    void ReduceCurrency()
    {
        SaveData.currencyMeta -= 10;
        SaveLoad.Save();
        
        butlerCoins.text = SaveData.currencyMeta.ToString();
    }

    void Reset()
    {
        print("CLEAR DATA");

        //edit data
        SaveData.Score = 0;
        SaveData.currencyMeta = 0;
        SaveData.DoorUpgradeLevel = 0;
        SaveData.Power1UpgradeLevel = 0;
        SaveData.Power2UpgradeLevel = 0;
        SaveData.Power3UpgradeLevel = 0;
        SaveData.Power4UpgradeLevel = 0;
        SaveData.Power5UpgradeLevel = 0;
        //save data
        SaveLoad.Save();
        SaveLoad.Load("Jeeves.cjc");
        butlerCoins.text = SaveData.currencyMeta.ToString();
        SetCosts();
    }


    void SetCosts()
    {
        UpPower1();
        UpPower2();
        UpPower3();
        UpPower4();
        UpPower5();
    }

    void UpPower1()
    {
     
        //SetCost
        if (SaveData.Power1UpgradeLevel != 0)
        {
            txt.text = "$ " + cost[SaveData.Power1UpgradeLevel].ToString();
        }
        else
        {
            txt.text = "$ " + cost[SaveData.Power1UpgradeLevel].ToString();
        }
        if (SaveData.Power1UpgradeLevel == 3)
        {
            txt.text = "NA";
        }

        //rich text example
        //"This is <color=blue><b>test text</b></color>.\n" +
       //"This is <color=#34abef><i>new line</i></color> text.";//

        //Set Stats
        if(SaveData.Power1UpgradeLevel != 3)
        {
            p1s1.text =   "Speed: "    +  power1stats[0 + (4 * SaveData.Power1UpgradeLevel)].ToString() + " -> " + power1stats[0 + (4 * (SaveData.Power1UpgradeLevel + 1))].ToString();
            p1s2.text = "Duration: " +  power1stats[1 + (4 * SaveData.Power1UpgradeLevel)].ToString() + " -> " +    power1stats[1 + (4 * (SaveData.Power1UpgradeLevel + 1))].ToString();
            p1s3.text = "Cost: " +      power1stats[2 + (4 * SaveData.Power1UpgradeLevel)].ToString() + " -> " +    power1stats[2 + (4 * (SaveData.Power1UpgradeLevel + 1))].ToString();
            p1s4.text = "CoolDown: " +  power1stats[3 + (4 * SaveData.Power1UpgradeLevel)].ToString() + " -> " +    power1stats[3 + (4 * (SaveData.Power1UpgradeLevel + 1))].ToString();
        }
        else
        {
            p1s1.text = "Speed: " + power1stats[0 + (4 * SaveData.Power1UpgradeLevel)].ToString();
            p1s2.text = "Duration: " + power1stats[1 + (4 * SaveData.Power1UpgradeLevel)].ToString();
            p1s3.text = "Cost: " + power1stats[2 + (4 * SaveData.Power1UpgradeLevel)].ToString();
            p1s4.text = "CoolDown: " + power1stats[3 + (4 * SaveData.Power1UpgradeLevel)].ToString();
        }



        //DisableStars
        starArray[0].transform.Find("Star1").gameObject.SetActive(false);
        starArray[0].transform.Find("Star2").gameObject.SetActive(false);
        starArray[0].transform.Find("Star3").gameObject.SetActive(false);

        //ReActivate Stars
        if (SaveData.Power1UpgradeLevel > 0)
        {
            starArray[0].transform.Find("Star1").gameObject.SetActive(true);
        }

        if (SaveData.Power1UpgradeLevel > 1)
        {
            starArray[0].transform.Find("Star2").gameObject.SetActive(true);
        }

        if (SaveData.Power1UpgradeLevel > 2)
        {
            starArray[0].transform.Find("Star3").gameObject.SetActive(true);
        }

    }

    void UpPower2()
    {
       
        //SetCost
        if (SaveData.Power2UpgradeLevel != 0)
        {
            txt1.text = "$ " + cost1[SaveData.Power2UpgradeLevel].ToString();
        }
        else
        {
            txt1.text = "$ " + cost1[SaveData.Power2UpgradeLevel].ToString();
        }
        if (SaveData.Power2UpgradeLevel == 3)
        {
            txt1.text = "NA";
        }

        //Set Stats
        if (SaveData.Power2UpgradeLevel != 3)
        {
            p2s1.text = "Speed: "    + power2stats[0 + (4 * SaveData.Power2UpgradeLevel)].ToString() + " -> " + power2stats[0 + (4 * (SaveData.Power2UpgradeLevel + 1))].ToString();
            p2s2.text = "Duration: " + power2stats[1 + (4 * SaveData.Power2UpgradeLevel)].ToString() + " -> " + power2stats[1 + (4 * (SaveData.Power2UpgradeLevel + 1))].ToString();
            p2s3.text = "Cost: "     + power2stats[2 + (4 * SaveData.Power2UpgradeLevel)].ToString() + " -> " + power2stats[2 + (4 * (SaveData.Power2UpgradeLevel + 1))].ToString();
            p2s4.text = "CoolDown: " + power2stats[3 + (4 * SaveData.Power2UpgradeLevel)].ToString() + " -> " + power2stats[3 + (4 * (SaveData.Power2UpgradeLevel + 1))].ToString();
        }
        else
        {
            p2s1.text = "Speed: "    + power2stats[0 + (4 * SaveData.Power2UpgradeLevel)].ToString();
            p2s2.text = "Duration: " + power2stats[1 + (4 * SaveData.Power2UpgradeLevel)].ToString();
            p2s3.text = "Cost: "     + power2stats[2 + (4 * SaveData.Power2UpgradeLevel)].ToString();
            p2s4.text = "CoolDown: " + power2stats[3 + (4 * SaveData.Power2UpgradeLevel)].ToString();
        }


        //DisableStars
        starArray[1].transform.Find("Star1").gameObject.SetActive(false);
        starArray[1].transform.Find("Star2").gameObject.SetActive(false);
        starArray[1].transform.Find("Star3").gameObject.SetActive(false);


        //ReActivate Stars
        if (SaveData.Power2UpgradeLevel > 0)
        {
            starArray[1].transform.Find("Star1").gameObject.SetActive(true);
        }

        if (SaveData.Power2UpgradeLevel > 1)
        {
            starArray[1].transform.Find("Star2").gameObject.SetActive(true);
        }

        if (SaveData.Power2UpgradeLevel > 2)
        {
            starArray[1].transform.Find("Star3").gameObject.SetActive(true);
        }

    }
    void UpPower3()
    {
        
        //SetCost
        if (SaveData.DoorUpgradeLevel != 0)
        {
            txt2.text = "$ " + cost2[SaveData.DoorUpgradeLevel].ToString();
        }
        else
        {
            txt2.text = "$ " + cost2[SaveData.DoorUpgradeLevel].ToString();
        }
        if (SaveData.DoorUpgradeLevel == 3)
        {
            txt2.text = "NA";
        }

        //SetStats
        if (SaveData.DoorUpgradeLevel != 3)
        {
            DoorStaminaCost.text = "Cost: " + doorCost1[SaveData.DoorUpgradeLevel].ToString() + " -> " + doorCost1[SaveData.DoorUpgradeLevel + 1].ToString();
        }
        else
        {
            DoorStaminaCost.text = "Cost: " + doorCost1[SaveData.DoorUpgradeLevel].ToString();
        }

        //DisableStars
        starArray[2].transform.Find("Star1").gameObject.SetActive(false);
        starArray[2].transform.Find("Star2").gameObject.SetActive(false);
        starArray[2].transform.Find("Star3").gameObject.SetActive(false);

        //ReActivate Stars
        if (SaveData.DoorUpgradeLevel > 0)
        {
            starArray[2].transform.Find("Star1").gameObject.SetActive(true);
        }

        if (SaveData.DoorUpgradeLevel > 1)
        {
            starArray[2].transform.Find("Star2").gameObject.SetActive(true);
        }

        if (SaveData.DoorUpgradeLevel > 2)
        {
            starArray[2].transform.Find("Star3").gameObject.SetActive(true);
        }

    }
    void UpPower4()
    {
        
        //SetCost
        if (SaveData.Power3UpgradeLevel != 0)
        {
            txt3.text = "$ " + cost3[SaveData.Power3UpgradeLevel].ToString();
        }
        else
        {
            txt3.text = "$ " + cost3[SaveData.Power3UpgradeLevel].ToString();
        }
        if (SaveData.Power3UpgradeLevel == 3)
        {
            txt3.text = "NA";
        }

        //SetStats
      
        if (SaveData.Power3UpgradeLevel != 3)
        {
            p3s1.text = "Cost: "     + power3stats[0 + (2 * SaveData.Power3UpgradeLevel)].ToString() + " -> " + power3stats[0 + (2 * (SaveData.Power3UpgradeLevel + 1))].ToString();
            p3s2.text = "CoolDown: " + power3stats[1 + (2 * SaveData.Power3UpgradeLevel)].ToString() + " -> " + power3stats[1 + (2 * (SaveData.Power3UpgradeLevel + 1))].ToString();
        }
        else
        {
            p3s1.text = "Cost: "     + power3stats[0 + (2 * SaveData.Power3UpgradeLevel)].ToString();
            p3s2.text = "CoolDown: " + power3stats[1 + (2 * SaveData.Power3UpgradeLevel)].ToString();

        }
        
      
       


        //DisableStars
        starArray[3].transform.Find("Star1").gameObject.SetActive(false);
        starArray[3].transform.Find("Star2").gameObject.SetActive(false);
        starArray[3].transform.Find("Star3").gameObject.SetActive(false);

        //ReActivate Stars

        if (SaveData.Power3UpgradeLevel > 0)
        {
            starArray[3].transform.Find("Star1").gameObject.SetActive(true);
        }
        if (SaveData.Power3UpgradeLevel > 1)
        {
            starArray[3].transform.Find("Star2").gameObject.SetActive(true);
        }

        if (SaveData.Power3UpgradeLevel > 2)
        {
            starArray[3].transform.Find("Star3").gameObject.SetActive(true);
        }
    }


    void UpPower5()
    {
        
        //SetCostText
        if (SaveData.Power4UpgradeLevel != 0)
        {
            txt4.text = "$ " + cost4[SaveData.Power4UpgradeLevel].ToString();
        }
        else
        {
            txt4.text = "$ " + cost4[SaveData.Power4UpgradeLevel].ToString();
        }
        if (SaveData.Power4UpgradeLevel == 3)
        {
            txt4.text = "NA";
        }

        //SetStats

        if (SaveData.Power4UpgradeLevel != 3)
        {
            p4s1.text = "Cost: " + power4stats[0 + (2 * SaveData.Power4UpgradeLevel)].ToString() + " -> " + power4stats[0 + (2 * (SaveData.Power4UpgradeLevel + 1))].ToString();
            p4s2.text = "CoolDown: " + power4stats[1 + (2 * SaveData.Power4UpgradeLevel)].ToString() + " -> " + power4stats[1 + (2 * (SaveData.Power4UpgradeLevel + 1))].ToString();
        }
        else
        {
            p4s1.text = "Cost: " + power4stats[0 + (2 * SaveData.Power4UpgradeLevel)].ToString();
            p4s2.text = "CoolDown: " + power4stats[1 + (2 * SaveData.Power4UpgradeLevel)].ToString();

        }

        //diable all stars


        starArray[4].transform.Find("Star1").gameObject.SetActive(false);
        starArray[4].transform.Find("Star2").gameObject.SetActive(false);
        starArray[4].transform.Find("Star3").gameObject.SetActive(false);

        //ReActivate
        if (SaveData.Power4UpgradeLevel > 0)
        {
            starArray[4].transform.Find("Star1").gameObject.SetActive(true);
        }

        if (SaveData.Power4UpgradeLevel > 1)
        {
            starArray[4].transform.Find("Star2").gameObject.SetActive(true);
        }

        if (SaveData.Power4UpgradeLevel > 2)
        {
            starArray[4].transform.Find("Star3").gameObject.SetActive(true);
        }
    }

}
