using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "Prueba nueva",menuName ="Herramientas/prueba",order =0)]
[System.Serializable]
public class Prueba : ScriptableObject
{
    public List<Sprite[]> lista = new List<Sprite[]>();
    public List<Sprite> l;
    public int numero;

    public void Save(string fileName = null)
    {
        BinaryFormatter br = new BinaryFormatter();
        string path = UnityEngine.Application.persistentDataPath + "/" + fileName + ".data" ;
        //Debug.Log("DIRECCION DONDE SE CREA EL ARCHIVO: " + path);
        FileStream file = File.Create(path);
        var json = JsonUtility.ToJson(this);

        br.Serialize(file, json);

        Debug.Log("SE CREO ARCHIVO: "+file.Name+": "+ File.Exists(GetPath(fileName)));
        Debug.Log("LISTA EN GUARDAR: " + l.Count);
        file.Close();
    }

    public void Load(string fileName = null)
    {
       
        if (File.Exists(GetPath(fileName)))
        {
            Debug.Log("CARGAR DATOS IF: " + fileName);
            BinaryFormatter br = new BinaryFormatter();
            FileStream file = File.Open(GetPath(fileName), FileMode.Open);
            
            JsonUtility.FromJsonOverwrite((string)br.Deserialize(file),this);
            Debug.Log("LISTA EN CARGAR: " + l.Count);
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
        string retorno = string.Format("{0}/{1}.data", UnityEngine.Application.persistentDataPath, fullfileName);
        return retorno;
    }
}
