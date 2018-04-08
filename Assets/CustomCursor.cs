using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour {

    public Vector2 cursorOffset;
    public Sprite cursorDefault;
    Image cursorOnScreen;
    Jeeves m_jeevesScript;
    public GameObject customCursor;
    public GameObject customCursorInv;
        
    [System.Serializable]
    public class cursors
    {
        public string m_tag;
        public Sprite[] m_cursor;
        public int m_frame = 0;
        float lastFrame = 0;

        public void Step()
        {
            if (Time.time - lastFrame > (1.0f / m_cursor.Length))
            {
                m_frame++;
                if (m_frame >= m_cursor.Length) m_frame = 0;
                lastFrame = Time.time;
            }
        }
    }

    public cursors[] cursorsCustom;

    private void Awake()
    {
        SaveLoad.Load("Jeeves.cjc");
        if (SaveData.cursorInvert && gameObject != customCursorInv)
            FlipCursors();
    }

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
                    if (m_jeevesScript.m_mouseTarget.tag == "Task")
                    {
                        if (!m_jeevesScript.m_mouseTarget.GetComponent<Task>().isBaron)
                            cursorOnScreen.sprite = GetCursor(m_jeevesScript.m_mouseTarget.tag);
                    }
                    else
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
            {
                cursor.Step();
                return cursor.m_cursor[cursor.m_frame];
            }
        }

        return cursorDefault;
    }

    public void FlipCursors()
    {
        if(customCursorInv==gameObject)
        {
            customCursor.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            customCursorInv.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
