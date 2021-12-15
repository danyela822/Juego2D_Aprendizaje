using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsController : Reference
{
    public FileLists file;
    public void ResetValues()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("ESTADO CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));
        /*Debug.Log("ESTADO ClassificationList: " + PlayerPrefs.GetInt("ClassificationList", 0));
        Debug.Log("ESTADO CharacteristicsList: " + PlayerPrefs.GetInt("CharacteristicsList", 0));
        Debug.Log("ESTADO AchievementsList: " + PlayerPrefs.GetInt("AchievementsList", 0));*/
        file.classificationGameList = new List<int>();
        file.characteristicsGameList = new List<int>();
        file.achievementsList = new List<int>();
        File.Delete(file.GetPath("P"));
        //SceneManager.LoadScene("MainMenuScene");
    }
}
