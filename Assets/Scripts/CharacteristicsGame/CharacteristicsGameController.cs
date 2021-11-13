using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacteristicsGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    static List<Sprite[]> allImages;

    //Lista para guardar todos los enunciados del juego
    static List<string> texts;

    //Lista para guardar todas las respuestas del juego
    static List<string> answers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //String para almacenar la respuesta que dio el jugador
    string answer;

    //Objeto que contiene el controlador del juego
    public static CharacteristicsGameController gameController;
    
    
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
            answers = new List<string>();

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
        }
        else if (gameController != this && allImages.Count>0)
        {
            //Destruir el objeto
            Destroy(gameObject);

            //Numero random para seleccionar un conjunto de imagenes
            number = Random.Range(0, allImages.Count);

            //Ubicar el enunciado en la pantalla
            PutImages();

            //Ubicar el enunciado en la pantalla
            PutText();
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
    public void LoadImages()
    {
        allImages = App.generalModel.characteristicsGameModel.LoadImages();
    }
    /*
    * Metodo para ubicar todas las imagenes seleccionas en la pantalla
    */
    public void PutImages()
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        List<Sprite> images = ChangeOrderList(allImages[number]);

        for (int i = 0; i < images.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.characteristicsGameView.buttons[i].image.sprite = images[i];
        }
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompa�ar a cada nivel
    */
    public void LoadTexts()
    {
        texts = App.generalModel.characteristicsGameModel.LoadTexts();
    }
    /*
    * Metodo para ubicar el texto en la pantalla del jugador
    */
    public void PutText()
    {
        //Obtener un enunciado en especifico
        string text = texts[number];

        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.characteristicsGameView.statement.text = text;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
    {
        answers = App.generalModel.characteristicsGameModel.LoadAnswers();
    }
    /*
    * Metodo para guardar la opcion elegida por el jugador
    */
    public void SaveOption(string selectedOption)
    {
        //Guardar la opcion seleccionada como la respuesta del jugador
        answer = selectedOption;
    }
    /*
    * Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    */
    public bool CheckAnswer()
    {
        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (answer == answers[number])
        {
            allImages.RemoveAt(number);
            texts.RemoveAt(number);
            answers.RemoveAt(number);
            /*if(allImages.Count == 0)
            {
                enableButton = true;
                Debug.Log("LISTA VACIA");
                //App.generalView.gamesMenuView.playButtons[1].enabled=false;
            }*/
            return true;
        }
        //De lo contrario pierde
        else
        {
            return false;
        }
    }
    /*
    * Metodo que permite desordenar la lista de imagenes seleccionada
    */
    public List<Sprite> ChangeOrderList(Sprite[] list)
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
