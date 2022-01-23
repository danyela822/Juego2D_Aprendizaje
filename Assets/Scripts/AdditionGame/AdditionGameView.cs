using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionGameView : Reference
{
    //valor inicial
    public Text principalText;
    //Lista de botones que tiene los resultados 
    public List<Button> answerButtons = new List<Button>();
    //lista que me pinta los signos de las operaciones
    public List<Text> listText = new List<Text>();
    //lista que muestra los resultados de las operaciones
    public List<Text> resultsText = new List<Text>();

    //
    public Text solutionText;
    //
    public GameObject solutionPanel;

    //metodo que recibe la repsuesta del usuario y verifica si es 
    //la correcta
    public void CheckAnswer(GameObject text){

        string answer = text.GetComponent<Text>().text;
        App.generalController.additionGameController.CheckAnswer(answer);
    }
    public void ShowSolution()
    {
        App.generalController.additionGameController.ShowSolution();
    }
}
