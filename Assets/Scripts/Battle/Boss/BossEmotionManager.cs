using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossEmotionManager : MonoBehaviour
{
    [Header("Script References")]
    public DialogueTrigger dialogueTrigger;
    public ScreenShake enemyShake;

    [Header("Boss Emotion (Phases)")]
    [SerializeField] private string[] bossPhases;
    public Emotion emotion = new();
    public int phase1Turn;
    public int phase2Turn;
    public static int turnCounter;
    public bool phase1;
    public bool phase2;
    public bool gameOver;

    [Header("Boss Emotion Details")]
    public float playerMinThreshold;
    public float playerMaxThreshold;//Setting values that should be altered
    public int enemyMinSafeZone, enemyMaxSafeZone;
    public float pointerStart, pointerEnd;

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
        gameOver = false;
        InitBoss();
        Time.timeScale = 1;
    }

    private void Update()
    {
        turnCounterText.text = "Turns Left: " + turnCounter;

        if(turnCounter <= 0)
        {
            if(checkTargetThreshold() == false && gameOver == false)
            {
                gameOver = true;
                BossDialogueManager.instance.EnterDialogueMode(dialogueTrigger.gameOver);
            }
            else
            {
                if(phase1 && gameOver == false)
                {
                    phase1 = false;
                    phase2 = true;
                    //init boss for phase 2
                    InitBoss();
                    BossDialogueManager.instance.EnterDialogueMode(dialogueTrigger.phase2Dialogue);
                }
            }

            if(gameOver)
            {
                if (BossDialogueManager.instance.storyIsPlaying == false)
                {
                    SceneManager.LoadScene((int)sceneIndex.GAMEOVER);
                }
            }
        }

        //Debug.Log($"Min max L {tempMinThreshold}, {tempMaxThreshold} == Current : {currentThreshold}");
    }


    void InitBoss()
    {
        BossDialogueManager.instance.firstTurn = true;

        float addRand = Random.Range(enemyMinSafeZone, enemyMaxSafeZone);

        tempMinThreshold = Random.Range(50, 70);
        tempMaxThreshold = tempMinThreshold + addRand;
        currentThreshold = Random.Range(pointerStart, pointerEnd);

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

        emoPointer.anchoredPosition = new Vector2((currentThreshold >= 50 ? (((currentThreshold - 50) / 100) * 600) : ((50 - currentThreshold) / 100) * -600), 296);

        safeLeft.anchoredPosition = new Vector2((tempMinThreshold >= 50 ? (((tempMinThreshold - 50) / 100) * 600) : ((50 - tempMinThreshold) / 100) * -600), 296);
        safeRight.anchoredPosition = new Vector2((tempMaxThreshold >= 50 ? ((tempMaxThreshold - 50) / 100 * 600) : (50 - tempMaxThreshold) / 100 * -600), 296);
        Debug.Log($"Emotion type: {emotion.currentType}");
    }

    public void DealDamage(float baseDamage, string damageType)// not completed
    {
        currentThreshold += baseDamage * emotion.TypeMultiplier[damageType];
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);

        if(emotion.TypeMultiplier[damageType] == 1.0f)
        {
            enemyShake.ShakeScreen(0.2f);
        }
        if (emotion.TypeMultiplier[damageType] == 1.5f)
        {
            enemyShake.ShakeScreen(0.3f, 0.6f);
        }
        else
        {
            enemyShake.ShakeScreen(0.2f, 0.1f);
        }

        StartCoroutine(moveEmoPointer());

    }

    public void Recover(float recoverAmount)
    {
        currentThreshold -= recoverAmount * 1;
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        StartCoroutine(moveEmoPointer());
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

    IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
