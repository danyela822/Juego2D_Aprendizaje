using System.Collections.Generic;
using UnityEngine;
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
    int countPerfectWins = 0;

    //Numero de veces que ha jugado
    int countPlay = 0;

    //numero que cuenta las veces que ha completado niveles sin errores
    int countPerfectGame = 0;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    //Imagen de la respuesta correcta
    Sprite correctAnswer;

    private void Start()
    {
        //Numero random para seleccionar un conjunto de imagenes
        number = Random.Range(0, 6);

        bool centinela = true;
        while (centinela)
        {
            if (App.generalModel.characteristicsGameModel.file.characteristicsGameList.Contains(number))
            {
                //Instaciar Lista que guardara todas las imagenes
                images = App.generalModel.characteristicsGameModel.LoadImages(number);

                //Instaciar Lista que guardara todos los enunciados
                statement = App.generalModel.characteristicsGameModel.LoadTexts(number);

                centinela = false;
            }
            else
            {
                number = Random.Range(0, 6);
            }
        }
        Debug.Log("ELIMINAR: " + number);
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
    /*
    * Metodo para ubicar el texto en la pantalla del jugador
    */
    public void PutText()
    {
        //Asignar el enunciado seleccionado al texto de la vista
        App.generalView.characteristicsGameView.statement.text = statement;
    }
    /*
    * Metodo para verificar si la respuesta final de jugador es correcta o incorrecta
    */
    public void CheckAnswer(string selectedOption)
    {
        if (PlayerPrefs.GetInt("PlayGame2", 0) == 0)
        {
            countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
            PlayerPrefs.SetInt("PlayOneLevel", countPlay);
            Debug.Log("A Jugado: " + countPlay);
            PlayerPrefs.SetInt("PlayGame2", 1);
        }
        counter++;
        //Si la respuesta del jugador a la respuesta que corresponde al enunciado en pantalla, el jugador gana el juego
        if (selectedOption == "correct")
        {
            App.generalModel.characteristicsGameModel.file.characteristicsGameList.Remove(number);
            App.generalModel.characteristicsGameModel.file.Save("P");

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

                App.generalView.gameOptionsView.ShowWinCanvas(3);
            }
            else if(counter == 2)
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 20);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 2);
                countPerfectGame = 0;
                PlayerPrefs.SetInt("PerfectGame2", countPerfectGame);
                App.generalView.gameOptionsView.ShowWinCanvas(2);
            }
            else 
            {
                App.generalModel.characteristicsGameModel.SetPoints(App.generalModel.characteristicsGameModel.GetPoints() + 10);
                App.generalModel.characteristicsGameModel.SetTotalStars(App.generalModel.characteristicsGameModel.GetTotalStars() + 1);
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
    void CheckAttempt()
    {
        attempts--;
        if (attempts == 0)
        {
            App.generalView.gameOptionsView.correctAnswer.sprite = correctAnswer;
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
