using System.Collections;
using System.Collections.Generic;
using EventSystem.Data;
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
    public static GameManager instance;
    public Canvas CanvasShoot;
    public Canvas CanvasGallery;
    public GameEvent CameraCaptureEvent;
    public float RateCapture;
    public AudioSource AudioPlayer;
    public AudioClip SFXClick;
    public UnityEngine.Video.VideoPlayer VideoPlayer;

    Camera m_mainCamera;
    GameState m_gameState;
    PhaseManager m_phaseManager;
    PhotoGallery m_photoGallery;
    float m_lastCaptureTime;
    bool m_justTaken;

    //headset mount flag
    bool isHeadsetMounted = false;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private EventSystem.GameEvent _swipePhotosEvent;

    private void Awake() 
    {
        instance = this;
    }
    
    void Start()
    {
        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
        m_mainCamera = Camera.main;
        m_gameState = GameState.Shoot;
        m_phaseManager = FindObjectOfType<PhaseManager>();
        m_photoGallery = FindObjectOfType<PhotoGallery>();

        m_lastCaptureTime = float.NegativeInfinity;
        m_justTaken = false;
        
        VideoPlayer.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        // Shoot
        if (m_gameState == GameState.Shoot)
        {
            // Open the gallery
            if (Input.GetKeyDown(KeyCode.P) || inputHandler.isGalleryButtonDown)
            {
                m_gameState = GameState.Gallery;
                m_photoGallery.EnterGallery();
                CanvasShoot.gameObject.SetActive(false);
                CanvasGallery.gameObject.SetActive(true);
            }
            // Capture/Flash
            else if (Input.GetKeyDown(KeyCode.Space) || inputHandler.isShutterButtonDown)
            {
                if (RateCapture + m_lastCaptureTime < Time.time)
                {
                    m_gameState = GameState.Shooting;
                    CanvasShoot.gameObject.SetActive(false);
                    m_lastCaptureTime = Time.time;

                    //if(m_phaseManager.GetPhase() == Phase.AboutToKill2)
                    //{
                    //    m_phaseManager.WaitToMovePhaseForward(Phase.Flee2, 0f);
                    //}
                    //else if (m_phaseManager.GetPhase() == Phase.AboutToKill3)
                    //{
                    //    m_phaseManager.WaitToMovePhaseForward(Phase.Flee3, 0f);
                    //}

                    if (m_phaseManager.GetPhase() == Phase.StandAfterKilling1 && m_mainCamera.fieldOfView <= 10)
                    {
                        m_phaseManager.WaitToMovePhaseForward(m_phaseManager.GetPhase() + 1, 0.1f);
                    }
                    
                }                
            }
        }
        // The moment of Shooting 
        else if(m_gameState == GameState.Shooting)
        {
            if (!m_justTaken)
            {
                AudioPlayer.clip = SFXClick;
                AudioPlayer.Play();
                VideoPlayer.gameObject.SetActive(true);
                m_photoGallery.Capture();
                CameraCaptureEvent.Raise();
                m_justTaken = true;
            }
            if(RateCapture + m_lastCaptureTime < Time.time)
            {
                m_gameState = GameState.Shoot;
                m_justTaken = false;
                VideoPlayer.gameObject.SetActive(false);
                CanvasShoot.gameObject.SetActive(true);
            }
        }
        // Gallery
        else if(m_gameState == GameState.Gallery)
        {
            // Return to shoot
            if (Input.GetKeyDown(KeyCode.P) || inputHandler.isGalleryButtonDown)
            {
                m_gameState = GameState.Shoot;
                CanvasShoot.gameObject.SetActive(true);
                CanvasGallery.gameObject.SetActive(false);
            }
            // Review details of the clue if there is any
            if (Input.GetKeyDown(KeyCode.Space) || inputHandler.isShutterButtonDown)
            {
                m_photoGallery.OpenOrCloseDetails();
            }
            // Scroll pictures, left-ward
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || inputHandler.isLeftButtonDown)
            {
                _swipePhotosEvent.Raise(new SwipePhotoEventData{Left = true});
                //m_photoGallery.ShowPrevPhoto();
            }
            // Scroll pictures, right-ward
            else if (Input.GetKeyDown(KeyCode.RightArrow) || inputHandler.isRightButtonDown)
            {
                _swipePhotosEvent.Raise(new SwipePhotoEventData{Left = false});
                //m_photoGallery.ShowNextPhoto();
            }
        }
    }

    public GameState GetGameState()
    {
        return m_gameState;
    }

    public void FindClue(Vector3 viewPos, string clueName, Phase phaseBelongTo)
    {
        m_photoGallery.AddPromptToPhoto(viewPos, clueName, phaseBelongTo);
        if (m_phaseManager.GetProgress() == 1f)
            return;
        float progress = m_phaseManager.UpdateProgress(clueName);
        if (progress == 1f)
        {
            // Pop up prompts
            GameObject GoPromptsText = CanvasShoot.gameObject.transform.Find("PromptAfterClearingOnePhase").gameObject;
            StartCoroutine(LetGoAppearForAWhile(GoPromptsText, 7f));
        }
    }

    public Phase GetPhase()
    {
        return m_phaseManager.GetPhase();
    }

    IEnumerator LetGoAppearForAWhile(GameObject go, float timeAppearing)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(timeAppearing);
        go.SetActive(false);
    }

    void HandleHMDMounted()
    {
        isHeadsetMounted = true;
        CanvasShoot.enabled = true;
    }

    void HandleHMDUnmounted()
    {
        isHeadsetMounted = false;
        CanvasShoot.enabled = false;
    }

    public bool IsHeadsetMounted()
    {
        return isHeadsetMounted;
    }
}
