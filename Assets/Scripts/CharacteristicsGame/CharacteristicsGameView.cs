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

    //Color del boton cuando se presiona
    public Color color;

    //Boton seleccionado
    Button selectedButton;

    //Canvas que indica que debe seleccionar una opcion antes de verificar
    //public Canvas warningCanvas;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 2;

    /*
    * Metodo que captura el boton que oprimio el jugador y captura el nombre de la imagen que posee ese boton
    */
    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        for (int i = 0; i < buttons.Length; i++)
        {
            //Cambiar el color del boton seleccionado
            if(button.name == buttons[i].name)
            {
                button.image.color = color;
                selectedButton = button;
            }
            else
            {
                buttons[i].image.color = Color.white;
            }
        }

        //Guardar el nombre de la imagen
        App.generalController.characteristicsGameController.SaveOption(nameImage);
    }
    /*
    * Metodo para activar los canvas que indican si el jugador gano o perdio el juego
    */
    public void CheckAnswer()
    {
        if (selectedButton == null)
        {
            App.generalView.gameOptionsView.ShowWarningCanvas();
        }
        else
        {
            selectedButton.image.color = Color.white;
            selectedButton.interactable = false;
            //Determinar si el jugador gano o perdio
            //bool isWin = App.generalController.characteristicsGameController.CheckAnswer();

            int numberStars = App.generalController.characteristicsGameController.CheckAnswer();

            /*if (isWin)
            {
                //Activar el canvas de ganar
                App.generalView.gameOptionsView.WinCanvas.enabled = true;
            }
            else
            {
                //Activar el canvas de perder
                Debug.Log("PERDIO");
            }*/

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
            else if(numberStars == -1)
            {
                App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
                attempts--;
            }
            else
            {
                App.generalView.gameOptionsView.ShowLoseCanvas();
            }
        }        
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
