using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCommonStatus
{
    public static Dictionary<string, Remnant> remDic = new Dictionary<string, Remnant>();
    public static float sanityValue = 100;//Default static value
    public static int runCount = 0;//Times ran from battle
    public static int runChance = 70;
    public static int typeBeepChance = 5;//Chance of typing beep sound triggering per character
    //Anything else that is carried over to other scenes can be added here

    public static void modifySanity(float mod)
    {
        sanityValue = Mathf.Clamp(sanityValue+mod, 0, 100);
    }

    public static float getSanity()
    {
        return sanityValue;
    }
    
    public static void addRunCount()
    {
        runCount++;
    }
    public static void setRunChance(int ch)
    {
        runChance = Mathf.Clamp(ch, 0, 100);
    }
    public static int getRunCount()
    {
        return runCount;
    }

    public static void addRemnant(Remnant rem)
    {
        remDic.Add(rem.remnantName, rem);
    }
    public static void acquireRemnant(string remName)
    {
        remDic[remName].acquired = true;
    }
    public static bool checkIfRemnantExist(string remName)
    {
        if (remDic.ContainsKey(remName))
        {
            return true;
        }
        else
        {
            return false;
        }    
    }
    public static bool checkRemnantAcquired(string remName)
    {
        return remDic[remName].acquired;
    }
    public static void sacrificeRemnants()// REMEMBER TO CALL THIS IF WE'RE SWITCHING TO LEVEL 2, OR THERE WILL BE MIX OF DESCRIPTIONS
    {
        remDic.Clear();
        runChance = 70;
        sanityValue = 100;
        runCount = 0;
    }

    //----------------------------Get remnant details with scene index
    public static string getRemnantDescriptionWithSceneIndex(int index)
    {
        foreach(string key in remDic.Keys)
        {
            Remnant thisRem = remDic[key];
            if(thisRem.remnantSceneIndex == index)
            {
                return thisRem.remnantDescription;
            }
        }
        return "No description Found";
    }
    public static string getRemnantNameWithSceneIndex(int index)
    {
        foreach (string key in remDic.Keys)
        {
            Remnant thisRem = remDic[key];
            if (thisRem.remnantSceneIndex == index)
            {
                return thisRem.remnantName;
            }
        }
        return "Empty";
    }
    public static Sprite getRemnantSpriteWithSceneIndex(int index)
    {
        foreach (string key in remDic.Keys)
        {
            Remnant thisRem = remDic[key];
            if (thisRem.remnantSceneIndex == index)
            {
                return thisRem.remnantSprite;
            }
        }
        return null;
    }
}
