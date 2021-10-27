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
        float newPos = (menu.transform.position.x) - 1080f;
        
        if (newPos >= -7020f)
        {
            Debug.Log("NEW D: " + newPos);
            menu.position = new Vector3(newPos, menu.position.y, 0);
        }
    }
    public void MoveToLeft()
    {
        float newPos = (menu.transform.position.x) + 1080;

        if (newPos <= 540f)
        {
            Debug.Log("NEW I: " + newPos);
            menu.position = new Vector3(newPos, menu.position.y, 0);
        }
    }

    public void Play()
    {
        //Optimizar la lista de los nombres del juego con una lista
        for(int i = 0; i < games.Length; i++)
        {
            float x = games[i].transform.position.x;
            string nombre = games[i].name;

            if(x == 540f)
            {
                Debug.Log("ENTRO: "+nombre);
                string nombreJuego = games[i].GetComponentInChildren<Text>().text;
                Debug.Log("NOMBRE DEL JUEGO: " + nombreJuego);

                if (nombreJuego == "Descubre el conjunto")
                {
                    SceneManager.LoadScene("ClassificationGameScene");
                }
                else if (nombreJuego == "Desifra el elemento")
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
    }
}
