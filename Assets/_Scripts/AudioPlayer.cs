using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayer : MonoBehaviour
{
    [SerializeField] protected float pitchRandomness = 0.05f;
    protected AudioSource m_AudioSource;
    protected float basePitch;
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        basePitch = m_AudioSource.pitch;
    }
    protected void PlayClipWithRandomPitch(AudioClip clip)
    {
        var randomPitch = UnityEngine.Random.Range(-pitchRandomness, pitchRandomness);
        m_AudioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }

}
