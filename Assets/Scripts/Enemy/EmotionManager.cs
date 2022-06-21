using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public Emotion emotion = new Emotion();
    public RectTransform PosBar, NegBar, SafeZone;
    List<string> EmotionList = new List<string>()
    {
        "Delusional", "Hatred", "Self_Loathing", "Despair", "Righteousness"
    };

    public float minDifference, maxDifference, startMinThreshold, startMaxThreshold;//Setting values that should be altered
    float minThreshold, maxThreshold;
    float currentThreshold;


    private void Awake()
    {
        int temp = Random.Range(0, 5);
        emotion.currentType = EmotionList[temp];

        /*emotion.currentType = "Delusional";*/
        gameObject.name = emotion.currentType;


        InitialiseType();
        //Safe zone
        float addRand = Random.Range(minDifference, maxDifference);
        minThreshold = Random.Range(20, 80);
        maxThreshold = minThreshold + addRand;
        currentThreshold = Random.Range(startMinThreshold, startMaxThreshold);
        SafeZone.sizeDelta = new Vector2((addRand / 100) * 600, 15);
        float safeZoneOffset = minThreshold > 50 ? 300 : -300;
        SafeZone.anchoredPosition = new Vector2(((minThreshold+(addRand/2))/100)* safeZoneOffset, 310); 
        updateEmotionBar();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // just initialises the dictionary and debug log
        {
            
            //Debug.Log($"{emotion.currentType}, Rationality: {emotion.TypeMultiplier["Rationality"]}, Love: {emotion.TypeMultiplier["Love"]}, " +$" Hope: { emotion.TypeMultiplier["Hope"]}, Acceptance: { emotion.TypeMultiplier["Acceptance"]}");
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10, "Love");
        }

        //CurrentEmotionBar();
    }

    public void InitialiseType() //checks the enemy current type, and then has the corresponding multipliers
    {
        if(emotion.currentType == "Delusional")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.5f},
                {"Love", 1.0f},
                {"Hope", 1.0f},
                {"Acceptance", -1.0f}
            };
        }
        else if (emotion.currentType == "Hatred")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.0f},
                {"Love", -1.0f},
                {"Hope", 1.0f},
                {"Acceptance", 1.5f}
            };
        }
        else if (emotion.currentType == "Self_Loathing")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.0f},
                {"Love", 1.5f},
                {"Hope", -1.0f},
                {"Acceptance", 1.0f}
            };
        }
        else if (emotion.currentType == "Despair")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", -1.0f},
                {"Love", 1.0f},
                {"Hope", 1.5f},
                {"Acceptance", 1.0f}
            };
        }
        else if (emotion.currentType == "Righteousness")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.5f},
                {"Love", -1.0f},
                {"Hope", 1.0f},
                {"Acceptance", 1.0f}
            };
        }

    }

    public void TakeDamage(float baseDamage, string damageType)// not completed
    {
        currentThreshold += baseDamage * emotion.TypeMultiplier[damageType];
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        updateEmotionBar();
        //Debug.Log($"current = {currentThreshold}, dmg dealt {baseDamage * emotion.TypeMultiplier[damageType]}");
    }

    public void CurrentEmotionBar()
    {
        Debug.Log($"Current Emotion bar: {currentThreshold}");
    }

    public void checkTargetThreshold() //not completed
    {
        if(currentThreshold >= minThreshold && currentThreshold <= maxThreshold)
        {
            Debug.Log("Threshold reached");
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    public void updateEmotionBar()
    {
        PosBar.sizeDelta = new Vector2((currentThreshold/100)*600, 15);
        NegBar.sizeDelta = new Vector2(((100-currentThreshold)/ 100)*600, 15);
    }

}
