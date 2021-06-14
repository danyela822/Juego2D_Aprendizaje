using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Reference
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Objects
{
    public int x { get; set; }
    public int y { get; set; }
    public int type { get; set; }

    /*Metodo constructor de la clase objetos que recibe tres parametros
    * row -> posicion en X donde esta ubicado el Objeto
    * col -> posicion en Y donde esta ubicado el objeto
    * t -> tipo de objeto que es (0 -> piso, 1 -> limite, 2 -> punto Llegada, 3 -> Personaje Principal)
    */
    public Objects (int row, int col, int t)
    {
        this.x = row;
        this.y = col;
        this.type = t;
    }
}
