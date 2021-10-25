using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesMenuController : MonoBehaviour
{
    public RectTransform menu;
    float posFinal;
    bool abrirMenu = true;
    public float tiempo = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        posFinal = menu.transform.position.x;
        Debug.Log("POSICION INICIAL: "+posFinal);
        menu.position = new Vector3(posFinal, menu.position.y, 0);
    }
    public void onClick()
    {
        float newPos = (menu.transform.position.x) + 890f;
        if (newPos>=-3115f && newPos<=3655f)
        {
            Debug.Log("NEW D: " + newPos);
            menu.position = new Vector3(newPos, menu.position.y, 0);
        } 
    }
    public void onClick2()
    {
        float newPos = (menu.transform.position.x) - 890f;
        if (newPos >= -3115f)
        {
            menu.position = new Vector3(newPos, menu.position.y, 0);
            Debug.Log("NEW I: " + newPos);
        }
    }
}
