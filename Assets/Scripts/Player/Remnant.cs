using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remnant
{
    public string remnantName;//Kinda like ID
    public string remnantDescription;
    public bool acquired;
    public int remnantSceneIndex;

    public Remnant(string remName, string remDes, int remIndex)
    {
        remnantName = remName;
        remnantDescription = remDes;
        acquired = false;
        remnantSceneIndex = remIndex;
    }
}
