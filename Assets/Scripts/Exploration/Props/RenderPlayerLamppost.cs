using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPlayerLamppost : MonoBehaviour
{

    [SerializeField] SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            sr.sortingOrder = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            sr.sortingOrder = 1;
    }
}
