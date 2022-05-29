using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/LevelDef")]
    public class LevelDef : ScriptableObject
    {
        public string sceneAssetName;

        public string levelName;
        public string levelDescription;
        public Texture2D levelIcon;
    }
}
