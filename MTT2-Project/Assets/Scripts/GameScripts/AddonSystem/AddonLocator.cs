using Nebby.UnityUtils;
using UnityEngine;

namespace MTT2.Addon
{
    public class AddonLocator : MonoBehaviour
    {
        [SerializeField]
        private AddonController _topController, _bottomController, _frontController, _backController;

        public AddonController Top { get => _topController; }
        public AddonController Bottom { get => _bottomController; }
        public AddonController Front { get => _frontController; }
        public AddonController Back { get => _backController; }
    }
}