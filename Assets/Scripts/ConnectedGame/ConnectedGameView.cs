using UnityEngine;
using UnityEngine.UI;

public class ConnectedGameView : Reference
{
    
    public Button[] buttons;

    public Canvas colorsCanvas;

    public Text message;

    private void Awake()
    {
        //App.generalController.connectedGameController.CreateLevel();
        //BuildMatrix();
    }

    /*
     * Metodo de que enviar y las coordenas donde debe iniciar la matriz aleatoria 
     */
    public void BuildMatrix()
    {
        //Provicional
        //GameObject [,] Matrix = new GameObject[8,6];

        //App.generalController.gameController.LevelData("Principiante");
        //Objects[,] matrix = App.generalController.connectedGameController.ReturnArray();
        //Llamada al metodo para dibujar la matriz en la escena
        //App.generalController.connectedGameController.DrawMatrix(matrix,initialBlock,gameZone);
        
        int level = App.generalController.connectedGameController.ReturnLevel();

        for (int i = 0; i < level; i++)
        {
            buttons[i].interactable = true;
        }

        //App.generalController.connectedGameController.LoadText();
    }   
    public void ReturnButton()
    {
        colorsCanvas.enabled = true;
        //App.generalController.connectedGameController.LoadText();
    }

    public void ReturnMoves (int type)
    {
        App.generalController.connectedGameController.ReturnMoves(type);
        colorsCanvas.enabled = false;
    }
    public void SelectColor(int tipo)
    {
        App.generalController.connectedGameController.SelectColor(tipo);
    }
    public void Move(string direction)
    {
        App.generalController.connectedGameController.Move(direction);
    }
    public void ShowSolution()
    {
        App.generalController.connectedGameController.ShowSolution();
    }
}
