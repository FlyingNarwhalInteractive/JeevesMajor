using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeevesFX : MonoBehaviour {

    //public ParticleSystem jeevesSpotsFX;
    public GameObject clickObjectFX;



   // public void BaronSpotedFX()
   // {
        //CreateDestroy(jeevesSpotsFX);
   // }

    public void ClickedObject(Vector3 loc)
    {
        CreateDestroy(clickObjectFX.gameObject, loc);
    }

    void Create(GameObject ps)
    {
        //Create Instance
        ParticleSystem fx = Instantiate(ps.GetComponent<ParticleSystem>(), transform, true);
    }


    void CreateDestroy(GameObject ps, Vector3 loc)
    {
        //Create Instance
        ParticleSystem fx = Instantiate(ps.GetComponent<ParticleSystem>(), loc, ps.transform.rotation);

        //CleanUp
        Destroy(fx.gameObject, 2.0f);
    }

    void CreateDestroy(GameObject ps)
    {
        //Create Instance
        ParticleSystem fx = Instantiate(ps.GetComponent<ParticleSystem>(), transform, true);

        //CleanUp
        Destroy(fx.gameObject, 2.0f);
    }
}
