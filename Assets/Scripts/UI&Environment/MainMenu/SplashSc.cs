using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashSc : MonoBehaviour
{ 
    public GameObject overlay;
    public SpriteRenderer color;
    public VideoPlayer video;
    public bool isDone;


    private void OnEnable()
    {
        video.Prepare();
        video.prepareCompleted += prepareCompleted;
        video.loopPointReached += loopPointReached;
    }
    
    void prepareCompleted(VideoPlayer v)
    {
        v.Play();
    }
    void loopPointReached(VideoPlayer v)
    {
        isDone = true;
    }
    private void OnDisable()
    {
        video.prepareCompleted -= prepareCompleted;
        video.loopPointReached -= loopPointReached;
    }

    private void Update()
    {
        if (isDone)
        {
            SceneManager.LoadScene((int)sceneIndex.MAINMENU);
        }
    }
}
