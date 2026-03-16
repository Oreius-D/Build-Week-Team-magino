using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    [SerializeField] private int banana;
    [SerializeField] private List<string> passiveID = new List<string>();
    [SerializeField] private List<string> consumableID = new List<string>();
    public int Banana
    {
        get => banana;
        set => banana = value;
    }
    public List<string> PassiveID
    {
        get => passiveID;
        set => passiveID = value;
    }
    public List<string> ConsumableID
    {
        get => consumableID;
        set => consumableID = value;
    }
}
