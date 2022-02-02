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
