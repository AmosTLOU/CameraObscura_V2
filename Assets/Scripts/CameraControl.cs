using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float MaxOffsetPosX;
    public float MaxOffsetPosY;
    public float SpeedZoom;
    public float SpeedRotateXY;
    public float SpeedRotateZ;
    public float MinFOV;
    public float MaxFOV;
    public bool IsKbMouseEnabled;

    [SerializeField]
    InputHandler inputHandler;

    GameManager m_gameManager;
    Camera m_mainCamera;

    Vector3 m_camRot;
    Vector3 m_camPos;
    float m_camFOV;
    Vector3 m_camInitialPos;
    Vector3 m_camInitialRot;
    float m_camInitialFOV;


    private void Start()
    {
        // m_gameManager = FindObjectOfType<GameManager>();
        m_mainCamera = Camera.main;

        m_camPos = transform.position;
        m_camRot = transform.eulerAngles;
        m_camFOV = m_mainCamera.fieldOfView;

        m_camInitialPos = transform.position;
        m_camInitialRot = transform.eulerAngles;
        m_camInitialFOV = m_mainCamera.fieldOfView;
    }

    private void Update()
    {
        // Reset to initial status
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_camRot = m_camInitialRot;
            m_camPos = m_camInitialPos;
            m_camFOV = m_camInitialFOV;
            return;
        }
        // If not in shoot state, it is not allowed to operate the camera.
        // if(m_gameManager.GetGameState() != GameState.Shoot)
        // {
        //     return;
        // }

        // If in shoot state, it is free to go.
        // Set new rotation
        //var rotationValues = inputHandler.GetRotationValues();
        //float offset_r_y = IsKbMouseEnabled ? Input.GetAxis("Mouse X") : -rotationValues.z;
        //float offset_r_x = IsKbMouseEnabled ? Input.GetAxis("Mouse Y") : -rotationValues.x;
        float offset_r_y = Input.GetAxis("Mouse X");
        float offset_r_x = Input.GetAxis("Mouse Y");
        m_camRot.x -= SpeedRotateXY * offset_r_x * Time.deltaTime;
        m_camRot.y += SpeedRotateXY * offset_r_y * Time.deltaTime;

        // Set new position
        float offset_pos_x = Input.GetAxis("Horizontal");
        float offset_pos_y = Input.GetAxis("Vertical");
        m_camPos.x += offset_pos_x * Time.deltaTime;
        m_camPos.x = Mathf.Clamp(m_camPos.x, m_camInitialPos.x - MaxOffsetPosX, m_camInitialPos.x + MaxOffsetPosX);
        m_camPos.y += offset_pos_y * Time.deltaTime;
        m_camPos.y = Mathf.Clamp(m_camPos.y, m_camInitialPos.y - MaxOffsetPosY, m_camInitialPos.y + MaxOffsetPosY);

        // Set new FOV (zoom)
        float offset_zoom = Input.GetAxis("Mouse ScrollWheel");
        m_camFOV -= SpeedZoom * offset_zoom * Time.deltaTime; ;
        m_camFOV = Mathf.Clamp(m_camFOV, MinFOV, MaxFOV);

        transform.eulerAngles = m_camRot;
        transform.position = m_camPos;
        Camera.main.fieldOfView = m_camFOV;
    }
}
