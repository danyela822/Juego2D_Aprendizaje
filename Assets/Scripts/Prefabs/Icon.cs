public class Icon 
{
    //valor que equivale al valor del objeto
    public int IdValue { get; set; }
    //valor que equivale id de la imagen 
    public int IdIcon { get; set; }

    public Icon(int idValue, int idIcon)
    {
        this.IdValue = idValue;
        this.IdIcon = idIcon;
    }
}
