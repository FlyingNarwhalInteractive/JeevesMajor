using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoLineRenderer : MonoBehaviour
{

    LineRenderer m_original;
    LineRenderer[] m_renderers;
    Vector3[] lastPos = new Vector3[2];

    // Use this for initialization
    void Start()
    {
        m_original = GetComponent<LineRenderer>();
        m_renderers = new LineRenderer[1];
        // temp renderer
        GameObject temp = new GameObject();
        temp.transform.SetParent(transform);
        temp.name = ("ChildLine0");
        m_renderers[0] = temp.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_original.enabled)
        {
            // linerender is disabled, disable ours
            for (int i = 1; i < m_renderers.Length; i++)
            {
                Destroy(m_renderers[i].gameObject);
            }
            m_renderers[0].positionCount = 1;
        }

        if (m_original.GetPosition(0) != lastPos[0] || m_original.GetPosition(m_original.positionCount-1) != lastPos[1])
        {
            lastPos[0] = m_original.GetPosition(0);
            lastPos[1] = m_original.GetPosition(m_original.positionCount - 1);
            // something has changed
            // delete all renderers
            for (int i = 1; i < m_renderers.Length; i++)
            {
                Destroy(m_renderers[i].gameObject);
            }
            // create new renderers
            if (m_original.positionCount < 2)
                return;
            LineRenderer tempR = m_renderers[0];
            m_renderers = new LineRenderer[m_original.positionCount - 1];
            m_renderers[0] = tempR;
            System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.DeclaredOnly;
            for (int i = 0; i < m_renderers.Length; i++)
            {
                // m_renderers[i] = m_original - copy mode \/\/
                if (i > 0)
                {
                    GameObject temp = new GameObject();
                    temp.transform.SetParent(transform);
                    temp.name = ("ChildLine" + i.ToString());
                    m_renderers[i] = temp.AddComponent<LineRenderer>();
                }
                // now set properties
                System.Reflection.PropertyInfo[] properties = m_original.GetType().GetProperties(flags);
                foreach (var prop in properties)
                {
                    if (prop.CanWrite)
                        prop.SetValue(m_renderers[i], prop.GetValue(m_original, null), null);
                }
                m_renderers[i].positionCount = 2;
                m_renderers[i].SetPosition(0, new Vector3(m_original.GetPosition(i).x, 0.1f, m_original.GetPosition(i).z));
                m_renderers[i].SetPosition(1, new Vector3(m_original.GetPosition(i + 1).x, 0.1f, m_original.GetPosition(i + 1).z));
                m_renderers[i].materials = m_original.materials;
            }
            m_original.positionCount = 2;
        }
    }
}
