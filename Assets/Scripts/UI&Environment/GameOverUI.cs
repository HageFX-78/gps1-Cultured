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
        SceneManager.LoadSceneAsync((int)sceneIndex.MAINMENU);
    }
    
}
