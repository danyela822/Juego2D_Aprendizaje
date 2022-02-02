public class Figure 
{
    //id del la figura principal
    public int Principal { get; set; }
    //cantidad de la figura
    public int ValueC { get; set; }
    //ide de la figura repetida
    public int Fig { get; set; }

    //contructos de la figura
    public Figure (int principal, int valueC, int fig){
        
        this.Principal = principal;
        this.ValueC = valueC;
        this.Fig = fig;

    }
}
