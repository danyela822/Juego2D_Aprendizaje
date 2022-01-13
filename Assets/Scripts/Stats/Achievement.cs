using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public int id { get; set; }
    public int conditionComplete { get; set; }
    public bool complete { get; set; }

    public Achievement(int id, int condtionComplete, bool complete)
    {
        this.id = id;
        this.conditionComplete = condtionComplete;
        this.complete = complete;
    }
}
