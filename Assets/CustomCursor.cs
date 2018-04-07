using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour {

    public Vector2 cursorOffset;
    public Sprite cursorDefault;
    Image cursorOnScreen;
    Jeeves m_jeevesScript;
    
    [System.Serializable]
    public class cursors
    {
        public string m_tag;
        public Sprite m_cursor;
    }

    public cursors[] cursorsCustom;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        cursorOnScreen = GetComponent<Image>();
        cursorOnScreen.sprite = cursorDefault;
        if(GameObject.FindGameObjectWithTag("Jeeves")!=null)
            m_jeevesScript = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + (Vector3)cursorOffset;

        if (m_jeevesScript != null)
        {
            if (m_jeevesScript.m_mouseTarget != null)
            {
                if (CursorExists(m_jeevesScript.m_mouseTarget.tag))
                {
                    cursorOnScreen.sprite = GetCursor(m_jeevesScript.m_mouseTarget.tag);
                }
            }
            else
                cursorOnScreen.sprite = cursorDefault;
        }
    }

    bool CursorExists(string tag)
    {
        foreach (var cursor in cursorsCustom)
        {
            if (cursor.m_tag == tag)
                return true;
        }
        return false;
    }

    Sprite GetCursor(string tag)
    {
        foreach (var cursor in cursorsCustom)
        {
            if (cursor.m_tag == tag)
                return cursor.m_cursor;
        }

        return cursorDefault;
    }
}
