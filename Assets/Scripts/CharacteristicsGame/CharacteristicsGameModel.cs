using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsGameModel : Reference
{
    int totalStars;
    int totalPoints;
    //public Prueba p;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //p.Load("P");
    }

    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public List<Sprite[]> LoadImages()
    {
        List<Sprite[]> allImages = new List<Sprite[]>();

        //Numero de conjuntos de imagenes
        int setOfImages = 4;
        for (int i = 1; i <= setOfImages; i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i));

            //Guardar el array de imagenes en la lista
            allImages.Add(spriteslist);

            //p.lista.Add(spriteslist);

        }
        //p.numero +=1;
        //Debug.Log("NUMERO: " + p.numero);
        //p.Save("P");
        //Debug.Log("NUMERO: " + p.numero);
        return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public List<string> LoadTexts()
    {
        List<string> texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        //StreamReader reader = new StreamReader("Assets/Resources/Files/statements_characteristics.txt");
        //Object s = Resources.Load("Files/statements_characteristics.txt");
        TextAsset textAsset = Resources.Load("Files/statements_characteristics") as TextAsset;
   
        //texts.Add(s.Split('\n')[0]);
        //string[] cadena = s.Split('\n');
        //reader = new StreamReader(Resources.Load("Files/statements_characteristics.txt"));
     
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
        for (int i = 0; i < 4; i++)
        {
            texts.Add(text.Split('\n')[i]);
        }

        return texts;
    }
    /*
    * Metodo para cargar y guardar las respuestas de cada nivel
    */
    public List<string> LoadAnswers()
    {
        List<string> answers = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene las respuestas requeridas para cada nivel
        //StreamReader reader = new StreamReader("Assets/Resources/Files/correct_characteristics.txt");
        TextAsset textAsset = Resources.Load("Files/correct_characteristics") as TextAsset;
        //string para almacenar linea a linea el contenido del texto
        //string line;

        //Leer la primera linea de texto
        //line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        /*while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente de la lista
            answers.Add(line);
            //Leer una nueva linea
            line = reader.ReadLine();
        }*/

        string answer = textAsset.text;
        for (int i = 0; i < 4; i++)
        {
            answers.Add(answer.Split(',')[i]);
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
}
