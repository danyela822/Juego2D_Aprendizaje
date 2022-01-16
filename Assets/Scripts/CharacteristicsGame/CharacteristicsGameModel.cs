using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameModel : Reference
{
    public int totalStars;
    public int totalPoints;
    public int countPerfectWins;
    public FileLists file;

    /// <summary>
    /// Metodo para verificar si un elemento de un nivel en especifico existe
    /// </summary>
    /// <param name="number">Numero del elemento de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>Bool que indica si exsite o no ese elemento</returns>
    public bool FileExist(int number, int level)
    {
        switch (level)
        {
            case 1:
                //Verificar si el elemento del nivel 1 se encuentra en la lista
                if (file.imageListGame2_1.Contains(number))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                //Verificar si el elemento del nivel 2 se encuentra en la lista
                if (file.imageListGame2_2.Contains(number))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                //Verificar si el elemento del nivel 3 se encuentra en la lista
                if (file.imageListGame2_3.Contains(number))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default: return false;
        }
    }
    /// <summary>
    /// Metodo para cargar y guardar todas la imagnes que necesita el nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>Array de Sprites con las imagenes del nivel indicado</returns>
    public Sprite[] LoadImages(int number, int level)
    {
        //Cargar y guardar un set de imagenes en un array
        Sprite[] images = Resources.LoadAll<Sprite>("Characteristics/Level_" + level + "/set_" + number);

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

        //Seleccionar el enunciado indicado por el numero de representa el conjunto de imagenes
        string statement = text.Split('\n')[number];

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
        //
        totalStars = PlayerPrefs.GetInt("TotalStarsGame2", 0);
        App.generalModel.statsModel.totalStars += GetTotalStars();
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame2", 0);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        /*if (totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints2", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints2", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
            int logro8 = PlayerPrefs.GetInt("ThreeTundredPoints", 0);
            
            if(logro8 == 3)
            {
                if (!App.generalController.statsController.IsAchievements(8))
                {
                    App.generalController.statsController.DeleteAchievements(8);
                }
            }
        }*/
        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        /*if (totalPoints >= 100 && (App.generalModel.statsModel.totalPoints / 100 == 3))
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            //PlayerPrefs.SetInt("GamePoints2", 1);
            if (!App.generalController.statsController.IsAchievements(8))
            {
                App.generalController.statsController.DeleteAchievements(8);
            }
            
            //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }
        if (totalPoints >= 100 && Files.achievements[8].conditionComplete == 3)
        {
           
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            //PlayerPrefs.SetInt("GamePoints2", 1);
            if (!App.generalController.statsController.IsAchievements(8))
            {
                App.generalController.statsController.DeleteAchievements(8);
            }

            //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }*/
        return totalPoints;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("TotalPointsGame2", PlayerPrefs.GetInt("TotalPointsGame2", 0) + value);
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame2", 0);
        Debug.Log("LLEVA ESTOS PUNTOS: " + totalPoints);
        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints()+totalPoints);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (totalPoints >= 100 && PlayerPrefs.GetInt("GamePoints2", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints2", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
            int achievement = PlayerPrefs.GetInt("ThreeTundredPoints", 0);

            if (achievement == 3)
            {
                if (!App.generalController.statsController.IsAchievements(8))
                {
                    App.generalController.statsController.DeleteAchievements(8);
                }
            }
        }

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
        PlayerPrefs.SetInt("GetThreeStars2", PlayerPrefs.GetInt("GetThreeStars2", 0) + value);
        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars2", 0);
        Debug.Log("LLEVA: "+countPerfectWins+" PERFECTAS");

        if (countPerfectWins == 3)
        {
            Debug.Log("SE PROCEDE A CUMPLIR EL LOGRO");
            if (!App.generalController.statsController.IsAchievements(1))
            {
                App.generalController.statsController.DeleteAchievements(1);
            }
        }
        else if (countPerfectWins == 6)
        {
            if (!App.generalController.statsController.IsAchievements(2))
            {
                App.generalController.statsController.DeleteAchievements(2);
            }
        }
    }
    /// <summary>
    /// Metodo que devuelve la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <returns></returns>
    public int GetPerfectGame()
    {
        return PlayerPrefs.GetInt("PerfectGame2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame2", value);
    }
    /// <summary>
    /// Metodo que devuelve el nivel actual del juego
    /// </summary>
    /// <returns>Int del nivel del juego</returns>
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Game2Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game2Levels", level);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad de veces que ha jugado
    /// </summary>
    /// <returns></returns>
    public int GetTimesPlayed()
    {
        return PlayerPrefs.GetInt("TimesPlayedGame2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha jugado
    /// </summary>
    /// <param name="value"></param>
    public void UpdateTimesPlayed(int value)
    {
       // countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
        PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);

        //Debug.Log("A Jugado: " + countPlay);
        PlayerPrefs.SetInt("TimesPlayedGame2", value);

        int achievement = PlayerPrefs.GetInt("PlayOneLevel", 0);

        if (achievement == 3)
        {
            if (!App.generalController.statsController.IsAchievements(0))
            {
                App.generalController.statsController.DeleteAchievements(0);
            }
        }
    }
}
