using UnityEngine;

public class Files : Reference
{
    public FileLists file;
    void Awake()
    {
        CreateLists();
    }
    /// <summary>
    /// Metodo para crear las listas de los juegos no Random
    /// </summary>
    void CreateLists()
    {
        file.Load("P");
        if (PlayerPrefs.GetInt("CreateLists", 0) == 0)
        {
            //Creacion de lista de imagenes del juego 1
            for (int j = 0; j < 10; j++)
            {
                file.imageListGame1.Add(j);
            }
            //Creacion de lista de imagenes del juego 2
            for (int j = 0; j < 5; j++)
            {
                file.imageListGame2_1.Add(j);
                file.imageListGame2_2.Add(j);
                file.imageListGame2_3.Add(j);
            }
            ////Creacion de lista de imagenes del juego 8
            for (int j = 1; j <= 4; j++)
            {
                //union
                file.imageListGame8_1_1.Add(j);
                file.imageListGame8_1_2.Add(j);
                file.imageListGame8_1_3.Add(j);

                //interseccion
                file.imageListGame8_2_1.Add(j);
                file.imageListGame8_2_2.Add(j);
                file.imageListGame8_2_3.Add(j);
            }
            //Creacion de lista de los objetivos
            for (int j = 0; j < 15; j++)
            {
                file.achievementsList.Add(j);
            }
            //Cambiar estado de crear listas para evitar crearlas mas de una vez
            PlayerPrefs.SetInt("CreateLists", 1);
            Debug.Log("CAMBIO DE CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));
            //Guardar estado
            file.Save("P");
        }
    }
}
