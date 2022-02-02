using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager audioManager;
    AudioSource music;
    void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            music = GetComponent<AudioSource>();
            if (PlayerPrefs.GetInt("Music", 1) == 0)
            {
                DisableMusic();
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DisableMusic()
    {
        music.Stop();
    }
    public void ActivateMusic()
    {
        music.Play();
    }
}
