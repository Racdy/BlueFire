using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera AimCam;
    private CinemachinePOV pov;

    private int toggleXValueInt;
    private int toggleYValueInt;

    private void Start()
    {
         pov = AimCam.GetCinemachineComponent<CinemachinePOV>();
    }
    private void FixedUpdate()
    {
        SetCameraAimSettings();
    }
    public void SetCameraAimSettings()
    {
        pov.m_HorizontalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("AimSenX");
        pov.m_VerticalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("AimSenY");

        toggleXValueInt = PlayerPrefs.GetInt("AimAxisX");
        if (toggleXValueInt == 0)
            pov.m_HorizontalAxis.m_InvertInput = false;
        else
            pov.m_HorizontalAxis.m_InvertInput = true;

        toggleYValueInt = PlayerPrefs.GetInt("AimAxisY");
        if (toggleYValueInt == 0)
            pov.m_VerticalAxis.m_InvertInput = false;
        else
            pov.m_VerticalAxis.m_InvertInput = true;

        //Debug.Log("X" + pov.m_HorizontalAxis.m_InvertInput);
        //Debug.Log("y" + pov.m_VerticalAxis.m_InvertInput);
    }
}
