using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : SingletonPersistent<AudioSourceManager>
{
    private AudioSource m_Source;
    [SerializeField] private AudioClip audioClip;
    void Start()
    {   
        m_Source = GetComponent<AudioSource>();
        m_Source.clip = audioClip;
        m_Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
