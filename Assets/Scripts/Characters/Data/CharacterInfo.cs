using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Utility;

namespace Characters.Data {
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "Data/Character", order = 0)]
    public class CharacterInfo : ScriptableObject {
        // Read-only properties: Only set through editor
        [SerializeField] private string id;
        [SerializeField] private string characterName;
        [SerializeField] private Sprite image;
        
        // Can be set throughout the gameplay
        [SerializeField] private bool isSuspect;
        [SerializeField] private bool dead;
        [SerializeField] private List<string> notes = new List<string>();
        
        // Getters
        public string ID => id;
        public string CharacterName => characterName;
        public Sprite Image => image;
        public List<string> Notes => notes;
        public bool IsSuspect => isSuspect;
        public bool Dead => dead;

        public void AddNote(string note) {
            notes.Add(note);
        }
        private void OnValidate(){
            Log.I("Character info change".Color(Color.red));
        }
    }
}