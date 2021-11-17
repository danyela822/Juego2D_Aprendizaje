using System.Collections;
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

    //Variable para contar la cantidad de veces que se presionan los botones
    int counter = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 2;

    void Start()
    {
        items = new List<GameObject>();
        for(int i = 0; i < checkArray.Length; i++)
        {
            checkArray[i] = true;
        }
    }
    /*
    * Metodo que captura el boton que oprimio el jugador y captura el nombre de la imagen que posee ese boton
    */
    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Numero del boton
        buttonNumber = Int32.Parse(button.name);

        if(checkArray[buttonNumber])
        {
            //Agregar el nombre de la imagen en el panel del boton
            button.GetComponentInChildren<Text>().text = nameImage.ToUpper();

            //Agregar el nombre del elemento seleccionado a la lista
            items.Add(AddItemsToTheList(nameImage));
            
            //Guardar el nombre de la imagen de la lista
            App.generalController.classificationGameController.SaveChoise(nameImage, checkArray[buttonNumber]);

            counter++;
            Debug.Log("Presiono: " + counter);
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

            checkArray[buttonNumber] = true;
        }
    }
    /*
     * Metodo para agregar los nombres de los elementos seleccionados a una lista en pantalla
     */
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
    /*
     * Metodo para eliminar los nombres de los elementos seleccionados de la lista en la pantalla
     */
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
    /*
    * Metodo para activar un panel en cada boton seleccionado
    */
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
    /*
    * Metodo para activar los canvas que indican si el jugador gano o perdio el juego
    */
    public void CheckAnswer()
    {
        if (items.Count == 0)
        {
            App.generalView.gameOptionsView.ShowWarningCanvas();
        }
        else
        {
            //Determinar si el jugador gano o perdio
            int numberStars = App.generalController.classificationGameController.CheckAnswer();

            if (numberStars == 3)
            {
                App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
            }
            else if (numberStars == 2)
            {
                App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
            }
            else if (numberStars == 1)
            {
                App.generalView.gameOptionsView.ShowWinCanvas(numberStars);
            }
            else if (numberStars == -1)
            {
                App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
                attempts--;
            }
            else
            {
                App.generalView.gameOptionsView.ShowLoseCanvas();
            }
        }
    }
    /*
    * Metodo que oculta el canvas inicial del juego
    */
    public void StartGame()
    {
        App.generalView.gameOptionsView.TutorialCanvas.enabled = false;
    }
}
