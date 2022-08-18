using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool onTop;
    public GameObject door;

    public SpriteRenderer thisSR;
    public Sprite onSprite;

    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip SF;

    private void Start()
    {
        thisSR = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        if (onTop)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                audioSrc.clip = SF;
                audioSrc.Play();
                door.SetActive(false);
                thisSR.sprite = onSprite;
                thisSR.flipX = false;
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTop = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        onTop = false;
    }
}
