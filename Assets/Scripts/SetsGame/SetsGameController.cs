using System.Collections.Generic;
using UnityEngine;

public class SetsGameController : Reference
{
    static Sprite[] images;

    static List<Sprite> panelImages;
    
    static List<Sprite> buttonImages;

    Sprite correctAnswer;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    //
    int level;

    //
    int type = 1;

    //
    bool isLastLevel;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Numero para acceder a un conjunto de imagenes en especifico
    int number;

    //Numero de veces que ha jugado
    int countPlay;

    private void Start()
    {
        BuildLevel();
    }

    /// <summary>
    /// Busca e instancia la lista de imagenes necesarias para crear el nivel
    /// </summary>
    public void BuildLevel()
    {
        panelImages = new List<Sprite>();
        buttonImages = new List<Sprite>();
        
        //Obtener el nivel actual
        level = App.generalModel.setsGameModel.GetLevel();

        type = App.generalModel.setsGameModel.GetTypeOfSet();

        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(1, 4);

        //Variable que determina si existe el conjunto de imagenes para ese nivel
        bool exists = false;

        //Ciclo para buscar el conjunto de imagenes para ese nivel
        while (!exists)
        {
            //Se determina si es posible cargar el conjunto de imagenes requeridos
            if (App.generalModel.setsGameModel.FileExist(number, level, type))
            {
                //Instaciar Lista que guardara todas las imagenes
                images = App.generalModel.setsGameModel.LoadImages(number, level, type);
                Debug.Log("TAMAÑO IMAGENES: "+images.Length);

                //Cuando se instancian la lista se acaba el ciclo
                exists = true;
            }
            //Si no es posible se busca otro conjunto
            else
            {
                //Numero random para seleccionar un conjunto de imagenes
                number = Random.Range(1, 4);
            }
        }
        ActivatePanels();
        Debug.Log("NIVEL: " + level + " SET: " + number + "TIPO: "+ type);

        LoadText1();

        //Ubicar en la pantalla las imagenes seleccionadas
        PutImages1();

        //Ubicar el enunciado en la pantalla
        //PutText();
    }

    public void ActivatePanels()
    {
        // se cambia el tres por un 4 
        if(level == 1 || level == 4)
        {
            for (int i = 0; i < 2; i++)
            {
                App.generalView.setsGameView.panels[i].enabled = true;
            }
        }
        else
        { 
            for (int i = 2; i < 5; i++)
            {
                App.generalView.setsGameView.panels[i].enabled = true;
            }

        }
    }

    public void DesativatePanels()
    {
        for (int i = 0; i < 5; i++)
        {
            App.generalView.setsGameView.panels[i].enabled = false;
        }
        
    }
    public void PutImages1()
    {
        LoadLists1();

        int i = 0;
        int limit = 2;

        //Se cambia el 3 por un 4
        if (level != 1 && level != 4)
        {
            i = 2;
            limit = 5;
        }

        int k = 0;
        for (int m = i; m < limit; m++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.panels[m].sprite = panelImages[k];
            k++;
        }
        List<Sprite> options = ChangeOrderList(buttonImages);

        for (int j = 0; j < options.Count; j++)
        {
            if (options[j].name == "correct")
            {
                correctAnswer = options[j];
            }
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.buttons[j].image.sprite = options[j];
        }

    }
    public void LoadLists1()
    {
        int limit = 2;
        // Se cambia el 3 por un 4
        if (level != 1 && level != 4) limit = 3;

        for (int i = 0; i < images.Length; i++)
        {
            if (i < limit)
            {
                panelImages.Add(images[i]);
            }
            else buttonImages.Add(images[i]);
        }
    }
    public void LoadText1()
    {
        string message = "";
        if (type == 1)
        {
            switch (level)
            {
                case 1:
                    message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta";
                    break;
                case 2:
                    message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta";
                    break;
                case 3:
                    message = "Unión\n\nRealice la unión entre las cartas y elija la respuesta correcta, no se fije en el orden";
                    break;
            }
        }
        else
        {
            switch (level)
            {
                case 1:
                    message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
                    break;
                case 2:
                    message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
                    break;
                case 3:
                    message = "Intersección\n\nRealice la intersección entre las cartas y elija la respuesta correcta, no se fije en el orden";
                    break;
            }
        }

        App.generalView.setsGameView.message.text = message;
    }
    public void CheckAnswer(string answer)
    {
        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.setsGameModel.GetTimesPlayed();

        App.generalModel.setsGameModel.UpdateTimesPlayed(++countPlay);

        if (answer == correctAnswer.name)
        {
            //Si ya paso el nivel 1 puede pasar al 2
            if (level == 1)
            {
                if (type == 1)
                {
                    //Eliminar el numero del conjunto de imagenes de union
                    App.generalModel.setsGameModel.file.imageListGame8_1_1.Remove(number);
                }
                else
                {
                    //Eliminar el numero del conjunto de imagenes de interseccion
                    App.generalModel.setsGameModel.file.imageListGame8_2_1.Remove(number);
                }
                //Actualizar el nivel del juego
                App.generalModel.setsGameModel.UpdateLevel(2);
            }
            //Si ya paso el nivel 2 puede pasar al 3
            else if (level == 2)
            {
                if (type == 1)
                {
                    //Eliminar el numero del conjunto de imagenes de union
                    App.generalModel.setsGameModel.file.imageListGame8_1_2.Remove(number);
                }
                else
                {
                    //Eliminar el numero del conjunto de imagenes de interseccion
                    App.generalModel.setsGameModel.file.imageListGame8_2_2.Remove(number);
                }
                

                //Actualizar el nivel del juego
                App.generalModel.setsGameModel.UpdateLevel(3);

            }
            else
            {
                if(type == 1)
                {
                    //Eliminar el numero del conjunto de imagenes de union
                    App.generalModel.setsGameModel.file.imageListGame8_1_3.Remove(number);

                    //Si ya termino los 3 niveles de union, se comienzan con los niveles de interseccion
                    App.generalModel.setsGameModel.UpdateTypeOfSet(2);
                }
                else
                {
                    //Eliminar el numero del conjunto de imagenes de interseccion
                    App.generalModel.setsGameModel.file.imageListGame8_2_3.Remove(number);

                    //Si ya termino los 3 niveles de interseccion, se comienza nuevamente con los de union en una nueva partida
                    App.generalModel.setsGameModel.UpdateTypeOfSet(1);

                    //Indicar que este es el ultimo nivel
                    isLastLevel = true;
                }

                //Actualizar el nivel a 1 para empezar otra nueva ronda
                App.generalModel.setsGameModel.UpdateLevel(1);
            }

            //Guardar estado
            App.generalModel.setsGameModel.file.Save("P");

            //Asignar Estrellas y Puntos obtenidos
            SetPointsAndStars();
        }
        else{
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

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (counter == 1)
        {
            points = App.generalModel.setsGameModel.GetPoints() + 30;
            stars = App.generalModel.setsGameModel.GetStars() + 3;
            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.setsGameModel.UpdatePerfectWins(App.generalModel.setsGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.setsGameModel.UpdatePerfectGame(App.generalModel.setsGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.setsGameModel.GetPoints() + 20;
            stars = App.generalModel.setsGameModel.GetStars() + 2;
            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.setsGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.setsGameModel.GetPoints() + 10;
            stars = App.generalModel.setsGameModel.GetStars() + 1;
            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.setsGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.setsGameModel.UpdatePoints(points);
        App.generalModel.setsGameModel.UpdateStars(stars);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage,isLastLevel);

    }
    /// <summary>
    /// Metodo para contar y verificar los errores
    /// </summary>
    void CheckAttempt ()
    {
        //Dismuir la cantidad de intentos
        attempts--;

        //Si se queda sin intentos pierde el juego (y se le muestra la respuesta)
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

            App.generalView.setsGameView.solutionPanel.SetActive(true);
            App.generalView.setsGameView.solutionImage.sprite = correctAnswer;
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }
    public List<Sprite> ChangeOrderList(List<Sprite> list)
    {
        List<Sprite> originalList = new List<Sprite>();

        for (int i = 0; i < list.Count; i++)
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
