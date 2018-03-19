using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    // variables
    public GameObject displayQuad;
    public Material quadMaterial;
    public Texture progressBar;
    public int xSize;
    public int ySize;
    public int numFrames;
    public int frameSize;

    public Task m_task;
    public float initialTimeToComplete;

    public float taskPercentComplete;
    public Vector2 stepping = new Vector2(0,0);

	// Use this for initialization
	void Start () {
        displayQuad.AddComponent<LookAtCamera>();

        xSize = (int)progressBar.width;
        ySize = (int)progressBar.height;
        Vector2 newScale = new Vector2(1, 1);
        
        if (xSize > ySize)
        {
            numFrames = xSize / ySize;
            frameSize = ySize;
            newScale.x = 1.0f / numFrames;
            stepping.x = 1.0f / numFrames;
        }
        else
        {
            numFrames = ySize / xSize;
            frameSize = xSize;
            newScale.y = 1.0f / numFrames;
            stepping.y = 1.0f / numFrames;
        }

        m_task = GetComponent<Task>();
        initialTimeToComplete = m_task.timeToComplete;

        // set up shader
        quadMaterial = displayQuad.GetComponent<Renderer>().material;
        quadMaterial.SetTexture("_MainTex", progressBar);
        quadMaterial.SetTextureScale("_MainTex", newScale);
    }
	
	// Update is called once per frame
	void Update ()
    {
        taskPercentComplete = (1.0f - (m_task.timeToComplete / initialTimeToComplete)) * 100; ;

        // display correct frame
        float perChunk = 100 / numFrames;
        int currentFrame = (int)((taskPercentComplete) / perChunk);


        Vector2 currentOffset = stepping * currentFrame;
        quadMaterial.SetTextureOffset("_MainTex", currentOffset);

        //displayQuad.transform.LookAt(Camera.main.transform.position * -1.0f);
	}
}
