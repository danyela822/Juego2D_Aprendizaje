public class Objects
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Type { get; set; }

    /*Metodo constructor de la clase objetos que recibe tres parametros
    * row -> posicion en X donde esta ubicado el Objeto
    * col -> posicion en Y donde esta ubicado el objeto
    * t -> tipo de objeto que es (0 -> piso, 1 -> limite, 2 -> punto Llegada, 3 -> Personaje Principal)
    */
    public Objects(int row, int col, int t)
    {
        this.X = row;
        this.Y = col;
        this.Type = t;
    }
}
