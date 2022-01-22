using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketModel : Reference
{
    //public int solutionTickets;
    public int GetTickets()
    {
        //solutionTickets = PlayerPrefs.GetInt("SolutionTickets", 0);

        return PlayerPrefs.GetInt("SolutionTickets", 0); ;
    }
    public void SetTickets(int valor)
    {
        PlayerPrefs.SetInt("SolutionTickets", valor);

        if (GetTickets() == 1)
        {
            PlayerPrefs.SetInt("GetTicket", PlayerPrefs.GetInt("GetTicket", 0) + 1);

            //Verificar si cumplio el logro 5: Obtener un pase de solucion
            if (PlayerPrefs.GetInt("GetTicket", 0) == 1)
            {
                if (!App.generalController.statsController.IsAchievements(5))
                {
                    App.generalController.statsController.DeleteAchievements(5);
                }
            }
        }
    }
}
