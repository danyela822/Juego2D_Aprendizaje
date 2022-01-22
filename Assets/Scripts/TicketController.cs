using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketController : Reference
{
    public int solutionTickets;

    public bool IncreaseTickets()
    {
        //SetTickets(GetTickets() + 1);
        Debug.Log("HAY: " + App.generalModel.statsModel.GetTotalPoints() + " PUNTOS");

        if (App.generalModel.statsModel.GetTotalPoints() >= 100)
        {
            App.generalModel.ticketModel.SetTickets(App.generalModel.ticketModel.GetTickets() + 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DecraseTickets()
    {
        if (App.generalModel.ticketModel.GetTickets() > 0)
        {
            App.generalModel.ticketModel.SetTickets(App.generalModel.ticketModel.GetTickets() - 1);

            //Verificar si cumplio el logro 6: Usar un pase de solucion
            if (!App.generalController.statsController.IsAchievements(6))
            {
                App.generalController.statsController.DeleteAchievements(6);
            }
        }

    }
    /*public int GetTickets()
    {
        solutionTickets = PlayerPrefs.GetInt("SolutionTickets", 0);
        return solutionTickets;
    }
    public void SetTickets(int valor)
    {
        PlayerPrefs.SetInt("SolutionTickets", valor);
    }*/
}
