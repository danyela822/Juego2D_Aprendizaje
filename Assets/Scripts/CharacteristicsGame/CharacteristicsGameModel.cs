using UnityEngine;

public class CharacteristicsGameModel : Reference
{
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
        return Resources.LoadAll<Sprite>("Characteristics/Level_" + level + "/set_" + number); ;
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
        return text.Split('\n')[number]; ;
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetStars()
    {
        return PlayerPrefs.GetInt("StarsGame2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame2", stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame2", value);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 300)
        {
            if (PlayerPrefs.GetInt("AchievementUnlocked_2", 0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("AchievementUnlocked_2", 1);

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
        return PlayerPrefs.GetInt("TimesPlayedGame2", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame2", value);

        if (value == 1)
        {
            PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);
            //print("ENTRO A SUMAR AL LOGRO DE JUGAR UN NIVEL");
        }
        
        // Verificar si consiguio el logro 1: Juega un nivel de cada juego
        if (PlayerPrefs.GetInt("PlayOneLevel", 0) == 8)
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
        return PlayerPrefs.GetInt("GetThreeStars2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars2", value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars2", 0);

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
        return PlayerPrefs.GetInt("PerfectGame2", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame2", value);

        Debug.Log("LLEVA: "+GetPerfectGame()+" PERFECTOS");

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
}
