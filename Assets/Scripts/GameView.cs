using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : Reference
{
    /*
     * Metodo que captura el nombre del boton el cual se esta pulsando
     */
    public void OnClickButtons(string name_button)
    {
        App.generalController.gameController.OnClickButtons(name_button);
    }
}
