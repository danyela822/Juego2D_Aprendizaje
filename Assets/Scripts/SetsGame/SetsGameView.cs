using UnityEngine;
using UnityEngine.UI;

public class SetsGameView : Reference
{
    public Text title;

    public Text message;

    public Text messageLoseCanvas;

    public Image correctAnswer;

    public Canvas tryCanvas;

    public Canvas optionCanvas;

    public Canvas loseCanvas;
    //Canvas que muestra el contenido inicial del juego
    public Canvas startCanvas;
    //Matriz de botones que puede pulsar el jugador
    public Button[] buttons;

    public Image[] panels;

    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Desactivar el boton que se ha presionado
        //button.interactable = false;

        //Guardar el nombre de la imagen
        App.generalController.setsGameController.CheckAnswer(nameImage);
    }

    /*
     * Metodo que oculta el canvas inicial del juego
     */
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void TextGame(string text)
    {
        startCanvas.GetComponent<Canvas>().enabled = true;
        message.text = text;
    }

    public void LoseCanvas ()
    {
        loseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void TryCanvas ()
    {
        tryCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ContinueGame ()
    {
        if(App.generalController.setsGameController.ReturnLevel() < 7)
        {
            App.generalView.gameOptionsView.WinCanvas.enabled = false;
            loseCanvas.enabled = false;
            App.generalController.setsGameController.DesativatePanels();
            App.generalController.setsGameController.StartGame();
        }
        else
        {
            App.generalView.gameOptionsView.BackGameLeves();
        }
        
    }

    /*public void OptionCanvas()
    {
        optionCanvas.GetComponent<Canvas>().enabled = false;
        App.generalController.setsGameController.SelectQuestionType();
        startCanvas.enabled = true;
    }*/
}
