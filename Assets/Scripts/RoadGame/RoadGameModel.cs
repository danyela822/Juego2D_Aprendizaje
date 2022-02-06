using UnityEngine;
public class RoadGameModel : Reference
{
    /*float totalTime;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "MiniGamesScene")
        {
            totalTime += GetTime();
            totalTime += Time.deltaTime;
            SetTime(totalTime);
        }
    }

    public float GetTime()
    {
        totalTime = PlayerPrefs.GetFloat("TotalTime", 0);
        return totalTime;
    }

    public void SetTime(float time)
    {
        PlayerPrefs.SetFloat("TotalTime", time);
    }*/

    /// <summary>
    /// Metodo que carga los sprites del piso del mapa
    /// </summary>
    /// <param name="theme">Tema del nivel a cargar</param>
    /// <returns>Sprites del tema solicitado</returns>
    public Sprite[,] GetMapFloor(string theme)
    {
        Sprite[] spriteslist;
        Sprite[,] mapFloor = new Sprite[8, 6];
        
        int index = 0;

        if (theme == "Castle")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Castle/dirt");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Forest/grass");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Sea/sand");
        }

        for (int i = 0; i < mapFloor.GetLength(0); i++)
        {

            for (int j = 0; j < mapFloor.GetLength(1); j++)
            {
                mapFloor[i, j] = spriteslist[index];
                index++;
            }
        }
        return mapFloor;
    }
    /// <summary>
    /// Metodo que carga los sprites de bloqueo del mapa
    /// </summary>
    /// <param name="theme">Tema del nivel a cargar</param>
    /// <returns>Sprites del tema solicitado</returns>
    public Sprite[,] GetMapLock(string theme)
    {
        Sprite[] spriteslist;
        Sprite[,] mapLock = new Sprite[8, 6];

        int index = 0;

        if (theme == "Castle")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Castle/fence");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Forest/rocks");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Sea/water");
        }

        for (int i = 0; i < mapLock.GetLength(0); i++)
        {

            for (int j = 0; j < mapLock.GetLength(1); j++)
            {
                mapLock[i, j] = spriteslist[index];
                index++;
            }
        }
        return mapLock;
    }
    
    public int countPerfectWins;

    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetTotalStars()
    {
        return PlayerPrefs.GetInt("StarsGame5", 0); ;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame5", stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame5", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame5", value);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 300)
        {
            if (PlayerPrefs.GetInt("AchievementUnlocked_4", 0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("AchievementUnlocked_4", 1);

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
        return PlayerPrefs.GetInt("TimesPlayedGame5", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame5", value);

        if (value == 1)
        {
            PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);
            print("ENTRO A SUMAR AL LOGRO DE JUGAR UN NIVEL");
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
        return PlayerPrefs.GetInt("GetThreeStars5", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars5", value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars5", 0);

        //Debug.Log("LLEVA: " + countPerfectWins + " PERFECTAS");

        //Verificar si cumplio el logro 2: Obtén 3 estrellas en 3 niveles de un juego
        if (countPerfectWins == 3)
        {
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
        return PlayerPrefs.GetInt("PerfectGame5", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame5", value);

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
        return PlayerPrefs.GetInt("Game5Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game5Levels", level);
    }
}

