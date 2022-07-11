using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRemnant : MonoBehaviour
{
    public static bool collect;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected");
            collect = true;
            this.gameObject.SetActive(false);
        }
    }
}
