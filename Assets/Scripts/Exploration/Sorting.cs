using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [SerializeField] private GameObject playerTest;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Set to true if dealing with horizontal prop")]
    [SerializeField] private bool horizontal;
    private void Start()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
        playerTest = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (this.transform.position.y < playerTest.transform.position.y && !horizontal)
        {
            sprite.sortingOrder = 3;
        }
        else if (!horizontal)
        {
            sprite.sortingOrder = 1;
        }

        if (this.transform.position.x < playerTest.transform.position.x && horizontal)
        {
            sprite.sortingOrder = 3;
        }
        else if (horizontal)
        {
            sprite.sortingOrder = 1;
        }
    }
}
