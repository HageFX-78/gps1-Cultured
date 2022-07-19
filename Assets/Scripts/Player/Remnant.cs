using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remnant
{
    public string remnantName;//Kinda like ID
    public string remnantDescription;
    public bool acquired;

    public Remnant(string remName, string remDes)
    {
        remnantName = remName;
        remnantDescription = remDes;
        acquired = false;
    }
}
