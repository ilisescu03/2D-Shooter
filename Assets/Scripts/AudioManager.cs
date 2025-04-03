using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;
   [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider SFXSlider;
    [SerializeField]
    private AudioClip Reload;
    [SerializeField]
    private AudioClip ZombieSpawn;

    [SerializeField]
    private AudioClip Hurt;

    [SerializeField]
    private AudioClip MinigunShot;

    [SerializeField]
    private AudioClip emptyGun;

    [SerializeField]
    private AudioClip CoinCollect;


    [SerializeField]
    private AudioClip Purchase;

    [SerializeField]
    private AudioClip MainMenuMusic;

    public AudioMixer audioMixer;
    void Start()
    {
        SetMusicVolume();
    }
    public void PlayMainMenuMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = MainMenuMusic;
            musicSource.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayMinigun()
    {
        SFXSource.PlayOneShot(MinigunShot);
    }
    public void PlayReloadSFX()
    {
        SFXSource.PlayOneShot(Reload);
    }
    public void PlayZombieSpawn()
    {
        SFXSource.PlayOneShot(ZombieSpawn);
    }
    public void PlayHurt()
    {
        SFXSource.PlayOneShot(Hurt);
    }
    public void PlayEmptySFX()
    {
        SFXSource.PlayOneShot(emptyGun);
    }
    public void PlayPurchaseSFX()
    {
        SFXSource.PlayOneShot(Purchase);
    }
    public void PlayCollectSFX()
    {
        SFXSource.PlayOneShot(CoinCollect);
    }
}
