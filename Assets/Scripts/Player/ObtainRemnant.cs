using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainRemnant : MonoBehaviour
{
    private int remnantObtained;

    private void Update()
    {
       // Debug.Log($"remnant obtained: {remnantObtained}");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Remnant"))
        {
            remnantObtained++;
            Destroy(collision.gameObject);
        }
    }
}
