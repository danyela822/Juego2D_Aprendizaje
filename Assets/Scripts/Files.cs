using UnityEngine;

public class Files : Reference
{
    public FileLists file;
    private void Awake()
    {
        file.Load("P");
        if (PlayerPrefs.GetInt("CreateLists", 0) == 0)
        {
            for (int j = 0; j < 5; j++)
            {
                file.characteristicsGameList.Add(j);
            }
            for (int j = 0; j < 10; j++)
            {
                file.classificationGameList.Add(j);
            }
            for (int j = 0; j < 15; j++)
            {
                file.achievementsList.Add(j);
            }
            PlayerPrefs.SetInt("CreateLists", 1);
            PlayerPrefs.SetInt("Level", 1);
            Debug.Log("CAMBIO DE CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));
            file.Save("P");
        }
    }
}
