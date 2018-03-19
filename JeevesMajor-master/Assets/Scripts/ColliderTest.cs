using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public GameObject roomToCull;
    public string[] tagsThatCull;
    Component[] wallRenderer;

    // Use this for initialization
    void Start ()
    {
        wallRenderer = roomToCull.GetComponentsInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        foreach(string tag in tagsThatCull)
        {
            if (other.tag == tag)
            {
                foreach (Renderer wall in wallRenderer)
                {
                    Renderer render = wall.GetComponent<Renderer>();
                    if (wall.tag == "Cull")
                        render.enabled = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (string tag in tagsThatCull)
        {
            if (other.tag == tag)
            {
                foreach (Renderer wall in wallRenderer)
                {
                    Renderer render = wall.GetComponent<Renderer>();
                    if (wall.tag == "Cull")
                        render.enabled = true;
                }
            }
        }
    }
}
