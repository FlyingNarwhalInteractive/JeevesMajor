using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverMouse : MonoBehaviour
{

    // public GameObject info;

    public int powerNum;
    public float fadeTime;
    public  bool displayInfo;
    public Image img;
    private Color imgOri;

    private BuyPower bpRef;
    
    [SerializeField] Text[] txtArray = new Text[5];
    //public Color imgOri;
  


    void Start()
    {


        bpRef = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<BuyPower>();

        imgOri = img.color;

        int count = 0;

        foreach (Transform child in transform)
        {

            if (child.gameObject.GetComponent<Text>() != null)
            {
                txtArray[count] = child.gameObject.GetComponent<Text>();
                count++;
            }
        }
        foreach (Transform child in transform)
        {
            foreach (Transform nchild in child.transform)
            {
                if (nchild.gameObject.GetComponent<Text>() != null)
                {
                    txtArray[count] = nchild.gameObject.GetComponent<Text>();
                    count++;
                }

            }
        }
    }

    void Update()
    {
        Fade();
    }
    
   public void OnPointerClick()
    {
        print("Clicked");

       if(powerNum == 0)
        {
            bpRef.OnClick();
        }
        else if (powerNum == 1)
        {
            bpRef.OnClick1();
        }
        else if (powerNum == 2)
        {
            bpRef.OnDoorBuy();
        }
        else if (powerNum == 3)
        {
            bpRef.OnClick2();
        }
        else if (powerNum == 4)
        {
            bpRef.OnClick3();
        }
 
    }


   public void OnMouseOver()
    {
        displayInfo = true;
        print("ENTERRR");
    }

   public  void OnMouseExit()
    {
        displayInfo = false;
        print("EXXIITTT");
    }


    void Fade()
    {

        if(displayInfo)
        {
            img.color = Color.Lerp(img.color, imgOri, fadeTime * Time.deltaTime);
            print("SHOW");

            foreach(Text t in txtArray)
            {
                if(t != null)
                {
                t.color = Color.Lerp(t.color, Color.white, fadeTime * Time.deltaTime);
                }
                
            }
           
        }
        else
        {
            img.color  = Color.Lerp(img.color, Color.clear, fadeTime * Time.deltaTime);
            print("FADE");
            foreach (Text t in txtArray)
            {
                if(t != null)
                {
                t.color = Color.Lerp(t.color, Color.clear, fadeTime * Time.deltaTime);
                }
               
            }
        }

       
    }
}
