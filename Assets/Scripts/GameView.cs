using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameView : Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, TutorialCanvas;

    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

    //Arreglo con los personajes del juego
    public List<GameObject> allCharacters;
    //public GameObject [] allCharacters;


    public static GameView gameView;
    
    private void Awake()
    {
        GameCanvas = GameObject.Find("Game Canvas").GetComponent<Canvas>();
        PauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        SolutionCanvas = GameObject.Find("Solution Canvas").GetComponent<Canvas>();
        TutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();

        gameZone = GameObject.Find("Game Zone");

        BuildMatrix();
        
        LocateCharacters();
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

        //App.generalController.gameController.LevelData("Principiante");
        Objects[,] matrix = App.generalController.gameController.ReturnArray();
        //Llamada al metodo para dibujar la matriz en la escena
        App.generalController.gameController.DrawMatrix(matrix,initialBlock,gameZone);
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("MiniGame3Scene");
    }

    public void DrawSolution()
    {
        App.generalController.gameController.DrawSolution();
    }


    //DANY//

    public void LocateCharacters()
    {
        App.generalController.charactersController.CreateCharacters();
        App.generalController.charactersController.SelectCharactersLevel(3, "Castle", allCharacters);
    }

    public void ActivateMovement(int type)
    {
        App.generalController.charactersController.ActivateMovement(type);
    }

    public void MoveCharacter(string direction)
    {
        App.generalController.charactersController.Move(direction);
    }

    public void NotMoveCharacter()
    {
        App.generalController.charactersController.NotMove();
    }
}
