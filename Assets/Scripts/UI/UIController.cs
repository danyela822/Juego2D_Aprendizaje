using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Reference
{
    public Canvas canvasCategories;
    public Canvas canvasLevels;
    public void OnClickButtons(string name_button)
    {
        //Botones del menu principal
        if (name_button == "Play Button")
        {
            SceneManager.LoadScene("GamesMenuScene");
        }
        if (name_button == "Settings Button")
        {
            SceneManager.LoadScene("SettingsScene");
        }
        if (name_button == "Stats Button")
        {
            SceneManager.LoadScene("StatsScene");
        }
        if (name_button == "Info Button")
        {
            SceneManager.LoadScene("InfoScene");
        }


        //Boton de regreso (Puede servir para cualquier escena menos para la escena de los niveles)
        if (name_button == "Back Button")
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        if (name_button == "Back Button_N")
        {
            canvasLevels.enabled = false;
            canvasCategories.enabled = true;
            App.generalView.UIView.category.text = "Categorias";
        }

        if (name_button == "Home Button")
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        //Botones de la vista de categorias
        if(name_button == "Begginer Button")
        {
            //SceneManager.LoadScene("ConnectedGameScene");
            //App.generalController.connectedGameController.CreateLevel();
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("principiante");
        }
        if (name_button == "Medium Button")
        {
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("moderado");
        }
        if (name_button == "Advanced Button")
        {
            canvasCategories.enabled = false;
            canvasLevels.enabled = true;
            ChangeCategory("avanzado");
        }
        if (name_button == "Random Button")
        {
            //Escena del nivel aleatorio
            SceneManager.LoadScene("GameScene");
            App.generalController.roadGameController.RamdonLevel();
        }

        //Botones de Niveles
        if(name_button == "Level Button")
        {
            SceneManager.LoadScene("GameScene");
            string categoryName = App.generalView.UIView.NameCategory();
            App.generalController.roadGameController.LevelData(categoryName);
        }

        //Botones de la vista de Configuracion
        if (name_button == "Music Button")
        {
            //LLamar al metodo de la musica
        }
        if (name_button == "Sound Button")
        {
            //LLamar al metodo del sonido
        }
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
