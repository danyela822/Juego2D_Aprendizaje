using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string nameCharacter { get; set; }
    public string theme { get; set; }
    public int type { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public int posArrayX { get; set; }
    public int posArrayY { get; set; }
    public int numCharacter { get; set; }
    public Character(string nameCharacter, string theme, int type, float x, float y,int posArrayX, int posArrayY, int numCharacter)
    {
        this.nameCharacter = nameCharacter;
        this.theme = theme;
        this.type = type;
        this.x = x;
        this.y = y;
        this.posArrayX = posArrayX;
        this.posArrayY = posArrayY;
        this.numCharacter = numCharacter;
    }
}
