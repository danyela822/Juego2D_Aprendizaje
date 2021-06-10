using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Reference
{
    /*
     * Metodo determina que accion realizar al oprimir un botón
     * en la interfaz de la vista del juego
     */
    public void OnClickButtons(string name_button)
    {
        //El boton pause abre el canvas del menu de pause
        if(name_button == "Button Pause")
        {
            App.generalView.gameView.PauseCanvas.enabled = true;
        }
        //El boton help abre el tutorial del juego
        if (name_button == "Button Help")
        {
            App.generalView.gameView.TutorialCanvas.enabled = true;
        }
        //El boton solution abre un canvas para ir a los minijuegos
        if (name_button == "Button Solution")
        {
            App.generalView.gameView.SolutionCanvas.enabled = true;
        }

        ////////////////////////////////// Botones del menu de pausa, tutorial y solucion ////////////////////////////////

        //El boton back regresa a la partida
        if (name_button == "Button Back")
        {
            if (App.generalView.gameView.PauseCanvas.enabled == true)
            {
                App.generalView.gameView.PauseCanvas.enabled = false;
            }

            if (App.generalView.gameView.SolutionCanvas.enabled == true)
            {
                App.generalView.gameView.SolutionCanvas.enabled = false;
            }

            if (App.generalView.gameView.TutorialCanvas.enabled == true)
            {
                App.generalView.gameView.TutorialCanvas.enabled = false;
            }
        }
        //El boton back regresa al menu principal
        if (name_button == "Button Menu")
        {
            //Falta la escena del menu principal
            //SceneManager.LoadScene("");
        }
    }
}
