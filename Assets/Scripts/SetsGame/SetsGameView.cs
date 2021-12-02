using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetsGameView : Reference
{
    public Text title;

    public Text message;

    public Text messageLoseCanvas;

    public Image correctAnswer;

    public Canvas tryCanvas;

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

    public void LoseCanvas ()
    {
        loseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void TryCanvas ()
    {
        tryCanvas.GetComponent<Canvas>().enabled = false;
    }
}
