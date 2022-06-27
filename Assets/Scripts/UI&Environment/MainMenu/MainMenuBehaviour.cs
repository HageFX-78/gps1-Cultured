using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private TextMeshProUGUI volumeTXT = null;
    [SerializeField] private Slider volSlider = null;
    [SerializeField] private AudioSource bgm = null;

    private void Start()
    {
        volSlider.value = AudioListener.volume * 100;
    }
    public void StartNG()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SetVol()
    {
        AudioListener.volume = volSlider.value * 0.01f;
        volumeTXT.text = volSlider.value.ToString("0.0");
        PlayerPrefs.SetFloat("Master Volume", AudioListener.volume);
    }


}
