using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;

namespace MTT2.Addons
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/AddonDef")]
    public class AddonDef : ScriptableObject
    {
        public GameObject addonPrefab;
        public string addonName;
        public string addonDescription;
    }
}