using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class ClassificationGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    static List<Sprite[]> allImages;
    //Sprite[,] allPictures;

    //Array para guardar todos los enunciados del juego
    static List<string> texts;
    //string[] allTexts;

    //Lista para guardar todos los conjuntos de respuestas del juego
    static List<string[]> allAnswers;
    //List<string[]> allAnswers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Lista para almacenar las opciones seleccionadas por el jugador
    List<string>  choises;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Objeto que contiene el controlador del juego
    public static ClassificationGameController gameController;
    
    
    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameObject);

            //Instaciar Lista que guardara todas las imagenes
            allImages = App.generalModel.classificationGameModel.LoadImages();

            //Instaciar Lista que guardara todos los enunciados
            texts = App.generalModel.classificationGameModel.LoadTexts();

            //Instaciar Lista que guardara todas las respuestas
            allAnswers = App.generalModel.classificationGameModel.LoadAnswers();
            //Numero random para seleccionar un conjunto de imagenes
            //number = Random.Range(0, allImages.Count);

            //Cargar todas las imagenes (Solo una vez)
            //LoadImages();

            //Ubicar en la pantalla las imagenes seleccionadas
            //PutImages();

            //Cargar todos los enunciados (Solo una vez)
            //LoadTexts();

            //Ubicar el enunciado en la pantalla
            //PutText();

            //Cargar todas las respuestas (Solo una vez)
            //LoadAnswers();

            //Variable para guardar las opciones marcadas por el jugador
            choises = new List<string>();
        }
        else if (gameController != this && allImages.Count > 0)
        {
            //Destruir el objeto
            Destroy(gameObject);

            if (gameObject.scene.name == "ClassificationGameScene")
            { 
                //Numero random para seleccionar un conjunto de imagenes
                number = Random.Range(0, allImages.Count);

                //Ubicar el enunciado en la pantalla
                PutImages();

                //Ubicar el enunciado en la pantalla
                PutText();

                //Variable para guardar las opciones marcadas por el jugador
                choises = new List<string>();
            }

        }
        else
        {
            Debug.Log("LISTA VACIA");
            //SceneManager.LoadScene("GamesMenuScene");
        }
    }
    /*
    * Metodo para ubicar todas las imagenes seleccionas en la pantalla
    */
    void PutImages()
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        List<Sprite> pictures = ChangeOrderList(allImages[number]);

        for (int i = 0; i < pictures.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.classificationGameView.buttons[i].image.sprite = pictures[i];
        }
    }
    /*
    * Metodo para ubicar el texto en la pantalla del jugador
    */
    void PutText()
    {
        //Obtener un enunciado en especifico
        string text = texts[number];

        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.classificationGameView.statement.text = text;
    }
    /*
    * Metodo para guardar en una lista cada opcion seleccionada por el jugador
    */
    public void SaveChoise(string choise,bool pressed)
    {
        if(pressed && !choises.Contains(choise))
        {
            //Guardar cada opcion en la lista
            choises.Add(choise);
            //Debug.Log("SE AÑADIO A LA LISTA: "+choise);
        }
    }
    public void DeleteChoise(string choise, bool pressed)
    {
        if (!pressed)
        {
            //Eliminar la opcion de la lista
            choises.Remove(choise);
            //Debug.Log("SE ELIMINO DE LA LISTA: " + choise);
        }
    }
    /*
    * Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    */
    public int CheckAnswer()
    {
        counter++;

        //Obtener un conjunto de respuestas en especifico
        string[] answers = allAnswers[number];

        if(answers.Length == choises.Count)
        {
            var result = answers.Except(choises);
            if (result.Count() == 0)
            {
                allImages.RemoveAt(number);
                texts.RemoveAt(number);
                allAnswers.RemoveAt(number);
                App.generalModel.classificationGameModel.q.classificationGameList.RemoveAt(number);

                if (counter == 1)
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 30);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 3);
                    return 3;
                }
                else if (counter == 2)
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 20);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 2);
                    return 2;
                }
                else
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 10);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 1);
                    return 1;
                }
            }
            else
            {
                if (counter == 3)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
        else
        {
            if (counter == 3)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
    /*
    * Metodo que permite desordenar la lista de imagenes seleccionada
    */
    List<Sprite> ChangeOrderList (Sprite[] list)
    {
        List<Sprite> originalList = new List<Sprite>();

        for (int i = 0; i < list.Length; i++)
        {
            originalList.Add(list[i]);
        }

        List<Sprite> newList = new List<Sprite>();

        while (originalList.Count > 0)
        {
            int index = Random.Range(0, originalList.Count - 1);
            newList.Add(originalList[index]);
            originalList.RemoveAt(index);
        }

        return newList;
    }
}

