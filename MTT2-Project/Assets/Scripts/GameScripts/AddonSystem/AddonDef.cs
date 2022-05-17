using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;

namespace MTT2.Addon
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/AddonDef")]
    public class AddonDef : ScriptableObject
    {
        [SerializableSystemType.RequiredBaseType(typeof(AddonBehaviourBase))]
        public SerializableSystemType behaviourClass;
    }
}