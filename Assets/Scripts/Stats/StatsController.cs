using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : Reference
{
    bool[] achievements;
    //public FileLists file;

    public bool IsAchievements(int numberAchievement)
    {
        if (App.generalModel.statsModel.file.achievementsList.Contains(numberAchievement))
        {
            Debug.Log("EL logro: "+numberAchievement+" aun no se ha completado");
            return false;
        }
        else
        {
            Debug.Log("EL logro: " + numberAchievement + " ya se completo");
            return true;
        }
    }
    public void DeleteAchievements(int numberAchievement)
    {
        Debug.Log("EL logro: " + numberAchievement + " se ha completado");
        App.generalModel.statsModel.file.achievementsList.Remove(numberAchievement);
        App.generalModel.statsModel.file.Save("P");
    }
    public string GetLeague()
    {
        return App.generalModel.statsModel.GetLeague();
    }
    public int GetTotalStars()
    {
        return App.generalModel.statsModel.GetTotalStars();
    }
    /*public List<int> GetAchievementsList(int number)
    {
        return App.generalModel.statsModel.CheckAchievements(number);
    }*/
    public Sprite GetLeagueImage(string league)
    {
        return App.generalModel.statsModel.LoadLeagueImage(league);
    }
}
