using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] GyroInput gyroInput;

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
        m_gameManager = FindObjectOfType<GameManager>();
        m_mainCamera = Camera.main;

        m_camPos = transform.position;
        m_camRot = transform.eulerAngles;
        m_camFOV = m_mainCamera.fieldOfView;

        m_camInitialPos = transform.position;
        m_camInitialRot = transform.eulerAngles;
        m_camInitialFOV = 5;


    }

    private void Update()
    {
        // Reset to initial status
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_camRot = m_camInitialRot;
            m_camPos = m_camInitialPos;
            m_camFOV = 5;
            return;
        }
        // If not in shoot state, it is not allowed to operate the camera.
        if(m_gameManager.GetGameState() != GameState.Shoot)
        {
            return;
        }

        // If in shoot state, it is free to go.
        // Set new rotation
        //var rotationValues = gyroInput.GetRotationValues();
        //float offset_r_y = -rotationValues.z;
        //float offset_r_x = -rotationValues.x;
        //m_camRot.x -= SpeedRotateXY * offset_r_x * Time.deltaTime;
        //m_camRot.y += SpeedRotateXY * offset_r_y * Time.deltaTime;

        // Set new position
        //float offset_pos_x = Input.GetAxis("Horizontal");
        //float offset_pos_y = Input.GetAxis("Vertical");
        //m_camPos.x += offset_pos_x * Time.deltaTime;
        //m_camPos.x = Mathf.Clamp(m_camPos.x, m_camInitialPos.x - MaxOffsetPosX, m_camInitialPos.x + MaxOffsetPosX);
        //m_camPos.y += offset_pos_y * Time.deltaTime;
        //m_camPos.y = Mathf.Clamp(m_camPos.y, m_camInitialPos.y - MaxOffsetPosY, m_camInitialPos.y + MaxOffsetPosY);

        // Set new FOV (zoom)
        float offset_zoom = Input.GetAxis("Mouse ScrollWheel");
        //m_camFOV -= SpeedZoom * offset_zoom * Time.deltaTime; ;
        //m_camFOV = Mathf.Clamp(m_camFOV, MinFOV, MaxFOV);

        //transform.eulerAngles = m_camRot;
        transform.localRotation = gyroInput.GetRotationValues();
        transform.position = m_camPos;
        Camera.main.fieldOfView = 5;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        m_camRot = m_camInitialRot;
        m_camPos = m_camInitialPos;
        m_camFOV = m_camInitialFOV;
    }
}
