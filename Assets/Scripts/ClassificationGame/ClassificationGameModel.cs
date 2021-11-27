using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClassificationGameModel : Reference
{
    static List<Sprite[]> allImages;
    static List<string> texts;
    static List<string[]> allAnswers;

    static List<Tuple<string, Sprite[]>> sprites;

    int totalStars;
    int totalPoints;
    int createList;
    public Prueba q;


    //public static ClassificationGameModel gameModel;

    private void Start()
    {

        q.Load("P");
        //PlayerPrefs.DeleteAll();
        //Debug.Log("GETLIST " + GetList());
        /*if (GetList() == 0)
        {
            Debug.Log(q.classificationGameList.Count);
            for (int j = 0; j < 10; j++)
            {
                q.classificationGameList.Add(j);
                Debug.Log("TAMAÑO: " + q.classificationGameList.Count);
            }
            SetList(1);
            Debug.Log("CAMBIO DE LISTA CLASSIFICATION: " + GetList());
            q.Save("P");
        }*/
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
        /*if (GetList()==0)
        {
             //Cargar y guardar un set de imagenes en un array
            q.listaSprites1 = Resources.LoadAll<Sprite>("Sets/set_1");
            q.listaSprites2 = Resources.LoadAll<Sprite>("Sets/set_2");
            q.listaSprites3 = Resources.LoadAll<Sprite>("Sets/set_3");
            q.listaSprites4 = Resources.LoadAll<Sprite>("Sets/set_4");
            q.listaSprites5 = Resources.LoadAll<Sprite>("Sets/set_5");
            q.listaSprites6 = Resources.LoadAll<Sprite>("Sets/set_6");
            q.listaSprites7 = Resources.LoadAll<Sprite>("Sets/set_7");
            q.listaSprites8 = Resources.LoadAll<Sprite>("Sets/set_8");
            q.listaSprites9 = Resources.LoadAll<Sprite>("Sets/set_9");
            q.listaSprites10 = Resources.LoadAll<Sprite>("Sets/set_10");
            Debug.Log("LISTAS CLASSIFICATION CREADAS");
            SetList(1);
            q.Save("P");
        }*/
    }

    /*void Awake()
    {
        if (gameModel == null)
        {
            gameModel = this;
            DontDestroyOnLoad(gameObject);

            //PlayerPrefs.DeleteAll();
            if (GetList() == 0)
            {
                Debug.Log("CREAR LISTA CLASSIFICATION: " + GetList());
                for (int j = 0; j < 10; j++)
                {
                    q.classificationGameList.Add(j);
                }
                //LoadImages();
                //LoadTexts();
                //LoadAnswers();
                SetList(1);
                Debug.Log("CAMBIO DE LISTA CLASSIFICATION: " + GetList());
            }
            q.Load("P");
        }
        else
        {
            //Destruir el objeto
            Destroy(gameObject);
        }
    }*/
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public void LoadImages()
    {
        allImages = new List<Sprite[]>();

        /*if (GetList() == 0)
        {
            Debug.Log("CREAR LISTA CLASSIFICATION: " + GetList());
            for (int j = 0; j < 10; j++)
            {
                q.classificationGameList.Add(j);
            }
            
            SetList(1);
            Debug.Log("CAMBIO DE LISTA CLASSIFICATION: " + GetList());
        }*/

        //Numero de conjuntos de imagenes
        //int setOfImages = 10;
        sprites = new List<Tuple<string, Sprite[]>>();
        int numero = 1;

        while (numero <= 10)
        {
            if (q.classificationGameList.Contains("set_"+numero))
            {
                //Cargar y guardar un set de imagenes en un array
                Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (numero));

                sprites.Add(new Tuple<string, Sprite[]>("set_" + numero, spriteslist));
               
                //Guardar el array de imagenes en la lista
                allImages.Add(spriteslist);
            }
            numero++;
        }

        for (int i = 0; i < q.classificationGameList.Count; i++)
        {
            Debug.Log("item: " + q.classificationGameList[i]);
        }

        foreach (var pair in sprites)
        {
            Debug.Log("TUPLE: " + pair);
        }

/*for (int i = 0; i < q.classificationGameList.Count; i++)
{
   if (q.classificationGameList.Contains("set_"+(i+1)))
   {
       //Cargar y guardar un set de imagenes en un array
       Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (i + 1));

       sprites.Add(new Tuple<string, Sprite[]>("set_" + (i + 1), spriteslist));

       //Guardar el array de imagenes en la lista
       allImages.Add(spriteslist);

       //Debug.Log("item: " + q.classificationGameList[i]);
       //Debug.Log("SET: "+(i+1));

   }
   //Cargar y guardar un set de imagenes en un array
   //Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (i+1));

   //Guardar el array de imagenes en la lista
   //allImages.Add(spriteslist);
}*/

        /*foreach (var pair in sprites)
        {
            Debug.Log("TUPLE: " + pair);
        }
        Debug.Log("LISTA DE NUMEROS EN CLASIFICATION: " + q.classificationGameList.Count);*/
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

        /*for (int i = 0; i < q.classificationGameList.Count; i++)
        {
            if (q.classificationGameList.Contains("set_"+(i+1)))
            {
                texts.Add(text.Split('\n')[i]);
                //Debug.Log("TEXTS: " + texts[i]);
            }
        }*/

        //return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public void LoadAnswers()
    {
        allAnswers = new List<string[]>();

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


        /*for (int i = 0; i < q.classificationGameList.Count; i++)
        {
            values = answers.Split('.');
            values2 = values[i].Split(',');

            for (int j = 0; j < values2.Length; j++)
            {
                values2[j].TrimEnd('.');
            }
            if (q.classificationGameList.Contains("set_" + (i + 1)))
            {
                allAnswers.Add(values2);
            }
        }*/

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
    public List<Tuple<string, Sprite[]>> GetTupla()
    {
        return sprites;
    }
}
