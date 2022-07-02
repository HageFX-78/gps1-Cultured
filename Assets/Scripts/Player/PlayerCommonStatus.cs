using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCommonStatus
{
    public static float sanityValue = 21;//Default static value

    //Anything else that is carried over to other scenes can be added here

    public static void modifySanity(float mod)
    {
        sanityValue = Mathf.Clamp(sanityValue+mod, 0, 100);
    }

    public static float getSanity()
    {
        return sanityValue;
    }
}
