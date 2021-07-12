using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    //Variables para la vista del MiniJuego 1

    //Botones para elegir una de las posibles opciones de la secuencia

    public List<Button> sequenceButtons;

    //Textos de los botones de secuencia
    public List<Text> sequenceText;

    //Variables para la vista del MiniJuego 2

    //Texto del enunciado del acertijo, texto de la solucion y texto de fallo
    public Text riddleText,solutionText,loseText;

    //Botones para elegir una de las posibles opciones
    public List<Button> riddleButtons;

    //Textos de los botones
    public List<Text> riddleTextButtons;

    //Imagen para representar una incognita y para representar el acertijo
    public Image riddleImage, solutionImage, loseImage;

    //Canvas que muestra si la opcion elegida es correcta o no
    public Canvas winCanvas, loseCanvas;

    //Variable para mostrar si la opcion elegida fue o no la correcta
    //public GameObject winPanel, losePanel;

    // Start is called before the first frame update
    void Start()
    {
        App.generalController.miniGame2Controller.LoadRiddles();
    }

    public void CheckAnswer(GameObject text)
    {
        string answer = text.GetComponent<Text>().text;
        App.generalController.miniGame2Controller.CheckAnswer(answer);
    }

    public void CheckAnswerSequence(GameObject text)
    {
        string answer = text.GetComponent<Text>().text;
        App.generalController.miniGame1Controller.CheckAnswerSequence(answer);
    }
}
