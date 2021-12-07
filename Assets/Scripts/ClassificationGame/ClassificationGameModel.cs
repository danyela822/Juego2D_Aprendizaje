using System.Collections.Generic;
using UnityEngine;
public class ClassificationGameModel : Reference
{
    static List<Sprite[]> allImages;
    static List<string> texts;
    static List<string[]> allAnswers;

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
                file.classificationGameList.Add("set_"+(j +1));
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
    public List<Sprite[]> LoadImages()
    {
        allImages = new List<Sprite[]>();

        int numero = 1;

        while (numero <= 10)
        {
            if (file.classificationGameList.Contains("set_"+numero))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Classification/set_" + (numero));
               
                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            numero++;
        }

        file.Save("P");
        return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public List<string> LoadTexts()
    {
        texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_sets") as TextAsset;

        string text = textAsset.text;

        int numero = 0;

        while (numero < 10)
        {
            if (file.classificationGameList.Contains("set_"+(numero +1)))
            {
                texts.Add(text.Split('\n')[numero]);
            }
            numero++;
        }
        return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public List<string[]> LoadAnswers()
    {
        allAnswers = new List<string[]>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        TextAsset textAsset = Resources.Load("Files/correct_sets") as TextAsset;

        string answers = textAsset.text;
        string[] values;
        string[] values2;

        int numero = 0;

        while (numero < 10)
        {
            values = answers.Split('.');
            values2 = values[numero].Split(',');

            for (int j = 0; j < values2.Length; j++)
            {
                values2[j].TrimEnd('.');
            }
            if (file.classificationGameList.Contains("set_" + (numero + 1)))
            {
                allAnswers.Add(values2);
            }
            numero++;
        }

        return allAnswers;
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
