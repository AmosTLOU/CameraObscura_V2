using System.Collections;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace Characters {
    public class Player : MonoBehaviour {
        [SerializeField] private GameEvent wakeUpEvent;

        public void OnEventGameStart(IGameEventData data){
            Utils.TryConvertVal(data, out MiniGameStateEventData eventData);
            if (eventData.NewGame){
                Log.Info("Player waking up..", Constants.TagTimeline);
                StartCoroutine(WakeUp());
            }
        }

        private IEnumerator  WakeUp(){
            // Play the wake up animation
            yield return new WaitForSeconds(2);
            wakeUpEvent.Raise(new MiniGameStateEventData{NewGame = true, DayNumber = 1});
        }
    }
}