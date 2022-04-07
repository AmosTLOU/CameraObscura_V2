using System;
using UnityEngine;
using CharacterInfo = Characters.Data.CharacterInfo;

namespace Characters {
    /// <summary>
    /// The Base Character class. Real character scripts will be different
    /// </summary>
    public abstract class BaseCharacter: MonoBehaviour {
        [Header("Character Info")] 
        [SerializeField] private CharacterInfo info;
        
        public CharacterInfo Info => info;
        protected Animator _animator;

        protected void Awake(){
            _animator = GetComponent<Animator>();
        }
    }
}