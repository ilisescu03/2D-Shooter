using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;

    [SerializeField]
    private AudioClip Reload;
    [SerializeField]
    private AudioClip ZombieSpawn;

    [SerializeField]
    private AudioClip Hurt;
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
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
}
