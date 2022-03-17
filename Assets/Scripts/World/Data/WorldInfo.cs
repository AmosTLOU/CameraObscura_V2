using System.Collections.Generic;
using UnityEngine;

namespace World.Data {
    
    [CreateAssetMenu(fileName = "WorldInfo", menuName = "Data/WorldInfo", order = 0)]
    public class WorldInfo : ScriptableObject {
        public List<HouseInfo> houses;
        public List<CharacterInfo> characters;
    }
}