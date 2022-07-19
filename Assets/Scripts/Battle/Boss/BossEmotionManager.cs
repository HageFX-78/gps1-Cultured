using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEmotionManager : MonoBehaviour
{
    public Emotion[] emotionList;
    public EmotionManager emotionManager;
    public RectTransform PosBar, NegBar, SafeZone;
    public TextMeshProUGUI positiveEmotionTxt, enemyEmotionTxt;

}
