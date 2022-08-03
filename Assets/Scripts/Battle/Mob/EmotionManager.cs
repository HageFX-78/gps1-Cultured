using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EmotionManager : MonoBehaviour
{
    public Emotion emotion = new Emotion();
    public RectTransform PosBar, NegBar, SafeZone;
    public TextMeshProUGUI positiveEmotionTXT, enemyEmotionTXT;
    List<string> EmotionList = new List<string>()
    {
        "Delusional", "Hatred", "Self_Loathing", "Despair", "Righteousness"
    };
    [Header("Emo Bar Initial Settings")]
    public float startMinThreshold, startMaxThreshold;//Setting values that should be altered
    public int minDifference, maxDifference;
    public float minThreshold, maxThreshold;
    public float currentThreshold;

    [Header("Emo Bar Move Settings")]
    [SerializeField] float moveEmoBarLoopCooldown;
    [SerializeField] float moveEmoBarLoopSplit;

    [Header("New Emo Indicator References")]
    [SerializeField] RectTransform emoPointer;
    [SerializeField] RectTransform safeL;
    [SerializeField] RectTransform safeR;

    private void Awake()
    {
        int temp = Random.Range(0, 5);
        emotion.currentType = EmotionList[temp];

        /*emotion.currentType = "Delusional";*/
        gameObject.name = emotion.currentType;


        InitialiseType();

        //enemyEmotionTXT.text = emotion.currentType;
        enemyEmotionTXT.text = "???";
        positiveEmotionTXT.text = "???";

        //Safe zone & Size
        float addRand = Random.Range(minDifference, maxDifference);

        minThreshold = Random.Range(20, 50);
        maxThreshold = minThreshold + addRand;
        currentThreshold = Random.Range(startMinThreshold, startMaxThreshold);

/*        //OLD EMOBAR CALCULATION
        SafeZone.sizeDelta = new Vector2((addRand / 100) * 600, 23);

        //Safe zone position
        float safeZoneMidOffeset = (addRand / 2);
        float safeZoneMidpointX = (minThreshold + safeZoneMidOffeset);
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


        updateEmotionBar();//DOnt remove


        //New pointer code
        safeL.anchoredPosition = new Vector2((minThreshold>=50?(((minThreshold - 50)/100)*600):((50- minThreshold) / 100) * -600), 296);
        safeR.anchoredPosition = new Vector2((maxThreshold>=50?((maxThreshold-50)/ 100 * 600) :(50-maxThreshold)/ 100 * -600), 296);

    }

    private void Update()
    {

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
        //updateEmotionBar();
        StartCoroutine(moveEmoPointer());

        //CurrentEmotionBar();//Logging only - comment when we done
        //Debug.Log($"current = {currentThreshold}, dmg dealt {baseDamage * emotion.TypeMultiplier[damageType]}");
    }
    public void selfHarm(float selfDMG)
    {
        currentThreshold -= selfDMG * 1;
        currentThreshold = Mathf.Clamp(currentThreshold, 0, 100);
        //updateEmotionBar();
        StartCoroutine(moveEmoPointer());
    }
    public void CurrentEmotionBar()
    {
        Debug.Log($"Min max L {minThreshold}, {maxThreshold} == Current : {currentThreshold}");
    }

    public bool checkTargetThreshold() //not completed
    {
        if(currentThreshold >= minThreshold && currentThreshold <= maxThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void updateEmotionBar()//DOnt delete, need for initial bar setting
    {
        //PosBar.sizeDelta = new Vector2((currentThreshold/100)*600, 15);
        //NegBar.sizeDelta = new Vector2(((100-currentThreshold)/ 100)*600, 15);
        emoPointer.anchoredPosition = new Vector2((currentThreshold >= 50 ? (((currentThreshold - 50) / 100) * 600) : ((50 - currentThreshold) / 100) * -600), 296);
    }

    public float emotionEffectivenss(string type)
    {
        //Debug.Log(emotion.TypeMultiplier["Love"]);
        return emotion.TypeMultiplier[type];
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

    //OLD MOVE EMOTION BAR
    /*IEnumerator moveEmotionBar()
    {
        float newXVal = new Vector2((currentThreshold / 100) * 600, 15).x;
        float oldXVal = PosBar.sizeDelta.x;

        float incrementVal = (newXVal > oldXVal ? newXVal - oldXVal:oldXVal-newXVal) / moveEmoBarLoopSplit;

        if(newXVal > oldXVal)
        {
            while (PosBar.sizeDelta.x < newXVal)
            {
                PosBar.sizeDelta = new Vector2(PosBar.sizeDelta.x + incrementVal, 15);
                yield return new WaitForSeconds(moveEmoBarLoopCooldown);
            }
        }
        else
        {
            while (PosBar.sizeDelta.x > newXVal)
            {
                PosBar.sizeDelta = new Vector2(PosBar.sizeDelta.x - incrementVal, 15);
                yield return new WaitForSeconds(moveEmoBarLoopCooldown);
            }
        }
        PosBar.sizeDelta = new Vector2(newXVal, 15);

    }*/
}
