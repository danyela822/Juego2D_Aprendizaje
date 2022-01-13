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
    }
}
