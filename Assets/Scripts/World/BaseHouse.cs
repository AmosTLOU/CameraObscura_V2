using UnityEngine;
using World.Data;

namespace World {
    public class BaseHouse : MonoBehaviour {
        [Header("Character information object")] 
        [SerializeField] private HouseInfo info;
        
        public HouseInfo Info => info;
    }
}