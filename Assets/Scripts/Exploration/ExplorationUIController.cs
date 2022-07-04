using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExplorationUIController : MonoBehaviour
{
    [Header("SceneLoad")]
    [SerializeField] private int sceneNum;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadSlider;
    
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen = null;
    [SerializeField] private GameObject settingScreen = null;
    [HideInInspector] public static bool isPaused = false;

    [Header("Master Volume")]
    [SerializeField] private TextMeshProUGUI masterTXT = null;
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
        //if(MainMenuBehaviour.loadGame == true)
        //{
        //    PlayerCommonStatus.sanityValue = PlayerPrefs.GetInt("Load Sanity");
        //}
        volSlider.value = PlayerPrefs.GetFloat("Master Volume") * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume") * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume") * 100;
        SetMusic();
        SetSFX();
        SetVol();
        Apply();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.gameObject.activeSelf == false && settingScreen.gameObject.activeSelf == false)
            {  
                pauseScreen.SetActive(true);
                PauseGame();
            }
            else if (settingScreen.gameObject.activeSelf == true && pauseScreen.gameObject.activeSelf == false)
            {
                settingScreen.SetActive(false);
                pauseScreen.SetActive(true);
            }
            else if (pauseScreen.gameObject.activeSelf == true && settingScreen.gameObject.activeSelf == false)
            {
                pauseScreen.SetActive(false);
                UnpauseGame();
                Cancel();
            }                   
        }
    }
    public void Back()
    {
        //PlayerPrefs.SetFloat("Load Sanity", PlayerCommonStatus.sanityValue);
        PlayerPrefs.SetInt("Load Scene", SceneManager.GetActiveScene().buildIndex); 
        StartCoroutine(LoadAsynchronously(sceneNum));
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    public void SetVol()
    {
        AudioListener.volume = volSlider.value * 0.01f;
        masterTXT.text = volSlider.value.ToString("0.0");
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
