using UnityEngine;
public class ClassificationGameModel : Reference
{
    public int countPerfectWins;
    public FileLists file;

    /// <summary>
    /// Metodo para verificar si un elemento en especifico existe
    /// </summary>
    /// <param name="number">Numero del elemento de imagenes</param>
    /// <returns>Bool que indica si exsite o no ese elemento</returns>
    public bool FileExist(int number)
    {
        if (file.classificationGameList.Contains(number))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Metodo para cargar todas la imagnes que necesita el juego
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <returns>Array de Sprites con las imagenes de conjunto indicado</returns>
    public Sprite[] LoadImages(int number)
    {
        //Cargar y guardar un set de imagenes en un array
        return Resources.LoadAll<Sprite>("Classification/set_" + (number));
    }
    /// <summary>
    /// Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <returns>String con el enunciado del conjunto indicado</returns>
    public string LoadTexts(int number)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_sets") as TextAsset;

        string text = textAsset.text;
        
        //Seleccionar el enunciado
        return text.Split('\n')[number]; ;
    }
    /// <summary>
    /// Metodo para cargar las respuestas de cada nivel
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <returns>Array de strings con las respuestas del juego</returns>
    public string[] LoadAnswers(int number)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        TextAsset textAsset = Resources.Load("Files/correct_sets") as TextAsset;

        string allAnswers = textAsset.text;
        string[] values;
        string[] answers;

        values = allAnswers.Split('.');
        answers = values[number].Split(',');

        for (int j = 0; j < answers.Length; j++)
        {
            answers[j].TrimEnd('.');
        }

        return answers;
    }
    /// <summary>
    /// Metodo para cargar la imagen de la respuesta correcta
    /// </summary>
    /// <param name="number">Numero del conjunto de imagenes</param>
    /// <returns>Spritr de la respuesta correcta</returns>
    public Sprite LoadAnswerImages(int number)
    {
        Debug.Log("CARGAR RESPUESTA DE SET: " + number);

        return Resources.Load<Sprite>("Classification/Correct_sets/correct_set_" + number); ;
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de estrellas que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de estrellas </returns>
    public int GetTotalStars()
    {
        return PlayerPrefs.GetInt("StarsGame1", 0); ;
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de estrellas que han obtenido en este juego
    /// </summary>
    /// <param name="stars">Cantidad de estrellas para agregar</param>
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("StarsGame1", stars);

        //Actualizar la cantidad total de estrellas
        App.generalModel.statsModel.UpdateTotalStars(stars);
    }
    /// <summary>
    /// Metodo que devuelve la cantidad total de puntos que se han obtenido en este juego
    /// </summary>
    /// <returns> Int de la cantidad de puntos </returns>
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("PointsGame1", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de puntos que han obtenido en este juego
    /// </summary>
    /// <param name="value">Cantidad de puntos a sumar</param>
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("PointsGame1", value);
        
        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints() + GetPoints());

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (GetPoints() >= 30)
        {
            if(PlayerPrefs.GetInt("hola",0) == 0)
            {
                PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
                PlayerPrefs.SetInt("hola", 1);

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
        return PlayerPrefs.GetInt("TimesPlayedGame1", 0);
    }
    /// <summary>
    /// Metodo que actuliza la cantidad de veces que ha jugado este juego
    /// </summary>
    /// <param name="value">valor a incrementar</param>
    public void UpdateTimesPlayed(int value)
    {
        PlayerPrefs.SetInt("TimesPlayedGame1", value);

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
        return PlayerPrefs.GetInt("GetThreeStars1", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars1", PlayerPrefs.GetInt("GetThreeStars1", 0) + value);

        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars1", 0);

        //Debug.Log("LLEVA: " + countPerfectWins + " PERFECTAS");

        if (countPerfectWins == 3)
        {
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
        return PlayerPrefs.GetInt("PerfectGame1", 0);
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado un juego de forma perfecta
    /// </summary>
    /// <param name="value"></param>
    public void UpdatePerfectGame(int value)
    {
        PlayerPrefs.SetInt("PerfectGame1", value);

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
}
