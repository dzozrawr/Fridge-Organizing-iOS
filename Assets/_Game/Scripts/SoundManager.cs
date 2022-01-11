using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip stackingSound, fridgeClose;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        stackingSound = Resources.Load<AudioClip>("stackingSound");
        fridgeClose = Resources.Load<AudioClip>("fridgeClose");

        audioSrc = GetComponent<AudioSource>();
        //GetComponent()
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "stackingSound":
              //  if (audioSrc.isPlaying) return;
                audioSrc.PlayOneShot(stackingSound);
                break;
            case "fridgeClose":
                //  if (audioSrc.isPlaying) return;
                audioSrc.PlayOneShot(fridgeClose);
                break;
        }
    }
}
