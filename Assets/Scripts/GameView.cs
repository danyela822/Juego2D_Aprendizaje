using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas;

    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

    public static GameView gameView;
    
    private void Awake()
    {
        GameCanvas = GameObject.Find("Game Canvas").GetComponent<Canvas>();
        PauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        SolutionCanvas = GameObject.Find("Solution Canvas").GetComponent<Canvas>();
        TutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();

        gameZone = GameObject.Find("Game Zone");

        BuildMatrix();
    }

    /*
     * Metodo que captura el nombre del boton el cual se esta pulsando
     */
    public void OnClickButtons(string name_button)
    {
        App.generalController.gameController.OnClickButtons(name_button);
    }

    /*
     * Metodo de que enviar y las coordenas donde debe iniciar la matriz aleatoria 
     */
    public void BuildMatrix()
    {
        //Provicional
        //GameObject [,] Matrix = new GameObject[8,6];

        //Objects[,] matrix = App.generalController.gameController.LevelData("Begginer");
        Objects[,] matrix = App.generalController.gameController.ReturnArray();
        //Llamada al metodo para dibujar la matriz en la escena
        App.generalController.gameController.DrawMatrix(matrix,initialBlock,gameZone);
    }

    public void LocateSolution()
    {
        App.generalController.gameController.LocateSolucion();

        App.generalController.gameController.DrawSolution();
    }

}
