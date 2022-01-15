using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionGameModel : Reference
{
    int totalStars;
    int totalPoints;
    int level;

    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStarsGame6", 0);
        return totalStars;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame6", stars);
        App.generalModel.statsModel.totalStars += GetTotalStars();
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame6", 0);

        return totalPoints;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("TotalPointsGame6", PlayerPrefs.GetInt("TotalPointsGame6", 0) + value);
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame6", 0);
        Debug.Log("LLEVA ESTOS PUNTOS: " + totalPoints);
        //App.generalModel.statsModel.totalPoints += GetPoints();

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (totalPoints >= 100 && PlayerPrefs.GetInt("GamePoints6", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints6", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
            int logro8 = PlayerPrefs.GetInt("ThreeTundredPoints", 0);

            if (logro8 == 3)
            {
                if (!App.generalController.statsController.IsAchievements(8))
                {
                    App.generalController.statsController.DeleteAchievements(8);
                }
            }
        }
    }
    public int GetLevel()
    {
        level = PlayerPrefs.GetInt("Game6Levels", 1);
        return level;
    }
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game6Levels", level);
    }
}
