using Characters;
using PathCreation;
using UnityEngine;

namespace Gameplay {
    public class KillerPath : MonoBehaviour {
        [SerializeField] public PathCreator sneakPath;
        [SerializeField] public BaseCharacter victim;
        [SerializeField] public PathCreator runAwayPath;
    }
}