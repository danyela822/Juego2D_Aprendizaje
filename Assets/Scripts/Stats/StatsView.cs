using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : Reference
{
    public List<GameObject> checkListLogros;

    public Text starsText, leagueText;

    public Image leagueImage;

    // Start is called before the first frame update
    void Start()
    {
        ShowTotalStars();
        ShowLeague();
    }
    void ShowTotalStars()
    {
        int stars = App.generalController.statsController.GetTotalStars();
        //int stars = PlayerPrefs.GetInt("TotalStarsGame1", 0) + PlayerPrefs.GetInt("TotalStarsGame2", 0); ;
        starsText.text = stars.ToString();
    }
    void ShowLeague()
    {
        string league = App.generalController.statsController.GetLeague();
        leagueText.text = league;
    }
}
