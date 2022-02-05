using UnityEngine;
public class Block : MonoBehaviour
{
    public int Id { get; set; }
    public int NumVisited { get; set; }

    public bool visited = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ENTRO");
        visited = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("SALIO");
        visited = false;
    }
}
