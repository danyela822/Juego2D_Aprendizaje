 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedGameView : Reference
{
    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

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
