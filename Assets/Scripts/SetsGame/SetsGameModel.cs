using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsGameModel : Reference
{
    public int countPerfectWins;
    public FileLists file;

    /// <summary>
    /// Metodo para verificar si un elemento de un nivel en especifico existe
    /// </summary>
    /// <param name="number">Numero del elemento de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>Bool que indica si exsite o no ese elemento</returns>
    public bool FileExist(int number, int level,int type)
    {
        if (type == 1)
        {
            switch (level)
            {
                case 1:
                    //Verificar si el elemento del nivel 1 se encuentra en la lista
                    if (file.imageListGame8_1_1.Contains(number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    //Verificar si el elemento del nivel 2 se encuentra en la lista
                    if (file.imageListGame8_1_2.Contains(number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    //Verificar si el elemento del nivel 3 se encuentra en la lista
                    if (file.imageListGame8_1_3.Contains(number))
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
        else
        {
            switch (level)
            {
                case 1:
                    //Verificar si el elemento del nivel 1 se encuentra en la lista
                    if (file.imageListGame8_2_1.Contains(number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    //Verificar si el elemento del nivel 2 se encuentra en la lista
                    if (file.imageListGame8_2_2.Contains(number))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    //Verificar si el elemento del nivel 3 se encuentra en la lista
                    if (file.imageListGame8_2_3.Contains(number))
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
    }
    /// <summary>
    /// Metodo para cargar y guardar todas la imagnes que necesita el nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>Array de Sprites con las imagenes del nivel indicado</returns>
    public Sprite[] LoadImages(int number, int level,int type)
    {
        //Cargar y guardar un set de imagenes en un array
        return Resources.LoadAll<Sprite>("SetsGame/"+type+"/Level"+level+"/" + number); ;
    }
    /// <summary>
    /// Metodo para cargar todos los enunciados que deben acompa�ar a cada nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <param name="level">Nivel del juego</param>
    /// <returns>String con el enunciado del nivel indicado</returns>
    public string LoadTexts(int number, int level)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/Characteristics/Texts/characteristics_level_" + level) as TextAsset;

        //Capturar el texto del archivo
        string text = textAsset.text;

        //Seleccionar el enunciado indicado por el numero de representa el conjunto de imagenes
        return text.Split('\n')[number]; ;
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetStars()
    {
        return PlayerPrefs.GetInt("StarsGame8", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame8", stars);

        App.generalModel.statsModel.UpdateTotalStars(stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame8", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame8", value);

        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints() + GetPoints());

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 30)
        {
            if (PlayerPrefs.GetInt("hola1", 0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("hola1", 1);

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
        return PlayerPrefs.GetInt("TimesPlayedGame8", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame8", value);

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
        return PlayerPrefs.GetInt("GetThreeStars8", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars8", PlayerPrefs.GetInt("GetThreeStars8", 0) + value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars2", 0);

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
        return PlayerPrefs.GetInt("PerfectGame8", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame8", value);

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
        return PlayerPrefs.GetInt("Game8Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game8Levels", level);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetTypeOfSet()
    {
        return PlayerPrefs.GetInt("Type", 1);
    }
    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    public void UpdateTypeOfSet(int type)
    {
        PlayerPrefs.SetInt("Type", type);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetNumber()
    {
        return PlayerPrefs.GetInt("Number", 1);
    }
    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    public void UpdateNumber(int number)
    {
        PlayerPrefs.SetInt("Number", number);
    }
}
