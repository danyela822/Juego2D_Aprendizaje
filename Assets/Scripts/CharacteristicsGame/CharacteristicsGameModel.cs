using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameModel : Reference
{
    int totalStars;
    int totalPoints;
    public FileLists file;

    int level;
    int imagesSet;

    /*private void Start()
    {
        file.Load("P");
        //Debug.Log("TAMAÑO LISTA Characteristics: " + file.characteristicsGameList.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 6; j++)
            {
                file.characteristicsGameList.Add(j);
            }
            listaDeGuardado = file.characteristicsGameList;
            Debug.Log("LISTA DE GUARDADO " + listaDeGuardado.Count);
            SetList(1);
            //Debug.Log("CAMBIO DE LISTA Characteristics: " + GetList());
            App.generalView.gamesMenuView.playButtons[1].enabled = true;
            file.Save("P");
        }
    }*/

    /// <summary>
    /// Metodo para cargar y guardar todas la imagnes que necesita el nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>Array de Sprites con las imagenes del nivel indicado</returns>
    public Sprite[] LoadImages(int number,int level)
    {
        //Crear array de imagenes
        Sprite[] images = new Sprite[4];

        //Verificar que el set de imagenes solicitado esta disponible
        if (file.characteristicsGameList.Contains(number))
        {
            //Cargar y guardar un set de imagenes en un array
            images = Resources.LoadAll<Sprite>("Characteristics/Level_"+level+"/set_" + number);
        }
        return images;
    }
    /// <summary>
    /// Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>String con el enunciado del nivel indicado</returns>
    public string LoadTexts(int number,int level)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/Characteristics/Texts/characteristics_level_"+level) as TextAsset;

        //Capturar el texto del archivo
        string text = textAsset.text;
        
        //Declarar el string que contendra el enunciado
        string statement = "";

        //Verificar que el enunciado solicitado esta disponible
        if (file.characteristicsGameList.Contains(number))
        {
            //Seleccionar el enunciado indicado por el numero de representa el conjunto de imagenes
            statement = text.Split('\n')[number];
        }
        return statement;
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStarsGame2", 0);
        return totalStars;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame2", stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame2", 0);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints2", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints2", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }
        return totalPoints;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("TotalPointsGame2", value);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <returns>Int de la cantidad de veces que ha obtenido 3 estrellas</returns>
    public int GetPerfectWins()
    {
        return PlayerPrefs.GetInt("GetThreeStars2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars2", value);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetPerfectGame()
    {
        Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        return PlayerPrefs.GetInt("PerfectGame2", 0);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame2", value);
    }
    public int GetLevel()
    {
        level = PlayerPrefs.GetInt("Game2Levels", 0);
        return level;
    }
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game2Levels", level);
    }
    public int GetImagesSet()
    {
        imagesSet = PlayerPrefs.GetInt("SetOfImages", 0);
        return imagesSet;
    }
    public void ChangeImagesSet(int number)
    {
        PlayerPrefs.SetInt("SetOfImages", number);
    }
    public void UpdateNumberAttempts(int number)
    {
        PlayerPrefs.SetInt("AttemptsGame2", number);
    }
    public int GetNumberAttempts()
    {
        return PlayerPrefs.GetInt("AttemptsGame2", 0);
    }
    public int GetTimesPlayed()
    {
        return PlayerPrefs.GetInt("TimesPlayedGame2", 0);
    }
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame2", value);
    }
}
