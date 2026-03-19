using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scores
{
    private string playerName;
    private int point;

    public string PlayerName 
    {
        get => playerName; set => playerName = value;
    }
    public int Point 
    {
        get => point; set => point = value;
    }
}
