using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;

    public AudioClip shootSound;
    public AudioClip explosionSound;

    void Awake()
    {
        instance = this;
    }

    public void PlayShoot()
    {
        sfxSource.PlayOneShot(shootSound);
    }

    public void PlayExplosion()
    {
        sfxSource.PlayOneShot(explosionSound);
    }
}
