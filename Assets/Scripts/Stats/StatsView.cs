using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : Reference
{
    public List<GameObject> checkAchievementsList;

    public Text starsText, leagueText;

    public Image imageLeague;

    public List<Sprite> leagueImages;

    void Start()
    {
        ShowTotalStars();
        ShowLeague();
        //CheckAchievements();
    }
    void ShowTotalStars()
    {
        starsText.text = App.generalController.statsController.GetTotalStars().ToString();
    }
    /// <summary>
    /// Metodo para mostar la liga actual del jugador
    /// </summary>
    void ShowLeague()
    {
        string league = App.generalController.statsController.GetLeague();

        //Sprite imageLeague = App.generalController.statsController.GetLeagueImage(league);

        imageLeague.sprite = App.generalController.statsController.GetLeagueImage(league);

        leagueText.text = league;

        /*for(int i = 0; i < leagueImages.Count; i++)
        {
            Sprite currentImage = leagueImages[i];
            if (currentImage.name == league.ToLower())
            {
                leagueImage.sprite = currentImage;
            }
        }*/

    }
    /// <summary>
    /// Metodo para marcar los logros completados
    /// </summary>
    /*public void CheckAchievements()
    {
        for (int i = 0; i < checkAchievementsList.Count; i++)
        {
            //Verificar si el logro ya ha sido alcanzado
            if (!(App.generalController.statsController.GetAchievementsList(i).Contains(i)))
            {
                //Si el logro ya se completo muestra en la pantala
                checkAchievementsList[i].GetComponent<Image>().enabled = true;
            }    
        }
    }*/
}
