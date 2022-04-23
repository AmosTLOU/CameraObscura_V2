using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using EventSystem;

public class InputHandler : MonoBehaviour
{
    public bool isShutterButtonDown = false;
    // public bool isGalleryButtonDown = false;
    private bool isHeadsetMounted = false;
    [SerializeField] private float potentiometerValue = 0f;
    [SerializeField] private GameEvent shutterClickEvent;

    SerialPort port = new SerialPort("COM3", 115200);
    void Start()
    {
        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
        port.Open();
        port.ReadTimeout = 1;
        port.NewLine = "\n";
    }

    void FixedUpdate()
    {
        // isGalleryButtonDown = false;
        isShutterButtonDown = false;
    }

    void Update()
    {
        if (port.IsOpen && port.BytesToRead > 32)
        {
            try
            {
                string input = port.ReadLine();
                var inputMeta = input.Split(':');
                SetInputData(inputMeta);
            }
            catch (System.Exception ex)
            {
                print("Could not read from serial port: " + ex.Message);
            }
        }
    }

    public float GetZoomValue()
    {
        return ((potentiometerValue-80)/160)*50;  //255 : potentiometer scale limit | 50 : maxFov - minFov
    }

    private void SetInputData(string[] inputMeta)
    {
        switch(inputMeta[0])
        {
            case "Button":
                if(inputMeta[1] == "1")
                {
                    isShutterButtonDown = true;
                    shutterClickEvent.Raise();
                    isShutterButtonDown = false;
                }
                break;
            case "Pot":
                potentiometerValue = float.Parse(inputMeta[1]);
                break;
            default:
                print("un-identified input.");
                break;
        }
    }

    void HandleHMDMounted()
    {
        isHeadsetMounted = true;
        // CanvasShoot.enabled = true;
    }

    void HandleHMDUnmounted()
    {
        isHeadsetMounted = false;
        // CanvasShoot.enabled = false;
    }

    public bool IsHeadsetMounted()
    {
        return isHeadsetMounted;
    }
}
