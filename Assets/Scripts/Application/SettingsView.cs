using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsView : Reference
{
    public Canvas resetCanvas;
    public GameObject window1, window2;
    public Image musicOn, musicOff, effectsOn, effectsOff;

    private void Start()
    {
        Debug.Log("Muscia: " + PlayerPrefs.GetInt("Music", 1));
        Debug.Log("Effects: " + PlayerPrefs.GetInt("Effects", 1));
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            musicOn.enabled = true;
            musicOff.enabled = false;
        }
        else
        {
            musicOn.enabled = false;
            musicOff.enabled = true;
        }
        if (PlayerPrefs.GetInt("Effects", 1) == 1)
        {
            effectsOn.enabled = true;
            effectsOff.enabled = false;
        }
        else
        {
            effectsOn.enabled = false;
            effectsOff.enabled = true;
        }
    }

    public void ShowResetCanvas()
    {
        resetCanvas.enabled = true;
    }
    public void HideResetCanvas()
    {
        resetCanvas.enabled = false;
    }
    public void ResetValues()
    {
        App.generalController.settingsController.ResetValues();
        window1.SetActive(false);
        window2.SetActive(true);
    }
    public void ChangeMusicStatus()
    {
        App.generalController.settingsController.ChangeMusicStatus();
    }
    public void ChangeEffectsStatus()
    {
        App.generalController.settingsController.ChangeEffectsStatus();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
