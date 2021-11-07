using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class CharacteristicsGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    static List<Sprite[]> allImages;
    //Sprite[,] allPictures;

    //Lista para guardar todos los enunciados del juego
    static List<string> texts;
    //string[] allTexts;

    //Lista para guardar todas las respuestas del juego
    static List<string> answers;
    //string[] allAnswers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //String para almacenar la respuesta que dio el jugador
    string answer;

    //Variable que indica cuando desactivar el boton de play
    //public bool enableButton;

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
    private void Start()
    {
        //Numero random para seleccionar un conjunto de imagenes
        //number = Random.Range(0, allImages.Count);
        //Debug.Log("NUMERO: " + number);
        //Cargar todas las imagenes
        //LoadPictures();
        
        //Ubicar en la pantalla las imagenes seleccionadas
        //PutPictures();
       
        //Cargar todos los enunciados
        //LoadTexts();
       
        //Ubicar el enunciado en la pantalla
        //PutText();

        //Cargar todas las respuestas
        //LoadAnswers();
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadImages()
    {
        /*allPictures = new Sprite[4, 4];

        for (int i = 0; i < allPictures.GetLength(0); i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i + 1));
            

            for (int j = 0; j < allPictures.GetLength(1); j++)
            {
                //Guardar cada set de imagenes en la matriz. La matriz contiene todos los sets de imagenes que requiere el juego
                allPictures[i, j] = spriteslist[j];
            }
        }*/

        //Numero de conjuntos de imagenes
        int setOfImages = 4;
        for (int i = 1; i <= setOfImages; i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i));

            //Guardar el array de imagenes en la lista
            allImages.Add(spriteslist);
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
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public void LoadTexts()
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements_characteristics.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Array que guardara todos los enunciados
        //allTexts = new string[4];
        //texts = new List<string>();
        //Variable para acceder a cada posicion del array
        //int index = 0;

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente del array
            //allTexts[index] = line;
            //index++;
            texts.Add(line);
            //Leer la siguiente linea de texto
            line = reader.ReadLine();
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
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        StreamReader reader = new StreamReader("Assets/Resources/Files/correct_characteristics.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Variable para acceder a cada posicion del array
        //int index = 0;

        //Array que guardara todas las respuestas
        //allAnswers = new string[4];
        //answers = new List<string>();
        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente del array
            //allAnswers[index] = line;
            //index++;
            answers.Add(line);
            //Leer una nueva linea
            line = reader.ReadLine();
        }
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
