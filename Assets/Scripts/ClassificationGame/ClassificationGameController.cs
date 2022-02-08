using System.Collections.Generic;
using System.Collections;
using UnityEngine;
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

    //Numero de veces que ha jugado
    int countPlay;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    ///////////////////////////////////////// ENVIAR DATOS A HOJA DE CALCULO////////////////////////////////
    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSea82gFv0hoJ0GEqBVM0xo3FWcLpzB4Gd4pd3SZPgpqyjTisQ/formResponse";
    
    IEnumerator DataChoise(string personChoise)
    {
        print("Form Data");
        WWWForm form = new WWWForm();
        form.AddField("entry.1913480834", "Classification Game");
        form.AddField("entry.1172942235",number+"");
        form.AddField("entry.1147120928",statement+"");
        form.AddField("entry.1025191224",personChoise);
        form.AddField("entry.1351127637",UserData.userData.userName);
        form.AddField("entry.818409536",UserData.userData.userAge);


        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL,rawData);
        yield return www;
    }

    IEnumerator DataAnswer(string correctAnswer, string personAnswer)
    {
        print("Form Data");
        WWWForm form = new WWWForm();
        form.AddField("entry.1913480834", "Classification Game");
        form.AddField("entry.1172942235",number+"");
        form.AddField("entry.1147120928",statement+"");
        form.AddField("entry.2111097562",counter+"");
        form.AddField("entry.1733596609",correctAnswer);
        form.AddField("entry.1876414124",personAnswer);
        form.AddField("entry.1351127637",UserData.userData.userName);
        form.AddField("entry.818409536",UserData.userData.userAge);
        


        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL,rawData);
        yield return www;
    }

    public void Send(int type, string personChoise, string correctAnswer, string personAnswer)
    {
        print("Metodo send");
        if(type == 1) StartCoroutine(DataChoise(personChoise));
        else StartCoroutine(DataAnswer(correctAnswer, personAnswer));
    }

    void Start()
    {
        BuildLevel();
    }
    /// <summary>
    /// Metodo que busca e instancia la lista de imagenes y la lista de textos necesarias para crear el nivel
    /// </summary>
    public void BuildLevel()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 10);

        //Variable que determina si existe el conjunto de imagenes y textos
        bool exists = false;

        while (!exists)
        {
            //Verificar si existe si existe ese conjunto de imagenes y textos en especifico
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
                //Si no existe ese conjunto de imagenes y textos se seleciona otro
                number = Random.Range(0, 10);
            }

        }
        Debug.Log("ELIMINAR: " + number);

        //Ubicar las imagenes en los botones
        PutImages();

        //Ubicar el enunciado en la pantalla
        PutText();

        //Variable para guardar las opciones marcadas por el jugador
        choises = new List<string>();
    }
    /// <summary>
    /// Metodo para ubicar todas las imagenes en los botones
    /// </summary>
    void PutImages()
    {
        //Lista que guardara las imagenes pero cambiando su orden dentro de la lista
        List<Sprite> newList = ChangeOrderList(images);

        for (int i = 0; i < newList.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.classificationGameView.buttons[i].image.sprite = newList[i];
        }
    }
    /// <summary>
    /// Metodo para ubicar el texto en la pantalla del jugador
    /// </summary>
    void PutText()
    {
        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.classificationGameView.statement.text = statement;
    }
    /// <summary>
    /// Metodo para guardar en una lista cada opcion seleccionada por el jugador
    /// </summary>
    /// <param name="choise">Opcion seleccionada</param>
    /// <param name="pressed">Estado del boton (Presionado o no Presionado)</param>
    public void SaveChoise(string choise,bool pressed)
    {
        Send(1, choise, "", "");
        //Verificar si la opcion esta guardada en la lista y si el boton esta o no presionado
        if(pressed && !choises.Contains(choise))
        {
            //Guardar cada opcion en la lista
            choises.Add(choise);
        }
    }
    /// <summary>
    /// Metodo para eliminar una de una lista la opcion seleccionada por el jugador
    /// </summary>
    /// <param name="choise">Opcion seleccionada</param>
    /// <param name="pressed">Estado del boton (Presionado o no Presionado)</param>
    public void DeleteChoise(string choise, bool pressed)
    {
        //Verificar si el boton esta o no presionado
        if (!pressed)
        {
            //Eliminar la opcion de la lista
            choises.Remove(choise);
        }
    }
    /// <summary>
    /// Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    /// </summary>
    public void CheckAnswer()
    {
        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.classificationGameModel.GetTimesPlayed();

        App.generalModel.classificationGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        Send(2, "", string.Join(" ,", answers), string.Join(" ,", choises));

        //Verificar si la cantidad de opciones del jugador es igual a la cantidad de opciones guardadas en answers
        if (answers.Length == choises.Count)
        {
            //Verificar si las opciones del jugador son iguales a las opciones guardadas en answers
            var result = answers.Except(choises);
            if (result.Count() == 0)
            {
                //Activar el sonido de ganar
                SoundManager.soundManager.PlaySound(4);

                //Eliminar el numero del conjunto de imagenes y textos
                App.generalModel.classificationGameModel.file.imageListGame1.Remove(number);

                //Guardar estado
                App.generalModel.classificationGameModel.file.Save("P");

                //Asignar Estrellas y Puntos obtenidos
                SetPointsAndStars();
            }
            else
            {
                //Contar un error
                CheckAttempt();
            }
        }
        else
        {
            //Contar un error
            CheckAttempt();
        }
    }
    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    public void SetPointsAndStars()
    {
        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, totalStars, totalPoints, canvasStars;

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (counter == 1)
        {
            points = App.generalModel.classificationGameModel.GetPoints() + 30;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 3;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 30;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 3;

            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.classificationGameModel.UpdatePerfectWins(App.generalModel.classificationGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.classificationGameModel.UpdatePerfectGame(App.generalModel.classificationGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.classificationGameModel.GetPoints() + 20;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 2;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 20;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 2;

            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.classificationGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.classificationGameModel.GetPoints() + 10;
            stars = App.generalModel.classificationGameModel.GetTotalStars() + 1;
            
            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 10;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 1;

            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.classificationGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.classificationGameModel.UpdatePoints(points);
        App.generalModel.classificationGameModel.UpdateTotalStars(stars);

        App.generalModel.statsModel.UpdateTotalStars(totalStars);
        App.generalModel.statsModel.UpdateTotalPoints(totalPoints);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage,true);

    }
    /// <summary>
    /// Metodo para contar y verificar los errores
    /// </summary>
    void CheckAttempt()
    {
        //Dismuir la cantidad de intentos
        attempts--;

        //Si se queda sin intentos pierde el juego
        if (attempts == 0)
        {
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        //Si aun le quedan intentos se muestra un mensaje en pantalla
        else
        {
            //Obtener el mensaje de intento
            string attemptMessages = App.generalController.gameOptionsController.attemptMessages[attempts - 1];
            App.generalView.gameOptionsView.ShowMistakeCanvas(attemptMessages);
        }
    }
    /// <summary>
    /// Metodo para mostrar la solucion del nivel actual
    /// </summary>
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

            App.generalView.classificationGameView.solutionPanel.SetActive(true);
            App.generalView.classificationGameView.solutionImage.sprite = App.generalModel.classificationGameModel.LoadAnswerImages(number);
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }
    /// <summary>
    /// Metodo que permite desordenar la lista de imagenes seleccionada
    /// </summary>
    /// <param name="list"> Array de imagenes </param>
    /// <returns> Lista de imagenes desordenada </returns>
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

