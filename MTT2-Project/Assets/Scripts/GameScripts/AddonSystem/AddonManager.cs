using System;
using System.Collections.Generic;
using Nebby;
using Nebby.UnityUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    [RequireComponent(typeof(AddonLocator))]
    public class AddonManager : MonoBehaviour
    {
        [Serializable]
        public struct AddonSpawnData
        {
            public AddonDef topAddon;
            public AddonDef bottomAddon;
            public AddonDef frontAddon;
            public AddonDef backAddon;
        }

        public AddonLocator AddonLocator { get; private set; }
        public ChildLocator ChildLocator { get; private set; }
        public Vector2 AddonControl { get; set; }
        public Vector2 MousePos { get; set; }

        private void Awake()
        {
            AddonLocator = GetComponent<AddonLocator>();
            ChildLocator = GetComponent<ChildLocator>();
        }

        private void FixedUpdate()
        {
            foreach(AddonBehaviourBase behaviourBase in AddonLocator.AddonBehaviours)
            {
                if (!behaviourBase)
                    continue;
                behaviourBase.AddonControl(AddonControl);
                behaviourBase.MouseControl(MousePos);
            }
        }

        public void RelyTrigger(ref InputAction.CallbackContext context, AddonLocator.AddonLocation location)
        {
            AddonBehaviourBase behaviour = AddonLocator.GetAddon(location);
            if (!behaviour)
                return;
            behaviour.AddonTrigger(context);
        }

        public void RelyClick(ref InputAction.CallbackContext context, bool isRightClick)
        {
            foreach(AddonBehaviourBase behaviourBase in AddonLocator.AddonBehaviours)
            {
                if (!behaviourBase)
                    continue;

                if (isRightClick)
                    behaviourBase.RightClick(context);
                else
                    behaviourBase.LeftClick(context);
            }
        }

        public void SpawnAddons(AddonSpawnData data)
        {
            AddonLocator.SetAddon(data.topAddon?.addonPrefab, AddonLocator.AddonLocation.Top);
            AddonLocator.SetAddon(data.bottomAddon?.addonPrefab, AddonLocator.AddonLocation.Bottom);
            AddonLocator.SetAddon(data.frontAddon?.addonPrefab, AddonLocator.AddonLocation.Front);
            AddonLocator.SetAddon(data.backAddon?.addonPrefab, AddonLocator.AddonLocation.Back);
        }
    }
}