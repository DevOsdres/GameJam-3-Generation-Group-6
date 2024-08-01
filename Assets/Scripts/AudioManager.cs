using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolume();
    }

    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("musicVolume");
            myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
            if (musicSlider != null) musicSlider.value = volume;
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            float volume = PlayerPrefs.GetFloat("sfxVolume");
            myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
            if (sfxSlider != null) sfxSlider.value = volume;
        }
    }

    public void AssignSliders(Slider newMusicSlider, Slider newSfxSlider)
    {
        musicSlider = newMusicSlider;
        sfxSlider = newSfxSlider;

        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        LoadVolume();
    }
}
