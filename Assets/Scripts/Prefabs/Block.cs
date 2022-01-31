using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Reference
{
    public int id;
    public bool visited = false;
    public int numVisited = 0;
    public void SetId(int newId)
    {
        id = newId;
    }
    public int GetID()
    {
        return this.id;
    }

    public int getNumVisited()
    {
        return this.numVisited;
    }

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
