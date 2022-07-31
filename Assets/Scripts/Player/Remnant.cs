using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Remnant
{
    public string remnantName;//Kinda like ID
    public string remnantDescription;
    public bool acquired;
    public int remnantSceneIndex;
    public Sprite remnantSprite;
    public Remnant(string remName, string remDes, int remIndex, Sprite remSprite)
    {
        remnantName = remName;
        remnantDescription = remDes;
        acquired = false;
        remnantSceneIndex = remIndex;
        remnantSprite = remSprite;
    }
}
