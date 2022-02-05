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
            if (PlayerPrefs.GetInt("Effects", 1) == 0)
            {
                DisableSoundEffects();
            }
            else
            {
                ActivateSoundEffects();
            }
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
