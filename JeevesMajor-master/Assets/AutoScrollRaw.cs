using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollRaw : MonoBehaviour {

    public float speed = 1.0f;
    RawImage image;

	// Use this for initialization
	void Start () {
        image = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        Rect newRect = image.uvRect;
        newRect.y += speed * Time.deltaTime;
        image.uvRect = newRect;
	}
}
