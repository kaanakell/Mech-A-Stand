using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Flag
{
    public string Name;
    public bool state;
}

[CreateAssetMenu]
public class FlagsTable : ScriptableObject
{
    public List<Flag> flags;

    public Flag GetFlag(string nameOfFlag)
    {
        return flags.Find(x => x.Name == nameOfFlag);
    }
}
