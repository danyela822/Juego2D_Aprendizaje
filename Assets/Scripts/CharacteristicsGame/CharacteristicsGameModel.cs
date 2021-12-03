using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameModel : Reference
{
    static List<Sprite[]> allImages;
    static List<string> texts;
    static List<string> answers;

    int totalStars;
    int totalPoints;
    int createList;
    public FileLists file;

    private void Start()
    {
        file.Load("P");
        Debug.Log("TAMA�O LISTA Characteristics: " + file.characteristicsGameList.Count);
        //Debug.Log("TAMA�O LISTA SPRITES ANTES: "+q.listaSprites.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 6; j++)
            {
                file.characteristicsGameList.Add("characteristics_" + (j + 1));
                // Debug.Log("TAMA�O: " + q.classificationGameList.Count);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA Characteristics: " + GetList());
            App.generalView.gamesMenuView.playButtons[1].enabled = true;
            file.Save("P");
        }
    }

    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public List<Sprite[]> LoadImages()
    {
        allImages = new List<Sprite[]>();

        //Numero de conjuntos de imagenes
        int setOfImages = 6;

        int numero = 1;

        while (numero <= setOfImages)
        {
            if (file.characteristicsGameList.Contains("characteristics_" + numero))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (numero));

                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            numero++;
        }

        Debug.Log("LISTA DE NUMEROS EN CHARACTERISTICS: " + file.characteristicsGameList.Count);
        file.Save("P");
        return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompa�ar a cada nivel
    */
    public List<string> LoadTexts()
    {
        texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_characteristics") as TextAsset;

        int numero = 0;

        string text = textAsset.text;

        while (numero < 6)
        {
            if (file.characteristicsGameList.Contains("characteristics_" + (numero + 1)))
            {
                texts.Add(text.Split('\n')[numero]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
            numero++;
        }
        return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public List<string> LoadAnswers()
    {
        answers = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        TextAsset textAsset = Resources.Load("Files/correct_characteristics") as TextAsset;

        string answer = textAsset.text;

        int numero = 0;

        while (numero < 6)
        {
            if (file.characteristicsGameList.Contains("characteristics_" + (numero + 1)))
            {
                answers.Add(answer.Split(',')[numero]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
            numero++;
        }
        return answers;
    }
    /*
     * 
     */
    public int GetTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStarsGame2", 0);
        //Debug.Log("STARS HASTA AHORA: " + totalStars);
        return totalStars;
    }
    /*
     * 
     */
    public void SetTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStarsGame2", stars);
        //Debug.Log("STARS EN SET: " + stars);
    }
    /*
     * 
     */
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPointsGame2", 0);
        Debug.Log("PUNTOS HASTA AHORA CHARACTERISTICS: "+totalPoints);
        if (totalPoints >= 200 && PlayerPrefs.GetInt("GamePoints2", 0) == 0)
        {
            PlayerPrefs.SetInt("GamePoints2", 1);
            PlayerPrefs.SetInt("ThreeTundredPoints", PlayerPrefs.GetInt("ThreeTundredPoints", 0) + 1);
        }
        return totalPoints;
    }
    /*
     * 
     */
    public void SetPoints(int valor)
    {
        PlayerPrefs.SetInt("TotalPointsGame2", valor);
        //Debug.Log("PUNTOS EN SET: " + valor);
    }
    /*
     * 
     */
    public int GetList()
    {
        createList = PlayerPrefs.GetInt("CharacteristicsList", 0);
        return createList;
    }
    /*
     * 
     */
    public void SetList(int value)
    {
        PlayerPrefs.SetInt("CharacteristicsList", value);
    }
}
