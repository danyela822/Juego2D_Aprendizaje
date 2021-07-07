using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MiniGameModel : Reference
{
    //Lista de los acertijos del minijuego
    public List<Acertijo> riddlesList = new List<Acertijo>();

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
        StreamReader reader = new StreamReader("D:/Unity/Juego2D_Aprendizaje/Assets/Files/Acertijos.txt");

        //Leer la primera línea de texto
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
                riddlesList.Add(new Acertijo(number, riddle, answer, options, "Acertijos/" + number, 0));
                Debug.Log("Num: "+number+" Enun: "+riddle+" Resp: "+answer+" Opts: "+options[0]+" , "+options[1]+" , "+options[2]+" Adress: Acertijos / " + number + "dif: "+0);
                //Se inicia nuevamente el array de opciones
                options = new ArrayList();

                //Se inicia nuevamente el enunciado del acertijo
                riddle = "";
            }

            //Leer la siguente línea de texto
            line = reader.ReadLine();
        }
        //Cerrar el archivo
        reader.Close();
    }
    public Sprite LoadSprite(string image)
    {
        return Resources.Load<Sprite>(image);
    }
}
public class Acertijo
{
    public int number { get; set; }
    public string riddle { get; set; }
    public string answer { get; set; }
    public ArrayList options { get; set; }
    public string image { get; set; }
    public int difficulty { get; set; }

    public Acertijo(int number, string riddle, string answer, ArrayList options, string image, int difficulty)
    {
        this.number = number;
        this.riddle = riddle;
        this.answer = answer;
        this.options = options;
        this.image = image;
        this.difficulty = difficulty;
    }
}
