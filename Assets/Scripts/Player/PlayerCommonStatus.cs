using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCommonStatus
{
    public static float sanityValue = 100;//Default static value
    public static int runCount = 0;//Times ran from battle

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

    public static int getRunCount()
    {
        return runCount;
    }
}
