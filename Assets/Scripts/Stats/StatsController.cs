using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : Reference
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public string GetLeague()
    {
        string league = "";
        int stars = GetTotalStars();

        if (stars > 15 && stars <= 35)
        {
            league = "Hierro";
        }
        else if (stars > 35 && stars <= 55)
        {
            league = "Plata";
        }
        else if (stars > 55 && stars <= 75)
        {
            league = "Oro";
        }
        else if (stars > 75 && stars <= 100)
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
