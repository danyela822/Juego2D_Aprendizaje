using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "FileLists", menuName = "Herramientas/fileLists", order = 0)]
[System.Serializable]
public class FileLists : ScriptableObject
{
    public List<int> classificationGameList;

    public List<int> imageListGame2_1;
    public List<int> imageListGame2_2;
    public List<int> imageListGame2_3;

    public List<int> imageListGame8_1_1;
    public List<int> imageListGame8_1_2;
    public List<int> imageListGame8_1_3;

    public List<int> imageListGame8_2_1;
    public List<int> imageListGame8_2_2;
    public List<int> imageListGame8_2_3;

    public List<int> achievementsList;

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
        Debug.Log("LISTA LOGROS EN GUARDAR: " + achievementsList.Count);
        file.Close();
    }

    public void Load(string fileName = null)
    {

        if (File.Exists(GetPath(fileName)))
        {
            BinaryFormatter br = new BinaryFormatter();
            FileStream file = File.Open(GetPath(fileName), FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)br.Deserialize(file), this);
            //Debug.Log("LISTA CLASIFICACION EN CARGAR: " + classificationGameList.Count);
            Debug.Log("LISTA LOGROS EN CARGAR: " + achievementsList.Count);
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
