using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using CharacterInfo = Characters.Data.CharacterInfo;

namespace Characters {
    public class CharacterManager : SingletonBehaviour<CharacterManager> {
        [SerializeField] private List<CharacterInfo> _characters = new List<CharacterInfo>();

        private void Start(){
            _characters[0].AddNote("hello there");
        }

        private static int c = 1;
        private void Update(){
            if (c++ % 100 == 0){
                c -= 100;
                _characters[0].AddNote($"hello there {_characters[0].Notes.Count}");
            }
        }
    }
}