using UnityEngine;

namespace EventSystem.Data {
    /**
     * IGameEventData: Base struct for Event Data Types
     */
    public interface IGameEventData {
    }

    public struct EmptyGameEventData : IGameEventData {
        
    }

    public struct NightStartEventData : IGameEventData {
        public int dayNumber;
    }

    public struct VictimKilledEventData : IGameEventData {
        public string victimId;
    }
    
    public struct CameraFlashEventData : IGameEventData {
        public Vector2 location;
    }
}