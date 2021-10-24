using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsGameView : Reference
{
    //Matriz de botones que puede pulsar el jugador
    public Button[] buttons;

    //Texto para mostrar el eunciado del juego
    public Text statement;

    //Canvas que muestra el contenido inicial del juego
    public Canvas startCanvas;

    /*
    * Metodo que captura el boton que oprimio el jugador y captura el nombre de la imagen que posee ese boton
    */
    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Guardar el nombre de la imagen
        App.generalController.characteristicsGameController.SaveOption(nameImage);
    }
    /*
    * Metodo para activar los canvas que indican si el jugador gano o perdio el juego
    */
    public void CheckAnswer()
    {
        //Determinar si el jugador gano o perdio
        bool isWin = App.generalController.characteristicsGameController.CheckAnswer();

        if (isWin)
        {
            //Activar el canvas de ganar
            App.generalView.menuGamesView.WinCanvas.enabled = true;
            Debug.Log("GANO");
        }
        else
        {
            //Activar el canvas de perder
            Debug.Log("PERDIO");
        }
    }
    /*
     * Metodo que oculta el canvas inicial del juego
     */
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }
}
