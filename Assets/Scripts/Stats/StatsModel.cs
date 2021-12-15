using System.Collections.Generic;
using UnityEngine;

public class StatsModel : Reference
{
    public FileLists file;
    string currentLeague;

    public string GetLeague()
    {
        currentLeague = "";

        int stars = GetTotalStars();

        if (stars >= 15 && stars < 45)
        {
            currentLeague = "Hierro";
        }
        else if (stars >= 45 && stars < 85)
        {
            currentLeague = "Bronce";
        }
        else if (stars >= 85 && stars < 145)
        {
            currentLeague = "Plata";
        }
        else if (stars >= 145 && stars < 215)
        {
            currentLeague = "Oro";
        }
        else if (stars >= 215)
        {
            currentLeague = "Diamante";
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
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                        file.Save("P");
                }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 3 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 3)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 3 niveles de un juego");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 6 || PlayerPrefs.GetInt("GetThreeStars2", 0) >= 6)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en 6 niveles de un juego");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 3:
                    if (PlayerPrefs.GetInt("GetThreeStars1", 0) >= 10 && PlayerPrefs.GetInt("GetThreeStars2", 0) >= 10)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén 3 estrellas en todos los niveles de un juego");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 4:
                    //Obtener un pase de solucion
                    if (PlayerPrefs.GetInt("GetTicket", 0) == 1)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Obtén un pase de solución");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 5:
                    //Usar un pase de solucion
                    if (PlayerPrefs.GetInt("UseTicket", 0) == 1)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Usa un pase de solución");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 6:
                    //3 niveles seguidos
                    if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 3 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 3)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Completa 3 niveles seguidos sin errores");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 7:
                    //10 niveles seguidos
                    if (PlayerPrefs.GetInt("PerfectGame1", 0) >= 10 || PlayerPrefs.GetInt("PerfectGame2", 0) >= 10)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Completa 10 niveles seguidos sin errores");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 8:
                    //300 puntos en 3 juegos
                    if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 2)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO: Alcanza 300 puntos en 3 juegos");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 9:
                    //300 puntos en 6 juegos
                    if (PlayerPrefs.GetInt("ThreeTundredPoints", 0) == 6)
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 10:
                    //Liga hierro
                    if (currentLeague == "Hierro")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 11:
                    //Liga bronce
                    if (currentLeague == "Bronce")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 12:
                    //Liga plata
                    if (currentLeague == "Plata")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 13:
                    //Liga oro
                    if (currentLeague == "Oro")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
                    file.Save("P");
                }
                    break;
                case 14:
                    //Liga Diamante
                    if (currentLeague == "Diamante")
                    {
                        Debug.Log("CONSIGUIO EL LOGRO");
                        App.generalModel.statsModel.file.achievementsList.Remove(numero);
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
