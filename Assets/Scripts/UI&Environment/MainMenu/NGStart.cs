using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NGStart : MonoBehaviour
{
    public void startNG()
    {
        SceneManager.LoadScene("MainScene");
    }
}
