using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetPlayer;

    public bool isCutScene = false;


    void Start()
    {
        
    }

    
    void Update()
    {
        Debug.Log(isCutScene);

        if(!isCutScene) //if we are not loading a cutscene (move camera to reveal something on map), track player
        {
            trackPlayer();
        }
    }
    
    private void trackPlayer()
    {
        transform.position = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, transform.position.z);
    }

    private void cutScene()
    {

    }

}
