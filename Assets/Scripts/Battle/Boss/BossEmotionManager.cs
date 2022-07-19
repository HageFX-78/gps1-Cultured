using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEmotionManager : MonoBehaviour
{
    [Header("Boss Emotion (Phases)")]
    [SerializeField] private string[] bossPhases;
    public Emotion emotion = new();
    public int phase1Turn;
    public int phase2Turn;
    public static int turnCounter;
    public bool phase1;
    public bool phase2;

    [Header("Boss Emotion Details")]
    public float playerMinThreshold;
    public float playerMaxThreshold;//Setting values that should be altered
    public int enemyMinSafeZone, enemyMaxSafeZone;

    private float tempMinThreshold, tempMaxThreshold;
    private float currentThreshold;

    [Header("Damage Related")]
    public float minBaseDamage;
    public float maxBaseDamage;
    public float minSelfRecover;
    public float maxSelfRecover;

    [Header("Emotion Bar Details")]
    public RectTransform PosBar, NegBar, SafeZone;
    public TextMeshProUGUI posEmotionText, enemyEmotionText, turnCounterText;

    private int phaseIndex;

    private void Start()
    {
        InitBoss();
    }

    private void Update()
    {
        turnCounterText.text = "Turns Left: " + turnCounter;

        if(turnCounter <=0)
        {
            if(!checkTargetThreshold())
            {
                Debug.Log("GAME OVER");
            }
            else
            {
                phase1 = false;
                phase2 = true;
            }
        }
    }
   

    void InitBoss()
    {
        //takes the list of fields and sets the currentType based on the list
        posEmotionText.text = "???";
        enemyEmotionText.text = "???";
        phaseIndex = 0;
        emotion.currentType = bossPhases[phaseIndex];
        turnCounter = phase1Turn;
        phase1 = true;

        //sets the multipler based on the currentType set
        switch(emotion.currentType)
        {
            case "Delusional/Despair":
                if(phase1)
                {
                    emotion.TypeMultiplier = new Dictionary<string, float>()
                    {
                        {"Rational", 1.5f},
                        {"Love", -1.0f},
                        {"Hope", 1.0f},
                        {"Acceptance", -1.0f}
                    };
                }
                else if(phase2)
                {
                    emotion.TypeMultiplier = new Dictionary<string, float>()
                    {
                        {"Rational", 1.0f},
                        {"Love", -1.0f},
                        {"Hope", 1.5f},
                        {"Acceptance", -1.0f}
                    };
                }
                
                break;

            case "Hatred":
                emotion.TypeMultiplier = new Dictionary<string, float>()
                {
                    {"Rational", 1.0f},
                    {"Love", -1.0f},
                    {"Hope", 1.0f},
                    {"Acceptance", 1.5f}
                };
                break;

            case "Self_Loathing":
                emotion.TypeMultiplier = new Dictionary<string, float>()
                {
                    {"Rational", 1.0f},
                    {"Love", 1.5f},
                    {"Hope", -1.0f},
                    {"Acceptance", 1.0f}
                };
                break;

            case "Despair":
                emotion.TypeMultiplier = new Dictionary<string, float>()
                {
                    {"Rational", -1.0f},
                    {"Love", 1.0f},
                    {"Hope", 1.5f},
                    {"Acceptance", 1.0f}
                };
                break;

            case "Righteousness":
                emotion.TypeMultiplier = new Dictionary<string, float>()
                {
                    {"Rational", 1.5f},
                    {"Love", -1.0f},
                    {"Hope", 1.0f},
                    {"Acceptance", 1.0f}
                };
                break;
        }

        //sets Safe zone & Size
        float addRand = Random.Range(enemyMinSafeZone, enemyMaxSafeZone);

        tempMinThreshold = Random.Range(20, 50);
        tempMaxThreshold = tempMinThreshold + addRand;
        
        currentThreshold = Random.Range(playerMinThreshold, playerMaxThreshold);
        SafeZone.sizeDelta = new Vector2((addRand / 100) * 600, 23);

        //Safe zone position
        float safeZoneMidOffeset = (addRand / 2);
        float safeZoneMidpointX = (tempMinThreshold + safeZoneMidOffeset);
        float safeZoneOffset = 0;
        float safeZoneMidtoMax = 0;

        if (safeZoneMidpointX > 50)
        {
            safeZoneOffset = 300;
            safeZoneMidtoMax = safeZoneMidpointX - 50;
        }
        else
        {
            safeZoneOffset = -300;
            safeZoneMidtoMax = 50 - safeZoneMidpointX;
        }

        SafeZone.anchoredPosition = new Vector2((safeZoneMidtoMax / 100) * safeZoneOffset, 296);
        updateEmotionBar();
    }

    public void DealDamage(float baseDamage, string damageType)// not completed
    {
        currentThreshold += baseDamage * emotion.TypeMultiplier[damageType];
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        updateEmotionBar();
        CurrentEmotionBar();//Logging only - comment when we done
        //Debug.Log($"current = {currentThreshold}, dmg dealt {baseDamage * emotion.TypeMultiplier[damageType]}");
    }

    public void Recover(float recoverAmount)
    {
        currentThreshold -= recoverAmount * 1;
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        updateEmotionBar();
    }

    public void updateEmotionBar()
    {
        PosBar.sizeDelta = new Vector2((currentThreshold / 100) * 600, 15);
        NegBar.sizeDelta = new Vector2(((100 - currentThreshold) / 100) * 600, 15);
    }

    public bool checkTargetThreshold() //not completed
    {
        if (currentThreshold >= tempMinThreshold && currentThreshold <= tempMaxThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CurrentEmotionBar()
    {
        Debug.Log($"Min max L {tempMinThreshold}, {tempMaxThreshold} == Current : {currentThreshold}");
    }
}
