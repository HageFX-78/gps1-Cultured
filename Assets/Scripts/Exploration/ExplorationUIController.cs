using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExplorationUIController : MonoBehaviour
{
    [Header("Scene Switch")]
    [SerializeField] private int sceneNum;
    [Header("Return to Main")]
    [SerializeField] private TextMeshProUGUI volumeTXT = null;

    public void Back()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
