using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquialityGameModel : Reference
{
    public int countPerfectWins;

    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetStars()
    {
        return PlayerPrefs.GetInt("StarsGame7", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame7", stars);

        App.generalModel.statsModel.UpdateTotalStars(stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame7", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame7", value);

        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints() + GetPoints());

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 30)
        {
            if (PlayerPrefs.GetInt("hola2", 0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("hola2", 1);

            }

            if (!App.generalController.statsController.IsAchievements(8))
            {
                //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);

                Debug.Log("8 VAAAAAAAAAAA ENNNNNNNNNNNN: " + PlayerPrefs.GetInt("ThreeTundredPoints", 0));

                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 2)
                {
                    App.generalController.statsController.DeleteAchievements(8);
                }

            }
            else if (!App.generalController.statsController.IsAchievements(9))
            {
                //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);

                Debug.Log("9 VAAAAAAAAAAA ENNNNNNNNNNNN: " + PlayerPrefs.GetInt("ThreeTundredPoints", 0));

                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 4)
                {
                    App.generalController.statsController.DeleteAchievements(9);
                }
            }
        }
    }

    /// <summary>
     /// Metodo que devuelve la cantidad de veces que ha jugado
     /// </summary>
     /// <returns>Int de la cantidad de veces que ha jugado este juego</returns>
    public int GetTimesPlayed()
    {
        return PlayerPrefs.GetInt("TimesPlayedGame7", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame7", value);

        PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);

        int achievement_1 = PlayerPrefs.GetInt("PlayOneLevel", 0);

        if (achievement_1 == 3)
        {
            if (!App.generalController.statsController.IsAchievements(0))
            {
                App.generalController.statsController.DeleteAchievements(0);
            }
        }
    }
    /// <summary>
    /// Metodo que devuelve la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <returns>Int de la cantidad de veces que ha obtenido 3 estrellas</returns>
    public int GetPerfectWins()
    {
        return PlayerPrefs.GetInt("GetThreeStars7", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars7", PlayerPrefs.GetInt("GetThreeStars7", 0) + value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars7", 0);

        //Debug.Log("LLEVA: "+countPerfectWins+" PERFECTAS");

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
        return PlayerPrefs.GetInt("PerfectGame7", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame7", value);

        Debug.Log("LLEVA: " + GetPerfectGame() + " PERFECTOS");

        if (GetPerfectGame() == 3)
        {
            if (!App.generalController.statsController.IsAchievements(6))
            {
                App.generalController.statsController.DeleteAchievements(6);
            }
        }
        if (GetPerfectGame() == 10)
        {
            if (!App.generalController.statsController.IsAchievements(7))
            {
                App.generalController.statsController.DeleteAchievements(7);
            }
        }
    }
    /// <summary>
    /// Metodo que devuelve el nivel actual del juego
    /// </summary>
    /// <returns>Int del nivel del juego</returns>
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Game7Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game7Levels", level);
    }
}
