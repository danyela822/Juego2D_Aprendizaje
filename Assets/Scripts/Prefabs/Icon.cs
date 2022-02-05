public class Icon 
{
    //valor que equivale al valor del objeto
    public int IdValue { get; set; }
    //valor que equivale id de la imagen 
    public int IdIcon { get; set; }

    /// <summary>
    /// Contructor del Icono
    /// </summary>
    /// <param name="idValue">valor que equivale al valor del objeto</param>
    /// <param name="idIcon">valor que equivale id de la imagen </param>
    public Icon(int idValue, int idIcon)
    {
        this.IdValue = idValue;
        this.IdIcon = idIcon;
    }
}
