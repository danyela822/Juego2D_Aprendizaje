using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    public Canvas miniGame1Canvas;
    public Canvas miniGame2Canvas;
    public Canvas miniGame3Canvas;
    
    public Text question;
    public List<Text> easyText = new List<Text>();
    public List<Button> easyButton = new List<Button>();
    public List<Text> listText = new List<Text>(); 
    public List<Button> listButton = new List<Button>();
    public List<Text> listOptions = new List<Text>();
    
    // Canvas de minigame
    
    
    // Start is called before the first frame update
    
    /*void Start()
    {
        MiniGame3();
    }*/
    public void MiniGame1()
    {
        App.generalController.miniGameController.miniGame1Controller.MiniGame1();
    }

    public void MiniGame2()
    {
        App.generalController.miniGameController.miniGame2Controller.LoadRiddles();
    }
    public void MiniGame3()
    {
        App.generalController.miniGameController.miniGame3Controller.ReadCSV();
        string type = App.generalController.miniGameController.miniGame3Controller.Category(7);
        print("Mini game view MINIGAME3 type "+type);
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

    public void CheckAnswerMiniGame1(GameObject text)
    {
        string answer = text.GetComponent<Text>().text;
        App.generalController.miniGameController.miniGame1Controller.CheckAnswerSequence(answer);
    }
    public void CheckAnswerMiniGame2(GameObject text)
    {
        string answer = text.GetComponent<Text>().text;
        App.generalController.miniGameController.miniGame2Controller.CheckAnswer(answer);
    }
    public void CheckAnswerMiniGame3(Text option)
    {
        App.generalController.miniGameController.miniGame3Controller.CheckAnswer(option.text);
    }
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

    
}
