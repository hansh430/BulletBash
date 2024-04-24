using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region Dependencies
    public static SoundManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicAudioSource, sfxAudioSource;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button sfxBtn;
    [SerializeField] private Sprite muteImage,unmuteImage, musicOffImage,musicOnImage;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    #endregion
    #region MonoBehaviour

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        musicBtn.onClick.AddListener(ToggleMusic);
        sfxBtn.onClick.AddListener(ToggleSFx);
    }
    private void Start()
    {
        LoadMusicAndSFXState();
        PlayMusic("BGMusic");
    }
    #endregion
    #region Functionality methods
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        if (sound != null)
        {
            musicAudioSource.clip = sound.clip;
            musicAudioSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);
        if (sound != null)
        {
            sfxAudioSource.PlayOneShot(sound.clip);
        }
    }
    public void PlayClickSound(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);
        if (sound != null)
        {
            musicAudioSource.PlayOneShot(sound.clip);
        }
    }
    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
        musicBtn.GetComponent<Image>().sprite = musicAudioSource.mute ? muteImage : unmuteImage;
        SaveMusicAndSFXState();
    }
    public void ToggleSFx()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
        sfxBtn.GetComponent<Image>().sprite = sfxAudioSource.mute ? musicOffImage : musicOnImage;
        SaveMusicAndSFXState();
    }
    public void MusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
        SaveMusicAndSFXState();
    }
    public void SFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        SaveMusicAndSFXState();
    }
    #endregion

    #region Save States
    private void SaveMusicAndSFXState()
    {
        PlayerPrefs.SetInt("MusicMuteState", musicAudioSource.mute ? 1 : 0);
        PlayerPrefs.SetInt("SFXMuteState", sfxAudioSource.mute ? 1 : 0);
        PlayerPrefs.SetFloat("MusicCurrentState", musicAudioSource.volume);
        PlayerPrefs.SetFloat("SFXCurrentState", sfxAudioSource.volume);
        PlayerPrefs.SetFloat("MusicSliderState",musicSlider.value);
        PlayerPrefs.SetFloat("SFXSliderState", sfxSlider.value);
        PlayerPrefs.Save();
    }
    private void LoadMusicAndSFXState()
    {
        int musicMuteState = PlayerPrefs.GetInt("MusicMuteState", 0);
        int sfxMuteState = PlayerPrefs.GetInt("SFXMuteState", 0); 
        float musicCurrentState = PlayerPrefs.GetFloat("MusicCurrentState", 1);
        float sfxCurrentState = PlayerPrefs.GetFloat("SFXMuteState", 1);
        float musicSliderState = PlayerPrefs.GetFloat("MusicSliderState", 1);
        float sfxSliderState = PlayerPrefs.GetFloat("SFXSliderState", 1);
        musicAudioSource.mute = (musicMuteState == 1);
        sfxAudioSource.mute = (sfxMuteState == 1);
        musicAudioSource.volume = musicCurrentState;
        sfxAudioSource.volume=sfxCurrentState;
        musicSlider.value = musicSliderState;
        musicSlider.value = musicSliderState;
        musicBtn.GetComponent<Image>().sprite = musicAudioSource.mute ? muteImage : unmuteImage;
        sfxBtn.GetComponent<Image>().sprite = sfxAudioSource.mute ? musicOffImage : musicOnImage;
    }
    #endregion
}
