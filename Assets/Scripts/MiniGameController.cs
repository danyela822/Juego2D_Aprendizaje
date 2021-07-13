using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameController : Reference
{
    public MiniGame1Controller miniGame1Controller;
    public MiniGame2Controller miniGame2Controller;
    public MiniGame3Controller miniGame3Controller;

    void Start()
    {
        GenerateMiniGame();
    }

    void GenerateMiniGame()
    {
        int typeMiniGame = App.generalController.gameController.RamdonNumber(1,4);
        
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

    public void CorrectAnswer(bool answer)
    {
        if(answer == true)
        {
            print("Respuesta correcta desde MiniGameController");
            SceneManager.LoadScene("GameScene");
            App.generalController.gameController.LocateSolucion();

        }
        else
        {
            print("Respuesta incorrecta desde MiniGameController");
            SceneManager.LoadScene("GameScene");
        }
    }

    int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
    }
    

}
