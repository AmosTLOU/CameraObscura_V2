using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace EventSystem.Data {
    /**
     * IGameEventData: Base struct for Event Data Types
     */
    public interface IGameEventData {
    }

    public struct EmptyGameEventData : IGameEventData {
        
    }

    
    /// <summary>
    /// Contains the short game state data. Used for basic data info.
    /// WorldManager.Instance.GetMiniGameState() can be used to get the current mini game state
    /// </summary>
    public struct MiniGameStateEventData : IGameEventData {
        public bool NewGame;
        public bool Restart;
        public int DayNumber;
        public bool IsNight;
    }

    public struct BeatStartEventData : IGameEventData {
        public GameBeat Beat;
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
        public Photo Photo;
        // public Vector2 ViewPos;
        public float CameraFOV;
        public Camera Cam;
        public bool Flash;
    }
}