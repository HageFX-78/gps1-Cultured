using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RemnantBehaviour : MonoBehaviour
{
    [SerializeField] string remnantName;
    [TextArea][SerializeField] string remnantDescription;
    [SerializeField] int remnantSceneIndex;
    Sprite remSprite;

    public bool inRange;
    //ITS A TRIGGER REMEMBER TO CHECK TRIGGER
    void Start()
    {
        inRange = false;
        gameObject.name = remnantName;
        remSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        if(PlayerCommonStatus.checkIfRemnantExist(remnantName))
        {
            if(PlayerCommonStatus.checkRemnantAcquired(remnantName))
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Remnant thisRem = new Remnant(remnantName, remnantDescription, remnantSceneIndex, remSprite);
            PlayerCommonStatus.addRemnant(thisRem);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))&&inRange)
        {
            //Enable Ui
            ExplorationHUD.expHUD.showRemnant(remnantSceneIndex);
            PlayerCommonStatus.acquireRemnant(remnantName);
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
