using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceGameView : Reference{

    //lista de los botones de la interfaz
    public List<Button> buttons = new List<Button>();

    public void CheckAnswer(){

        App.generalController.sequenceGameController.CheckAnswerForUser();
    }

    public void AnswerForUser(GameObject text){

        string answer = text.GetComponent<Text>().text;
        App.generalController.sequenceGameController.AnswerForUser(answer);
    }

    public void ActivePanel(GameObject panel){

        App.generalController.sequenceGameController.ActivePanel(panel);
    }

//     public void ViewSolution(){
//         App.generalView.gameOptionsView.ShowSolutionCanvas();
//     }
 }
