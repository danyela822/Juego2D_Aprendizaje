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

    public bool getState()
    {
        return this.visited;
    }

    public int getNumVisited()
    {
        return this.numVisited;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numVisited == 0 && visited == false)
        {
            visited = true;
        }
        numVisited += 1;

        Debug.Log("Paso numero: " + numVisited);

        if(this.id == 4)
        {
            App.generalController.roadGameController.PuntoFinal();
        }
    }
}
