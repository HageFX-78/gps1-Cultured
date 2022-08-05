using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePrompt : MonoBehaviour
{
    [SerializeField] float yPositionOffset;

    public GameObject promptPrefab;
    private GameObject thisPref;
    private void Start()
    {
        yPositionOffset=(yPositionOffset>0||yPositionOffset<0)? yPositionOffset:2f;
        if (transform.Find("interact_prompt") ==null)
        {
            thisPref = Instantiate(promptPrefab, new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z), Quaternion.identity);
            thisPref.transform.SetParent(gameObject.transform);
            thisPref.name = "interact_prompt";
            thisPref.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.Find("interact_prompt") != null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space) &&  collision.gameObject.CompareTag("Player"))
        {
            if (transform.Find("interact_prompt") != null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (transform.Find("interact_prompt") != null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(false);
            }
        }

    }
}
