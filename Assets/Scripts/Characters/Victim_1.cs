using Core;
using EventSystem.Data;

namespace Characters {
    public class Victim_1 : BaseCharacter {
        public void TakeNap(IGameEventData data){
            Utils.TryConvertVal(data, out NightStartEventData nightStart);
            if (nightStart.dayNumber != 1){
                return;
            }
            Log.Info("Taking Nap");
        }

        //public void Killed(IGameEventData d){
        //    Utils.TryConvertVal(d, out VictimKilledEventData data);
        //    if (data.victimId.Equals(Info.ID)) {
        //        Log.Info("Killed");
        //    } else {
        //        Log.Info("someone else killed");
        //    }
        //}

        //public void Interrupted(){
        //    Log.Info("Checking for interrupted");

        //    // Add check
        //    Log.Info("Successfully interrupted");
        //}

        public override void Kill()
        {
            Log.Info($"Character {Info.name} Got Killed!! ");
            _animator.SetTrigger("Killed");
        }
    }
}