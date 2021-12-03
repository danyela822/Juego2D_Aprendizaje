using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : Reference
{
    public List<GameObject> checkListLogros;

    public Text starsText, leagueText;

    public Image leagueImage;

    public List<Sprite> leagueImages;

    // Start is called before the first frame update
    void Start()
    {
        ShowTotalStars();
        ShowLeague();
        CheckAchievements();
    }
    void ShowTotalStars()
    {
        int stars = App.generalController.statsController.GetTotalStars();
        starsText.text = stars.ToString();
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
            if (App.generalController.statsController.CheckAchievements(i) == true)
            {
                checkListLogros[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
