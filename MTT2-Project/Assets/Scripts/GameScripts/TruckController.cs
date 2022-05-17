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
        [SerializeField]
        private GameObject wheelPrefab;

        public List<WheelController> wheels = new List<WheelController>();
        public SpriteRenderer spriteRenderer;
        public float driveValue;
        public float steerValue;
        public bool breaking;
        [Header("Fuel Data")]
        public float fuel;
        public float baseFuelConsumptionRate;
        [Header("Motor Data")]
        public float baseSpeed;
        public float fuelMultiplierCoefficient;
        [Header("Other")]
        public float manouverability;

        private bool _isRunning;
        private Rigidbody2D _rigidBody;
        private ChildLocator _childLocator;
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _childLocator = GetComponent<ChildLocator>();
            SpawnWheels();
        }
        private void Start()
        {
            OnTruckDefChanged();
        }

        private void FixedUpdate()
        {
            FuelFixedUpdate();
            if (_isRunning)
            {
                MotorFixedUpdate();
            }
            _rigidBody.AddTorque(-steerValue * manouverability * Time.fixedDeltaTime);
        }

        public void SetWheelDef(WheelDef wheelDef)
        {
            foreach(WheelController controller in wheels)
            {
                controller.WheelDef = wheelDef;
            }
        }

        private void SpawnWheels()
        {
            var wheelTransforms = new Transform[] {_childLocator.FindChild("FrontWheel"), _childLocator.FindChild("BackWheel")};
            for(int i = 0; i < wheelTransforms.Length; i++)
            {
                var wheelControllerInstance = Instantiate(wheelPrefab, wheelTransforms[i], false).GetComponent<WheelController>();
                var wheelJoint = wheelControllerInstance.GetComponent<WheelJoint2D>();
                wheelJoint.connectedBody = _rigidBody;
                wheelJoint.connectedAnchor = wheelTransforms[i].localPosition;
                wheels.Add(wheelControllerInstance);
            }
        }

        private void OnTruckDefChanged()
        {
            if (!_truckDef)
                return;

            if (spriteRenderer)
                spriteRenderer.sprite = _truckDef.chasisSprite;

            _rigidBody.mass = _truckDef.mass;
            _rigidBody.sharedMaterial = _truckDef.chasisMaterial;

            fuel = _truckDef.maxFuel;
            baseFuelConsumptionRate = _truckDef.baseFuelConsumptionRate;

            baseSpeed = _truckDef.baseSpeed;
            fuelMultiplierCoefficient = _truckDef.fuelMultiplierCoefficient;

            manouverability = _truckDef.manouverability;

            foreach(WheelController wheel in wheels)
            {
                wheel.GetComponent<WheelJoint2D>().SetSuspension(_truckDef.dampingRatio, _truckDef.frequency, _truckDef.angle);
            }
        }

        private void MotorFixedUpdate()
        {
            for(int i = 0; i < wheels.Count; i++)
            {
                WheelController controller = wheels[i];
                if(!breaking)
                {
                    controller.RigidBody.AddTorque(baseSpeed * -driveValue * Time.fixedDeltaTime);
                }
                else
                {
                    controller.RigidBody.angularVelocity = controller.RigidBody.angularVelocity * controller.WheelDef.breakStrength;
                }
            }
        }

        private void FuelFixedUpdate()
        {
            var absDrive = Mathf.Abs(driveValue);
            var driveCoefficient = absDrive > 0 ? fuelMultiplierCoefficient * absDrive : 1;

            var finalCoefficient = driveCoefficient;
            fuel -= baseFuelConsumptionRate * finalCoefficient * Time.fixedDeltaTime;
            _isRunning = fuel > 0;
        }
    }
}