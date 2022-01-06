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

    public Image correctAnswer;

    public Text mistakeText, winText;
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
    public void HideTutorialCanvas()
    {
        TutorialCanvas.enabled = false;
    }
    public void ShowWinCanvas(int totalStars)
    {
        //Image imageWin = GameObject.Find("ImageStars").GetComponent<Image>();

        imageWin.sprite = App.generalModel.roadGameModel.GetStartsImage(totalStars);

        string text;

        if (totalStars == 1)
        {
            text = "Sigue adelante";
        }
        else if (totalStars == 2)
        {
            text = "¡Muy Bien!";
        }
        else
        {
            text = "¡Excelente!";
        }
        winText.text = text;
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
        string text;

        if (counter == 1)
        {
            text = "Ya estas cerca, vamos!";
        }
        else
        {
            text = "Intentalo de nuevo. ¡Tu puedes!";
        }
        //mistakeText.text = "Upss.. Has fallado. Te quedan " + counter + " intento(s)";
        mistakeText.text = text;

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
