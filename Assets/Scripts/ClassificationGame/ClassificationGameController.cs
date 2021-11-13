using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    //Objeto que contiene el controlador del juego
    public static ClassificationGameController gameController;
    
    
    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameObject);

            //Instaciar Lista que guardara todas las imagenes
            allImages = new List<Sprite[]>();

            //Instaciar Lista que guardara todos los enunciados
            texts = new List<string>();

            //Instaciar Lista que guardara todas las respuestas
            allAnswers = new List<string[]>();

            //Numero random para seleccionar un conjunto de imagenes
            number = Random.Range(0, allImages.Count);

            //Cargar todas las imagenes (Solo una vez)
            LoadImages();

            //Ubicar en la pantalla las imagenes seleccionadas
            PutImages();

            //Cargar todos los enunciados (Solo una vez)
            LoadTexts();

            //Ubicar el enunciado en la pantalla
            PutText();

            //Cargar todas las respuestas (Solo una vez)
            LoadAnswers();

            //Variable para guardar las opciones marcadas por el jugador
            choises = new List<string>();
        }
        else if (gameController != this && allImages.Count > 0)
        {
            //Destruir el objeto
            Destroy(gameObject);

            //Numero random para seleccionar un conjunto de imagenes
            number = Random.Range(0, allImages.Count);

            //Ubicar el enunciado en la pantalla
            PutImages();

            //Ubicar el enunciado en la pantalla
            PutText();

            //Variable para guardar las opciones marcadas por el jugador
            choises = new List<string>();
        }
        else
        {
            Debug.Log("LISTA VACIA");
            SceneManager.LoadScene("GamesMenuScene");
        }
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    void LoadImages()
    {
        allImages = App.generalModel.classificationGameModel.LoadImages();
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
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    void LoadTexts()
    {
        texts = App.generalModel.classificationGameModel.LoadTexts();
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
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    void LoadAnswers()
    {
        allAnswers = App.generalModel.classificationGameModel.LoadAnswers();
    }
    /*
    * Metodo para guardar en una lista cada opcion seleccionada por el jugador
    */
    public void saveChoise(string choise)
    {
        //Guardar cada opcion en la lista
        choises.Add(choise);
    }
    /*
    * Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    */
    public bool CheckAnswer()
    {
        //Obtener un conjunto de respuestas en especifico
        string[] answers = allAnswers[number];

        //Variable para contar cada acierto del jugador
        int count = 0;

        //Si la cantidad de opciones seleccionas por el jugador es igual a la cantidad de respuestas
        //se procede a verificar cada una de las opciones ingresadas
        if (choises.Count == answers.Length)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                //Por cada elemento en el array de opciones igual al de respuestas aumenta el contador de aciertos
                if (choises.Contains(answers[i]))
                {
                    Debug.Log("R: " + answers[i]);
                    count++;
                    Debug.Log("COUNT: " + count);
                }
            }
            //Si el contador de aciertos es igual al tamaño del array de respuestas
            //el juegador gana el juego
            if(count == answers.Length)
            {
                allImages.RemoveAt(number);
                texts.RemoveAt(number);
                allAnswers.RemoveAt(number);
                return true;
            }
            //De lo contrario, pierde
            else
            {
                return false;
            }
        }
        //Si la cantidad de opciones no es igual a la cantidad de respuestas no se puede verificar
        else
        {
            return false;
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

