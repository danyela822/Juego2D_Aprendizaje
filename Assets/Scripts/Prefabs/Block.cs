using UnityEngine;
public class Block : Reference
{
    public int Id { get; set; }
    public int NumVisited { get; set; }

    public bool visited = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTRO");
        visited = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("SALIO");
        visited = false;
    }
}
