
using System;
using Core;
using EventSystem.Data;
using Utility;

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
        
        public void OnEventBeatStart(IGameEventData eventData){
            if (!Utils.TryConvertVal(eventData, out BeatStartEventData data)){
                Log.Err("Error Converting Type");
                return;
            };
            Log.Info($"Starting New Act = {data.Beat}".Size(20).Color("White"));
            if(data.Beat == GameBeat.Suspect) {
                ChopAnimation();
            }
        }
    }
}