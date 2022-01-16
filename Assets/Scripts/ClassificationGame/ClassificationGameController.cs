using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

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
    //int countPerfectWins = 0;

    //Numero de veces que ha jugado
    //int countPlay = 0;

    //numero que cuenta las veces que ha completado niveles sin errores
    //int countPerfectGame = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    private void Start()
    {
        if (App.generalModel.classificationGameModel.file.classificationGameList.Count == 0)
        {

        }
        else
        {
            BuildLevel();
        }
    }
    /// <summary>
    /// Busca e instancia la lista de imagenes y la lista de textos necesarias para crear el nivel
    /// </summary>
    public void BuildLevel()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 10);

        //Variable que determina si existe el conjunto de imagenes y textos
        bool exists = false;

        while (!exists)
        {
            if (App.generalModel.classificationGameModel.FileExist(number))
            {
                //Instaciar Lista que guardara todas las imagenes
                images = App.generalModel.classificationGameModel.LoadImages(number);

                //Instaciar Lista que guardara todos los enunciados
                statement = App.generalModel.classificationGameModel.LoadTexts(number);

                //Instaciar Lista que guardara todas las respuestas
                answers = App.generalModel.classificationGameModel.LoadAnswers(number);

                //Cuando se instancian las lista se acaba el ciclo
                exists = true;
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
        List<Sprite> newList = ChangeOrderList(images);

        for (int i = 0; i < newList.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.classificationGameView.buttons[i].image.sprite = newList[i];
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
        if (App.generalModel.classificationGameModel.GetTimesPlayed() == 0)
        {
            /*countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
            PlayerPrefs.SetInt("PlayOneLevel", countPlay);
            Debug.Log("A Jugado: " + countPlay);
            PlayerPrefs.SetInt("PlayGame1", 1);*/
            App.generalModel.classificationGameModel.UpdateTimesPlayed(1);
        }

        if (answers.Length == choises.Count)
        {
            var result = answers.Except(choises);
            if (result.Count() == 0)
            {
                App.generalModel.classificationGameModel.file.classificationGameList.Remove(number);
                App.generalModel.classificationGameModel.file.Save("P");

                SetPointsAndStars();
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
    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    public void SetPointsAndStars()
    {
        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, canvasStars;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (counter == 1)
        {
            points = App.generalModel.classificationGameModel.totalPoints + 30;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 3;
            canvasStars = 3;

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.classificationGameModel.UpdatePerfectWins(App.generalModel.classificationGameModel.countPerfectWins + 1);

            //Actualizar las veces que ha ganado sin errores
            //App.generalModel.characteristicsGameModel.UpdatePerfectGame(App.generalModel.characteristicsGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.classificationGameModel.totalPoints + 20;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 2;
            canvasStars = 2;

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            //App.generalModel.classificationGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.classificationGameModel.totalPoints + 10;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 1;
            canvasStars = 1;

            //Actualizar las veces que ha ganado sin errores
            //App.generalModel.classificationGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.classificationGameModel.UpdatePoints(points);
        App.generalModel.classificationGameModel.UpdateTotalStars(stars);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, true);

    }
    void CheckAttempt()
    {
        attempts--;
        if (attempts == 0)
        {
            //App.generalView.gameOptionsView.correctAnswer.sprite = App.generalModel.classificationGameModel.LoadAnswerImages(number);
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        else
        {
            App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
        }
    }
    public GameObject characteristicsWindow;
    public Image solutionImage;
    public void ShowSolution()
    {
        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();
            App.generalView.gameOptionsView.ShowSolutionCanvas();
            characteristicsWindow.SetActive(true);
            solutionImage.sprite = App.generalModel.classificationGameModel.LoadAnswerImages(number);
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
        //App.generalView.gameOptionsView.correctAnswer.sprite = correctAnswer;
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

