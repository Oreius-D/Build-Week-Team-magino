using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    [SerializeField] private int banana;
    [SerializeField] private List<SO_PassiveUpgrade> passiveID = new List<SO_PassiveUpgrade>();
    [SerializeField] private List<SO_ConsumableUpgrade> consumableID = new List<SO_ConsumableUpgrade>();

    [SerializeField] private List<Scores> leaderBoard = new List<Scores>();
    public int Banana
    {
        get => banana;
        set => banana = value;
    }
    public List<SO_PassiveUpgrade> PassiveID
    {
        get => passiveID;
        set => passiveID = value;
    }
    public List<SO_ConsumableUpgrade> ConsumableID
    {
        get => consumableID;
        set => consumableID = value;
    }
    
    public List<Scores> LeaderBoard
    {
        get => leaderBoard;
        set => leaderBoard = value;
    }

}
