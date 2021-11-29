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

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Objeto que contiene el controlador del juego
    public static CharacteristicsGameController gameController;

    //public Prueba p;
    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameObject);
            //p.Load("P");
            //Cargar todas las imagenes (Solo una vez)
            //Instaciar Lista que guardara todas las imagenes
            //allImages = App.generalModel.characteristicsGameModel.LoadImages();

            App.generalModel.characteristicsGameModel.LoadImages();
            allImages = App.generalModel.characteristicsGameModel.GetListImages();
            Debug.Log("TAMAÑO LISTA IMAGENES: " + allImages.Count);

            //Cargar todos los enunciados (Solo una vez)
            //Instaciar Lista que guardara todos los enunciados
            //texts = App.generalModel.characteristicsGameModel.LoadTexts();

            App.generalModel.characteristicsGameModel.LoadTexts();
            texts = App.generalModel.characteristicsGameModel.GetListTexts();
            Debug.Log("TAMAÑO LISTA TEXTOS: " + texts.Count);

            //Cargar todas las respuestas (Solo una vez)
            //Instaciar Lista que guardara todas las respuestas
            //answers = App.generalModel.characteristicsGameModel.LoadAnswers();

            App.generalModel.characteristicsGameModel.LoadAnswers();
            answers = App.generalModel.characteristicsGameModel.GetListAnswers();
            Debug.Log("TAMAÑO LISTA RESPUESTAS: " + answers.Count);

            //Numero random para seleccionar un conjunto de imagenes
            number = Random.Range(0, allImages.Count);
            Debug.Log("NUMERO RANDOM: " + number);

            //Ubicar en la pantalla las imagenes seleccionadas
            PutImages();

            //Ubicar el enunciado en la pantalla
            PutText();
            // && gameObject.scene.name == "CharacteristicsGameScene"
            //Debug.Log("SCENE EN MENU: " + gameObject.scene.name);
        }
        else if (gameController != this && allImages.Count > 0)
        {
            //Debug.Log("SCENE: " + gameObject.scene.name);
            //Destruir el objeto
            Destroy(gameObject);

            //Ubicar el enunciado en la pantalla
            //PutImages();

            //Ubicar el enunciado en la pantalla
            //PutText();
            if(gameObject.scene.name == "CharacteristicsGameScene")
            {
                //Numero random para seleccionar un conjunto de imagenes
                number = Random.Range(0, allImages.Count);
                Debug.Log("NUMERO RANDOM: " + number);

                //Ubicar el enunciado en la pantalla
                PutImages();

                //Ubicar el enunciado en la pantalla
                PutText();
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
    //bool enableButton = false;
    public int CheckAnswer()
    {
        counter++;
        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (answer == answers[number])
        {
            allImages.RemoveAt(number);
            texts.RemoveAt(number);
            answers.RemoveAt(number);

            Debug.Log("ELIMINAR: " + number);
            App.generalModel.characteristicsGameModel.p.characteristicsGameList.RemoveAt(number);
            App.generalModel.characteristicsGameModel.p.Save("P");

            Debug.Log("LISTA DE IMAGENES MODELO: " + App.generalModel.characteristicsGameModel.GetListImages().Count);

            if (counter == 1)
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 30);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 3);
                return 3;
            }
            else if(counter == 2)
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 20);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 2);
                return 2;
            }
            else 
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 10);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 1);
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
