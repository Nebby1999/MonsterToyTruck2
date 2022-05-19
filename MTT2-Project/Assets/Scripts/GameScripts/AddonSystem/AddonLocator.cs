using Nebby.UnityUtils;
using System;
using UnityEngine;

namespace MTT2.Addons
{
    public class AddonLocator : MonoBehaviour
    {
        public enum AddonLocation
        {
            Unknown,
            Top,
            Bottom,
            Front,
            Back
        }
        private AddonBehaviourBase _top, _bottom, _front, _back;
        [SerializeField]
        private Transform _topTransform, _bottomTransform, _frontTransform, _backTransform;

        public AddonManager AddonManager { get; private set; }
        public AddonBehaviourBase[] AddonBehaviours { get => new AddonBehaviourBase[] { _top, _bottom, _front, _back }; }

        public void Awake()
        {
            AddonManager = GetComponent<AddonManager>();
        }
        public void RemoveAddon(AddonLocation location)
        {
            switch(location)
            {
                case AddonLocation.Top: Destroy(_top.gameObject); break;
                case AddonLocation.Bottom: Destroy(_bottom.gameObject); break;
                case AddonLocation.Front: Destroy(_front.gameObject); break;
                case AddonLocation.Back: Destroy(_back.gameObject); break;
            }
        }

        public void SetAddon(GameObject addon, AddonLocation location)
        {
            if (!addon)
                return;

            Transform parentTransform = null;
            switch(location)
            {
                case AddonLocation.Top: parentTransform = _topTransform; break;
                case AddonLocation.Bottom: parentTransform = _bottomTransform; break;
                case AddonLocation.Front: parentTransform = _frontTransform; break;
                case AddonLocation.Back: parentTransform = _backTransform; break;
            }

            var behaviourBase = Instantiate(addon, parentTransform, false).GetComponent<AddonBehaviourBase>();

            if (!behaviourBase)
                throw new NullReferenceException($"No addon controller found in {addon}");

            SetAddon(behaviourBase, location);
        }

        private void SetAddon(AddonBehaviourBase controller, AddonLocation location)
        {
            switch(location)
            {
                case AddonLocation.Top: _top = controller; break;
                case AddonLocation.Bottom: _bottom = controller; break;
                case AddonLocation.Front: _front = controller; break;
                case AddonLocation.Back: _back = controller; break;
            }
            controller.TruckController = GetComponent<TruckController>();
        }

        public AddonBehaviourBase GetAddon(AddonLocation location)
        {
            switch(location)
            {
                case AddonLocation.Top: return _top;
                case AddonLocation.Bottom: return _bottom;
                case AddonLocation.Front: return _front;
                case AddonLocation.Back: return _back;
            }
            return null;
        }

        public T GetAddon<T>(AddonLocation location) where T : AddonBehaviourBase
        {
            switch (location)
            {
                case AddonLocation.Top: return (T)_top;
                case AddonLocation.Bottom: return (T)_bottom;
                case AddonLocation.Front: return (T)_front;
                case AddonLocation.Back: return (T)_back;
            }
            return null;
        }
    }
}