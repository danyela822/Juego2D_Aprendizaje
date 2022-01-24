using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    public GameObject[] sounds;

    void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DisableSoundEffects()
    {
        foreach (GameObject sound in sounds)
        {
            sound.GetComponent<AudioSource>().mute = true;
        }
    }
    public void ActivateSoundEffects()
    {
        foreach (GameObject sound in sounds)
        {
            sound.GetComponent<AudioSource>().mute = false;
        }
    }
    public void PlaySound(int number)
    {
        Instantiate(sounds[number]);
    }
}
