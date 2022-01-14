using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketModel : Reference
{
    public int solutionTickets;
    /*public void IncreaseTickets()
    {
        SetTickets(GetTickets() + 1);
    }

    public void DecraseTickets()
    {
        if (GetTickets() > 0)
        {
            SetTickets(GetTickets() - 1);
        }

    }*/
    public int GetTickets()
    {
        solutionTickets = PlayerPrefs.GetInt("SolutionTickets", 0);

        return solutionTickets;
    }
    public void SetTickets(int valor)
    {
        PlayerPrefs.SetInt("SolutionTickets", valor);

        if (solutionTickets == 1)
        {
            //Se alcanzo el logro 5
            PlayerPrefs.SetInt("GetTicket", PlayerPrefs.GetInt("GetTicket", 0) + 1);

            int logro5 = PlayerPrefs.GetInt("GetTicket", 0);

            if (logro5 == 1)
            {
                if (!App.generalController.statsController.IsAchievements(5))
                {
                    App.generalController.statsController.DeleteAchievements(5);
                }
            }
        }
    }
}
