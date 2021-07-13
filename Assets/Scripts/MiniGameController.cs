using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameController : Reference
{
    public MiniGame3Controller miniGame3Controller;

   

    void Start()
    {
        int typeMiniGame = App.generalController.gameController.RamdonNumber(1,4);
        print("Mini Juego "+typeMiniGame);
        GenerateMiniGame(3);
    }

    void GenerateMiniGame(int type)
    {
        if(type == 1)
        {

        }
        else if(type == 2)
        {
           
        }
        else if(type == 3)
        {
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
