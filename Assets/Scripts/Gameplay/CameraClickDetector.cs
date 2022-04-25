using System;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay {
    public class CameraClickDetector : MonoBehaviour {
        [SerializeField] private GameEvent clickEvent;
        [SerializeField] private float maxFov = 15;
        [SerializeField] [Range(0,1)] private float detectRange;
        [SerializeField] private UnityEvent<CameraClickEventData> onDetected = new UnityEvent<CameraClickEventData>();

        private void Start(){
            Log.Info("Detector Start");
            var eventListener = gameObject.AddComponent<GameEventListener>().Init(clickEvent);
            Log.Info("Listener Init..ed");
            eventListener.response.AddListener(OnEventCameraClick);
        }

        private void OnEventCameraClick(IGameEventData arg0){
            if (!Utils.TryConvertVal(arg0, out CameraClickEventData cData)) return;
            
            
            Vector3 viewPos = cData.Cam.WorldToViewportPoint(transform.position);
            
            Log.Trace($"Camera click event handling:: ViewPos={viewPos} ClickData={cData}");
            // If zoom in close enough while photographing, then the clue is considered detected
            if(cData.CameraFOV < maxFov)
            {
                float extraRange = (1f - detectRange) / 2f;
                if(Math.Abs(viewPos.x - 0.5) < extraRange && Math.Abs(viewPos.y - 0.5) < extraRange) {
                    Log.Trace($"Camera click detected in {gameObject.name}", Constants.TagTimeline);
                    onDetected.Invoke(cData);
                } else Log.Trace($"Detect Range Check failed Extra Range={extraRange} || viewPos = {viewPos}", Constants.TagTimeline);
            } else{
                Log.Trace("FOV condition failed", Constants.TagTimeline);
            }
            // Check if the camera is clicked
            
        }
    }
}