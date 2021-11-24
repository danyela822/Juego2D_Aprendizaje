using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ClassificationGameModel : Reference
{
    int totalStars;
    int totalPoints;
    int createList;
    public Prueba q;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        q.Load("Q");
    }

    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public List<Sprite[]> LoadImages()
    {
        List<Sprite[]> allImages = new List<Sprite[]>();

        if (GetList() == 0)
        {
            Debug.Log("CREAR LISTA CLASIFICATION: " + GetList());
            for (int j = 0; j < 10; j++)
            {
                Debug.Log("ENTRO A LLENAR LISTA CLASIFICATION");
                q.classificationGameList.Add(j);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA CLASIFICATION: " + GetList());
        }

        //Numero de conjuntos de imagenes
        int setOfImages = 10;
        for (int i = 0; i < setOfImages; i++)
        {
            if (q.classificationGameList.Contains(i))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (i + 1));

                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            //Cargar y guardar un set de imagenes en un array
            //Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (i+1));

            //Guardar el array de imagenes en la lista
            //allImages.Add(spriteslist);
        }
        Debug.Log("LISTA DE NUMEROS EN CLASIFICATION: " + q.classificationGameList.Count);
        q.Save("Q");
        return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public List<string> LoadTexts()
    {
        List<string> texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        //StreamReader reader = new StreamReader("Assets/Resources/Files/statements_sets.txt");
        TextAsset textAsset = Resources.Load("Files/statements_sets") as TextAsset;
        //string para almacenar linea a linea el contenido del texto
        //string line;

        //Leer la primera linea de texto
        //line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        /*while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente de la lista
            texts.Add(line);
            //Leer la siguiente linea de texto
            line = reader.ReadLine();
        }*/

        string text = textAsset.text;
        for (int i = 0; i < 10; i++)
        {
            texts.Add(text.Split('\n')[i]);
            //Debug.Log("TEXTS: " + texts[i]);
        }

        return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public List<string[]> LoadAnswers()
    {
        List<string[]> allAnswers = new List<string[]>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        //StreamReader reader = new StreamReader("Assets/Resources/Files/correct_sets.txt");
        TextAsset textAsset = Resources.Load("Files/correct_sets") as TextAsset;
        //string para almacenar linea a linea el contenido del texto
        //string line;

        //Leer la primera linea de texto
        //line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        /*while (line != null)
        {
            //Array que guarda las respuestas de un set
            string[] values = line.Split(',');

            //Guardar cada set de respuestas en la lista
            allAnswers.Add(values);

            //Leer una nueva linea
            line = reader.ReadLine();
        }*/

        string answers = textAsset.text;
        string[] values;
        string[] values2;
        for (int i = 0; i < 10; i++)
        {
            values = answers.Split('.');
            values2 = values[i].Split(',');

            for (int j = 0; j < values2.Length; j++)
            {
                values2[j].TrimEnd('.');
            }
            allAnswers.Add(values2);
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
        createList = PlayerPrefs.GetInt("ListClasi", 0);
        return createList;
    }
    /*
     * 
     */
    public void SetList(int value)
    {
        PlayerPrefs.SetInt("ListClasi", value);
    }
}
