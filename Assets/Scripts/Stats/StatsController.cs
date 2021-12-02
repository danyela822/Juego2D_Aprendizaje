using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : Reference
{
    public string GetLeague()
    {
        string league = "";

        int stars = GetTotalStars();

        if (stars >= 15 && stars < 45)
        {
            league = "Hierro";
        }
        else if (stars >=45 && stars < 85)
        {
            league = "Bronce";
        }
        else if (stars >= 85 && stars < 145)
        {
            league = "Plata";
        }
        else if (stars >= 145 && stars < 215)
        {
            league = "Oro";
        }
        else if (stars >= 215)
        {
            league = "Diamante";
        }
        return league;
    }
    public int GetTotalStars()
    {
        int stars = PlayerPrefs.GetInt("TotalStarsGame1", 0) + PlayerPrefs.GetInt("TotalStarsGame2", 0); ;
        return stars;
    }
}
