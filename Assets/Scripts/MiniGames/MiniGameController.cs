using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameController : Reference
{
    public MiniGame1Controller miniGame1Controller;
    public MiniGame2Controller miniGame2Controller;
    public MiniGame3Controller miniGame3Controller;

    void Start()
    {
        GenerateMiniGame();
    }

    public void GenerateMiniGame()
    {
        DropClone();
        int typeMiniGame = App.generalController.gameController.RamdonNumber(1,4);
        App.generalView.miniGameView.EnabledCanvasMiniGames();
        if(typeMiniGame == 1)
        {
            App.generalView.miniGameView.MiniGame1();
            App.generalView.miniGameView.miniGame1Canvas.enabled = true;
        }
        else if(typeMiniGame == 2)
        {
            App.generalView.miniGameView.MiniGame2();
            App.generalView.miniGameView.miniGame2Canvas.enabled = true;
        }
        else if(typeMiniGame == 3)
        {
            App.generalView.miniGameView.MiniGame3();
            App.generalView.miniGameView.miniGame3Canvas.enabled = true;
        }
    }

    public void DropClone()
    {
        GameObject[] sequenceClone = GameObject.FindGameObjectsWithTag("Sequence");

        for (int i = 0; i < sequenceClone.Length; i++)
        {
            Destroy(sequenceClone[i]);
        }
    }

    public void OptionSolution(Text optionText)
    {
        if(optionText.text == "Volver al Juego Principal")
        {
            
            //App.generalModel.gameModel.solutionTickets++;
            SceneManager.LoadScene("GameScene");
            
            //App.generalController.gameController.LocateSolucion();
            App.generalController.gameController.IncreaseTickets();
            
        }
        else
        {
            App.generalView.miniGameView.SetActiveCanvas();
            GenerateMiniGame();
        }
        
    }

    public void ReturnGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
    }
    

}
