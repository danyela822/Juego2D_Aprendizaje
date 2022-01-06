using System.Collections.Generic;
using UnityEngine;

public class StatsController : Reference
{
    public string GetLeague()
    {
        return App.generalModel.statsModel.GetLeague();
    }
    public int GetTotalStars()
    {
        return App.generalModel.statsModel.GetTotalStars();
    }
    public List<int> GetAchievementsList(int number)
    {
        return App.generalModel.statsModel.CheckAchievements(number);
    }
    public Sprite GetLeagueImage(string league)
    {
        return App.generalModel.statsModel.LoadLeagueImage(league);
    }
}
