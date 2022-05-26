using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    public class Booster : AddonBehaviourBase
    {
        bool value;
        public float baseBoostCoefficient;
        public float maxBoostCoefficient;
        public float coefficientGain;
        float coefficient;
        public override void AddonTrigger(InputAction.CallbackContext context)
        {
            value = context.ReadValueAsButton();
        }

        public void FixedUpdate()
        {
            CalculateCoefficient();
            if(value)
            {
                TruckController.RigidBody2d.AddForceAtPosition(Vector2.right * coefficient, transform.position, ForceMode2D.Impulse);
            }
        }

        private void CalculateCoefficient()
        {
            if(value)
            {
                coefficient += coefficientGain * Time.fixedDeltaTime;
                if(coefficient > maxBoostCoefficient)
                    coefficient = maxBoostCoefficient;
            }
            else
            {
                coefficient -= coefficientGain * Time.fixedDeltaTime;
                if (coefficient < baseBoostCoefficient)
                    coefficient = baseBoostCoefficient;
            }
        }
    }
}