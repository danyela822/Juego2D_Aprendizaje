using System.Collections.Generic;
using UnityEngine;

public class Files : Reference
{
    public FileLists file;
    private void Awake()
    {
        
        file.Load("P");
        if (PlayerPrefs.GetInt("CreateLists", 0) == 0)
        {
            for (int j = 0; j < 10; j++)
            {
                file.classificationGameList.Add(j);
            }
            for (int j = 0; j < 15; j++)
            {
                file.achievementsList.Add(j);
            }
            for (int j = 0; j < 5; j++)
            {
                file.imageListGame2_1.Add(j);
                file.imageListGame2_2.Add(j);
                file.imageListGame2_3.Add(j);
            }
            for (int j = 1; j <= 3; j++)
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

            PlayerPrefs.SetInt("CreateLists", 1);
            PlayerPrefs.SetInt("Level", 1);
            Debug.Log("CAMBIO DE CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));
            file.Save("P");
        }
    }
}
