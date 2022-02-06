using UnityEngine;

public class TicketModel : Reference
{
    public int GetTickets()
    {
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
                if (!App.generalController.statsController.IsAchievements(4))
                {
                    App.generalController.statsController.DeleteAchievements(4);
                }
            }
        }
    }
}
