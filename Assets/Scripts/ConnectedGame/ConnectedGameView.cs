using UnityEngine;
using UnityEngine.UI;

public class ConnectedGameView : Reference
{
    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

    public Button[] buttons;

    //Canvas que muestra el contenido inicial del juego
    public Canvas startCanvas;

    public Canvas returnCanvas;

    public Text message;

    private void Awake()
    {
        BuildMatrix();
    }

    /*
     * Metodo de que enviar y las coordenas donde debe iniciar la matriz aleatoria 
     */
    public void BuildMatrix()
    {
        //Provicional
        //GameObject [,] Matrix = new GameObject[8,6];

        //App.generalController.gameController.LevelData("Principiante");
        Objects[,] matrix = App.generalController.connectedGameController.ReturnArray();
        //Llamada al metodo para dibujar la matriz en la escena
        App.generalController.connectedGameController.DrawMatrix(matrix,initialBlock,gameZone);
        
        int level = App.generalController.connectedGameController.ReturnLevel();

        for (int i = 0; i < level; i++)
        {
            buttons[i].interactable = true;
        }

        App.generalController.connectedGameController.LoadText();
    }

    /*
     * Metodo que oculta el canvas inicial del juego
     */
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void HelpButton()
    {
        startCanvas.GetComponent<Canvas>().enabled = true;
        App.generalController.connectedGameController.LoadText();
    }

    public void ContinueGame()
    {
        App.generalView.gameOptionsView.WinCanvas.enabled = false;
        
        App.generalController.connectedGameController.CreateLevel();
        BuildMatrix();
        
        //App.generalView.gameOptionsView.BackGameLeves();
    }

    public void ReturnButton()
    {
        returnCanvas.GetComponent<Canvas>().enabled = true;
        //App.generalController.connectedGameController.LoadText();
    }

    public void ReturnMoves (int type)
    {
        App.generalController.connectedGameController.ReturnMoves(type);
        returnCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void OnClickButtons(string name_button)
    {
        //El boton solution abre un canvas para ir a los minijuegos
        if (name_button == "Solution Button")
        {
            App.generalController.connectedGameController.MostrarSolucion();
            BuildMatrix();
        }
    }

    public void ColorEscogido(int tipo)
    {
        App.generalController.connectedGameController.SelectedObject(tipo);
    }
    public void Move(string direction)
    {
        App.generalController.connectedGameController.Move(direction);
    }

    
}
