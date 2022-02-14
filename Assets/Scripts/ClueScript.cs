using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public bool IsClueDetectionEnabled;
    public float MaxDetectFOV;
    public Phase PhaseBelongTo;
    // Defines how close the evidence should be to center of the screen for detection
    // 0 -> must be at the center; 1 -> okay if close to the edges 
    [Range(0, 1)]
    float m_evidenceDetectArea;

    GameManager m_gameManager;
    Camera m_mainCamera;    

    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_mainCamera = Camera.main;
        m_evidenceDetectArea = 0.3f;
    }

    public void CheckIfClueCaptured()
    {
        Vector3 viewPos = m_mainCamera.WorldToViewportPoint(transform.position);
        // If zoom in close enough while photographing, then the clue is considered detected
        if(IsClueDetectionEnabled && m_mainCamera.fieldOfView < MaxDetectFOV && PhaseBelongTo <= m_gameManager.GetPhase())
        {
            float extraRange = (1f - m_evidenceDetectArea) / 2f;
            if (extraRange <= viewPos.x  && viewPos.x <= (1f - extraRange) &&
               extraRange <= viewPos.y && viewPos.y <= (1f - extraRange))
            {
                m_gameManager.FindClue(viewPos, gameObject.name, PhaseBelongTo);
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}
