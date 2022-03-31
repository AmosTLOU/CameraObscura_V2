using System;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace GameInput {
    public class InputDebugger : SingletonBehaviour<InputDebugger> {

        [SerializeField] private GameEvent cameraFlashEvent;
        private void Update(){
            if (Input.GetKeyDown(KeyCode.P)){
                cameraFlashEvent.Raise(new CameraFlashEventData{debug = true});
            }
        }
    }
}