using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassificationGameView : Reference
{
    //Matriz de botones que puede pulsar el jugador
    public Button [] buttons;

    //Texto para mostrar el eunciado del juego
    public Text statement;

    //Objeto que representa cada una de las opciones que puede seleccionar el jugador
    public GameObject choise;

    //Objeto de contendra la lista de nombres de los elementos seleccionados
    public GameObject box;

    //Canvas que muestra el contenido inicial del juego
    public Canvas startCanvas;

    //Variable para aumentar la posicion en Y de los elementos que se agregan a la lista
    int i = 0;
    
    /*
    * Metodo que captura el boton que oprimio el jugador y captura el nombre de la imagen que posee ese boton
    */
    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Agregar el nombre de la imagen en el panel del boton
        button.GetComponentInChildren<Text>().text = nameImage.ToUpper();
        
        //Desactivar el boton
        button.enabled = false;

        //Guardar el nombre de la imagen
        App.generalController.classificationGameController.saveChoise(nameImage);

        //Agregar el nombre del elemento seleccionado a la lista
        AddItemsToTheList(nameImage);
    }
    /*
     * Metodo para agregar los nombres de los elementos seleccionados a una lista en pantalla
     */
    public void AddItemsToTheList(string name)
    {
        //Crear cada elemento de la lista en un posicion determinada en la pantalla
        GameObject texto = Instantiate(choise, new Vector3(choise.transform.position.x, choise.transform.position.y + (100 * i), 0), choise.transform.rotation);

        //Aumentar la posisicon en Y
        i++;

        //Agregar el nombre de la imagen cada elemento de la lista
        texto.GetComponent<Text>().text = name.ToUpper();

        //Asignar cada elemento de la lista al objeto box
        texto.transform.SetParent(box.transform);
    }
    /*
    * Metodo para activar un panel en cada boton seleccionado
    */
    public void ActivatedPanel(GameObject panel)
    {
        panel.GetComponent<Image>().enabled = true;
    }
    /*
    * Metodo para activar los canvas que indican si el jugador gano o perdio el juego
    */
    public void CheckAnswer()
    {
        //Determinar si el jugador gano o perdio
        bool isWin = App.generalController.classificationGameController.CheckAnswer();

        if (isWin)
        {
            //Activar el canvas de ganar
            App.generalView.gameOptionsView.WinCanvas.enabled = true;
        }
        else
        {
            //Activar el canvas de perder
            Debug.Log("PERDIO");
        }
    }
    /*
    * Metodo que oculta el canvas inicial del juego
    */
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }
}
