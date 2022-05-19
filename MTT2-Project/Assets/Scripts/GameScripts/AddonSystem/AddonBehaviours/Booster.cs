using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    public class Booster : AddonBehaviourBase
    {
        bool value;
        public override void AddonTrigger(InputAction.CallbackContext context)
        {
            value = context.ReadValueAsButton();
        }

        public void FixedUpdate()
        {
            Debug.Log(value);
            if(value)
            {
                TruckController.RigidBody2d.AddForceAtPosition(TruckController.transform.rotation.eulerAngles * 100, transform.position, ForceMode2D.Impulse);
            }
        }
    }
}