using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraInterfaceDisplay : MonoBehaviour
{
    public TextMeshProUGUI TimeAndDate;
    public TextMeshProUGUI RoomIndex;
    public Slider IndicatorZoom;
    public Slider ProgressBar;
    public CameraControl InstanceCameraControl;

    PhaseManager _phaseManager;

    void Start()
    {
        _phaseManager = FindObjectOfType<PhaseManager>();
    }

    void Update()
    {
        // Show time and date on camera screen
        TimeAndDate.text = System.DateTime.Now.ToString();
        // Show the extent of zoom in/out with a slider
        IndicatorZoom.value = (InstanceCameraControl.MaxFOV - Camera.main.fieldOfView) / (InstanceCameraControl.MaxFOV - InstanceCameraControl.MinFOV);
        ProgressBar.value = _phaseManager.GetProgress();
        RoomIndex.text = "ROOM" + (_phaseManager.GetRoomIndex() + 1);
    }
}
