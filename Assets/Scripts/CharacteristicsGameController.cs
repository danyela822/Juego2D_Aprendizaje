using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CharacteristicsGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    Sprite[,] allPictures;

    //Array para guardar todos los enunciados del juego
    string[] allTexts;

    //Array para guardar todas las respuestas del juego
    string[] allAnswers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //String para almacenar la respuesta que dio el jugador
    string answer;

    private void Start()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 2);
        
        //Cargar todas las imagenes
        LoadPictures();
        
        //Ubicar en la pantalla las imagenes seleccionadas
        PutPictures();
       
        //Cargar todos los enunciados
        LoadTexts();
       
        //Ubicar el enunciado en la pantalla
        PutText();

        //Cargar todas las respuestas
        LoadAnswers();
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadPictures()
    {
        allPictures = new Sprite[2, 4];

        for (int i = 0; i < allPictures.GetLength(0); i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i + 1));

            for (int j = 0; j < allPictures.GetLength(1); j++)
            {
                //Guardar cada set de imagenes en la matriz. La matriz contiene todos los sets de imagenes que requiere el juego
                allPictures[i, j] = spriteslist[j];
            }
        }
    }
    /*
    * Metodo que permite seleccionar un conjunto de imagenes en especifico de todas la imagenes que hay guardadas en una matriz
    */
    public Sprite[] SelectPictures(int number)
    {
        //Obtener todos los sets de imagenes
        Sprite[,] pictures = allPictures;

        //Array que guardara las imagenes seleccionadas para un nivel
        Sprite[] selectedPictures = new Sprite[pictures.GetLength(1)];

        for (int j = 0; j < pictures.GetLength(1); j++)
        {
            //Guardar cada imagen del set selecionado en el array
            selectedPictures[j] = pictures[number, j];
        }
        return selectedPictures;
    }
    /*
    * Metodo para ubicar todas las imagenes seleccionas en la pantalla
    */
    public void PutPictures()
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        List<Sprite> pictures = ChangeOrderList(SelectPictures(number));

        for (int i = 0; i < pictures.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.characteristicsGameView.buttons[i].image.sprite = pictures[i];
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
        allTexts = new string[2];

        //Variable para acceder a cada posicion del array
        int index = 0;

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente del array
            allTexts[index] = line;
            index++;

            //Leer la siguiente linea de texto
            line = reader.ReadLine();
        }
    }
    /*
    * Metodo que permite seleccionar un enunciado en especifico de todos los enunciados guardados para mostrarlo en la pantalla
    */
    public string SelectText(int number)
    {
        //Obtener todos los enunciados
        string[] texts = allTexts;

        //Seleccionar un enunciado en especifico
        return texts[number];
    }
    /*
    * Metodo para ubicar el texto en la pantalla del jugador
    */
    public void PutText()
    {
        //Obtener un enunciado en especifico
        string text = SelectText(number);

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
        int index = 0;

        //Array que guardara todas las respuestas
        allAnswers = new string[2];

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente del array
            allAnswers[index] = line;
            index++;

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
        if (answer == allAnswers[number])
        {
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
