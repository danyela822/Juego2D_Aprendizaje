using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquialityGameView : Reference
{
    //lista que tiene los numeros que equivales cada figura
    public List<Text> listTextNum = new List<Text>();
    //botones que contienen la posible respuesta al problema
    public List<Button> answerButtonsGame = new List<Button>();
    //
    public Text solutionText;
    //
    public GameObject solutionPanel;

    //metodo que recibe la respuesta del usuario y verifica si
    //es la correcta
    public void CheckAnswer(GameObject text){

        string answer = text.GetComponent<Text>().text;
        App.generalController.equialityGameController.CheckAnswer(answer);
    }
    public void ShowSolution()
    {
        App.generalController.equialityGameController.ShowSolution();
    }
}
