using System.Collections.Generic;
using System.IO;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

public class PhotoGalleryNew : SingletonBehaviour<PhotoGalleryNew> {

    [SerializeField] private GameEvent cameraClickEvent;
    [SerializeField] private List<Photo> _photos = new List<Photo>();

    private bool _flashEnabled = false;
    private Camera _mainCamera;

    private int _cntPhoto = 0;
    private string _pathPhotos;

    private void Start() {
        _mainCamera = Camera.main;
        // Clear the photos from the last time when game restarts
        _pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(_pathPhotos)) {
            Log.Info("Clearing Old Photos.");
            Directory.Delete(_pathPhotos, true);
        }
        Log.Info("Creating a new gallery.");
        Directory.CreateDirectory(_pathPhotos);
    }

    // Save the screenshot
    public void Capture() {
        // Log.Info("Image Captured!");
        var fileName = _pathPhotos + _cntPhoto + ".png";
        ScreenCapture.CaptureScreenshot(fileName);
        var photo = new Photo(fileName);
        _photos.Add(photo);
        _cntPhoto++;
        
        cameraClickEvent.Raise(new CameraClickEventData {
            Photo = photo,
            CameraFOV = _mainCamera.fieldOfView,
            Cam = _mainCamera,
            Flash = _flashEnabled
        });
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Capture();
        }
    }

    public void OnBeatStartEvent(IGameEventData data) {
        if (!Utils.TryConvertVal(data, out BeatStartEventData beatData)){
            Log.Err("Error Converting Type");
            return;
        }
        _flashEnabled = beatData.Beat switch {
            GameBeat.KillingAct1 => true,
            GameBeat.Suspect => false,
            _ => _flashEnabled
        };
    }

    public void AddPromptToPhoto(Vector3 viewPos, string clueName, Phase phaseBelongTo) {
        if (_cntPhoto <= 0) {
            Debug.Log("Clue is captured, but fail to take a photo.");
            return;
        }
        int lastIndex = _cntPhoto - 1;
        _photos[lastIndex].HasClue = true;
        _photos[lastIndex].ViewPos = viewPos;
        _photos[lastIndex].ClueName = clueName;
        _photos[lastIndex].PhaseBelongTo = phaseBelongTo;
    }
    
}