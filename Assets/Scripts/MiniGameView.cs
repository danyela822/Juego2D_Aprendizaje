using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    public Text question;
    public List<Text> easyText = new List<Text>();
    public List<Button> easyButton = new List<Button>();
    public List<Text> listText = new List<Text>(); 
    public List<Button> listButton = new List<Button>();
    public List<Text> listOptions = new List<Text>();
    
    // Canvas de minigame
    public Canvas miniGame3Canvas;
    
    // Start is called before the first frame update
    
    void Start()
    {
        MiniGame3();
    }
    
    void MiniGame3()
    {
        string type = App.generalController.miniGameController.miniGame3Controller.Category(9);

        if(type == "1")
        {
            LoadOperation(type, easyText, easyButton);
        }
        else if(type == "2" || type == "3")
        {
            LoadOperation(type, listText, listButton);
        }
    }
    
    public void Question()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
    }

    void LoadOperation(string type, List<Text> textList, List<Button> buttonList)
    {
        App.generalController.miniGameController.miniGame3Controller.LoadOperation(type, textList, buttonList);
    }

    public void OnclickButton(Text option)
    {
        App.generalController.miniGameController.miniGame3Controller.CheckAnswer(option.text);
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
    /*void Start()
    {
        App.generalController.miniGame2Controller.LoadRiddles();
    }*/

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
