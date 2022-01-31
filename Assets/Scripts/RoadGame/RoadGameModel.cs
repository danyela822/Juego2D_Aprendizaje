using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoadGameModel : Reference
{
    int solutionTickets;
    int totalPoints;

    //Para las estadisticas

    float totalTime;
    int totalStars;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "MiniGamesScene")
        {
            totalTime += GetTime();
            totalTime += Time.deltaTime;
            SetTime(totalTime);
        }
    }

    public float GetTime()
    {
        totalTime = PlayerPrefs.GetFloat("TotalTime", 0);
        return totalTime;
    }

    public void SetTime(float time)
    {
        PlayerPrefs.SetFloat("TotalTime", time);
    }

    public int GetTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStars", 0);
        return totalStars;
    }

    public void SetTotalStars(float stars)
    {
        PlayerPrefs.GetFloat("TotalStars", stars);
    }

    public Sprite GetStartsImage(int totalStarts)
    {
        Sprite startsImage = Resources.Load<Sprite>("Stars/" + totalStarts);
        return startsImage;
    }
    // Start is called before the first frame update
    public void IncreaseTickets()
    {
        SetTickets(GetTickets()+1);
    }

    public void DecraseTickets()
    {
        if(GetTickets() > 0)
        {
            SetTickets(GetTickets()-1);
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

    // Puntos
    public int GetPoints()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPoints", 0);
        return totalPoints;
    }

    public void SetPoints(int valor)
    {
        PlayerPrefs.SetInt("TotalPoints", valor);
    }

    public Sprite[,] GetMapFloor(string theme)
    {
        Sprite[] spriteslist;
        Sprite[,] mapFloor = new Sprite[8, 6];
        
        int index = 0;

        if (theme == "Castle")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Castle/dirt");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Forest/grass");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Sea/sand");
        }

        for (int i = 0; i < mapFloor.GetLength(0); i++)
        {

            for (int j = 0; j < mapFloor.GetLength(1); j++)
            {
                mapFloor[i, j] = spriteslist[index];
                index++;
            }
        }
        return mapFloor;
    }

    public Sprite[,] GetMapLock(string theme)
    {
        Sprite[] spriteslist;
        Sprite[,] mapLock = new Sprite[8, 6];

        int index = 0;

        if (theme == "Castle")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Castle/fence");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Forest/rocks");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/Sea/water");
        }

        for (int i = 0; i < mapLock.GetLength(0); i++)
        {

            for (int j = 0; j < mapLock.GetLength(1); j++)
            {
                mapLock[i, j] = spriteslist[index];
                index++;
            }
        }
        return mapLock;
    }
    public Sprite GetMapFinish(string theme)
    {
        Sprite mapFinish;
        if (theme == "Castle")
        {
            mapFinish = Resources.Load<Sprite>("Map/Castle/fence");
        }
        else if (theme == "Forest")
        {
            mapFinish = Resources.Load<Sprite>("Map/Forest/rocks");
        }
        else
        {
            mapFinish = Resources.Load<Sprite>("Map/Sea/water");
        }
        return mapFinish;
    }
    /// <summary>
    /// Metodo que devuelve el nivel actual del juego
    /// </summary>
    /// <returns>Int del nivel del juego</returns>
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Game5Levels", 1);
    }
    /// <summary>
    /// Metodo que actualiza el nivel del juego
    /// </summary>
    /// <param name="level">Nivel del juego</param>
    public void UpdateLevel(int level)
    {
        PlayerPrefs.SetInt("Game5Levels", level);
    }
}







public class Objects
{
    public int x { get; set; }
    public int y { get; set; }
    public int type { get; set; }

    /*Metodo constructor de la clase objetos que recibe tres parametros
    * row -> posicion en X donde esta ubicado el Objeto
    * col -> posicion en Y donde esta ubicado el objeto
    * t -> tipo de objeto que es (0 -> piso, 1 -> limite, 2 -> punto Llegada, 3 -> Personaje Principal)
    */
    public Objects (int row, int col, int t)
    {
        this.x = row;
        this.y = col;
        this.type = t;
    }

}