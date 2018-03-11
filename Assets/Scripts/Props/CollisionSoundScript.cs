using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundScript : MonoBehaviour {

    public string CollisionTag = "";
    private AudioSource m_audioSource;
    private float m_debouncePeriod = 0f;

    // Use this for initialization
    void Start() {
        m_debouncePeriod = 0f;
        m_audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //if(m_debouncePeriod > 0f) m_debouncePeriod -= Time.deltaTime;
        if((collision.gameObject.tag == CollisionTag))// && (m_debouncePeriod <= 0f))
        {
            if(!m_audioSource.isPlaying)
                m_audioSource.Play();
            //m_debouncePeriod = 0.1f;
        }
    }

}
