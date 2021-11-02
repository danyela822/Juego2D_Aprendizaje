using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon 
{
    //valor que equivale al valor del objeto
    public int idValue;

    //valor que equivale id de la imagen 
    public int idIcon;

    public Icon(int idValue, int idIcon){
        this.idValue = idValue;
        this.idIcon = idIcon;
    }

    public int IdValue{

        get{
            return idValue;
        }

        set{
            idValue = value;
        }
    }

    public int IdIcon{

        get{
            return idIcon;
        }

        set{
            idIcon = value;
        }
    }
}
