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
    public Prueba p;

    private void Start()
    {
        p.Load("P");
        Debug.Log("TAMAÑO LISTA Characteristics: " + p.characteristicsGameList.Count);
        //Debug.Log("TAMAÑO LISTA SPRITES ANTES: "+q.listaSprites.Count);
        if (GetList() == 0)
        {
            for (int j = 0; j < 6; j++)
            {
                p.characteristicsGameList.Add("characteristics_" + (j + 1));
                // Debug.Log("TAMAÑO: " + q.classificationGameList.Count);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA Characteristics: " + GetList());
            App.generalView.gamesMenuView.playButtons[1].enabled = true;
            p.Save("P");
        }
    }

    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadImages()
    {
        allImages = new List<Sprite[]>();

        //Numero de conjuntos de imagenes
        int setOfImages = 6;

        /*for (int i = 0; i < setOfImages; i++)
        {
            if (p.characteristicsGameList.Contains(i))
            {
                    //Cargar y guardar un set de imagenes en un array
                    Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i+1));

                    //Guardar el array de imagenes en la lista
                    allImages.Add(spriteslist);
            }
            
            //Cargar y guardar un set de imagenes en un array
            //Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i));

            //Guardar el array de imagenes en la lista
            //allImages.Add(spriteslist);

            //p.lista.Add(spriteslist);

        }*/
        int numero = 1;

        while (numero <= setOfImages)
        {
            if (p.characteristicsGameList.Contains("characteristics_" + numero))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (numero));

                //sprites.Add(new Tuple<string, Sprite[]>("characteristics__" + numero, spriteslist));

                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            numero++;
        }

        Debug.Log("LISTA DE NUMEROS EN CHARACTERISTICS: " + p.characteristicsGameList.Count);
        p.Save("P");
        //Debug.Log("NUMERO: " + p.numero);
        //return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public void LoadTexts()
    {
        texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        TextAsset textAsset = Resources.Load("Files/statements_characteristics") as TextAsset;

        int numero = 0;

        string text = textAsset.text;

        while (numero < 6)
        {
            if (p.characteristicsGameList.Contains("characteristics_" + (numero + 1)))
            {
                texts.Add(text.Split('\n')[numero]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
            numero++;
        }

        
        /*for (int i = 0; i < 4; i++)
        {
            if (p.characteristicsGameList.Contains(i))
            {
                texts.Add(text.Split('\n')[i]);
            }
        }*/

        //return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
    {
        answers = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        TextAsset textAsset = Resources.Load("Files/correct_characteristics") as TextAsset;

        string answer = textAsset.text;

        int numero = 0;

        while (numero < 6)
        {
            if (p.characteristicsGameList.Contains("characteristics_" + (numero + 1)))
            {
                answers.Add(answer.Split(',')[numero]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
            numero++;
        }

        /*for (int i = 0; i < 4; i++)
        {
            if (p.characteristicsGameList.Contains(i))
            {
                answers.Add(answer.Split(',')[i]);
            }
        }*/
        //return answers;
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
    public List<string> GetListAnswers()
    {
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
        //Debug.Log("PUNTOS HASTA AHORA: "+totalPoints);
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
