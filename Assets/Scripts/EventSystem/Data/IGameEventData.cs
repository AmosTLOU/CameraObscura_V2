using UnityEngine;

namespace EventSystem.Data {
    /**
     * IGameEventData: Base struct for Event Data Types
     */
    public interface IGameEventData {
    }

    public struct EmptyGameEventData : IGameEventData {
        
    }

    public struct TimeEventData : IGameEventData {
        public int dayNumber;
        public bool isNight;
    }

    public struct NightStartEventData : IGameEventData {
        public int dayNumber;
    }

    public struct VictimKilledEventData : IGameEventData {
        public string victimId;
    }

    public struct CameraClickEventData : IGameEventData {
        public bool debug;
    }
    public struct CameraFlashEventData : IGameEventData {
        public Vector2 location;
    }
}