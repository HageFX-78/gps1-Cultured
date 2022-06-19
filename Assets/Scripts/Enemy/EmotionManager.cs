using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    Emotion emotion = new Emotion();
    List<string> EmotionList = new List<string>()
    {
        "Delusional", "Hatred", "Self Loathing", "Despair", "Righteousness"
    };

    public float maxThreshold;
    public float currentThreshold;


    private void Awake()
    {
        /*int temp = Random.Range(0, 5);
        emotion.currentType = EmotionList[temp];*/

        emotion.currentType = "Delusional";
        gameObject.name = emotion.currentType;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // just initialises the dictionary and debug log
        {
            InitialiseType();
            Debug.Log($"{emotion.currentType}, {emotion.TypeMultiplier["Rationality"]}, {emotion.TypeMultiplier["Love"]}, { emotion.TypeMultiplier["Hope"]}, { emotion.TypeMultiplier["Acceptance"]}");
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(emotion.currentType, 10, "Rationality");
        }

        CurrentEmotionBar();
    }

    public void InitialiseType() //checks the enemy current type, and then has the corresponding multipliers
    {
        if(emotion.currentType == "Delusional")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.0f},
                {"Love", 0.5f},
                {"Hope", 0.5f},
                {"Acceptance", -1.0f}
            };
        }
        else if (emotion.currentType == "Hatred")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 0.5f},
                {"Love", -1.0f},
                {"Hope", 0.5f},
                {"Acceptance", 1.0f}
            };
        }
        else if (emotion.currentType == "Self Loathing")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 0.5f},
                {"Love", 1.0f},
                {"Hope", -1.0f},
                {"Acceptance", 1.0f}
            };
        }
        else if (emotion.currentType == "Despair")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", -1.0f},
                {"Love", 0.5f},
                {"Hope", 1.0f},
                {"Acceptance", 0.5f}
            };
        }
        else if (emotion.currentType == "Righteousness")
        {
            emotion.TypeMultiplier = new Dictionary<string, float>()
            {
                {"Rationality", 1.0f},
                {"Love", -1.0f},
                {"Hope", 0.5f},
                {"Acceptance", 0.5f}
            };
        }

    }

    public void TakeDamage(string enemyType, float baseDamage, string damageType)
    {
        currentThreshold -= baseDamage * emotion.TypeMultiplier[damageType];
    }

    public void CurrentEmotionBar()
    {
        Debug.Log(currentThreshold);
    }


}
