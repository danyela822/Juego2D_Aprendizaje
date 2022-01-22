using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    static Sprite[] images;

    //Lista para guardar todos los enunciados del juego
    static string statement;

    //Nivel del juego
    int level;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    int counter;

    //Numero para saber cuantas veces ha ganado 3 estrella
    //int countPerfectWins = 0;

    //Numero de veces que ha jugado
    int countPlay;

    //numero que cuenta las veces que ha completado niveles sin errores
    //int countPerfectGame = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    //Imagen de la respuesta correcta
    Sprite correctAnswer;

    //
    bool isLastLevel;

    private void Start()
    {
        BuildLevel();
    }
    /// <summary>
    /// Busca e instancia la lista de imagenes y la lista de textos necesarias para crear el nivel
    /// </summary>
    /// <param name="level"> Nivel a crear </param>
    public void BuildLevel()
    {
        //Obtener el nivel actual
        level = App.generalModel.characteristicsGameModel.GetLevel();

        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 5);

        //Variable que determina si existe el conjunto de imagenes y textos para ese nivel
        bool exists = false;

        //Ciclo para buscar el conjunto de imagenes y textos para ese nivel
        while (!exists)
        {
            //Se determina si es posible cargar el conjunto de imagenes y textos requeridos
            if (App.generalModel.characteristicsGameModel.FileExist(number, level))
            {
                //Instaciar Lista que guardara todas las imagenes
                images = App.generalModel.characteristicsGameModel.LoadImages(number, level);

                //Instaciar Lista que guardara todos los enunciados
                statement = App.generalModel.characteristicsGameModel.LoadTexts(number, level);

                //Cuando se instancian las lista se acaba el ciclo
                exists = true;
            }
            //Si no es posible se busca otro conjunto
            else
            {
                //Numero random para seleccionar un conjunto de imagenes
                number = Random.Range(0, 5);
            }
        }

        Debug.Log("NIVEL: " + level + " NUMERO RANDOM: " + number);
        //Ubicar en la pantalla las imagenes seleccionadas
        PutImages();

        //Ubicar el enunciado en la pantalla
        PutText();
    }
    /// <summary>
    /// Metodo para ubicar todas las imagenes seleccionas en la pantalla
    /// </summary>
    public void PutImages()
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        List<Sprite> newList = ChangeOrderList(images);

        for (int i = 0; i < newList.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.characteristicsGameView.buttons[i].image.sprite = newList[i];

            //Capturar la imagen que corresponde a la respuesta correcta
            if (newList[i].name == "correct")
            {
                correctAnswer = newList[i];
            }
        }
    }
    /// <summary>
    /// Metodo para ubicar el texto en la pantalla del jugador
    /// </summary>
    public void PutText()
    {
        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.characteristicsGameView.statement.text = statement;
    }
    /// <summary>
    /// Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    /// </summary>
    /// <param name="selectedOption"> Respuesta selecionada por el jugador </param>
    public void CheckAnswer(string selectedOption)
    {
        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.characteristicsGameModel.GetTimesPlayed();

        App.generalModel.characteristicsGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (selectedOption == "correct")
        {
            //Si ya paso el nivel 1 puede pasar al 2
            if (level == 1)
            {
                //Eliminar el numero del conjunto de imagenes y textos
                App.generalModel.characteristicsGameModel.file.imageListGame2_1.Remove(number);

                //Actualizar el nivel del juego
                App.generalModel.characteristicsGameModel.UpdateLevel(2);
            }
            //Si ya paso el nivel 2 puede pasar al 3
            else if (level == 2)
            {
                //Eliminar el numero del conjunto de imagenes y textos
                App.generalModel.characteristicsGameModel.file.imageListGame2_2.Remove(number);

                //Actualizar el nivel del juego
                App.generalModel.characteristicsGameModel.UpdateLevel(3);
 
            }
            //Si ya termino los 3 niveles de ese Set, se comienza de nuevo
            else
            {
                Debug.Log("Termino los 3");

                //Eliminar el numero del conjunto de imagenes y textos
                App.generalModel.characteristicsGameModel.file.imageListGame2_3.Remove(number);

                //Actualizar el nivel a 1 para empezar otra nueva ronda
                App.generalModel.characteristicsGameModel.UpdateLevel(1);

                //Indicar que este es el ultimo nivel
                isLastLevel = true;

            }
            //Numero de veces (en total) que selecciono una opcion
            Debug.Log("LO HIZO EN: " + counter + " INTENTOS");

            //Guardar estado
            App.generalModel.characteristicsGameModel.file.Save("P");

            //Asignar Estrellas y Puntos obtenidos
            SetPointsAndStars();
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
            points = App.generalModel.characteristicsGameModel.GetPoints() + 30;
            stars = App.generalModel.characteristicsGameModel.GetStars() + 3;
            canvasStars = 3;
            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.characteristicsGameModel.UpdatePerfectWins(App.generalModel.characteristicsGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.characteristicsGameModel.UpdatePerfectGame(App.generalModel.characteristicsGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.characteristicsGameModel.GetPoints() + 20;
            stars = App.generalModel.characteristicsGameModel.GetStars() + 2;
            canvasStars = 2;

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.characteristicsGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.characteristicsGameModel.GetPoints() + 10;
            stars = App.generalModel.characteristicsGameModel.GetStars() + 1;
            canvasStars = 1;

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.characteristicsGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.characteristicsGameModel.UpdatePoints(points);
        App.generalModel.characteristicsGameModel.UpdateStars(stars);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, isLastLevel);

    }
    /// <summary>
    /// Metodo para contar y verificar los errores
    /// </summary>
    void CheckAttempt()
    {
        //Dismuir la cantidad de intentos
        attempts--;

        //Si se queda sin intentos pierde el juego (y se le muestra la respuesta)
        if (attempts == 0)
        {
            //App.generalView.gameOptionsView.correctAnswer.sprite = correctAnswer;
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        //Si aun le quedan intentos se muestra un mensaje en pantalla
        else
        {
            App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
        }
    }
    
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

            App.generalView.characteristicsGameView.solutionPanel.SetActive(true);
            App.generalView.characteristicsGameView.solutionImage.sprite = correctAnswer;
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
        //App.generalView.gameOptionsView.correctAnswer.sprite = correctAnswer;
    }
    /// <summary>
    /// Metodo que permite desordenar la lista de imagenes seleccionada
    /// </summary>
    /// <param name="list"> Lista de imagenes </param>
    /// <returns> Lista de imagenes desordenada </returns>
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
