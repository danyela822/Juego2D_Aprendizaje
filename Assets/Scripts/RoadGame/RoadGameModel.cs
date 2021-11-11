using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadGameModel : Reference
{
    int solutionTickets;
    int totalPoints;

    //Para las estadisticas

    float totalTime;
    int totalStars;

    /*public static RoadGameModel gameModel;
    private void Awake()
    {
        if (gameModel == null)
        {
            gameModel = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameModel != this)
        {
            Destroy(gameObject);
        }
    }*/
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
            spriteslist = Resources.LoadAll<Sprite>("Map/dirt");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/grass");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/sand");
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
            spriteslist = Resources.LoadAll<Sprite>("Map/valla_2");
        }
        else if (theme == "Forest")
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/rocks");
        }
        else
        {
            spriteslist = Resources.LoadAll<Sprite>("Map/water");
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