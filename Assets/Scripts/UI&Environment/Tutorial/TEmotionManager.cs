using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TEmotionManager : MonoBehaviour
{
    public RectTransform PosBar, NegBar, SafeZone;
    public TextMeshProUGUI positiveEmotionTXT, enemyEmotionTXT;

    public float startMinThreshold, startMaxThreshold;//Setting values that should be altered
    public int minDifference, maxDifference;
    float minThreshold, maxThreshold;
    float currentThreshold;


    private void Awake()
    {
        int temp = Random.Range(0, 5);

        //enemyEmotionTXT.text = emotion.currentType;
        enemyEmotionTXT.text = "???";
        positiveEmotionTXT.text = "???";

        //Safe zone & Size
        float addRand = Random.Range(minDifference, maxDifference);
        //Debug.Log(addRand);
        minThreshold = Random.Range(20, 50);
        maxThreshold = minThreshold + addRand;
        currentThreshold = Random.Range(startMinThreshold, startMaxThreshold);
        SafeZone.sizeDelta = new Vector2((addRand / 100) * 600, 23);

        //Safe zone position
        //float safeZoneMidOffeset = (addRand / 2);
        //float safeZoneMidpointX = (minThreshold + safeZoneMidOffeset);
        //float safeZoneOffset = 0;
        //float safeZoneMidtoMax = 0;
        //if (safeZoneMidpointX > 50)
        //{
        //    safeZoneOffset = 300;
        //    safeZoneMidtoMax = safeZoneMidpointX - 50;
        //}
        //else
        //{
        //    safeZoneOffset = -300;
        //    safeZoneMidtoMax = 50 - safeZoneMidpointX;
        //}
        SafeZone.anchoredPosition = new Vector2(-50, 296);
        updateEmotionBar();
    }
    public void updateEmotionBar()
    {
        PosBar.sizeDelta = new Vector2((currentThreshold / 100) * 600, 15);
        NegBar.sizeDelta = new Vector2(((100 - currentThreshold) / 100) * 600, 15);

    }
}
