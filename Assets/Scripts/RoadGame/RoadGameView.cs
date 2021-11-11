using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RoadGameView : Reference
{
    //Declaracion de los canvas que contiene la vista del juego
    public Canvas GameCanvas, PauseCanvas, SolutionCanvas, StartCanvas, WinCanvas;

    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

    //Arreglo con los personajes del juego
    public List<GameObject> allCharacters;
    //public GameObject [] allCharacters;

    //Posibles temas del juego
    //private string[] themes = {"Castle", "Forest", "Sea"};

    //private string theme;

    //Botones para especificar que personaje se va a mover

    public Button character_1, character_2, character_3;

    public static RoadGameView gameView;
    
    private void Awake()
    {
        /*GameCanvas = GameObject.Find("Game Canvas").GetComponent<Canvas>();
        PauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        SolutionCanvas = GameObject.Find("Solution Canvas").GetComponent<Canvas>();
        TutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();

        gameZone = GameObject.Find("Game Zone");*/

        //theme = themes[Random.Range(0, 3)];

        

        PointsLevel();

        //BuildMatrix();
        
        //LocateCharacters();
    }

    public void PointsLevel()
    {
        Text cointsText = GameObject.Find("Coins Text").GetComponent<Text>();
        Text solutionTickets = GameObject.Find("SolutionTickets Text").GetComponent<Text>();

        cointsText.text = " x " + App.generalModel.roadGameModel.GetPoints();
        solutionTickets.text = " x " + App.generalModel.roadGameModel.GetTickets();
    }

    public void ActivateWinCanvas(int totalStars)
    {
        Image imageWin = GameObject.Find("ImageStars").GetComponent<Image>();

        imageWin.sprite = Resources.Load<Sprite>("Stars/" + totalStars);

        WinCanvas.enabled = true;
    }
    /*
     * Metodo que captura el nombre del boton el cual se esta pulsando
     */
    public void OnClickButtons(string name_button)
    {
        App.generalController.roadGameController.OnClickButtons(name_button);
    }

    /*
     * Metodo de que enviar y las coordenas donde debe iniciar la matriz aleatoria 
     */
    public void BuildMatrix()
    {
        //Provicional
        //GameObject [,] Matrix = new GameObject[8,6];

        //App.generalController.gameController.LevelData("Principiante");
        //Objects[,] matrix = App.generalController.roadGameController.ReturnArray();
        //Llamada al metodo para dibujar la matriz en la escena
        //App.generalController.roadGameController.DrawMatrix(matrix,initialBlock,gameZone, "Forest");
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("MiniGamesScene");
    }

    public void DrawSolution()
    {

        if( App.generalModel.roadGameModel.GetTickets() > 0)
        {
            App.generalController.roadGameController.DrawSolution();
            PointsLevel();
        }
        else{
            print("no tienes tickets de solucion");
        }
    }


    //DANY//
    //Variable para determinar la cantidad de personajes
    public int numCharacteres = 2;
    public void LocateCharacters()
    {
        App.generalController.charactersController.CreateCharacters("Forest", numCharacteres);
        App.generalController.charactersController.SelectCharactersLevel(allCharacters);
    }

    public void ActivateMovement(int type)
    {
        App.generalController.charactersController.ActivateMovement(type);
    }

    public void MoveCharacter(string direction)
    {
        App.generalController.charactersController.Move(direction);
    }
    public void StartGame()
    {
        StartCanvas.enabled = false;
        App.generalController.roadGameController.DrawMatrix();
    }
}
