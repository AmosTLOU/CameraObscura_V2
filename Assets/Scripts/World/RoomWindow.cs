using UnityEngine;

namespace World {
    public class RoomWindow : MonoBehaviour {

        protected Animator _animator;
        public bool remainClose;
        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            if (remainClose)
                _animator.SetTrigger("idle_close");
        }
        public void UpdateState(bool open){

            if (open)
                _animator.SetTrigger("open");
            else
                _animator.SetTrigger("close");
        }
    }
}