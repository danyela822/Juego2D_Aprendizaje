using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SettingsController : Reference
{
    public Prueba q;
    public void ResetValues()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("ESTADO LIST: " + PlayerPrefs.GetInt("ListClasi", 0));
        q.classificationGameList = new List<string>();
        //q.listaSprites = new List<Sprite[]>();
        File.Delete("C:/Users/Daniela/AppData/LocalLow/DefaultCompany/Juego2D_Aprendizaje/P.data");
        SceneManager.LoadScene("MainMenuScene");
    }
}
