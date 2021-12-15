using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameModel : Reference
{
    static Sprite[] images;
    static string statement;

    public static List<int> listaDeGuardado;

    int totalStars;
    int totalPoints;
    public FileLists file;

    /*private void Start()
    {
        file.Load("P");
        //Debug.Log("TAMAÑO LISTA Characteristics: " + file.characteristicsGameList.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 6; j++)
            {
                file.characteristicsGameList.Add(j);
            }
            listaDeGuardado = file.characteristicsGameList;
            Debug.Log("LISTA DE GUARDADO " + listaDeGuardado.Count);
            SetList(1);
            //Debug.Log("CAMBIO DE LISTA Characteristics: " + GetList());
            App.generalView.gamesMenuView.playButtons[1].enabled = true;
            file.Save("P");
        }
    }*/
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public Sprite[] LoadImages(int numero)
    {
        if (file.characteristicsGameList.Contains(numero))
        {
            //Cargar y guardar un set de imagenes en un array
            images = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + numero);
        }
        return images;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public string LoadTexts(int numero)
    {
        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_characteristics") as TextAsset;

        string text = textAsset.text;

        if (file.characteristicsGameList.Contains(numero))
        {
            statement = text.Split('\n')[numero];
        }
        return statement;
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
}
