using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float currentPitch = 0f;
    [SerializeField] private float currentRoll = 0f;
    [SerializeField] private float currentYaw = 0f;
    [SerializeField] private float distance = 0f;
    public bool isFlashButtonDown = false;
    public bool isShutterButtonDown = false;
    public bool isGalleryButtonDown = false;
    public bool isLeftButtonDown = false;
    public bool isRightButtonDown = false;

    private float prevPitch, prevRoll, prevYaw, prevDistance;

    void Start()
    {

    }

    void FixedUpdate()
    {
        isFlashButtonDown = false;
        isGalleryButtonDown = false;
        isShutterButtonDown = false;
        isLeftButtonDown = false;
        isRightButtonDown = false;
    }

    void Update()
    {

    }

    public Vector3 GetRotationValues()
    {
        return new Vector3(currentPitch-prevPitch, currentRoll-prevRoll, currentYaw-prevYaw);
    }

    public float GetZoomValue()
    {
        return distance-prevDistance;
    }

    private void SetInputData(string[] inputMeta)
    {
        switch(inputMeta[0])
        {
            case "Button":
                if(inputMeta[1] == "1")
                {
                    isShutterButtonDown = true;
                }
                if(inputMeta[1] == "2")
                {
                    isFlashButtonDown = true;
                }
                if(inputMeta[1] == "3")
                {
                    isRightButtonDown = true;
                }
                if(inputMeta[1] == "4")
                {
                    isGalleryButtonDown = true;
                }
                if(inputMeta[1] == "5")
                {
                    isLeftButtonDown = true;
                }
                break;
            case "Pitch":
                prevPitch = currentPitch;
                currentPitch = float.Parse(inputMeta[1]);
                break;
            case "Roll":
                prevRoll = currentRoll;
                currentRoll = float.Parse(inputMeta[1]);
                break;
            case "Yaw":
                prevYaw = currentYaw;
                currentYaw = float.Parse(inputMeta[1]);
                break;
            case "Distance":
                prevDistance = distance;
                distance = float.Parse(inputMeta[1]);
                break;
            default:
                print("un-identified input.");
                break;
        }
    }
}
