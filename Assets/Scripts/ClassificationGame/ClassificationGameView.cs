using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    //Variable para aumentar la posicion en Y de los elementos que se agregan a la lista
    int i = 0;

    //Array para verificar si el bonton esta pulsado o no
    readonly bool[] checkArray = new bool[16];

    //Variable para capturar el numero del boton con el que se esta interactuando
    int buttonNumber;

    //Lista de items que aparecen en la pantalla al seleccionar un boton
    List<GameObject> items;

    //Imagen para mostar la solucion del nivel actual
    public Image solutionImage;

    //Objeto que representa la ventana que muestra la solucion del nivel actual
    public GameObject solutionPanel;

    void Start()
    {
        items = new List<GameObject>();
        for(int i = 0; i < checkArray.Length; i++)
        {
            checkArray[i] = true;
        }
    }
    /// <summary>
    /// Metodo que captura el boton que oprimio el jugador y captura el nombre de la imagen que posee ese boton
    /// </summary>
    /// <param name="button">Boton que oprimio</param>
    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Numero del boton
        buttonNumber = Int32.Parse(button.name);

        //Verificar si el nombre de ese boton esta agregado en la lista
        if(checkArray[buttonNumber])
        {
            //Agregar el nombre de la imagen en el panel del boton
            button.GetComponentInChildren<Text>().text = nameImage.ToUpper();

            //Agregar el nombre del elemento seleccionado a la lista
            items.Add(AddItemsToTheList(nameImage));
            
            //Guardar el nombre de la imagen de la lista
            App.generalController.classificationGameController.SaveChoise(nameImage, checkArray[buttonNumber]);

            //Cambiar el estado -> Agregado en la lista
            checkArray[buttonNumber] = false;
        }
        else
        {
            //Eliminar de la lista el nombre del elemento seleccionado
            items.Remove(DeleteItemsFromList(nameImage));

            //Eliminar el nombre de la lista
            App.generalController.classificationGameController.DeleteChoise(nameImage, checkArray[buttonNumber]);

            //Agregar el nombre de la imagen en el panel del boton
            button.GetComponentInChildren<Text>().text = "";

            //Cambiar el estado -> Eliminado de la lista
            checkArray[buttonNumber] = true;
        }
    }
    /// <summary>
    /// Metodo para instanciar los nombres de los elementos seleccionados a una lista en pantalla
    /// </summary>
    /// <param name="name">Nombre del elemento</param>
    /// <returns>GameObject del elemento que se va a agregar a la lista</returns>
    public GameObject AddItemsToTheList(string name)
    {
        //Crear cada elemento de la lista en un posicion determinada en la pantalla
        GameObject text = Instantiate(choise, new Vector3(choise.transform.position.x, choise.transform.position.y + (100 * i), 0), choise.transform.rotation);

        //Cambiar el nombre del elemento
        text.name = name;

        //Aumentar la posisicon en Y
        i++;

        //Agregar el nombre de la imagen cada elemento de la lista
        text.GetComponent<Text>().text = name.ToUpper();

        //Asignar cada elemento de la lista al objeto box
        text.transform.SetParent(box.transform);

        return text;
    }
    /// <summary>
    /// Metodo para eliminar los nombres de los elementos seleccionados de la lista en la pantalla
    /// </summary>
    /// <param name="name">Nombre del elemento</param>
    /// <returns>GameObject del elemento que se va a eliminar a la lista</returns>
    public GameObject DeleteItemsFromList(string name)
    {
        //Buscar el elemento a eliminar
        GameObject item = GameObject.Find(name);

        //Buscar el index del elemento en la lista de los items
        int index = items.IndexOf(item);

        //Destruir el GameObject que esta en la lista
        Destroy(items[index]);

        return item;
    }
    /// <summary>
    /// Metodo para activar un panel en cada boton seleccionado
    /// </summary>
    /// <param name="panel">Panel seleccionado</param>
    public void ActivatedPanel(GameObject panel)
    {
        if (panel.GetComponent<Image>().enabled)
        {
            panel.GetComponent<Image>().enabled = false;
        }
        else
        {
            panel.GetComponent<Image>().enabled = true;
        }
    }
    /// <summary>
    /// Metodo para activar los canvas que indican si el jugador gano o perdio el juego
    /// </summary>
    public void CheckAnswer()
    {
        //Si no ha seleccionado ninguna opcion se muestra un canvas de advertencia
        if (items.Count == 0)
        {
            App.generalView.gameOptionsView.ShowWarningCanvas();
        }
        else
        {
            //Determinar si el jugador gano o perdio
            App.generalController.classificationGameController.CheckAnswer();
        }
    }
    /// <summary>
    /// Metodo que muestra la solucion del juego
    /// </summary>
    public void ShowSolution()
    {
        App.generalController.classificationGameController.ShowSolution();
    }
}
