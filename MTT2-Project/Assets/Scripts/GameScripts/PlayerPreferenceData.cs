using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTT2.Addons;
using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/PlayerPreferenceData")]
    public class PlayerPreferenceData : ScriptableObject
    {
        public TruckDef truckDef;
        public WheelDef wheelDef;

        public AddonManager.AddonSpawnData addonSpawnData;
    }
}
