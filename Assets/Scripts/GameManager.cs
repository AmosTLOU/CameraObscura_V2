using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public enum GameState
{
    Shoot,
    Shooting,
    Gallery,
}

// GameManager Class, charge of input and interaction
public class GameManager : MonoBehaviour
{
    public Canvas CanvasShoot;
    public Canvas CanvasGallery;
    public GameEvent CameraCaptureEvent;
    public float RateCapture;
    public AudioSource AudioPlayer;
    public AudioClip SFXClick;
    public UnityEngine.Video.VideoPlayer VideoPlayer;

    Camera _mainCamera;
    GameState _gameState;
    PhaseManager _phaseManager;
    PhotoGallery _photoGallery;
    float _lastCaptureTime;
    bool _justTaken;

    [SerializeField] private InputHandler inputHandler;

    void Start()
    {
        _mainCamera = Camera.main;
        _gameState = GameState.Shoot;
        _phaseManager = FindObjectOfType<PhaseManager>();
        _photoGallery = FindObjectOfType<PhotoGallery>();

        _lastCaptureTime = float.NegativeInfinity;
        _justTaken = false;
        
        VideoPlayer.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        // Shoot
        if (_gameState == GameState.Shoot)
        {
            // Open the gallery
            if (Input.GetKeyDown(KeyCode.P) || inputHandler.isGalleryButtonDown)
            {
                _gameState = GameState.Gallery;
                _photoGallery.EnterGallery();
                CanvasShoot.gameObject.SetActive(false);
                CanvasGallery.gameObject.SetActive(true);
            }
            // Capture/Flash
            else if (Input.GetKeyDown(KeyCode.Space) || inputHandler.isShutterButtonDown)
            {
                if (RateCapture + _lastCaptureTime < Time.time)
                {
                    _gameState = GameState.Shooting;
                    CanvasShoot.gameObject.SetActive(false);
                    _lastCaptureTime = Time.time;

                    //if(_phaseManager.GetPhase() == Phase.AboutToKill2)
                    //{
                    //    _phaseManager.WaitToMovePhaseForward(Phase.Flee2, 0f);
                    //}
                    //else if (_phaseManager.GetPhase() == Phase.AboutToKill3)
                    //{
                    //    _phaseManager.WaitToMovePhaseForward(Phase.Flee3, 0f);
                    //}

                    if (_phaseManager.GetPhase() == Phase.StandAfterKilling1 && _mainCamera.fieldOfView <= 10)
                    {
                        _phaseManager.WaitToMovePhaseForward(_phaseManager.GetPhase() + 1, 0.1f);
                    }
                    
                }                
            }
        }
        // The moment of Shooting 
        else if(_gameState == GameState.Shooting)
        {
            if (!_justTaken)
            {
                AudioPlayer.clip = SFXClick;
                AudioPlayer.Play();
                VideoPlayer.gameObject.SetActive(true);
                _photoGallery.Capture();
                CameraCaptureEvent.Raise();
                _justTaken = true;
            }
            if(RateCapture + _lastCaptureTime < Time.time)
            {
                _gameState = GameState.Shoot;
                _justTaken = false;
                VideoPlayer.gameObject.SetActive(false);
                CanvasShoot.gameObject.SetActive(true);
            }
        }
        // Gallery
        else if(_gameState == GameState.Gallery)
        {
            // Return to shoot
            if (Input.GetKeyDown(KeyCode.P) || inputHandler.isGalleryButtonDown)
            {
                _gameState = GameState.Shoot;
                CanvasShoot.gameObject.SetActive(true);
                CanvasGallery.gameObject.SetActive(false);
            }
            // Review details of the clue if there is any
            if (Input.GetKeyDown(KeyCode.Space) || inputHandler.isShutterButtonDown)
            {
                _photoGallery.OpenOrCloseDetails();
            }
            // Scroll pictures, left-ward
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || inputHandler.isLeftButtonDown)
            {
                _photoGallery.ShowPrevPhoto();
            }
            // Scroll pictures, right-ward
            else if (Input.GetKeyDown(KeyCode.RightArrow) || inputHandler.isRightButtonDown)
            {
                _photoGallery.ShowNextPhoto();
            }
        }
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public void FindClue(Vector3 viewPos, string clueName, Phase phaseBelongTo)
    {
        _photoGallery.AddPromptToPhoto(viewPos, clueName, phaseBelongTo);
        if (_phaseManager.GetProgress() == 1f)
            return;
        float progress = _phaseManager.UpdateProgress(clueName);
        if (progress == 1f)
        {
            // Pop up prompts
            GameObject GoPromptsText = CanvasShoot.gameObject.transform.Find("PromptAfterClearingOnePhase").gameObject;
            StartCoroutine(LetGoAppearForAWhile(GoPromptsText, 7f));
        }
    }

    public Phase GetPhase()
    {
        return _phaseManager.GetPhase();
    }

    IEnumerator LetGoAppearForAWhile(GameObject go, float timeAppearing)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(timeAppearing);
        go.SetActive(false);
    }
}
