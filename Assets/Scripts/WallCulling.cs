using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCulling : MonoBehaviour
{
    //------------------------------------------------------------------
    //                      Wall Culling Script
    // 
    // This script is placed into a Game Object with a collider that
    // acts as a trigger. When an object that is tagged with one of
    // the Tags That Cull listed, it will turn off the Renderer for
    // Any object in the Room Heirarchy that is marked with the Cull
    // tag.
    // 
    // Variables:
    // Room To Cull - The parent of the room that the collider will
    //                cull. The objects that need to be culled must
    //                be tagged as "Cull".
    // 
    // Wall Renderer - Private Variable that holds the list of mesh
    //                 renderers of all the objects in the room.
    //
    // Obj In Collider - This variable counts the number of objects
    //                   with the appropriate tags that are inside
    //                   the collider. When the number of objects is
    //                   zero, the walls stop culling. As long as one
    //                   object remains in the collider, the walls
    //                   will be culled.
    // 
    //------------------------------------------------------------------

    public GameObject roomToCull;
    Component[] wallRenderer;
    int objInCollider;

    // Start with filling the Wall Renderer array with the renderers.
    void Start()
    {
        // Get renderers and reset Objects In Collider counter.
        wallRenderer = roomToCull.GetComponentsInChildren<Renderer>();
        objInCollider = 0;
    }

    private void OnMouseEnter()
    {
            objInCollider++; // Increment Objects In Collider counter

            foreach (Renderer wall in wallRenderer) // Going through the list of renderers
            {
                Renderer render = wall.GetComponent<Renderer>();
                if (wall.tag == "Cull") // Confirm the renderer is tagged with "Cull"
                    render.enabled = false;
            }
    }

    private void OnMouseExit()
    {
        objInCollider--; // Decrements Objects In Collider counter

        if (objInCollider == 0) // Checks if the collider no longer has tagged objects in it.
        {
            foreach (Renderer wall in wallRenderer) // Going through the list of renderers
            {
                Renderer render = wall.GetComponent<Renderer>();
                if (wall.tag == "Cull") // Confirm the renderer is tagged with "Cull"
                {
                    render.enabled = true;
                }
            }
        }
    }

    // When an object enters the trigger, check it has the right tag.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Baron" || other.tag == "Jeeves" || other.tag == "DirLight") // Checks if object tag is either Baron or Jeeves.
        {
            objInCollider++; // Increment Objects In Collider counter

            foreach (Renderer wall in wallRenderer) // Going through the list of renderers
            {
                Renderer render = wall.GetComponent<Renderer>();
                if (wall.tag == "Cull") // Confirm the renderer is tagged with "Cull"
                    render.enabled = false;
            }
        }
    }

    // When an object leaves the trigger, check it has the right tag.
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Baron" || other.tag == "Jeeves" || other.tag == "DirLight") // Checks if object tag is either Baron or Jeeves.
        {
            objInCollider--; // Decrements Objects In Collider counter

            if (objInCollider == 0) // Checks if the collider no longer has tagged objects in it.
            {
                foreach (Renderer wall in wallRenderer) // Going through the list of renderers
                {
                    Renderer render = wall.GetComponent<Renderer>();
                    if (wall.tag == "Cull") // Confirm the renderer is tagged with "Cull"
                    {
                        render.enabled = true;
                    }
                }
            }
        }
    }
}