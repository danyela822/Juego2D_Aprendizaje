using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas;

    private void Awake()
    {
        GameCanvas = GameObject.Find("Game Canvas").GetComponent<Canvas>();
        PauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        SolutionCanvas = GameObject.Find("Solution Canvas").GetComponent<Canvas>();
        TutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();
    }

    /*
     * Metodo que captura el nombre del boton el cual se esta pulsando
     */
    public void OnClickButtons(string name_button)
    {
        App.generalController.gameController.OnClickButtons(name_button);
    }
}
