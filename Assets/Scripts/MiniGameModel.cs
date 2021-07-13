using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MiniGameModel : Reference
{
    //Lista de los acertijos del minijuego
    public List<Riddle> riddlesList = new List<Riddle>();

    //Metodo que lee un archivo de texto que contine los acertijos que se almacenaran en una lista
    public void CreateRiddles()
    {
        //Numero del acertijo
        int number = 0;

        //Texto del acertijo
        string riddle = "";

        //Respuesta del acertijo
        string answer = "";

        //Opciones de respuesta del acertijo
        ArrayList options = new ArrayList();

        //string para almacenar linea a linea el contenido del texto
        string line;

        //Pasar la ruta del archivo y el nombre del archivo al constructor de StreamReader
        StreamReader reader = new StreamReader("Assets/Files/Acertijos.txt");

        //Leer la primera l�nea de texto
        line = reader.ReadLine();

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            //Si la linea comienza con # indica que la linea contine texto del enunciado del acertijo
            if (line.StartsWith("#"))
            {
                riddle = riddle + line.Substring(1) + "\n";
            }
            //Si la linea comienza con - indica que la linea es una opcion de respuesta y si comienza con * indica que es la respuesta
            //Ambas deben almacenarse en el array de opciones
            else if (line.StartsWith("-") || line.StartsWith("*"))
            {
                //Se almacena cada opcion en el array de options
                options.Add(line.Substring(1));

                //
                if (line.StartsWith("*"))
                {
                    answer = line.Substring(1);
                }
            }
            //Si la linea comienza con / indica el final de un acetijo
            else if (line.StartsWith("/"))
            {
                //Aumenta el numero del acertijo
                number++;

                //Se guarda el acertijo con su numero, enunciado, respuesta, opciones, direcion de la imagen y dificultad
                riddlesList.Add(new Riddle(number, riddle, answer, options, "Acertijos/" + number, 0));

                //Se inicia nuevamente el array de opciones
                options = new ArrayList();

                //Se inicia nuevamente el enunciado del acertijo
                riddle = "";
            }

            //Leer la siguente l�nea de texto
            line = reader.ReadLine();
        }
        //Cerrar el archivo
        reader.Close();
    }
    //Metodo que recibe una direccion de una imagen especifica y retorna un sprite
    public Sprite LoadSprite(string image)
    {
        return Resources.Load<Sprite>(image);
    }
}
public class Riddle
{
    public int Number { get; set; }
    public string Text { get; set; }
    public string Answer { get; set; }
    public ArrayList Options { get; set; }
    public string Image { get; set; }
    public int Difficulty { get; set; }

    public Riddle(int number, string text, string answer, ArrayList options, string image, int difficulty)
    {
        Number = number;
        Text = text;
        Answer = answer;
        Options = options;
        Image = image;
        Difficulty = difficulty;
    }
}
