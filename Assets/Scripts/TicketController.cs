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

        if (App.generalModel.statsModel.totalPoints >= 100)
        {
            SetTickets(GetTickets() + 1);
            Debug.Log("HAY: " + solutionTickets + " TICKETS");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DecraseTickets()
    {
        if (GetTickets() > 0)
        {
            SetTickets(GetTickets() - 1);
        }

    }
    public int GetTickets()
    {
        solutionTickets = PlayerPrefs.GetInt("SolutionTickets", 0);

        return solutionTickets;
    }
    public void SetTickets(int valor)
    {
        PlayerPrefs.SetInt("SolutionTickets", valor);
    }
}
