using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameModel : Reference
{
    
}
public class Acertijo
{
    public int numero { get; set; }
    public string acertijo { get; set; }
    public string respuesta { get; set; }
    public ArrayList opciones { get; set; }
    public string imagen { get; set; }
    public int dificultad { get; set; }

    public Acertijo(int numero, string acertijo, string respuesta, ArrayList opciones, string imagen, int dificultad)
    {
        this.numero = numero;
        this.acertijo = acertijo;
        this.respuesta = respuesta;
        this.opciones = opciones;
        this.imagen = imagen;
        this.dificultad = dificultad;
    }
}
