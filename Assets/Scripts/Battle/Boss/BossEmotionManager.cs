using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEmotionManager : MonoBehaviour
{
    [Header("Dialogue Trigger")]
    public DialogueTrigger dialogueTrigger;

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
    public RectTransform safeLeft;
    public RectTransform safeRight; 
    public RectTransform emoPointer;
    public TextMeshProUGUI turnCounterText;

    [Header("Emo Bar Move Settings")]
    [SerializeField] float moveEmoBarLoopCooldown;
    [SerializeField] float moveEmoBarLoopSplit;

    private int phaseIndex;

    private void Start()
    {
        phase1 = true;
        phase2 = false;
        InitBoss();
    }

    private void Update()
    {
        turnCounterText.text = "Turns Left: " + turnCounter;

        if(turnCounter <=0)
        {
            if(checkTargetThreshold() == false)
            {
                Debug.Log("GAME OVER");
            }
            else
            {
                if(phase1)
                {
                    phase1 = false;
                    phase2 = true;
                    //init boss for phase 2
                    InitBoss();
                    BossDialogueManager.instance.EnterDialogueMode(dialogueTrigger.phase2Dialogue);
                }
            }
        }
    }
   

    void InitBoss()
    {
        //takes the list of fields and sets the currentType based on the list
        phaseIndex = 0;
        emotion.currentType = bossPhases[phaseIndex];
        if(phase1)
        {
            turnCounter = phase1Turn;
        }
        else if(phase2)
        {
            turnCounter = phase2Turn;
        }

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

            //below follwing not updated
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
        /*float addRand = Random.Range(enemyMinSafeZone, enemyMaxSafeZone);

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

        SafeZone.anchoredPosition = new Vector2((safeZoneMidtoMax / 100) * safeZoneOffset, 296);*/
        updateEmotionBar();

        safeLeft.anchoredPosition = new Vector2((playerMinThreshold >= 50 ? (((playerMinThreshold - 50) / 100) * 600) : ((50 - playerMinThreshold) / 100) * -600), 296);
        safeRight.anchoredPosition = new Vector2((playerMaxThreshold >= 50 ? ((playerMaxThreshold - 50) / 100 * 600) : (50 - playerMaxThreshold) / 100 * -600), 296);
    }

    public void DealDamage(float baseDamage, string damageType)// not completed
    {
        currentThreshold += baseDamage * emotion.TypeMultiplier[damageType];
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        //updateEmotionBar();
        StartCoroutine(moveEmoPointer());
        //CurrentEmotionBar();//Logging only - comment when we done
        //Debug.Log($"current = {currentThreshold}, dmg dealt {baseDamage * emotion.TypeMultiplier[damageType]}");
    }

    public void Recover(float recoverAmount)
    {
        currentThreshold -= recoverAmount * 1;
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        //updateEmotionBar();
        StartCoroutine(moveEmoPointer());
    }

    public void updateEmotionBar()
    {
        //PosBar.sizeDelta = new Vector2((currentThreshold / 100) * 600, 15);
        //NegBar.sizeDelta = new Vector2(((100 - currentThreshold) / 100) * 600, 15);
        emoPointer.anchoredPosition = new Vector2((currentThreshold >= 50 ? (((currentThreshold - 50) / 100) * 600) : ((50 - currentThreshold) / 100) * -600), 296);
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

    IEnumerator moveEmoPointer()
    {
        float newXVal = (currentThreshold >= 50 ? (((currentThreshold - 50) / 100) * 600) : ((50 - currentThreshold) / 100) * -600);
        float oldXVal = emoPointer.anchoredPosition.x;

        float incrementVal = (newXVal > oldXVal ? newXVal - oldXVal : oldXVal - newXVal) / moveEmoBarLoopSplit;

        if (newXVal > oldXVal)
        {
            while (emoPointer.anchoredPosition.x < newXVal)
            {
                emoPointer.anchoredPosition = new Vector2(emoPointer.anchoredPosition.x + incrementVal, 296);
                yield return new WaitForSeconds(moveEmoBarLoopCooldown);
            }
        }
        else
        {
            while (emoPointer.anchoredPosition.x > newXVal)
            {
                emoPointer.anchoredPosition = new Vector2(emoPointer.anchoredPosition.x - incrementVal, 296);
                yield return new WaitForSeconds(moveEmoBarLoopCooldown);
            }
        }
        emoPointer.anchoredPosition = new Vector2(newXVal, 296);

    }
}
