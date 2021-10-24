using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ClassificationGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    Sprite[,] allPictures;

    //Array para guardar todos los enunciados del juego
    string[] allTexts;

    //Lista para guardar todos los conjuntos de respuestas del juego
    List<string[]> allAnswers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Lista para almacenar las opciones seleccionadas por el jugador
    List<string>  choises;

    private void Start()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0,9);
       
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
        
        //Variable para guardar las opciones marcadas por el jugador
        choises = new List<string>();
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadPictures()
    {
        allPictures = new Sprite[10, 16];

        for (int i = 0; i < allPictures.GetLength(0); i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_"+(i+1));

            for (int j = 0; j < allPictures.GetLength(1); j++)
            {
                //Guardar cada set de imagenes en la matriz. La matriz contiene todos los sets de imagenes que requiere el juego
                allPictures[i,j] = spriteslist[j];
            }            
        }
    }
    public Sprite[,] getPictures()
    {
        return allPictures;
    }
    /*
    * Metodo que permite seleccionar un conjunto de imagenes en especifico de todas la imagenes que hay guardadas en una matriz
    */
    public Sprite[] SelectPictures(int number)
    {
        //Obtener todos los sets de imagenes
        Sprite[,] pictures = getPictures();

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
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public void LoadTexts()
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Array que guardara todos los enunciados
        allTexts = new string[10];

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
    public string[] getTexts()
    {
        return allTexts;
    }
    /*
    * Metodo que permite seleccionar un enunciado en especifico de todos los enunciados guardados para mostrarlo en la pantalla
    */
    public string SelectText(int number)
    {
        //Obtener todos los enunciados
        string[] texts = getTexts();

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
        App.generalView.classificationGameView.statement.text = text;
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
            App.generalView.classificationGameView.buttons[i].image.sprite = pictures[i];
        }
    }
    public List<string[]> GetAnswers()
    {
        return allAnswers;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        StreamReader reader = new StreamReader("Assets/Resources/Files/correct_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Lista que guardara cada set de respuestas correctas
        allAnswers = new List<string[]>();

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Array que guarda las respuestas de un set
            string[] values = line.Split(',');

            //Guardar cada set de respuestas en la lista
            allAnswers.Add(values);

            //Leer una nueva linea
            line = reader.ReadLine();        
        }
    }
    /*
    * Metodo que permite seleccionar las respuestas correctas de un conjunto en especifico
    */
    public string[] SelectAnswers(int number)
    {
        //Obtener las respuestas de un conjunto especifico
        string[] correctAnsers = GetAnswers()[number];
        return correctAnsers;
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
        string[] answers = SelectAnswers(number);

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
    public List<Sprite> ChangeOrderList (Sprite[] list)
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

