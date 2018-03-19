using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MonoBehaviour {

    // Custom UI paricle system - because unity just dont

    public Sprite m_texture;
    public Vector2 m_particleSize = new Vector2(1, 1);
    public int m_particleRate = 10;
    public int m_maxParticles = 30;
    public bool m_runOnAwake = false;
    public bool m_looping = false;
    public float m_timeToLive = 1.0f;
    public float m_runTime = 1.0f;
    public bool m_fadeOut = false;
    public Vector2 m_spawnArea = new Vector2(1, 1);
    public Vector2 m_particleVelocity = new Vector2(1, 0);

    GameObject[] m_particles;
    Image[] m_particleImages;
    RectTransform[] m_particleTransforms;
    float[] m_particleTimeAlive;

    float m_lastParticleTime = 0;
    bool m_isRunning = false;
    float m_timeRunning = 0;
    [SerializeField] int activeParticles = 0;


	// Use this for initialization
	void Start () {

        m_particles = new GameObject[m_maxParticles];
        m_particleImages = new Image[m_maxParticles];
        m_particleTransforms = new RectTransform[m_maxParticles];
        m_particleTimeAlive = new float[m_maxParticles];

        for (int i = 0; i < m_maxParticles; i++)
        {
            m_particles[i] = new GameObject();
            m_particles[i].name = "Particle" + i.ToString();
            m_particles[i].transform.SetParent(gameObject.transform);
            m_particleImages[i] = m_particles[i].AddComponent<Image>();
            m_particleImages[i].sprite = m_texture;
            m_particleImages[i].transform.localScale = (Vector3)m_particleSize;
            m_particleTransforms[i] = m_particles[i].GetComponent<RectTransform>();
            m_particleTimeAlive[i] = 0;
            m_particles[i].SetActive(false);
        }

        if (m_runOnAwake)
            m_isRunning = true;

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_isRunning || activeParticles > 0)
        {
            if (!m_looping && m_timeRunning>m_runTime)
            {
                // reset and stop
                m_isRunning = false;
                m_timeRunning = 0;
                // hide all particles
                /*for (int i = 0; i < m_particles.Length; i++)
                {
                   m_particles[i].SetActive(false);
                }*/
            }
            
            
            m_timeRunning += Time.deltaTime;
            // do particles!
            for (int i = 0; i < m_particles.Length; i++)
            {
                if(m_particles[i].activeSelf)
                {
                    m_particleTimeAlive[i] += Time.deltaTime;
                    // particle is active - tween it
                    // check time alive
                    if (m_particleTimeAlive[i] > m_timeToLive)
                    {
                        m_particles[i].SetActive(false);
                        activeParticles--;
                    }
                    else
                    {
                        // tween
                        m_particleTransforms[i].localPosition += (Vector3)(m_particleVelocity * Time.deltaTime);
                    }
                }
            }

            if (m_isRunning)
            {
                // create new particles
                m_lastParticleTime += Time.deltaTime;
                while (m_lastParticleTime > 1.0f / m_particleRate)
                {
                    // create a particle
                    m_lastParticleTime -= 1.0f / m_particleRate;
                    for (int i = 0; i < m_particles.Length; i++)
                    {
                        if (!m_particles[i].activeSelf)
                        {
                            // wake it up!
                            m_particles[i].SetActive(true);
                            activeParticles++;
                            m_particleTimeAlive[i] = 0;
                            m_particleTransforms[i].localPosition = new Vector2(Random.Range(-m_spawnArea.x, m_spawnArea.x), Random.Range(-m_spawnArea.y, m_spawnArea.y));
                            i = m_particles.Length;
                        }
                    }
                }
            }

            
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, m_spawnArea);

    }

    public void Fire()
    {
        m_timeRunning = 0;
        m_isRunning = true;
    }

    public void Stop()
    {
        m_isRunning = false;
    }
}
