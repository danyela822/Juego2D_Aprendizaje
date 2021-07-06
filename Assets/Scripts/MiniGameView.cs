using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    //Variables para la vista del MiniJuego 2

    //Texto del acercito y texto de la solucion
    public Text riddle,solution;

    //Botones para elegir una de las posibles opciones
    public Button opcion_0, opcion_1,opcion_2;

    //Imagen para representar una incognita y para representar el acertijo
    public Image riddle_imagen, solution_imagen;

    //Variable que servira para cambiar el sprite de la imagen del acertijo
    Sprite sprite;

    //Variable para mostrar si la opcion elegida fue o no la correcta
    GameObject panel;
    //Acertijo
    Acertijo a;
    // Start is called before the first frame update
    void Start()
    {
        if(this.name == "Mini Juego 1 Canvas")
        {
            riddle = GameObject.Find("Riddle Text").GetComponent<Text>();
            solution = GameObject.Find("Solution Text").GetComponent<Text>();
            opcion_0 = GameObject.Find("Opcion_0 Button").GetComponent<Button>();
            opcion_1 = GameObject.Find("Opcion_1 Button").GetComponent<Button>();
            opcion_2 = GameObject.Find("Opcion_2 Button").GetComponent<Button>();
            riddle_imagen = GameObject.Find("Riddle Image").GetComponent<Image>();
            solution_imagen = GameObject.Find("Solution Image").GetComponent<Image>();
            panel = GameObject.Find("Panel");
            panel.SetActive(false);
        }
        else if(this.name == "Mini Juego 2 Canvas")
        {

        }   
        else if(this.name == "Mini Juego 3 Canvas")
        {

        }

        //Texts texto = new Texts();
        //texto.CrearAcertijos();
        //CargarAcertijos();
        
    }

    public void CargarAcertijos()
    {
        int num = Random.Range(0, 34);

        //a = Texts.lista[num];


        //acertijo.text = a.acertijo;
       
        int cont = 0;
        int r = 0;
        bool op_0 = true, op_1 = true, op_2 = true;

        while (cont < 3)
        {
            r = Random.Range(0, 3);
            string nom = "opcion_" + r;
            print("R: " + r);

            if(nom == opcion_0.name && op_0 ==true)
            {
               // opcion_0.gameObject.GetComponentInChildren<Text>().text = a.opciones[r].ToString();
                op_0 = false;
                cont++;
            }
            else if(nom == opcion_1.name && op_1 == true)
            {
               // opcion_1.gameObject.GetComponentInChildren<Text>().text = a.opciones[r].ToString();
                op_1 = false;
                cont++;
            }
            else if(nom == opcion_2.name && op_2 == true)
            {
               // opcion_2.gameObject.GetComponentInChildren<Text>().text = a.opciones[r].ToString();
                op_2 = false;
                cont++;
            }
        }
    }
    public void VerificarRespuesta(GameObject respuesta)
    {
        /*if(respuesta.GetComponent<Text>().text == a.respuesta)
        {
            panel.SetActive(true);
            print("CORRECTO");
            sprite = Resources.Load<Sprite>(a.imagen);

            print("IMAGEN: " + sprite);
            imagen.enabled = true;
            imagen.sprite = sprite;

            text_1.enabled = true;
            text_1.text = "Correcto, la respuesta es: "+a.respuesta;

            imagen_1.enabled = false;
        }
        else
        {
            print("LA CAGO");
        }*/
    }
    public void Volver()
    {
        /*panel.SetActive(false);
        imagen.enabled = false;
        text_1.enabled = false;
        imagen_1.enabled = true;*/
    }
}
