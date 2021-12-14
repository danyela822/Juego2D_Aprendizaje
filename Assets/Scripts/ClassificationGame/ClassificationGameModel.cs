using System.Collections.Generic;
using UnityEngine;
public class ClassificationGameModel : Reference
{
    static Sprite[] images;
    static string statement;
    static string[] answers;

    int totalStars;
    int totalPoints;
    int createList;
    public FileLists file;

    private void Start()
    {
        file.Load("P");
        Debug.Log("TAMAÑO LISTA CLASIFICACION: " + file.classificationGameList.Count);
        //Debug.Log("TAMAÑO LISTA SPRITES ANTES: "+q.listaSprites.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 10; j++)
            {
                file.classificationGameList.Add(j);
               // Debug.Log("TAMAÑO: " + q.classificationGameList.Count);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA CLASSIFICATION: " + GetList());
            App.generalView.gamesMenuView.playButtons[0].enabled = true;
            file.Save("P");
        }
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public Sprite[] LoadImages(int numero)
    {
        if (file.classificationGameList.Contains(numero))
        {
            //Cargar y guardar un set de imagenes en un array
            images = Resources.LoadAll<Sprite>("Classification/set_" + (numero));
        }
        file.Save("P");
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
        if (file.classificationGameList.Contains(numero))
        {
            statement = text.Split('\n')[numero];
        }
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


        if (file.classificationGameList.Contains(numero))
        {
            values = allAnswers.Split('.');
            values2 = values[numero].Split(',');

            for (int j = 0; j < values2.Length; j++)
            {
                values2[j].TrimEnd('.');
            }
            answers = values2;
        }

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
    public void SetTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame1", stars);
        //Debug.Log("STARS EN SET: " + stars);
    }
    /*
     * 
     */
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame1", 0);
        //Debug.Log("PUNTOS HASTA AHORA CLASSIFICATION: "+totalPoints);
        if(totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints1",0) == 0)
        {
            PlayerPrefs.SetInt("GamePoints1", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }
        return totalPoints;
    }
    /*
     * 
     */
    public void SetPoints(int valor)
    {
        PlayerPrefs.SetInt("TotalPointsGame1", valor);
        //Debug.Log("PUNTOS EN SET: " + valor);
    }
    /*
    * 
    */
    public int GetList()
    {
        createList = PlayerPrefs.GetInt("ClassificationList", 0);
        return createList;
    }
    /*
     * 
     */
    public void SetList(int value)
    {
        PlayerPrefs.SetInt("ClassificationList", value);
    }
}
