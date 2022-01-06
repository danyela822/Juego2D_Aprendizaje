using System.Collections.Generic;
using UnityEngine;

public class StatsModel : Reference
{
    public FileLists file;
    string currentLeague;

    public Sprite LoadLeagueImage(string league)
    {
        return Resources.Load<Sprite>("LeaguesImages/" + league);
    }
    public string GetLeague()
    {
        currentLeague = "Sin liga";

        int stars = GetTotalStars();

        if (stars >= 15 && stars < 45)
        {
            currentLeague = "hierro";
        }
        else if (stars >= 45 && stars < 85)
        {
            currentLeague = "bronce";
        }
        else if (stars >= 85 && stars < 145)
        {
            currentLeague = "plata";
        }
        else if (stars >= 145 && stars < 215)
        {
            currentLeague = "oro";
        }
        else if (stars >= 215)
        {
            currentLeague = "diamante";
        }
        return currentLeague;
    }
    public int GetTotalStars()
    {
        int stars = PlayerPrefs.GetInt("TotalStarsGame1", 0) + PlayerPrefs.GetInt("TotalStarsGame2", 0);
        return stars;
    }
    public List<int> CheckAchievements(int numero)
    {
        if (file.achievementsList.Contains(numero))
        {
            switch (numero)
            {
                case 0:
                    if (PlayerPrefs.GetInt("PlayOneLevel", 0) == 2)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Juega un nivel de cada juego");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 3 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 3)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 3 niveles de un juego");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 6 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 6)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 6 niveles de un juego");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 3:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 10 && PlayerPrefs.GetInt("GetThreeStars2", 0) >= 10)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en todos los niveles de un juego");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }   
                    break;
                case 4:
                    //Obtener un pase de solucion
                    if (PlayerPrefs.GetInt("GetTicket", 0) == 1)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén un pase de solución");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 5:
                    //Usar un pase de solucion
                    if (PlayerPrefs.GetInt("UseTicket", 0) == 1)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Usa un pase de solución");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 6:
                    //3 niveles seguidos
                    if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 3 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 3)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Completa 3 niveles seguidos sin errores");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 7:
                    //10 niveles seguidos
                    if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 10 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 10)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Completa 10 niveles seguidos sin errores");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 8:
                    //300 puntos en 3 juegos
                    if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 2)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Alcanza 300 puntos en 3 juegos");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 9:
                    //300 puntos en 6 juegos
                    if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 6)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 10:
                    //Liga hierro
                    if (currentLeague == "hierro")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 11:
                    //Liga bronce
                    if (currentLeague == "bronce")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 12:
                    //Liga plata
                    if (currentLeague == "plata")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 13:
                    //Liga oro
                    if (currentLeague == "oro")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                case 14:
                    //Liga Diamante
                    if (currentLeague == "diamante")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        file.achievementsList.Remove(numero);
                        file.Save("P");
                    }
                    break;
                default:
                    break;
            }
        }
        return file.achievementsList;
    }

}
