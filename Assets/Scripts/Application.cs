using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public Application app 
    {
        get 
        {
            return GameObject.FindObjectOfType<Application>();
        } 
    }
}

public class Application : MonoBehaviour
{
    public GeneralController GeneralController;
    public GeneralView GeneralView;
    public GeneralModel GeneralModel;

    void Start()
    {
        
    }
}
