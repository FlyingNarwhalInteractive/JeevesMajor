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

    public Vector3 m_transformOrigin;
    public float shakeAmount = 30.0f;
    public float waitBetweenShakes = 1.0f;
    public float shakeDuration = 1.0f;
    public float lastShakeTime = 0;
    public Vector3 currentRotation;

	// Use this for initialization
	void Start () {
        //displayQuad.AddComponent<LookAtCamera>();

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

        LookAtCamera();
        m_transformOrigin = displayQuad.transform.rotation.eulerAngles;

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



        // wiggle dat indicator!
        if (taskPercentComplete == 0)
        {
            // shake dat.
            // sin(2pix)
            float currentShakeTime = Time.time - lastShakeTime;
            if (currentShakeTime < shakeDuration)
            {
                float shakeModifier = Mathf.Sin(2 / shakeDuration * Mathf.PI * currentShakeTime);
                currentRotation = m_transformOrigin;
                currentRotation.z += shakeModifier * shakeAmount;
                displayQuad.transform.rotation = Quaternion.Euler(currentRotation);
            }
            if (currentShakeTime > shakeDuration + waitBetweenShakes)
            {
                lastShakeTime = Time.time;
            }

        }
    }

    void LookAtCamera()
    {
        Vector3 current = transform.rotation.eulerAngles;
        transform.LookAt(Camera.main.transform.position * -1.0f);

        current.y = transform.eulerAngles.y - 37.0f;
        current.x = 45.0f;

        transform.rotation = Quaternion.Euler(current);
    }
}
