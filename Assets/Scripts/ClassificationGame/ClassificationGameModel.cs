using UnityEngine;
public class ClassificationGameModel : Reference
{
    static Sprite[] images;
    static string statement;
    static string[] answers;

    public int totalStars;
    public int totalPoints;
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
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public Sprite[] LoadImages(int numero)
    {
        //Cargar y guardar un set de imagenes en un array
        images = Resources.LoadAll<Sprite>("Classification/set_" + (numero));

        //file.Save("P");
        return images;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public string LoadTexts(int numero)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_sets") as TextAsset;

        string text = textAsset.text;
        
        statement = text.Split('\n')[numero];

        return statement;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public string[] LoadAnswers(int numero)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        TextAsset textAsset = Resources.Load("Files/correct_sets") as TextAsset;

        string allAnswers = textAsset.text;
        string[] values;
        string[] values2;


        values = allAnswers.Split('.');
        values2 = values[numero].Split(',');

        for (int j = 0; j < values2.Length; j++)
        {
            values2[j].TrimEnd('.');
        }
        answers = values2;


        return answers;
    }
    public Sprite LoadAnswerImages(int num)
    {
        Sprite correctAnswers = null;

        Debug.Log("CARGAR RESPUESTA DE SET: " + num);
        if (file.classificationGameList.Contains(num))
        {
            Debug.Log("ENTRO A CARGAR RESPUESTA CORRECTA");
            correctAnswers = Resources.Load<Sprite>("Classification/Correct_sets/correct_set_" + num);
        }
        return correctAnswers;
    }
    /*
    * 
    */
    public int GetTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStarsGame1", 0);
        //Debug.Log("STARS HASTA AHORA: " + totalStars);
        return totalStars;
    }
    /*
     * 
     */
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame1", stars);
        //Debug.Log("STARS EN SET: " + stars);
        App.generalModel.statsModel.totalStars += GetTotalStars();
    }
    /*
     * 
     */
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame1", 0);
        //Debug.Log("PUNTOS HASTA AHORA CLASSIFICATION: "+totalPoints);
        /*if(totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints1",0) == 0)
        {
            PlayerPrefs.SetInt("GamePoints1", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }*/
        if (totalPoints >= 100 && (App.generalModel.statsModel.totalPoints / 100 == 3))
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            //PlayerPrefs.SetInt("GamePoints2", 1);
            if (!App.generalController.statsController.IsAchievements(8))
            {
                App.generalController.statsController.DeleteAchievements(8);
            }

            //PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }
        return totalPoints;
    }
    /*
     * 
     */
    public void UpdatePoints(int value)
    {
        PlayerPrefs.SetInt("TotalPointsGame1", PlayerPrefs.GetInt("TotalPointsGame1", 0) + value);
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame1", 0);
        Debug.Log("LLEVA ESTOS PUNTOS: " + totalPoints);
        App.generalModel.statsModel.UpdateTotalPoints(App.generalModel.statsModel.GetTotalPoints() + totalPoints);

        //Verificar si ya cumplio con el logro de sumar mas de 300 puntos en este juego
        if (totalPoints >= 100 && PlayerPrefs.GetInt("GamePoints1", 0) == 0)
        {
            //Una vez cumplido el logro se cambia el estado de este para no volver a contarlo
            PlayerPrefs.SetInt("GamePoints1", 1);
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
    /// <summary>
    /// Metodo que devuelve la cantidad de veces que ha jugado
    /// </summary>
    /// <returns></returns>
    public int GetTimesPlayed()
    {
        return PlayerPrefs.GetInt("TimesPlayedGame1", 0);
    }
    public void UpdateTimesPlayed(int value)
    {
        // countPlay = PlayerPrefs.GetInt("PlayOneLevel", 0) + 1;
        PlayerPrefs.SetInt("PlayOneLevel", PlayerPrefs.GetInt("PlayOneLevel", 0) + 1);

        //Debug.Log("A Jugado: " + countPlay);
        PlayerPrefs.SetInt("TimesPlayedGame1", value);

        int logro1 = PlayerPrefs.GetInt("PlayOneLevel", 0);

        if (logro1 == 3)
        {
            if (!App.generalController.statsController.IsAchievements(0))
            {
                App.generalController.statsController.DeleteAchievements(0);
            }
        }
    }
    /// <summary>
    /// Metodo que actualiza la cantidad de veces que ha ganado 3 estrellas
    /// </summary>
    /// <param name="value">Cantidad de veces que ha ganado 3 estrellas</param>
    public void UpdatePerfectWins(int value)
    {
        PlayerPrefs.SetInt("GetThreeStars1", PlayerPrefs.GetInt("GetThreeStars1", 0) + value);
        countPerfectWins = PlayerPrefs.GetInt("GetThreeStars1", 0);
        Debug.Log("LLEVA: " + countPerfectWins + " PERFECTAS");

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
}
