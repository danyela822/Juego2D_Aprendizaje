public class Character
{
    public string NameCharacter { get; set; }
    public string Theme { get; set; }
    public int Type { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int PosArrayX { get; set; }
    public int PosArrayY { get; set; }
    public int NumCharacter { get; set; }
    public Character(string nameCharacter, string theme, int type, float x, float y,int posArrayX, int posArrayY, int numCharacter)
    {
        this.NameCharacter = nameCharacter;
        this.Theme = theme;
        this.Type = type;
        this.X = x;
        this.Y = y;
        this.PosArrayX = posArrayX;
        this.PosArrayY = posArrayY;
        this.NumCharacter = numCharacter;
    }
}
