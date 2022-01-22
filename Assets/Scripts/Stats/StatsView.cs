using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : Reference
{
    public List<GameObject> checkAchievementsList;

    public Text leagueText, pointsText, starsText;

    public Image leagueImage;

    string league;

    void Start()
    {
        ShowLeague();
        ShowTotalStars();
        ShowTotalPoints();
        ShowLeagueImage();
        ShowAchievements();
    }
    /// <summary>
    /// Metodo para mostar la liga actual del jugador
    /// </summary>
    public void ShowLeague()
    {
        league = App.generalModel.statsModel.GetLeague();
        leagueText.text = league;
    }
    /// <summary>
    /// Metodo para mostrar todas las estrellas obtenidas por el jugador
    /// </summary>
    public void ShowTotalStars()
    {
        starsText.text = App.generalModel.statsModel.GetTotalStars().ToString();
    }
    /// <summary>
    /// Metodo para mostrar todos los puntos obtenidos por el jugador
    /// </summary>
    public void ShowTotalPoints()
    {
        pointsText.text = App.generalModel.statsModel.GetTotalPoints().ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="league"></param>
    public void ShowLeagueImage()
    {
        leagueImage.sprite = App.generalModel.statsModel.LoadLeagueImage(league);
    }
    /// <summary>
    /// Metodo para marcar los logros completados
    /// </summary>
    public void ShowAchievements()
    {
        for (int i = 0; i < checkAchievementsList.Count; i++)
        {
            //Verificar si el logro ya ha sido alcanzado
            if (!(App.generalModel.statsModel.file.achievementsList.Contains(i)))
            {
                //Si el logro ya se completo muestra en la pantala
                checkAchievementsList[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
