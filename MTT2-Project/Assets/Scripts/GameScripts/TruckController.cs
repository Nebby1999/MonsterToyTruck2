using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.CSharpUtils;
using Nebby.UnityUtils;
using System;
using UnityEngine.InputSystem;
using MTT2.Addons;

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
        public WheelDef WheelDef
        {
            get
            {
                return _wheelDef;
            }
            set
            {
                if(_wheelDef != value)
                {
                    _wheelDef = value;
                    OnWheelDefChanged();
                }
            }
        }
        [SerializeField]
        private WheelDef _wheelDef;

        public List<WheelController> wheels = new List<WheelController>();
        public Transform frontWheel;
        public Transform backWheel;
        public float fuel;
        public float baseSpeed;
        public float manouverability;


        private float baseFuelConsumptionRate;
        private float fuelMultiplierCoefficient;
        private bool _isRunning;

        public Rigidbody2D RigidBody2d { get; private set; }
        public AddonManager AddonManager { get; private set; }
        public float DriveValue { get; set; }
        public float SteerValue { get; set; }
        public bool Breaking { get; set; }

        private void Awake()
        {
            RigidBody2d = GetComponent<Rigidbody2D>();
            AddonManager = GetComponent<AddonManager>();
        }

        private void FixedUpdate()
        {
            FuelFixedUpdate();
            if (_isRunning)
            {
                MotorFixedUpdate();
            }
            RigidBody2d.AddTorque(-SteerValue * manouverability * Time.fixedDeltaTime);
        }

        private void OnTruckDefChanged()
        {
            if (!_truckDef)
                return;

            RigidBody2d.mass = _truckDef.mass;

            fuel = _truckDef.maxFuelBase;
            baseFuelConsumptionRate = _truckDef.baseFuelConsumptionRate;

            baseSpeed = _truckDef.baseSpeed;
            fuelMultiplierCoefficient = _truckDef.fuelDrivingCoefficient;

            manouverability = _truckDef.manouverability;
        }

        private void OnWheelDefChanged()
        {
            if (!_wheelDef)
                return;

            foreach (Transform t in backWheel)
                Destroy(t.gameObject);
            foreach(Transform t in frontWheel)
                Destroy (t.gameObject);

            wheels.Clear();
            SpawnWheels();
        }

        private void SpawnWheels()
        {
            var wheelTransforms = new Transform[] { frontWheel, backWheel };
            var wheelJoints = GetComponents<WheelJoint2D>();
            for (int i = 0; i < wheelTransforms.Length; i++)
            {
                var wheel = Instantiate(_wheelDef.wheelPrefab, wheelTransforms[i], false);
                wheel.transform.localPosition -= Vector3.up * 2;

                var wheelControllerInstance = wheel.GetComponent<WheelController>();
                var wheelJoint = wheelJoints[i];
                wheelJoint.connectedBody = wheelControllerInstance.RigidBody;
                wheelJoint.anchor = wheelTransforms[i].localPosition - Vector3.up;
                wheelJoint.SetSuspension(_truckDef.dampingRatio, _truckDef.frequency, _truckDef.angle);
                //var wheelJoint = wheelControllerInstance.GetComponent<WheelJoint2D>();
                //wheelJoint.connectedBody = RigidBody2d;
                //wheelJoint.connectedAnchor = wheelTransforms[i].localPosition;
                //wheelJoint.SetSuspension(_truckDef.dampingRatio, _truckDef.frequency, _truckDef.angle);

                wheelControllerInstance.maxAngularVelocity = _wheelDef.maxAngularVelocity;
                wheelControllerInstance.RigidBody.mass = _wheelDef.wheelMass;
                wheelControllerInstance.RigidBody.angularDrag = _wheelDef.angularDrag;
                wheelControllerInstance.RigidBody.drag = _wheelDef.linearDrag;

                wheels.Add(wheelControllerInstance);
            }
        }

        private void MotorFixedUpdate()
        {
            for(int i = 0; i < wheels.Count; i++)
            {
                WheelController controller = wheels[i];
                if(!Breaking)
                {
                    controller.RigidBody.AddTorque(baseSpeed * -DriveValue * Time.fixedDeltaTime);
                }
                else
                {
                    controller.RigidBody.angularVelocity = controller.RigidBody.angularVelocity * WheelDef.breakStrength;
                }
            }
        }

        private void FuelFixedUpdate()
        {
            var absDrive = Mathf.Abs(DriveValue);
            var driveCoefficient = absDrive > 0 ? fuelMultiplierCoefficient * absDrive : 1;

            var finalCoefficient = driveCoefficient;
            fuel -= baseFuelConsumptionRate * finalCoefficient * Time.fixedDeltaTime;
            _isRunning = fuel > 0;
        }
    }
}