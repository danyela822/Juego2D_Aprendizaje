using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamesMenuController : Reference
{
    public void Play(string gameName)
    {
        Debug.Log("Nombre del juego: " + gameName);

        switch (gameName)
        {
            case "Descubre el conjunto":
                SceneManager.LoadScene("ClassificationGameScene");
                break;

            case "Desifra el elemento":
                SceneManager.LoadScene("CharacteristicsGameScene");
                break;

            case "Nombre del Juego 3":
                
                break;

            case "Conectados":
                SceneManager.LoadScene("ConnectedGameScene");
                App.generalController.connectedGameController.CreateLevel();

                break;

            case "Encuentra el camino":
                SceneManager.LoadScene("CategoriesScene");

                break;

            case "Nombre del Juego 6":

                break;

            case "Nombre del Juego 7":

                break;

            case "Nombre del Juego 8":

                break;

            default:
                Debug.Log("Escena no encontrada");
                break;
        }
    }
}
