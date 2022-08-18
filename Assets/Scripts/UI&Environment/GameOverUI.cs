using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]private Button gameOver;

    public void Awake()
    {
        gameOver.onClick.AddListener(GameOver);
    }
    public void GameOver()
    {
        PlayerPrefs.SetInt("Load Scene", (int)sceneIndex.LV1);
        PlayerMovement.transitionCount = 0;
        PlayerCommonStatus.sacrificeRemnants();
        SceneManager.LoadSceneAsync((int)sceneIndex.MAINMENU);
    }

}
