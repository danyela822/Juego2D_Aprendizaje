using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int id;

    public void SetId(int newId)
    {
        id = newId;
    }
    public int GetID()
    {
        return this.id;
    }
}
