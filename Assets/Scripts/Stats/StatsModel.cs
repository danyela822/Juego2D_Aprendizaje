using System.Collections.Generic;
using UnityEngine;

public class StatsModel : Reference
{
    public FileLists file;
    string currentLeague;

    public Sprite LoadLeagueImage(string league)
    {
        return Resources.Load<Sprite>("LeaguesImages/" + league);
    }
    public string GetLeague()
    {
        if (currentLeague == null)
        {
            return "Sin liga";
        }
        else
        {
            return currentLeague;
        }
    }
    public void UpdateTotalPoints(int value)
    {
       PlayerPrefs.SetInt("TotalPoints",value);
    }
    public int GetTotalPoints()
    {
        return PlayerPrefs.GetInt("TotalPoints", 0); ;
    }
    public int GetTotalStars()
    {
        return PlayerPrefs.GetInt("TotalStars", 0); ;
    }
    public void UpdateTotalStars(int stars)
    {
        PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars", 0) + stars);

        int totalStars = GetTotalStars();

        if (totalStars < 15)
        {
            currentLeague = "Sin liga";
        }
        else if (totalStars >= 15 && totalStars < 45)
        {
            currentLeague = "hierro";

            if (!App.generalController.statsController.IsAchievements(10))
            {
                App.generalController.statsController.DeleteAchievements(10);
            }
        }
        else if (totalStars >= 45 && totalStars < 85)
        {
            currentLeague = "bronce";

            if (!App.generalController.statsController.IsAchievements(11))
            {
                App.generalController.statsController.DeleteAchievements(11);
            }
        }
        else if (totalStars >= 85 && totalStars < 145)
        {
            currentLeague = "plata";

            if (!App.generalController.statsController.IsAchievements(12))
            {
                App.generalController.statsController.DeleteAchievements(12);
            }
        }
        else if (totalStars >= 145 && totalStars < 215)
        {
            currentLeague = "oro";

            if (!App.generalController.statsController.IsAchievements(13))
            {
                App.generalController.statsController.DeleteAchievements(13);
            }
        }
        else if (totalStars >= 215)
        {
            currentLeague = "diamante";

            if (!App.generalController.statsController.IsAchievements(14))
            {
                App.generalController.statsController.DeleteAchievements(14);
            }
        }
    }
}
