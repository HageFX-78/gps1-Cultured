using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRemnant : MonoBehaviour
{
    [SerializeField] private GameObject remnant;
    private bool onTop;
    private void Update()
    {
        if (onTop && Input.GetKeyDown(KeyCode.Space))
        {
            remnant.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        onTop = true;
    }
}
