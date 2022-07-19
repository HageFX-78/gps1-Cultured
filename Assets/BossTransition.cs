using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTransition : MonoBehaviour
{
    [SerializeField] private float timerToTransition;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            Debug.Log(collision.gameObject.name);

            StartCoroutine(LoadingBoss());
        }

        IEnumerator LoadingBoss()
        {
            Time.timeScale = 0;

            yield return new WaitForSecondsRealtime(timerToTransition);
            //load scene after timer goes;
            SceneManager.LoadSceneAsync("Battle Scene");

        }
    }
}
