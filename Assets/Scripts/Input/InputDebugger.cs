using System;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace GameInput {
    public class InputDebugger : SingletonBehaviour<InputDebugger> {

        [SerializeField] private GameEvent cameraClickEvent;
        private void Update(){
            if (Input.GetKeyDown(KeyCode.P)){
                cameraClickEvent.Raise(new CameraClickEventData{debug = true});
            }
        }
    }
}