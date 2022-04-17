﻿using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterInfo = Characters.Data.CharacterInfo;

namespace Characters {
    public class CharacterManager : SingletonBehaviour<CharacterManager> {
        [SerializeField] private List<BaseCharacter> characters = new List<BaseCharacter>();
        [SerializeField] private Killer killer;
        
        private Dictionary<string, BaseCharacter> _characters = new Dictionary<string, BaseCharacter>();

        private void Start(){
            foreach (var character in characters){
                var id = character.Info.ID;
                if (_characters.ContainsKey(id)){
                    Log.Err($"Two characters have the same id {id}");
                }
                _characters[id] = character;
            }

            _characters[killer.Info.ID] = killer;
        }

        public BaseCharacter GetCharacter(string id) {
            return _characters[id];
        }

        public Killer GetKiller(){
            return killer;
        }
    }
}