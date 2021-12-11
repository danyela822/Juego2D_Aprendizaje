using System;
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

    //Numero de intentos que tiene el jugador para ganar el juego
    //int attempts = 2;

    /*
    * Metodo que captura el nombre de la imagen que posee un boton y activa el canvas de ganar, perder o volver a intentar
    */
    public void CheckAnswer(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        button.interactable = false;
        App.generalController.characteristicsGameController.CheckAnswer(nameImage);
        /*int numberStars = App.generalController.characteristicsGameController.CheckAnswer(nameImage);

        if (numberStars == 3)
        {
            App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
        }
        else if (numberStars == 2)
        {
            App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
        }
        else if (numberStars == 1)
        {
            App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
        }
        else if (numberStars == -1)
        {
            App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
            attempts--;
        }
        else
        {
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }*/
    }
    /*
     * Metodo que oculta el canvas inicial del juego
     */
    public void StartGame()
    {
        App.generalView.gameOptionsView.TutorialCanvas.enabled = false;
    }
    public void HideWarningCanvas()
    {
        App.generalView.gameOptionsView.HideWarningCanvas();
    }
}
