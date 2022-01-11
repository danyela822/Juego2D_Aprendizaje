using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsController : Reference
{
    public FileLists file;
    public void ResetValues()
    {
        //Borrar los datos de todos los PlayerPrefs
        PlayerPrefs.DeleteAll();

        Debug.Log("ESTADO CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));

        //Crear nuevamente las listas de cada juego limitado

        //Lista juego 1
        file.classificationGameList = new List<int>();

        //Listas del juego 2
        file.imageListGame2_1 = new List<int>();
        file.imageListGame2_2 = new List<int>();
        file.imageListGame2_3 = new List<int>();
        file.achievementsList = new List<int>();

        //Borrar el archivo.data
        File.Delete(file.GetPath("P"));
    }
}
