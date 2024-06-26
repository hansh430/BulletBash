using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    [SerializeField]
    private AudioClip hitClip = null, deathClip = null, voiceLineClip = null;

    public void PlayHitSound()
    {
        PlayClipWithRandomPitch(hitClip);
    }
    public void PlayDeathSound()
    {
        PlayClip(deathClip);
    }
    public void PlayVoiceSound()
    {
        PlayClipWithRandomPitch(voiceLineClip);
    }
}
