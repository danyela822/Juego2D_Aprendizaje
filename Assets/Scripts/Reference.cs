using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public Application App
    {
        get
        {
            return GameObject.FindObjectOfType<Application>();
        }
    }

}
