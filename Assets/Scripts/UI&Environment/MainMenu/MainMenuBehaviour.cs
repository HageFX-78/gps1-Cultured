using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Scene Switch")]
    [SerializeField] private int sceneNum;
    [SerializeField] private Button load;
    [SerializeField] public static bool loadGame = false;
    [SerializeField] private GameObject noSaveTXT = null;

    [Header("Master Volume")]
    [SerializeField] private TextMeshProUGUI volumeTXT = null;
    [SerializeField] private Slider volSlider = null;

    [Header("Music Volume")]
    [SerializeField] private TextMeshProUGUI musicTXT = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private AudioSource music = null;
    
    [Header("SFX Volume")]
    [SerializeField] private TextMeshProUGUI sfxTXT = null;
    [SerializeField] private Slider sfxSlider = null;
    [SerializeField] private AudioSource[] sfx = null;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Load Scene") == 0)
        {
            load.interactable = false;
        }
        volSlider.value = PlayerPrefs.GetFloat("Master Volume") * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume") * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume") * 100;
        SetMusic();
        SetSFX();
        SetVol();
        Apply();
    }
    public void StartNG()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Load Scene", 1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Load Scene"));
        loadGame = true;
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
    }

    public void SetMusic()
    {
        music.volume = musicSlider.value * 0.01f;
        musicTXT.text = musicSlider.value.ToString("0.0"); 
    }

    public void SetSFX()
    {
        sfx[0].volume = sfxSlider.value * 0.01f;
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = sfx[0].volume;
        }

        sfxTXT.text = sfxSlider.value.ToString("0.0");
    }

    public void ResetAll()
    {
        AudioListener.volume = 0.5f;
        music.volume = 0.5f;
        sfx[0].volume = 0.5f;
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = sfx[0].volume;
        }

        PlayerPrefs.SetFloat("Master Volume", AudioListener.volume);
        PlayerPrefs.SetFloat("Music Volume", music.volume);
        PlayerPrefs.SetFloat("SFX Volume", sfx[0].volume);

        volSlider.value = AudioListener.volume * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume") * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume") * 100;
    }

    public void Apply()
    {
        sfx[0].volume = sfxSlider.value * 0.01f;
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = sfx[0].volume;
        }

        music.volume = musicSlider.value * 0.01f;
        AudioListener.volume = volSlider.value * 0.01f;

        PlayerPrefs.SetFloat("Master Volume", AudioListener.volume);
        PlayerPrefs.SetFloat("SFX Volume", sfx[0].volume);
        PlayerPrefs.SetFloat("Music Volume", music.volume);

    }

    public void Cancel()
    {
        volSlider.value = PlayerPrefs.GetFloat("Master Volume") * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume") * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume") * 100;

        sfx[0].volume = sfxSlider.value * 0.01f;
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = sfx[0].volume;
        }

        music.volume = musicSlider.value * 0.01f;
        AudioListener.volume = volSlider.value * 0.01f;
    }
}
