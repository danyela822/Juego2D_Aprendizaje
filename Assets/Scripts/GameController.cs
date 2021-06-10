using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Reference
{
    /*
     * Metodo determina que accion realizar al oprimir un botón
     * en la interfaz de la vista del juego
     */
    public void OnClickButtons(string name_button)
    {
        //EL boton pause abre el canvas del menu de pause
        if(name_button == "Button Pause")
        {

        }
        //El boton help abre el tutorial del juego
        if (name_button == "Button Help")
        {

        }
        //El boton solucion abre un canvas para ir a los minijuegos
        if (name_button == "Button Solution")
        {

        }
    }
}
