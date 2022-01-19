using System.Collections.Generic;
using UnityEngine;

public class Files : Reference
{
    public FileLists file;
    public static List<Achievement> achievements = new List<Achievement>();
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
                if (j == 0)
                {
                    achievements.Add(new Achievement(j, 8, false));
                }
                else if(j == 3 || j == 8)
                {
                    achievements.Add(new Achievement(j, 3, false));
                }
                else if (j == 9)
                {
                    achievements.Add(new Achievement(j, 6, false));
                }
                else
                {
                    achievements.Add(new Achievement(j, 1, false));
                }
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
            
            achievements.Add(new Achievement(0,1,false));

            PlayerPrefs.SetInt("CreateLists", 1);
            PlayerPrefs.SetInt("Level", 1);
            Debug.Log("CAMBIO DE CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));
            file.Save("P");
        }
    }
}
