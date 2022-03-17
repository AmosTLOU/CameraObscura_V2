using Core;
using EventSystem.Data;
using UnityEngine;

namespace Characters {
    public class FirstVictim : BaseCharacter {
        public void TakeNap(IGameEventData data){
            Utils.TryConvertVal(data, out NightStartEventData nightStart);
            if (nightStart.dayNumber != 1){
                return;
            }
            Log.I("Taking Nap");
        }

        public void Killed(IGameEventData d){
            Utils.TryConvertVal(d, out VictimKilledEventData data);
            if (data.victimId.Equals(Info.ID)) {
                Log.I("Killed");
            } else {
                Log.I("someone else killed");
            }
        }

        public void Interrupted(){
            Log.I("Checking for interrupted");
            
            // Add check
            Log.I("Successfully interrupted");
        }
    }
}