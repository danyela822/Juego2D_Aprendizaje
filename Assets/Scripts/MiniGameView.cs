using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    //Variables para la vista del MiniJuego 2

    //Texto del enunciado del acertijo, texto de la solucion y texto de fallo
    public Text riddleText,solutionText,loseText;

    //Botones para elegir una de las posibles opciones
    public Button option0, option1, option2;

    //Textos de los botones
    public Text text0, text1, text2;

    //Imagen para representar una incognita y para representar el acertijo
    public Image riddleImage, solutionImage, loseImage;

    //Variable para mostrar si la opcion elegida fue o no la correcta
    public GameObject winPanel, losePanel;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Mini Juego 1 Canvas")
        {
            
        }
        else if (this.name == "Mini Juego 2 Canvas")
        {
            MiniGame2();
        }
        else if (this.name == "Mini Juego 3 Canvas")
        {

        }
    }
    
    void MiniGame2()
    {
        riddleText = GameObject.Find("Riddle Text").GetComponent<Text>();
        solutionText = GameObject.Find("Solution Text").GetComponent<Text>();
        loseText = GameObject.Find("Lose Text").GetComponent<Text>();

        option0 = GameObject.Find("Option_0 Button").GetComponent<Button>();
        option1 = GameObject.Find("Option_1 Button").GetComponent<Button>();
        option2 = GameObject.Find("Option_2 Button").GetComponent<Button>();
        
        text0 = option0.GetComponentInChildren<Text>();
        text1 = option1.GetComponentInChildren<Text>();
        text2 = option2.GetComponentInChildren<Text>();

        riddleImage = GameObject.Find("Riddle Image").GetComponent<Image>();
        solutionImage = GameObject.Find("Solution Image").GetComponent<Image>();
        loseImage = GameObject.Find("Lose Image").GetComponent<Image>();

        winPanel = GameObject.Find("Win Panel");
        winPanel.SetActive(false);
        losePanel = GameObject.Find("Lose Panel");
        losePanel.SetActive(false);

        App.generalController.miniGame2Controller.LoadRiddles();
    }
    public void CheckAnswer(GameObject respuesta)
    {
        string answer = respuesta.GetComponent<Text>().text;
        App.generalController.miniGame2Controller.CheckAnswer(answer);
    }
    public void Back()
    {
        /*panel.SetActive(false);
        solution_imagen.enabled = false;
        solution.enabled = false;
        riddle.enabled = true;*/
    }
}
