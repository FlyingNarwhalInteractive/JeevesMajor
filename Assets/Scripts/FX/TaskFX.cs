using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FX/TaskFX")]
public class TaskFX : MonoBehaviour {

    public ParticleSystem spawnFX;
    public ParticleSystem completingFX;
   public ParticleSystem completedFX;



    public void SpawnFX()
    {
        CreateDestroy(spawnFX);

    }

    public void CompletingFX()
    {
        Create(completingFX);
    }


    public void CompletedFX()
    {
        CreateDestroy(completedFX);
    }


    void Create(ParticleSystem ps)
    {
      //Create Instance
      ParticleSystem fx =   Instantiate(ps, transform, true);
    }


    void CreateDestroy(ParticleSystem ps)
    {
        //Create Instance
        ParticleSystem fx = Instantiate(ps, transform, true);

        //CleanUp
        Destroy(fx, 10.0f);
    }
}
