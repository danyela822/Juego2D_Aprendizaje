using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : Reference
{
    public List<GameObject> checkListLogros;

    public Text starsText, leagueText;

    public Image leagueImage;

    public List<Sprite> leagueImages;

    void Start()
    {
        ShowTotalStars();
        ShowLeague();
        CheckAchievements();
    }
    void ShowTotalStars()
    {
        starsText.text = App.generalController.statsController.GetTotalStars().ToString();
    }
    void ShowLeague()
    {
        string league = App.generalController.statsController.GetLeague();

        leagueText.text = league;

        for(int i = 0; i < leagueImages.Count; i++)
        {
            Sprite currentImage = leagueImages[i];
            if (currentImage.name == league.ToLower())
            {
                leagueImage.sprite = currentImage;
            }
        }

    }
    public void CheckAchievements()
    {
        for (int i = 0; i < checkListLogros.Count; i++)
        {
            if (!(App.generalController.statsController.GetAchievementsList().Contains(i)))
            {
                checkListLogros[i].GetComponent<Image>().enabled = true;
            }    
        }
    }
}
