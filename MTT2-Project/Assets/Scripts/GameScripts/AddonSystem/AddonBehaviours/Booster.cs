using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    public class Booster : AddonBehaviourBase
    {
        public float baseBoostCoefficient;
        public float maxBoostCoefficient;
        public float coefficientGain;
        public Sprite on;
        public Sprite off;

        SpriteRenderer spriteRenderer;
        float coefficient;
        bool value;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public override void AddonTrigger(InputAction.CallbackContext context)
        {
            value = context.ReadValueAsButton();
        }

        public void FixedUpdate()
        {
            CalculateCoefficient();
            if(value)
            {
                TruckController.RigidBody2d.AddForceAtPosition(TruckController.transform.right * coefficient, transform.position, ForceMode2D.Impulse);
            }
            spriteRenderer.sprite = value ? on : off;
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