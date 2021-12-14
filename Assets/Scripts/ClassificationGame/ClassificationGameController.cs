using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class ClassificationGameController : Reference
{
    //Array para guardar el conjunto de imagenes del juego
    static Sprite[] images;

    //String para guardar un enunciado del juego
    static string statement;

    //Array para guardar las respuesta de respuestas del juego
    static string[] answers;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Lista para almacenar las opciones seleccionadas por el jugador
    List<string>  choises;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Objeto que contiene el controlador del juego
    public static ClassificationGameController gameController;

    //Numero para saber cuantas veces ha ganado 3 estrella
    int countPerfectWins = 0;

    //Numero de veces que ha jugado
    int countPlay = 0;

    //numero que cuenta las veces que ha completado niveles sin errores
    int countPerfectGame = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    public FileLists file;
    private void Start()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 10);
        bool centinela = true;
        while(centinela)
        {
            if (file.classificationGameList.Contains(number))
            {
                //Instaciar Lista que guardara todas las imagenes
                images = App.generalModel.classificationGameModel.LoadImages(number);
                //Instaciar Lista que guardara todos los enunciados
                statement = App.generalModel.classificationGameModel.LoadTexts(number);
                //Instaciar Lista que guardara todas las respuestas
                answers = App.generalModel.classificationGameModel.LoadAnswers(number);
                centinela = false;
            }
            else
            {
                number = Random.Range(0, 10);
            }          
        }
        Debug.Log("ELIMINAR: " + number);

        //Ubicar en la pantalla las imagenes seleccionadas
        PutImages();

        //Ubicar el enunciado en la pantalla
        PutText();

        //Variable para guardar las opciones marcadas por el jugador
        choises = new List<string>();
    }
    /*
    * Metodo para ubicar todas las imagenes seleccionas en la pantalla
    */
    void PutImages()
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        List<Sprite> pictures = ChangeOrderList(images);

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
        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.classificationGameView.statement.text = statement;
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
    public void CheckAnswer()
    {

        counter++;
        if (PlayerPrefs.GetInt("PlayGame1", 0) == 0)
        {
            countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
            PlayerPrefs.SetInt("PlayOneLevel", countPlay);
            Debug.Log("Jugo: " + countPlay);
            PlayerPrefs.SetInt("PlayGame1", 1);
        }

        if (answers.Length == choises.Count)
        {
            var result = answers.Except(choises);
            if (result.Count() == 0)
            {
                App.generalModel.classificationGameModel.file.classificationGameList.Remove(number);
                App.generalModel.classificationGameModel.file.Save("P");

                if (counter == 1)
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 30);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 3);

                    //Veces que ha ganado 3 estrellas
                    countPerfectWins = PlayerPrefs.GetInt("GetThreeStars1", 0) + 1;
                    Debug.Log("GANO 3 ESTRELLAS: " + countPerfectWins);
                    PlayerPrefs.SetInt("GetThreeStars1", countPerfectWins);

                    //Veces que ha ganadi sin errores
                    countPerfectGame = PlayerPrefs.GetInt("PerfectGame1", 0) + 1;
                    Debug.Log("Lleva: " + countPerfectGame);
                    PlayerPrefs.SetInt("PerfectGame1", countPerfectGame);
                    App.generalView.gameOptionsView.ShowWinCanvas(3);
                }
                else if (counter == 2)
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 20);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 2);
                    countPerfectGame = 0;
                    PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);
                    App.generalView.gameOptionsView.ShowWinCanvas(2);
                }
                else
                {
                    App.generalModel.classificationGameModel.SetPoints(App.generalModel.classificationGameModel.GetPoints() + 10);
                    App.generalModel.classificationGameModel.SetTotalStars(App.generalModel.classificationGameModel.GetTotalStars() + 1);
                    countPerfectGame = 0;
                    PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);
                    App.generalView.gameOptionsView.ShowWinCanvas(1);
                }
            }
            else
            {
                CheckAttempt();
            }
        }
        else
        {
            CheckAttempt();
        }
    }
    void CheckAttempt()
    {
        attempts--;
        if (attempts == 0)
        {
            App.generalView.gameOptionsView.correctAnswer.sprite = App.generalModel.classificationGameModel.LoadAnswerImages(number);
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        else
        {
            App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
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

