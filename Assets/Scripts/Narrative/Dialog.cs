using UnityEngine;

namespace Narrative {
    [CreateAssetMenu(fileName = "Narrative", menuName = "Narrative/Dialog", order = 0)]
    public class Dialog : ScriptableObject {
        public string speakerName = "UNKNOWN";
        public string text = "Sample Text";
        public AudioClip voice;
    }
}