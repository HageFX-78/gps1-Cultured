using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ToMain : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI loading;
    [SerializeField] private float timer = 3f;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            loading.text = "Loading";
            loading.text = "Loading . ";
            loading.text = "Loading . .";
            loading.text = "Loading . . .";
            loading.text = "Loading . . . .";
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
