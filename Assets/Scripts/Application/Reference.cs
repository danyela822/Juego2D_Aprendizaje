using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public App App
    {
        get 
        {
            return GameObject.FindObjectOfType<App>();
        }
    }
}
