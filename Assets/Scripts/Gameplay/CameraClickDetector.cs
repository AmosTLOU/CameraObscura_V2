using System;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay {
    public class CameraClickDetector : MonoBehaviour {
        [SerializeField] private GameEvent clickEvent;
        [SerializeField] private float maxFov;
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
            
            Log.Info($"Camera click event handling:: ViewPos={viewPos} ClickData={cData}");
            // If zoom in close enough while photographing, then the clue is considered detected
            if(cData.CameraFOV < maxFov)
            {
                float extraRange = (1f - detectRange) / 2f;
                if (extraRange <= viewPos.x  && viewPos.x <= (1f - extraRange) &&
                    extraRange <= viewPos.y && viewPos.y <= (1f - extraRange)) {
                    Log.Info("Camera click detected", Constants.TagTimeline);
                    onDetected.Invoke(cData);
                } else Log.Info("Detect Range Check failed", Constants.TagTimeline);
            } else{
                Log.Info("FOV condition failed", Constants.TagTimeline);
            }
            // Check if the camera is clicked
            
        }
    }
}