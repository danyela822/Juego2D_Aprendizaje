public class Figure 
{
    //id del la figura principal
    public int Principal { get; set; }
    //cantidad de la figura
    public int ValueC { get; set; }
    //ide de la figura repetida
    public int Fig { get; set; }

    /// <summary>
    /// Contructor de la figura
    /// </summary>
    /// <param name="principal">id del la figura principal</param>
    /// <param name="valueC">cantidad de la figura</param>
    /// <param name="fig">id de la figura repetida</param>
    public Figure (int principal, int valueC, int fig){
        
        this.Principal = principal;
        this.ValueC = valueC;
        this.Fig = fig;

    }
}
