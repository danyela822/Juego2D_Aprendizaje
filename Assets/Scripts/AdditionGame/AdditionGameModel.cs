using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionGameModel : Reference
{
    public int countPerfectWins;

    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetStars()
    {
        return PlayerPrefs.GetInt("StarsGame6", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame6", stars);

        App.generalModel.statsModel.UpdateTotalStars(stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame6", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame6", value);

        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints() + GetPoints());

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 300)
        {
            if (PlayerPrefs.GetInt("AchievementUnlocked_5", 0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("AchievementUnlocked_5", 1);

            }

            if (!App.generalController.statsController.IsAchievements(8))
            {
                //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);

                Debug.Log("8 VAAAAAAAAAAA ENNNNNNNNNNNN: " + PlayerPrefs.GetInt("ThreeTundredPoints", 0));

                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 3)
                {
                    App.generalController.statsController.DeleteAchievements(8);
                }

            }
            else if (!App.generalController.statsController.IsAchievements(9))
            {
                //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);

                Debug.Log("9 VAAAAAAAAAAA ENNNNNNNNNNNN: " + PlayerPrefs.GetInt("ThreeTundredPoints", 0));

                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 6)
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
        return PlayerPrefs.GetInt("TimesPlayedGame6", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame6", value);

        if (value == 1)
        {
            PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);
            print("ENTRO A SUMAR AL LOGRO");
        }

        // Verificar si consiguio el logro 1: Juega un nivel de cada juego
        if (PlayerPrefs.GetInt("PlayOneLevel", 0) == 7)
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
        return PlayerPrefs.GetInt("GetThreeStars6", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars6", value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars6", 0);

        //Debug.Log("LLEVA: "+countPerfectWins+" PERFECTAS");

        //Verificar si cumplio el logro 2: Obtén 3 estrellas en 3 niveles de un juego
        if (countPerfectWins == 3)
        {
            Debug.Log("SE PROCEDE A CUMPLIR EL LOGRO");
            if (!App.generalController.statsController.IsAchievements(1))
            {
                App.generalController.statsController.DeleteAchievements(1);
            }
        }
        //Verificar si cumplio el logro 3: Obtén 3 estrellas en 6 niveles de un juego
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
        return PlayerPrefs.GetInt("PerfectGame6", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame6", value);

        Debug.Log("LLEVA: " + GetPerfectGame() + " PERFECTOS");

        //Verificar si cumplio el logro 7: Completa 3 niveles seguidos sin errores
        if (GetPerfectGame() == 3)
        {
            if (!App.generalController.statsController.IsAchievements(6))
            {
                App.generalController.statsController.DeleteAchievements(6);
            }
        }
        //Verificar si cumplio el logro 8:  Completa 10 niveles seguidos sin errores
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
        return PlayerPrefs.GetInt("Game6Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game6Levels", level);
    }
}
