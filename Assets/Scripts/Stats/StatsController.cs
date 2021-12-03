using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : Reference
{
    string league;
    public string GetLeague()
    {
        league = "";

        int stars = GetTotalStars();

        if (stars >= 15 && stars < 45)
        {
            league = "Hierro";
        }
        else if (stars >=45 && stars < 85)
        {
            league = "Bronce";
        }
        else if (stars >= 85 && stars < 145)
        {
            league = "Plata";
        }
        else if (stars >= 145 && stars < 215)
        {
            league = "Oro";
        }
        else if (stars >= 215)
        {
            league = "Diamante";
        }
        return league;
    }
    public int GetTotalStars()
    {
        int stars = PlayerPrefs.GetInt("TotalStarsGame1", 0) + PlayerPrefs.GetInt("TotalStarsGame2", 0);
        return stars;
    }
    public bool CheckAchievements(int numeroLogro)
    {
        switch (numeroLogro)
        {
            case 0:
                if (PlayerPrefs.GetInt("PlayOneLevel", 0) == 2)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Juega un nivel de cada juego");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Juega un nivel de cada juego");
                    return false;
                }
            case 1:
                if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 3 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 3)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 3 niveles de un juego");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Obtén 3 estrellas en 3 niveles de un juego");
                    return false;
                }
            case 2:
                if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 6 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 6)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 6 niveles de un juego");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Obtén 3 estrellas en 6 niveles de un juego");
                    return false;
                }
            case 3:
                if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 10 && PlayerPrefs.GetInt("GetThreeStars2", 0) >= 10)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en todos los niveles de un juego");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Obtén 3 estrellas en todos los niveles de un juego");
                    return false;
                }
            case 4:
                //Obtener un pase de solucion
                if (PlayerPrefs.GetInt("GetTicket", 0) == 1)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Obtén un pase de solución");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Obtén un pase de solución");
                    return false;
                }
            case 5:
                //Usar un pase de solucion
                if (PlayerPrefs.GetInt("UseTicket", 0) == 1)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Usa un pase de solución");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Usa un pase de solución");
                    return false;
                }
            case 6:
                //3 niveles seguidos
                if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 3 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 3)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Completa 3 niveles seguidos sin errores");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Completa 3 niveles seguidos sin errores");
                    return false;
                }
            case 7:
                //10 niveles seguidos
                if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 10 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 10)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Completa 10 niveles seguidos sin errores");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Completa 10 niveles seguidos sin errores");
                    return false;
                }
            case 8:
                //300 puntos en 3 juegos
                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 2)
                {
                    Debug.Log("CONSIGUIO EL LOGRO: Alcanza 300 puntos en 3 juegos");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO: Alcanza 300 puntos en 3 juegos");
                    return false;
                }
            case 9:
                //300 puntos en 6 juegos
                if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 6)
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            case 10:
                //Liga hierro
                if (league == "Hierro")
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            case 11:
                //Liga bronce
                if (league == "Bronce")
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            case 12:
                //Liga plata
                if (league == "Plata")
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            case 13:
                //Liga oro
                if (league == "Oro")
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            case 14:
                //Liga Diamante
                if (league == "Diamante")
                {
                    Debug.Log("CONSIGUIO EL LOGRO");
                    return true;
                }
                else
                {
                    Debug.Log("NO CONSIGUIO EL LOGRO");
                    return false;
                }
            default:
                return false;
        }
    }
}
