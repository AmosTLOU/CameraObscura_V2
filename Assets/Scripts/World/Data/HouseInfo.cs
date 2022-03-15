using Core;
using UnityEngine;

namespace World.Data {
    [CreateAssetMenu(fileName = "HouseInfo", menuName = "Data/HouseInfo", order = 0)]
    public class HouseInfo : ScriptableObjectDef {
        // Read-only properties: Only set through editor
        [SerializeField] private string id;
        
        // Getters
        public string ID => id;

        protected override void ResetData() {
            
        }
    }
}