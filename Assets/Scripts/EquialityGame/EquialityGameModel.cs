using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquialityGameModel : Reference
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
        totalStars = PlayerPrefs.GetInt("TotalStarsGame7", 0);
        return totalStars;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame7", stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame7", 0);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        /*if (totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints2", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints2", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }*/
        return totalPoints;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("TotalPointsGame7", value);
    }
    public int GetLevel()
    {
        level = PlayerPrefs.GetInt("Game7Levels", 1);
        return level;
    }
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game7Levels", level);
    }
}
