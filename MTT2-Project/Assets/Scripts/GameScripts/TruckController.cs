using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.CSharpUtils;
using Nebby.UnityUtils;
using System;
using UnityEngine.InputSystem;

namespace MTT2
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TruckController : MonoBehaviour
    {
        public TruckDef TruckDef
        { 
            get
            {
                return _truckDef;
            }
            set
            {
                if(_truckDef != value)
                {
                    _truckDef = value;
                    OnTruckDefChanged();
                }
            }
        }
        [SerializeField]
        private TruckDef _truckDef;

        public WheelJoint2D[] wheels = Array.Empty<WheelJoint2D>();
        public SpriteRenderer spriteRenderer;
        public bool isRunning;
        public float driveValue;
        public float steerValue;
        [Header("Fuel Data")]
        public float fuel;
        public float baseFuelConsumptionRate;
        [Header("Motor Data")]
        public float baseSpeed;
        public float fuelMultiplierCoefficient;

        private float _currentSpeed;
        private Rigidbody2D _truckRigidbody;
        private void Awake()
        {
            _truckRigidbody = GetComponent<Rigidbody2D>();
        }
        public void Start()
        {
            OnTruckDefChanged();
        }

        public void FixedUpdate()
        {
            FuelFixedUpdate();
            if (isRunning)
            {
                MotorFixedUpdate();
            }
        }

        private void OnTruckDefChanged()
        {
            if (!_truckDef)
                return;

            if (spriteRenderer)
                spriteRenderer.sprite = _truckDef.chasisSprite;

            _truckRigidbody.mass = _truckDef.mass;
            _truckRigidbody.sharedMaterial = _truckDef.chasisMaterial;

            fuel = _truckDef.maxFuel;
            baseFuelConsumptionRate = _truckDef.baseFuelConsumptionRate;

            baseSpeed = _truckDef.baseSpeed;
            fuelMultiplierCoefficient = _truckDef.fuelMultiplierCoefficient;
        }

        private void MotorFixedUpdate()
        {
            Mathf.SmoothDamp(_currentSpeed, baseSpeed * driveValue, ref _currentSpeed, 1);
            for(int i = 0; i < wheels.Length; i++)
            {
                wheels[i].SetMotorSpeed(_currentSpeed);
            }
        }

        private void FuelFixedUpdate()
        {
            var absDrive = Mathf.Abs(driveValue);
            var driveCoefficient = absDrive > 0 ? fuelMultiplierCoefficient * absDrive : 1;

            var finalCoefficient = driveCoefficient;
            fuel -= baseFuelConsumptionRate * finalCoefficient * Time.fixedDeltaTime;
            isRunning = fuel > 0;
        }
    }
}