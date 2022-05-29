using MTT2.Addons;
using Nebby.UnityUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTT2
{
    public class MTT2Application : SingletonBehaviour<MTT2Application>
    {
        public PlayerPreferenceData playerPreferenceData;

        public TruckDef[] Trucks;
        public WheelDef[] Wheels;
        public AddonDef[] Addons;
        public LevelDef[] levels;
        private int currentTruck, currentWheel;

        public override void Awake()
        {
            base.Awake();
            currentTruck = Trucks.ToList().IndexOf(playerPreferenceData.truckDef);
            currentWheel = Wheels.ToList().IndexOf(playerPreferenceData.wheelDef);
        }
        public TruckDef GetNextTruckDef()
        {
            if (currentTruck >= Trucks.Length - 1)
                currentTruck = 0;
            else
                currentTruck++;

            playerPreferenceData.truckDef = Trucks[currentTruck];
            return Trucks[currentTruck];
        }
        public WheelDef GetNextWheelDef()
        {
            if (currentWheel >= Wheels.Length - 1)
                currentWheel = 0;
            else
                currentWheel++;

            playerPreferenceData.wheelDef = Wheels[currentWheel];
            return Wheels[currentWheel];
        }
    }
}
