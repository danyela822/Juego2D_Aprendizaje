using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamesMenuView : MonoBehaviour
{
    public RectTransform menu;
    public GameObject [] games;
    float posFinal;
    bool abrirMenu = true;
    public float tiempo = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        posFinal = menu.transform.position.x;
        Debug.Log("POSICION INICIAL: " + posFinal);
        menu.position = new Vector3(posFinal, menu.position.y, 0);
    }
    public void MoveToRigth()
    {
        float newPos = (menu.transform.position.x) + 890f;
        if (newPos >= -3115f && newPos <= 3655f)
        {
            Debug.Log("NEW D: " + newPos);
            menu.position = new Vector3(newPos, menu.position.y, 0);
        }
    }
    public void MoveToLeft()
    {
        float newPos = (menu.transform.position.x) - 890f;
        if (newPos >= -3115f)
        {
            menu.position = new Vector3(newPos, menu.position.y, 0);
            Debug.Log("NEW I: " + newPos);
        }
    }

    public void Play()
    {
        for(int i = 0; i < games.Length; i++)
        {
            float x = games[i].transform.position.x;
            string nombre = games[i].name;
            Debug.Log("X: " + x + " nombre: "+nombre);
            if(x == 540f)
            {
                Debug.Log("ENTRO: "+nombre);
                string nombreJuego = games[i].GetComponentInChildren<Text>().text;
                Debug.Log("NOMBRE DEL JUEGO: " + nombreJuego);

                if (nombreJuego == "Nombre del Juego 1")
                {
                    SceneManager.LoadScene("ClassificationGameScene");
                }
                else if (nombreJuego == "Nombre del Juego 2")
                {
                    SceneManager.LoadScene("CharacteristicsGameScene");
                }
                else if (nombreJuego == "Nombre del Juego 3")
                {

                }
                else if(nombreJuego == "Nombre del Juego 4")
                {

                }
            }
        }
        //public RectTransform subMenu = GameObject.Find("Ga").transform.position
    }
}
