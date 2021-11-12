using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptionsView: Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas, WinCanvas;

    public Image imageWin;
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
    public void ShowWinCanvas(int totalStars)
    {
        //Image imageWin = GameObject.Find("ImageStars").GetComponent<Image>();

        imageWin.sprite = Resources.Load<Sprite>("Stars/" + totalStars);

        WinCanvas.enabled = true;
    }
    public void HidePauseCanvas()
    {
        PauseCanvas.enabled = false;
    }
    public void HideSolutionCanvas()
    {
        SolutionCanvas.enabled = false;
    }
    public void BackGameLeves()
    {
        SceneManager.LoadScene("GamesMenuScene");
    }
}
