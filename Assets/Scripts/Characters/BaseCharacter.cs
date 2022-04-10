using Core;
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
        private static readonly int Dead = Animator.StringToHash("dead");

        protected void Awake(){
            _animator = GetComponent<Animator>();
        }

        public virtual void Kill(){
            Log.Info($"Character Killed!! {info.name}");
            _animator.SetBool(Dead, true);
        }
    }
}