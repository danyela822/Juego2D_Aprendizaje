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

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Objeto que contiene el controlador del juego
    public static CharacteristicsGameController gameController;

    //Numero para saber cuantas veces ha ganado 3 estrella
    int countPerfectWins = 0;

    //Numero de veces que ha jugado
    int countPlay = 0;

    //numero que cuenta las veces que ha completado niveles sin errores
    int countPerfectGame = 0;

    private void Start()
    {
        //Instaciar Lista que guardara todas las imagenes
        allImages = App.generalModel.characteristicsGameModel.LoadImages();

        //Instaciar Lista que guardara todos los enunciados
        texts = App.generalModel.characteristicsGameModel.LoadTexts();

        //Instaciar Lista que guardara todas las respuestas
        answers = App.generalModel.characteristicsGameModel.LoadAnswers();

        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, allImages.Count);

        //Ubicar en la pantalla las imagenes seleccionadas
        PutImages();

        //Ubicar el enunciado en la pantalla
        PutText();
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
    * Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    */
    public int CheckAnswer(string selectedOption)
    {
        if (PlayerPrefs.GetInt("PlayGame2", 0) == 0)
        {
            countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
            PlayerPrefs.SetInt("PlayOneLevel", countPlay);
            Debug.Log("Jugo: " + countPlay);
            PlayerPrefs.SetInt("PlayGame2", 1);
        }
        counter++;
        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (selectedOption == answers[number])
        {
            allImages.RemoveAt(number);
            texts.RemoveAt(number);
            answers.RemoveAt(number);

            Debug.Log("ELIMINAR: " + number);
            App.generalModel.characteristicsGameModel.file.characteristicsGameList.RemoveAt(number);
            App.generalModel.characteristicsGameModel.file.Save("P");

            //Debug.Log("LISTA DE IMAGENES MODELO: " + App.generalModel.characteristicsGameModel.GetListImages().Count);

            if (counter == 1)
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 30);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 3);

                //Veces que ha ganado 3 estrellas
                countPerfectWins = PlayerPrefs.GetInt("GetThreeStars2", 0) + 1;
                Debug.Log("GANO 3 ESTRELLAS: " + countPerfectWins);
                PlayerPrefs.SetInt("GetThreeStars2", countPerfectWins);

                //Veces que ha ganadi sin errores
                countPerfectGame = PlayerPrefs.GetInt("PerfectGame2", 0) + 1;
                Debug.Log("Lleva: " + countPerfectGame);
                PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);

                return 3;
            }
            else if(counter == 2)
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 20);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 2);
                countPerfectGame = 0;
                PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);
                return 2;
            }
            else 
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 10);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 1);
                countPerfectGame = 0;
                PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);
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
