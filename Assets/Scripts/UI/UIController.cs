using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Reference
{
    public Canvas infoCanvas, imagesCanvas, musicCanvas, creatorsCanvas;

    public void OnClickButtons(string name_button)
    {
        //Botones del menu principal
        if (name_button == "PlayButton")
        {
            SceneManager.LoadScene("GamesMenuScene");
        }
        if (name_button == "SettingsButton")
        {
            SceneManager.LoadScene("SettingsScene");
        }
        if (name_button == "StatsButton")
        {
            SceneManager.LoadScene("StatsScene");
        }
        if (name_button == "InfoButton")
        {
            SceneManager.LoadScene("InfoScene");
        }
        if (name_button == "ExitButton")
        {
            Application.Quit();
        }

        //Botones de la pagina de informacion
        if (name_button == "ImagesButton")
        {
            infoCanvas.enabled = false;
            imagesCanvas.enabled = true;
            musicCanvas.enabled = false;
            creatorsCanvas.enabled = false;
        }
        if (name_button == "MusicaButton")
        {
            infoCanvas.enabled = false;
            imagesCanvas.enabled = false;
            musicCanvas.enabled = true;
            creatorsCanvas.enabled = false;
        }
        if (name_button == "CreatorsButton")
        {
            infoCanvas.enabled = false;
            imagesCanvas.enabled = false;
            musicCanvas.enabled = false;
            creatorsCanvas.enabled = true;
        }

        //Boton de regreso de la paggina de informacion
        if (name_button == "InfoBackButton")
        {
            infoCanvas.enabled = true;
            imagesCanvas.enabled = false;
            musicCanvas.enabled = false;
            creatorsCanvas.enabled = false;
        }

        //Boton de regreso al nivel principal
        if (name_button == "BackButton")
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
