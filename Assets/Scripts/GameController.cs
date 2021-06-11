using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Reference
{
    /*
     * Metodo determina que accion realizar al oprimir un botón
     * en la interfaz de la vista del juego
     */
    public void OnClickButtons(string name_button)
    {
        //El boton pause abre el canvas del menu de pause
        if(name_button == "Button Pause")
        {
            App.generalView.gameView.PauseCanvas.enabled = true;
        }
        //El boton help abre el tutorial del juego
        if (name_button == "Button Help")
        {
            App.generalView.gameView.TutorialCanvas.enabled = true;
        }
        //El boton solution abre un canvas para ir a los minijuegos
        if (name_button == "Button Solution")
        {
            App.generalView.gameView.SolutionCanvas.enabled = true;
        }

        ////////////////////////////////// Botones del menu de pausa, tutorial y solucion ////////////////////////////////

        //El boton back regresa a la partida
        if (name_button == "Button Back")
        {
            if (App.generalView.gameView.PauseCanvas.enabled == true)
            {
                App.generalView.gameView.PauseCanvas.enabled = false;
            }

            if (App.generalView.gameView.SolutionCanvas.enabled == true)
            {
                App.generalView.gameView.SolutionCanvas.enabled = false;
            }

            if (App.generalView.gameView.TutorialCanvas.enabled == true)
            {
                App.generalView.gameView.TutorialCanvas.enabled = false;
            }
        }
        //El boton back regresa al menu de niveles
        if (name_button == "Button Levels")
        {
            //Falta la escena de los niveles
            //SceneManager.LoadScene("");
        }

        if (name_button == "Button Pay")
        {
            //Canjea codigo y se muestra la solucion
        }

        if (name_button == "Button MiniGames")
        {
            //Falta la escena de los minijuegos
            //SceneManager.LoadScene("");
        }
    }

    /*
     * Metodo que dibuja en la pantalla la matriz
     */
    public void DrawMatrix(GameObject[,] matrix, GameObject initialBlock, GameObject gameZone)
    {
        //Posicion inicial del bloque
        float posStarX = initialBlock.transform.position.x;
        float posStarY = initialBlock.transform.position.y;

        //Tamaño del bloque
        Vector2 blockSize = initialBlock.GetComponent<BoxCollider2D>().size;


        for (int i = 0; i < matrix.GetLength(0); i++)
        {

            for (int j = 0; j < matrix.GetLength(1); j++)
            {

                //Crear cada bloque en la escena en una posicion data.
                //Cada bloque esta contenido en una determinada posicion de la matriz
                matrix[i, j] = Instantiate(initialBlock, new Vector3(posStarX + (blockSize.x * j),
                                                                      posStarY + (blockSize.y * -i),
                                                                      0),
                                                                      initialBlock.transform.rotation);

                matrix[i,j].name = string.Format("Block[{0}][{1}]", i, j);

                //Temporal - se asigna un 0 para indicar que es bloque disponible o 1 para indicar que es bloque obstaculo
                matrix[i, j].GetComponent<Block>().SetId(UnityEngine.Random.Range(0, 2));

                //Asignar la matriz al objeto de GameZone
                matrix[i, j].transform.parent = gameZone.transform;

                //Si es 0 se pinta de blanco (Azul por defecto)
                if (matrix[i, j].GetComponent<Block>().GetID() == 0)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
                //Si es 1 se pinta de negro
                if (matrix[i, j].GetComponent<Block>().GetID() == 1)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }
}
