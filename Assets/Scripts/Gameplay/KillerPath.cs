using Characters;
using PathCreation;
using UnityEngine;

namespace Gameplay {
    public class KillerPath : MonoBehaviour {
        [SerializeField] private PathCreator sneakPath;
        [SerializeField] private BaseCharacter victim;
        [SerializeField] private PathCreator runAwayPath;
    }
}