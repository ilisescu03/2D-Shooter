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

    [SerializeField]
    private AudioClip MinigunShot;

    [SerializeField]
    private AudioClip emptyGun;

    [SerializeField]
    private AudioClip CoinCollect;


    [SerializeField]
    private AudioClip Purchase;


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
