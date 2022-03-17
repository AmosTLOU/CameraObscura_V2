using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Utility;

namespace Characters.Data {
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "Data/Character", order = 0)]
    public class CharacterInfo : ScriptableObjectDef {
        // Read-only properties: Only set through editor
        [SerializeField] private string id;
        [SerializeField] private string characterName;
        [SerializeField] private Sprite image;
        
        // Can be set throughout the gameplay
        [SerializeField] private bool isSuspect = false;
        [SerializeField] private bool dead = false;
        [SerializeField] private List<string> startNotes = new List<string>();
        
        private List<string> notes = new List<string>();
        
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

        protected override void ResetData(){
            notes.Clear();
            notes.AddRange(startNotes);
        }
    }
}