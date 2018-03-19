using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractTell : MonoBehaviour {

    public GameObject m_tell;
    Jeeves m_jeevesScript;
   
    // Use this for initialization
    void Start () {
        
        m_jeevesScript = GetComponent<Jeeves>();
        m_tell = Instantiate(m_tell, transform.root);
        m_tell.SetActive(false);
    
    }
	
	// Update is called once per frame
	void Update () {
        if(m_jeevesScript.m_mouseTarget != null)
        {
        switch (m_jeevesScript.m_mouseTarget.tag)
        {
            case "Door":
                TellOn(m_jeevesScript.m_mouseTarget.transform.position);
                break;
            case "Task":
                if(!m_jeevesScript.m_mouseTarget.GetComponent<Task>().isBaron)
                    TellOn(m_jeevesScript.m_mouseTarget.transform.position);
                break;
            case "Fetch":
                TellOn(m_jeevesScript.m_mouseTarget.transform.position);
                break;

            case "Baron":
                if(m_jeevesScript.hasFetch == true)
                {
                    TellOn(m_jeevesScript.m_mouseTarget.transform.position);
                }
                break;

                default:
                TellOff();
                break;
        }
        }
		else
		{
			TellOff();
		}
      
    }

    void TellOn(Vector3 pos)
    {
        Vector3 newPos = m_tell.transform.position;
        newPos.x = pos.x;
        newPos.z = pos.z;

        m_tell.transform.position = newPos;
        m_tell.SetActive(true);
    }

    void TellOff()
    {
        m_tell.SetActive(false);
    }
}
