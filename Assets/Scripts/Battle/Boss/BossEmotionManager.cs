using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEmotionManager : MonoBehaviour
{


    [Header("Boss Emotion (Phases)")]
    [SerializeReference] private string[] bossPhases;
    public Emotion emotion = new();

    [Header("Emotion Bar Details")]
    public RectTransform PosBar, NegBar, SafeZone;
    public TextMeshProUGUI positiveEmotionTxt, enemyEmotionTxt;

    private int index;

    private void Start()
    {
        index = 0;
        emotion.currentType = bossPhases[index];
        Debug.Log(emotion.currentType);
        index++;

        emotion.currentType = bossPhases[index];
        Debug.Log(emotion.currentType);
    }

    public void InitialiseType() //checks the enemy current type, and then has the corresponding multipliers
    {
        if (emotion.currentType == "Delusional")
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
}
