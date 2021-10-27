using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamesMenuView : Reference
{
    //Menu del juego
    public RectTransform menu;
    
    //Variable para guardar el nombre de cada panel que posee los juegos
    string gamePanelName;

    //Variable que almacena la posicion inicial del menu en el eje X
    float initialPosition;

    //Variable que almacena la nueva posicion que se indique por el jugador
    float nextPosition;

    //Variable para cambiar el panel del juego con el movimiento de izquierda o derecha
    int cont = 1;
    void Start()
    {
        //Posicion en inicial del menu
        initialPosition = menu.transform.position.x;
        //Panel que se muestra por defecto al cargar la escena
        gamePanelName = "GamePanel1";
        //Ubicar el panel en la posicion inicial
        menu.position = new Vector3(initialPosition, menu.position.y, 0);
    }
    /*
     * Metodo para mover a la derecha el menu de juegos
     */
    public void MoveToRigth()
    {
        //Calcular la nueva posicion del menu
        nextPosition = (menu.transform.position.x) - 1080f;
        
        //La nueva posicion debe ser mayor a -7020 para evitar mostrar paneles vacios
        if (nextPosition >= -7020f)
        {
            //Aumentar el valor del contador
            cont++;

            //Obtener el nombre del panel del juego
            gamePanelName = "GamePanel" + cont;

            //Ubicar el panel en la nueva posicion (Hacia la derecha)
            menu.position = new Vector3(nextPosition, menu.position.y, 0);
        }
    }
    /*
     * Metodo para mover a la derecha el menu de juegos
     */
    public void MoveToLeft()
    {
        //Calcular la nueva posicion del menu
        nextPosition = (menu.transform.position.x) + 1080;

        //La nueva posicion debe ser menor a 540  para evitar mostrar paneles vacios
        if (nextPosition <= 540f)
        {
            //Disminuir el valor del contador
            cont--;

            //Obtener el nombre del panel del juego
            gamePanelName = "GamePanel" + cont;

            //Ubicar el panel en la nueva posicion (Hacia la izquierda)
            menu.position = new Vector3(nextPosition, menu.position.y, 0);
        }
    }
    /*
     * Metodo obtener el nombre del juego a ejecutar
     */
    public void Play()
    {
        //Obtener el nombre del juego que esta en la pantalla
        string gameName = GameObject.Find(gamePanelName).GetComponentInChildren<Text>().text;

        //Enviar el nombre del juego para mostrar su escena correspondiente
        App.generalController.gamesMenuController.Play(gameName);
    }
}
