using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject ContinuePanel;
    public GameObject NewGamePanel;
    public GameObject OptionsPanel;
    public GameObject AudioPanel;
    public GameObject ControllerPanel;
    public GameObject CameraPanel;

    public GameObject CreditsPanel;
    public GameObject ExitGamePanel;

    public GameObject continueEnableButton;

    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider SFxSlider;

    private float musicValue;
    private float SFXValue;

    public Slider aimSenXSlider;
    public Slider aimSenYSlider;

    public float aimSenXValue;
    public float aimSenYValue;

    public Toggle invAxisXToggle;
    public Toggle invAxisYToggle;

    public bool invAxisXValue;
    public bool invAxisYValue;

    public int invAxisIntValueX;
    public int invAxisIntValueY;


    public int scene;

    public CinemachineVirtualCamera continueCamera;
    public CinemachineVirtualCamera restartCamera;

    private void Start()
    {
        continueCamera.Priority = 0;
        restartCamera.Priority = 0;

        mainMenuPanel.SetActive(true);

        LoadSettingsLevel();
        LoadAudioSetting();
        LoadAimCamSen();
        LoadInvertAxis();
    }

    public void LoadSettingsLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            scene = PlayerPrefs.GetInt("Level");
            continueEnableButton.SetActive(true);
        }
        else
        {
            continueEnableButton.SetActive(false);
            scene = 1;
            PlayerPrefs.SetInt("Level", 1);
        }
    }

    public void CleanPanel()
    {
        ContinuePanel.SetActive(false);
        NewGamePanel.SetActive(false);
        AudioPanel.SetActive(false);
        ControllerPanel.SetActive(false);
        CameraPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void OptionsCleanPanel()
    {
        AudioPanel.SetActive(false);
        ControllerPanel.SetActive(false);
        CameraPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        ContinuePanel.SetActive(true);
    }

    public void ContinueGameYes()
    {
        CleanPanel();
        mainMenuPanel.SetActive(false);
        continueCamera.Priority = 12;
        StartCoroutine("LoadContinue");
    }

    public IEnumerator LoadContinue()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }

    public void ContinueGameNo()
    {
        ContinuePanel.SetActive(false);
    }

    public void NewGame()
    {
        NewGamePanel.SetActive(true);
    }

    public void NewGameYes()
    {
        CleanPanel();
        mainMenuPanel.SetActive(false);
        restartCamera.Priority = 12;
        PlayerPrefs.SetInt("Level", 1);
        StartCoroutine("LoadRestart");
    }

    public IEnumerator LoadRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    public void NewGameNo()
    {
        NewGamePanel.SetActive(false);
    }

    public void Options()
    {
        OptionsPanel.SetActive(true);
        OptionsCleanPanel();
        AudioPanel.SetActive(true);
    }

    public void AudioOptions()
    {
        OptionsCleanPanel();
        AudioPanel.SetActive(true);
    }

    public void ControllerOptions()
    {
        OptionsCleanPanel();
        ControllerPanel.SetActive(true);
    }

    public void CameraOptions()
    {
        OptionsCleanPanel();
        CameraPanel.SetActive(true);
    }

    public void OptionsExit()
    {
        CleanPanel();
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }

    public void CreditsExit()
    {
        CreditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void SetMusicVolume(float volume)
    {
        musicValue = volume;
        audioMixer.SetFloat("MusicVolume", musicValue);

        SaveAudioSettings();
    }

    public void SetSFxVolume(float volume)
    {
        SFXValue = volume;
        audioMixer.SetFloat("SFxVolume", SFXValue);

        SaveAudioSettings();
    }

    public void GetVolume()
    {
        audioMixer.GetFloat("MusicVolume", out musicValue);
        audioMixer.GetFloat("SFxVolume", out SFXValue);

        musicSlider.value = musicValue;
        SFxSlider.value = SFXValue;
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
        PlayerPrefs.SetFloat("SFxVolume", SFXValue);
    }

    public void LoadAudioSetting()
    {
        if (PlayerPrefs.HasKey("MusicVolume")|| PlayerPrefs.HasKey("SFxVolume"))
        {
            musicValue = PlayerPrefs.GetFloat("MusicVolume");
            SFXValue = PlayerPrefs.GetFloat("SFxVolume");
        }
        else
        {
            musicValue = 0f;
            SFXValue = 0f;
        }

        musicSlider.value = musicValue;
        SFxSlider.value = SFXValue;
    }

    public void SetAIMSenX(float sensitivity)
    {
        aimSenXValue = sensitivity;
        PlayerPrefs.SetFloat("AimSenX", aimSenXValue);
    }

    public void SetAIMSenY(float sensitivity)
    {
        aimSenYValue = sensitivity;
        PlayerPrefs.SetFloat("AimSenY", aimSenYValue);
    }

    public void LoadAimCamSen()
    {
        if (PlayerPrefs.HasKey("AimSenX") || PlayerPrefs.HasKey("AimSenY"))
        {
            aimSenXValue = PlayerPrefs.GetFloat("AimSenX");
            aimSenYValue = PlayerPrefs.GetFloat("AimSenY");
        }
        else
        {
            aimSenXValue = 2f;
            aimSenYValue = 2f;
        }

        aimSenXSlider.value = aimSenXValue;
        aimSenYSlider.value = aimSenYValue;
    }

    public void SetInvAxisX(bool toggleValue)
    {
        invAxisXValue = toggleValue;

        //Debug.Log("invAxisXValue" + invAxisXValue);

        if (invAxisXValue)
            PlayerPrefs.SetInt("AimAxisX", 1);
        else
            PlayerPrefs.SetInt("AimAxisX", 0);
    }

    public void SetInvAxisY(bool toggleValue)
    {
        invAxisYValue = toggleValue;
        if (invAxisYValue)
            PlayerPrefs.SetInt("AimAxisY", 1);
        else
            PlayerPrefs.SetInt("AimAxisY", 0);
    }

    public void LoadInvertAxis()
    {
        if (PlayerPrefs.HasKey("AimAxisX") || PlayerPrefs.HasKey("AimAxisY"))
        {
            invAxisIntValueX = PlayerPrefs.GetInt("AimAxisX");
            invAxisIntValueY = PlayerPrefs.GetInt("AimAxisY");

            if (invAxisIntValueX == 0)
                invAxisXValue = false;
            else
                invAxisXValue = true;

            if (invAxisIntValueY == 0)
                invAxisYValue = false;
            else
                invAxisYValue = true;
        }
        else
        {
            invAxisXValue = false;
            invAxisYValue = true;

            invAxisIntValueX = 0;
            invAxisIntValueX = 1;
        }

        invAxisXToggle.isOn = invAxisXValue;
        invAxisYToggle.isOn = invAxisYValue;
    }

}
