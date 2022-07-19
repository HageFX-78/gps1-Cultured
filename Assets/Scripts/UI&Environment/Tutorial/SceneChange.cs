using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [Header("Scene Switch")]
    [SerializeField] public static bool loadGame = false;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadSlider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (TRemnant.collect)
        {
            if (collision.CompareTag("Player"))
            {
                LoadAsynchronously((int)sceneIndex.LV1);
            }
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScreen.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
