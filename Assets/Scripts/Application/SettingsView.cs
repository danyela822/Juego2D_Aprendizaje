using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsView : Reference
{
    public Canvas resetCanvas;
    public GameObject window1, window2;

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
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
