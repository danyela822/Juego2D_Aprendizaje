using System.Collections.Generic;

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
}
