
using System;

namespace Characters {
    public class Chef : BaseCharacter {

        public bool Chop;
        public bool SlowRun;
        public bool Idle;
        private void Start()
        {
            
        }

        private void Update()
        {
            if (Chop)
            {
                Chop = false;
                _animator.SetTrigger("Chop");
            }

            if (SlowRun)
            {
                SlowRun = false;
                _animator.SetTrigger("SlowRun");
            }

            if (Idle)
            {
                Idle = false;
                _animator.SetTrigger("Idle");
            }
        }

        public void ChopAnimation()
        {
            _animator.SetTrigger("Chop");
        }

        public void SlowRunAnimation()
        {
            _animator.SetTrigger("SlowRun");
        }

        public void IdleAnimation()
        {
            _animator.SetTrigger("Idle");
        }
    }
}