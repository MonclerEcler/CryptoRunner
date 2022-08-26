using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioClip[] sound;
   public AudioSource a;

   public void pauseSound()
    {
        a.clip = sound[0];
        a.Play();
    }

    public void tapButton()
    {
        a.clip = sound[1];
        a.Play();
    }
}
