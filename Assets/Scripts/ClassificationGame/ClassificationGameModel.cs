using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ClassificationGameModel : Reference
{
    /*
    * Metodo para cargar y guardar todas la imagnes que necesita el juego
    */
    public List<Sprite[]> LoadImages()
    {
        List<Sprite[]> allImages = new List<Sprite[]>();

        //Numero de conjuntos de imagenes
        int setOfImages = 10;
        for (int i = 1; i <= setOfImages; i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_" + (i));

            //Guardar el array de imagenes en la lista
            allImages.Add(spriteslist);
        }

        return allImages;
    }
    /*
    * Metodo para cargar todos los enunciados que deben acompañar a cada nivel
    */
    public List<string> LoadTexts()
    {
        List<string> texts = new List<string>();

        //Pasar la ruta del archivo y el nombre del archivo que contiene los enunciados requeridos para cada nivel
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Guardar cada linea del archivo de texto en una posicion diferente de la lista
            texts.Add(line);
            //Leer la siguiente linea de texto
            line = reader.ReadLine();
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
        StreamReader reader = new StreamReader("Assets/Resources/Files/correct_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Leer la primera linea de texto
        line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Array que guarda las respuestas de un set
            string[] values = line.Split(',');

            //Guardar cada set de respuestas en la lista
            allAnswers.Add(values);

            //Leer una nueva linea
            line = reader.ReadLine();
        }

        return allAnswers;
    }
}
