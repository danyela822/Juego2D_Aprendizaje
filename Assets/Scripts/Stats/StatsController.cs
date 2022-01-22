using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : Reference
{
    public Text leagueText, pointsText, starsText;
    public Image leagueImage;
    public List<GameObject> achievements;

    private void Start()
    {
        Debug.Log("START STATS");
    }
    /// <summary>
    /// Metodo para verificar si el un logo ya ha sido completado
    /// </summary>
    /// <param name="numberAchievement">Int nuemero del logro</param>
    /// <returns>bool que indica si el logro esta completado o no</returns>
    public bool IsAchievements(int numberAchievement)
    {
        if (App.generalModel.statsModel.file.achievementsList.Contains(numberAchievement))
        {
            Debug.Log("EL logro: "+numberAchievement+" aun no se ha completado");
            return false;
        }
        else
        {
            Debug.Log("EL logro: " + numberAchievement + " ya se completo");
            return true;
        }
    }
    /// <summary>
    /// Metodo para borrar un logro que ya ha sido completado
    /// </summary>
    /// <param name="numberAchievement">Int nuemero del logro</param>
    public void DeleteAchievements(int numberAchievement)
    {
        Debug.Log("EL logro: " + numberAchievement + " se ha completado");
        App.generalModel.statsModel.file.achievementsList.Remove(numberAchievement);
        App.generalModel.statsModel.file.Save("P");
    }
    /// <summary>
    /// Metodo para mostar la liga actual del jugador
    /// </summary>
    public void ShowLeague()
    {
        leagueText.text = App.generalModel.statsModel.GetLeague();
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
    public void ShowLeagueImage(string league)
    {
        leagueImage.sprite = App.generalModel.statsModel.LoadLeagueImage(league);
    }
    /// <summary>
    /// Metodo para marcar los logros completados
    /// </summary>
    public void ShowAchievements()
    {
        for (int i = 0; i < achievements.Count; i++)
        {
            //Verificar si el logro ya ha sido alcanzado
            if (!(App.generalModel.statsModel.file.achievementsList.Contains(i)))
            {
                //Si el logro ya se completo muestra en la pantala
                achievements[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
