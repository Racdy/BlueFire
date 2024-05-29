using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject HUDPanel;
    public GameObject exitPanel;
    public GameObject optionsPanel;
    public GameObject audioPanel;
    public GameObject controllerPanel;
    public GameObject cameraPanel;
    public GameObject restartPanel;

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

    private void Start()
    {
        LoadAudioSetting();
        LoadAimCamSen();
        LoadInvertAxis();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void CleanPanel()
    {
        pausePanel.SetActive(false);
        HUDPanel.SetActive(false);
        exitPanel.SetActive(false);
        optionsPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void OptionsCleanPanel()
    {
        audioPanel.SetActive(false);
        controllerPanel.SetActive(false);
        cameraPanel.SetActive(false);
    }

    public void Pause()
    {
        CleanPanel();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        CleanPanel();
        HUDPanel.SetActive(true);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        CleanPanel();
        restartPanel.SetActive(true);
    }

    public void RestarYes()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene);
    }

    public void RestarNo()
    {
        Pause();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
        OptionsCleanPanel();
        audioPanel.SetActive(true);
    }

    public void AudioOptions()
    {
        OptionsCleanPanel();
        audioPanel.SetActive(true);
    }

    public void ControllerOptions()
    {
        OptionsCleanPanel();
        controllerPanel.SetActive(true);
    }

    public void CameraOptions()
    {
        OptionsCleanPanel();
        cameraPanel.SetActive(true);
    }

    public void OptionsExit()
    {
        OptionsCleanPanel();
        Pause();
    }

    public void ExitGame()
    {
        CleanPanel();
        exitPanel.SetActive(true);
    }

    public void ExitGameYes()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void ExitGameNo()
    {
        Pause();
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
        if (PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFxVolume"))
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
            PlayerPrefs.SetInt("AimAxisX",1);
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

            if(invAxisIntValueX == 0)
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
            PlayerPrefs.SetInt("AimAxisX", 0);
            PlayerPrefs.SetInt("AimAxisY", 1);

            invAxisXValue = false;
            invAxisYValue = true;

            invAxisIntValueX = 0;
            invAxisIntValueX = 1;
        }

        invAxisXToggle.isOn = invAxisXValue;
        invAxisYToggle.isOn = invAxisYValue;
    }
}
