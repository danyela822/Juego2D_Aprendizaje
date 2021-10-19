using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGamesView : Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas, WinCanvas;
    public void ShowPauseCanvas()
    {
        PauseCanvas.enabled = true;
    }
    public void ShowSolutionCanvas()
    {
        SolutionCanvas.enabled = true;
    }
    public void ShowTutorial()
    {
        TutorialCanvas.enabled = true;
    }
    public void HidePauseCanvas()
    {
        PauseCanvas.enabled = false;
    }

    public void HideSolutionCanvas()
    {
        SolutionCanvas.enabled = false;
    }
    public void BackGameLeves(string levelsMenu)
    {
        if (levelsMenu == "Classification Game")
        {
            //Regresar a la escena de niveles del juego de clasificacion
        }
    }
}
