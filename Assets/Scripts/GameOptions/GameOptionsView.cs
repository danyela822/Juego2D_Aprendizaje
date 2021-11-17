using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptionsView: Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    //Canvas que indica que debe seleccionar una opcion antes de verificar
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas, WinCanvas, LoseCanvas, MistakeCanvas, WarningCanvas;

    public Image imageWin;

    public Text textMistake;
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

        imageWin.sprite = App.generalModel.roadGameModel.GetStartsImage(totalStars);

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
    public void ShowMistakeCanvas(int counter)
    {
        MistakeCanvas.enabled = true;
        textMistake.text = "Upss.. Has fallado. Te quedan " + counter + " intento(s)";

    }
    public void HideMistakeCanvas()
    {
        MistakeCanvas.enabled = false;
    }
    public void ShowLoseCanvas()
    {
        LoseCanvas.enabled = true;
    }
    public void HideLoseCanvas()
    {
        LoseCanvas.enabled = false;
    }
    public void ShowWarningCanvas ()
    {
        WarningCanvas.enabled = true;
    }
    public void HideWarningCanvas()
    {
        WarningCanvas.enabled = false;
    }
    public void BackGameLeves()
    {
        SceneManager.LoadScene("GamesMenuScene");
    }
    public void ReloadGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
