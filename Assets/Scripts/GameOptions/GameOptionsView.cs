using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptionsView: Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    //Canvas que indica que debe seleccionar una opcion antes de verificar
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas, WinCanvas, LoseCanvas, MistakeCanvas, WarningCanvas, TicketsCanvas;

    public Image imageWin;

    public GameObject BuyTicketsWindow, NoneTicketsWindow, UsedTicketsWindow;

    //public Image correctAnswer;

    public Text mistakeText, winText, ticketsText;

    public Button backButton, continueButton;

    private void Start()
    {
        //Debug.Log("EMPEZAMOS CON CONTINUAR TRUE Y VOLVER FALSE");
        continueButton.GetComponent<Image>().enabled = true;
        continueButton.GetComponentInChildren<Text>().enabled = true;

        backButton.GetComponent<Image>().enabled = false;
        backButton.GetComponentInChildren<Text>().enabled = false;
    }

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
    public void ShowWinCanvas(int totalStars,bool isLastLevel)
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

        if (isLastLevel)
        {
            backButton.GetComponent<Image>().enabled = true;
            backButton.GetComponentInChildren<Text>().enabled = true;

            continueButton.GetComponent<Image>().enabled = false;
            continueButton.GetComponentInChildren<Text>().enabled = false;
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
    
    public void ShowTicketsCanvas(bool activateBuyWindow)
    {
        if (activateBuyWindow)
        {
            BuyTicketsWindow.SetActive(true);
            NoneTicketsWindow.SetActive(false);
        }
        else
        {
            BuyTicketsWindow.SetActive(false);
            NoneTicketsWindow.SetActive(true);
            UsedTicketsWindow.SetActive(false);
            //Debug.Log("DEBE AUMENTAR");
            //App.generalController.ticketController.IncreaseTickets();
            //ticketsText.text = "Has comprado un pase. Ahora tienes "+ App.generalController.ticketController.GetTickets()+" pase(s)";

        }
        TicketsCanvas.enabled = true;
    }
    public void BuyTickets()
    {
        Debug.Log("DEBE AUMENTAR");
        if(App.generalController.ticketController.IncreaseTickets())
        {

        }
        else
        {
            BuyTicketsWindow.SetActive(false);
            //NoneTicketsWindow.SetActive(true);
            UsedTicketsWindow.SetActive(true);
        }


    }
    public void HideTicketsCanvas()
    {
        TicketsCanvas.enabled = false;
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
