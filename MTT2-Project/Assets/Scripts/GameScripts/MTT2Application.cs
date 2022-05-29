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
    }
}
