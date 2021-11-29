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
    public Prueba q;

    private void Start()
    {
        q.Load("P");
        Debug.Log("TAMAÑO LISTA CLASIFICACION: " + q.classificationGameList.Count);
        //Debug.Log("TAMAÑO LISTA SPRITES ANTES: "+q.listaSprites.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 10; j++)
            {
                q.classificationGameList.Add("set_"+(j +1));
               // Debug.Log("TAMAÑO: " + q.classificationGameList.Count);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA CLASSIFICATION: " + GetList());
            App.generalView.gamesMenuView.playButtons[0].enabled = true;
            q.Save("P");
        }
    }
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadImages()
    {
        allImages = new List<Sprite[]>();

        int numero = 1;

        while (numero <= 10)
        {
            if (q.classificationGameList.Contains("set_"+numero))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (numero));

                //sprites.Add(new Tuple<string, Sprite[]>("set_" + numero, spriteslist));
               
                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            numero++;
        }

        q.Save("P");
        //return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public void LoadTexts()
    {
        texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel

        TextAsset textAsset = Resources.Load("Files/statements_sets") as TextAsset;

        string text = textAsset.text;

        int numero = 0;

        while (numero < 10)
        {
            if (q.classificationGameList.Contains("set_"+(numero +1)))
            {
                texts.Add(text.Split('\n')[numero]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
            numero++;
        }
        //return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
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
            if (q.classificationGameList.Contains("set_" + (numero + 1)))
            {
                allAnswers.Add(values2);
            }
            numero++;
        }

        //return allAnswers;
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
        //Debug.Log("PUNTOS HASTA AHORA: "+totalPoints);
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
    /*
     * 
     */
    public List<Sprite[]> GetListImages()
    {
        return allImages;
    }
    public List<string> GetListTexts()
    {
        return texts;
    }
    public List<string[]> GetListAnswers()
    {
        return allAnswers;
    }
    /*public List<Tuple<string, Sprite[]>> GetTupla()
    {
        return sprites;
    }*/
}
