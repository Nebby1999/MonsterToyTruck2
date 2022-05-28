using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    public abstract class AddonBehaviourBase : MonoBehaviour
    {
        public TruckController TruckController { get; set; }
        public virtual void AddonControl(Vector2 controlInfo) { }
        public virtual void MouseControl(Vector2 mousePos) { }
        public virtual void AddonTrigger(InputAction.CallbackContext context) { }
        public virtual void LeftClick(InputAction.CallbackContext context) { }
        public virtual void RightClick(InputAction.CallbackContext context) { }
    }
}