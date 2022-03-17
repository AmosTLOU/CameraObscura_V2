using System;
using UnityEngine;
using Utility;

namespace Core {
    public abstract class ScriptableObjectDef : ScriptableObject {
        protected abstract void ResetData();

        public void OnDisable(){
            ResetData();
        }
        
        /*
        private void Reset(){
            Log.I("Data reset in editor");
        }

        private void OnEnable(){
            Log.I("SO Enabled!!".Color("green"));
        }

        private void OnDisable(){
            Log.I("SO Disabled!!".Color("red"));
            ResetData();
        }

        private void OnDestroy(){
            Log.I("Object destroyed");
        }

        public virtual void OnValidate(){
            Log.I("data changed in editor");
        }
        */
    }
}