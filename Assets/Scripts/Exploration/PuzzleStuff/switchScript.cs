using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour
{
    [SerializeField] public puzzleManager manager;

    [Header("Put number in the order the buttons supposed to be pressed")]
    public float buttonOrder;
    private new Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            renderer.material.color = Color.green;
            manager.ButtonPress(buttonOrder);
        }
    }

    public void PuzzleFail()
    {
        renderer.material.color = Color.red;
    }
}
