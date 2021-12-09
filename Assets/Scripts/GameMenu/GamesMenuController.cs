using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamesMenuController : Reference
{
    public FileLists file;
    public void Play(string gameName)
    {
        //Debug.Log("Nombre del juego: " + gameName);

        switch (gameName)
        {
            case "Descubre el conjunto":
                if(file.classificationGameList.Count==0)
                {
                    App.generalView.gamesMenuView.playButtons[0].interactable = false;
                    App.generalView.gamesMenuView.finishedCanvas.enabled = true;
                }
                else
                {
                    SceneManager.LoadScene("ClassificationGameScene");
                }
                break;
            case "�Quien soy?":
                if (file.characteristicsGameList.Count == 0)
                {
                    App.generalView.gamesMenuView.playButtons[1].interactable = false;
                    App.generalView.gamesMenuView.finishedCanvas.enabled = true;
                }
                else
                {
                    SceneManager.LoadScene("CharacteristicsGameScene");
                }
                break;
            case "Sigue la secuencia":
                SceneManager.LoadScene("SequenceGameScene");
                break;

            case "Conectados":
                SceneManager.LoadScene("ConnectedGameScene");
                App.generalController.connectedGameController.CreateLevel();

                break;

            case "Encuentra el camino":
                SceneManager.LoadScene("CategoriesScene");

                break;

            case "Desifra el operando":
                SceneManager.LoadScene("AdditionGameScene");
                
            case "Union e Interseccion":
                SceneManager.LoadScene("SetsGameScene");
                break;

            case "Juego de equivalencias":
                SceneManager.LoadScene("EquialityGameScene");
                break;

            case "Nombre del Juego 8":

                break;
            case "Nombre del Juego 9":

                break;

            default:
                Debug.Log("Escena no encontrada");
                break;
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void GetPointsGame()
    {
        //int points = App.generalModel.characteristicsGameModel.GetPoints();
    }
}
