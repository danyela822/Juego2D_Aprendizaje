using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Reference
{
    public Canvas canvasCategories, canvasLevel, infoCanvas, imagesCanvas, musicCanvas, creatorsCanvas;

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

        //Boton de regreso (Puede servir para cualquier escena menos para la escena de los niveles)
        if (name_button == "InfoBackButton")
        {
            infoCanvas.enabled = true;
            imagesCanvas.enabled = false;
            musicCanvas.enabled = false;
            creatorsCanvas.enabled = false;
        }

        //Boton de regreso (Puede servir para cualquier escena menos para la escena de los niveles)
        if (name_button == "BackButton")
        {
            SceneManager.LoadScene("MainMenuScene");
        }


        /*
        if (name_button == "BackButton_N")
        {
            canvasLevels.enabled = false;
            canvasCategories.enabled = true;
            App.generalView.UIView.category.text = "Categorias";
        }

        if (name_button == "HomeButton")
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        //Botones de la vista de categorias
        if(name_button == "BegginerButton")
        {
            //SceneManager.LoadScene("ConnectedGameScene");
            //App.generalController.connectedGameController.CreateLevel();
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("principiante");
        }
        if (name_button == "MediumButton")
        {
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("moderado");
        }
        if (name_button == "AdvancedButton")
        {
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("avanzado");
        }
        if (name_button == "RandomButton")
        {
            //Escena del nivel aleatorio
            SceneManager.LoadScene("GameScene");
            App.generalController.roadGameController.RamdonLevel();
        }

        //Botones de Niveles
        if(name_button == "LevelButton")
        {
            SceneManager.LoadScene("GameScene");
            string categoryName = App.generalView.UIView.NameCategory();
            App.generalController.roadGameController.LevelData(categoryName);
        }
        */
    }
    private string nameCategory;
    public void ChangeCategory(string category)
    {
        nameCategory = "";
        switch (category)
        {
            case "principiante":
                nameCategory = "Principiante";
                break;

            case "moderado":
                nameCategory = "Medio";
                break;

            case "avanzado":
                nameCategory = "Avanzado";
                break;

            default:
                break;
        }

        App.generalView.UIView.ChangeTextCategory(nameCategory);
    }
}
