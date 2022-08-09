using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour
{
    [SerializeField] public puzzleManager manager;

    [Header("Button ID")]
    public float buttonOrder;
    public SpriteRenderer sprite;
    public Sprite green;
    public Sprite red;
    private bool onTop = false;

    [HideInInspector] public bool activated;
    public int puzzleType;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (onTop)
            {
                switch (puzzleType) 
                {
                    case 1:
                        sprite.sprite = green;
                        manager.ButtonPress(buttonOrder);
                        break;
                    case 2:
                        manager.ButtonPressType2(buttonOrder);
                        break;
                    case 3:
                        if (!manager.puzzleDone) 
                        {
                            StartCoroutine(SwitchColourTemp());
                            manager.ResetPuzzle(); 
                        }
                        break;
                }
            }
        }

        //For reset puzzle button
        if (!manager.puzzleDone) return;
        if (puzzleType != 3) return;
        sprite.sprite = green;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
           onTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            onTop = false;
    }

    private IEnumerator SwitchColourTemp()
    {
        sprite.sprite = green;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            sprite.sprite = red;
        }
    }
}
