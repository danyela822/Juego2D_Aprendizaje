using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "FileLists", menuName = "Herramientas/fileLists", order = 0)]
[System.Serializable]
public class FileLists : ScriptableObject
{
    //Lista para representar las imagenes del juego 1
    public List<int> imageListGame1;

    //Listas para representar las imagenes del juego 2
    public List<int> imageListGame2_1;
    public List<int> imageListGame2_2;
    public List<int> imageListGame2_3;

    //Listas para representar las imagenes del juego 8
    public List<int> imageListGame8_1_1;
    public List<int> imageListGame8_1_2;
    public List<int> imageListGame8_1_3;

    public List<int> imageListGame8_2_1;
    public List<int> imageListGame8_2_2;
    public List<int> imageListGame8_2_3;

    //Listas para representar los objetivos del juego
    public List<int> achievementsList;

    /// <summary>
    /// Metodo para guardar el estado actual del juego (LISTAS DE LOS JUEGOS)
    /// </summary>
    /// <param name="fileName">Nombre del archivo a guardar</param>
    public void Save(string fileName = null)
    {
        BinaryFormatter br = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".data";
        //Debug.Log("DIRECCION DONDE SE CREA EL ARCHIVO: " + path);
        FileStream file = File.Create(path);
        var json = JsonUtility.ToJson(this);

        br.Serialize(file, json);

        //Debug.Log("SE CREO ARCHIVO: " + file.Name + ": " + File.Exists(GetPath(fileName)));
        //Debug.Log("LISTA CLASIFICACION EN GUARDAR: " + classificationGameList.Count);
        //Debug.Log("LISTA LOGROS EN GUARDAR: " + achievementsList.Count);
        file.Close();
    }
    /// <summary>
    /// Metodo para cargar el estado actual del juego (LISTAS DE LOS JUEGOS)
    /// </summary>
    /// <param name="fileName">Nombre del archivo a cargar</param>
    public void Load(string fileName = null)
    {

        if (File.Exists(GetPath(fileName)))
        {
            BinaryFormatter br = new BinaryFormatter();
            FileStream file = File.Open(GetPath(fileName), FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)br.Deserialize(file), this);
            Debug.Log("LISTA LOGROS EN CARGAR: " + achievementsList.Count);
            Debug.Log("LISTA U/I EN CARGAR: " + imageListGame8_2_3.Count);
            Debug.Log("LISTA CARACTERISTICAS EN CARGAR: " + imageListGame2_3.Count);
            Debug.Log("LISTA CLASIFICACION EN CARGAR: " + imageListGame1.Count);
            file.Close();
        }
        else
        {
            Debug.Log("No existe: " + GetPath(fileName));
        }
    }

    public string GetPath(string fileName = null)
    {
        var fullfileName = string.IsNullOrEmpty(fileName) ? name : fileName;
        string retorno = string.Format("{0}/{1}.data", Application.persistentDataPath, fullfileName);
        return retorno;
    }
}
