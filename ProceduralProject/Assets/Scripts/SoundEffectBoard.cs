using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class SoundEffectBoard : MonoBehaviour
{
    public static SoundEffectBoard main;

    public AudioClip soundShot;
    public AudioClip soundHit;
    public AudioClip soundDie;
    public AudioClip soundPunch;

    private AudioSource player;

    void Start()
    {
        if(main == null)
        {
            main = this;
            player = GetComponent<AudioSource>();
        } else {
            Destroy(this.gameObject);
        }
    }


    public static void PlayShot()
    {
        main.player.PlayOneShot(main.soundShot);
    }

    public static void PlayHit()
    {
        main.player.PlayOneShot(main.soundHit);
    }

    public static void PlayDie()
    {
        main.player.PlayOneShot(main.soundDie);
    }

    public static void PlayPunch()
    {
        main.player.PlayOneShot(main.soundPunch);
    }
}