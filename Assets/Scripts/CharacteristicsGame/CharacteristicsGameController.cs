using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacteristicsGameController : Reference
{
    //Matriz para guardar todos los conjuntos de imagenes del juego
    static Sprite[] images;

    //Lista para guardar todos los enunciados del juego
    static string statement;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Objeto que contiene el controlador del juego
    public static CharacteristicsGameController gameController;

    //Numero para saber cuantas veces ha ganado 3 estrella
    //int countPerfectWins = 0;

    //Numero de veces que ha jugado
    int countPlay = 0;

    //numero que cuenta las veces que ha completado niveles sin errores
    //int countPerfectGame = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    //Imagen de la respuesta correcta
    Sprite correctAnswer;

    private void Start()
    {
        SetLevel();
    }
    ///<summary>
    ///Determina el nivel a jugar (1, 2 o 3) y el conjunto de imagenes y textos a mostrar
    ///</summary>
    public void SetLevel()
    {
        //Si es el primer nivel se debe selecionar aleatoriamente el cojunto de imagenes
        if (App.generalModel.characteristicsGameModel.GetLevel() == 1)
        {
            //Numero random para seleccionar un conjunto de imagenes
            number = Random.Range(0, 5);
            
            //Guardar el numero del conjunto selecionado
            App.generalModel.characteristicsGameModel.ChangeImagesSet(number);
            
            //Iniciar el conteo de intentos en 0
            App.generalModel.characteristicsGameModel.UpdateNumberAttempts(0);
        }
        //Si no es el primer nivel se debe recuperar el conjunto de imagenes establecido previamente
        else
        {
            //Quitar el aviso
            App.generalView.gameOptionsView.HideTutorialCanvas();

            //Obtener el numero del conjunto de imagenes
            number = App.generalModel.characteristicsGameModel.GetImagesSet();
            //number = Random.Range(0, 5);
        }

        //Obtener el nivel actual
        int level = App.generalModel.characteristicsGameModel.GetLevel();

        //Construir el nivel
        BuildLevel(level);
    }
    /// <summary>
    /// Busca e instancia la lista de imagenes y la lista de textos necesarias para crear el nivel
    /// </summary>
    /// <param name="level"> Nivel a crear </param>
    public void BuildLevel(int level)
    {
        //Variable que determina si existe el conjunto de imagenes y textos para ese nivel
        bool exists = false;

        //Ciclo para buscar el conjunto de imagenes y textos para ese nivel
        while (!exists)
        {
            //Se determina si es posible cargar el conjunto de imagenes y textos requeridos
            if (App.generalModel.characteristicsGameModel.file.characteristicsGameList.Contains(number))
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

                //Guardar el numero del conjunto selecionado
                App.generalModel.characteristicsGameModel.ChangeImagesSet(number);
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
        //Verificar si ya jugo un nivel de este juego
        if (App.generalModel.characteristicsGameModel.GetTimesPlayed() == 0)
        {
            countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
            PlayerPrefs.SetInt("PlayOneLevel", countPlay);

            Debug.Log("A Jugado: " + countPlay);
            App.generalModel.characteristicsGameModel.UpdateTimesPlayed(App.generalModel.characteristicsGameModel.GetTimesPlayed() + 1);
        }

        //Actualizar el numero de veces que ha seleccionado una opcion
        App.generalModel.characteristicsGameModel.UpdateNumberAttempts(App.generalModel.characteristicsGameModel.GetNumberAttempts() + 1);

        Debug.Log("INTENTOS HASTA AHORA: "+ App.generalModel.characteristicsGameModel.GetNumberAttempts());

        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (selectedOption == "correct")
        {
            //Si ya paso el nivel 1 puede pasar al 2
            if (App.generalModel.characteristicsGameModel.GetLevel() == 1)
            {
                App.generalModel.characteristicsGameModel.UpdateLevel(2);
                App.generalView.characteristicsGameView.transition.enabled = true;
            }
            //Si ya paso el nivel 2 puede pasar al 3
            else if (App.generalModel.characteristicsGameModel.GetLevel() == 2)
            {
                App.generalModel.characteristicsGameModel.UpdateLevel(3);
                App.generalView.characteristicsGameView.transition.enabled = true;
 
            }
            //Si ya termino los 3 niveles de ese Set, se comienza de nuevo
            else if(App.generalModel.characteristicsGameModel.GetLevel() == 3)
            {
                Debug.Log("Termino los 3");

                //Eliminar el numero del conjunto de imagenes y textos
                App.generalModel.characteristicsGameModel.file.characteristicsGameList.Remove(number);
                //Guardar estado
                App.generalModel.characteristicsGameModel.file.Save("P");

                //Actualizar el nivel a 1 para empezar otra nueva ronda
                App.generalModel.characteristicsGameModel.UpdateLevel(1);

                //Numero de veces (en total) que selecciono una opcion
                counter = App.generalModel.characteristicsGameModel.GetNumberAttempts();
                Debug.Log("LO HIZO EN: " + counter + " INTENTOS");

                //Asignar Estrellas y Puntos obtenidos
                SetPointsAndStars();
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
        if (counter == 3)
        {
            points = App.generalModel.characteristicsGameModel.GetPoints() + 30;
            stars = App.generalModel.characteristicsGameModel.GetTotalStars() + 3;
            canvasStars = 3;
            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.characteristicsGameModel.UpdatePerfectWins(App.generalModel.characteristicsGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            //App.generalModel.characteristicsGameModel.UpdatePerfectGame(App.generalModel.characteristicsGameModel.GetPerfectGame() + 1);
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter > 3 && counter < 9)
        {
            points = App.generalModel.characteristicsGameModel.GetPoints() + 20;
            stars = App.generalModel.characteristicsGameModel.GetTotalStars() + 2;
            canvasStars = 2;

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.characteristicsGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.characteristicsGameModel.GetPoints() + 10;
            stars = App.generalModel.characteristicsGameModel.GetTotalStars() + 1;
            canvasStars = 1;

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.characteristicsGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.characteristicsGameModel.UpdatePoints(points);
        App.generalModel.characteristicsGameModel.UpdateTotalStars(stars);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars);

        //Actualizar el numero de veces que ha seleccionado una opcion a cero
        App.generalModel.characteristicsGameModel.UpdateNumberAttempts(0);
    }
    /// <summary>
    /// Metodo para contar y verificar los errores
    /// </summary>
    void CheckAttempt()
    {
        //Dismuir la cantidad de intentos
        attempts--;

        //Si se queda sin intentos pierde el juego y se le muestra la respuesta (POR AHORA)
        if (attempts == 0)
        {
            App.generalView.gameOptionsView.correctAnswer.sprite = correctAnswer;
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        //Si aun le quedan intentos se muestra un mensaje en pantalla
        else
        {
            App.generalView.gameOptionsView.ShowMistakeCanvas(attempts);
        }
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
    /// <summary>
    /// Metodo para recargar la escena y mostrar el siguiente nivel
    /// </summary>
    public void GoNextlevel()
    {
        //Mostrar una ventana de transicion hacia el siguiente nivel
        App.generalView.characteristicsGameView.transition.enabled = false;

        //Recargar la escena
        SceneManager.LoadScene("CharacteristicsGameScene");
    }
    /// <summary>
    /// Metodo resta los intentos de un nivel
    /// </summary>
    public void RestartAttempts()
    {
        App.generalModel.characteristicsGameModel.UpdateNumberAttempts(App.generalModel.characteristicsGameModel.GetNumberAttempts() - 3);
        //SceneManager.LoadScene("CharacteristicsGameScene");
        //Reestablecer la cantidad de intentos
        attempts = 3;
    }
}
