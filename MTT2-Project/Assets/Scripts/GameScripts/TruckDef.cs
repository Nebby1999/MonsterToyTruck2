using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/TruckDef")]
    public class TruckDef : ScriptableObject
    {
        [Tooltip("The sprite tied to this truckDef")]
        public Sprite chasisSprite;
        [Tooltip("The physics material for this TruckDef")]
        public PhysicsMaterial2D chasisMaterial;

        [Tooltip("The Mass applied to the rigidbody")]
        public float mass;

        [Header("TruckController Settings")]
        [Tooltip("Amount of fuel this truckDef has")]
        public float maxFuelBase;
        [Tooltip("Base consumption rate for fuel, lower values means the fuel will be consumed slower")]
        public float baseFuelConsumptionRate;
        [Tooltip("The base speed for this truck")]
        public float baseSpeed;
        [Tooltip("When the truck is driving, this coefficient is applied to the consumption rate")]
        public float fuelDrivingCoefficient;
        [Tooltip("How easy it is to rotate in this truck")]
        public float manouverability;

        [Header("Wheel Joint Locations")]
        public Vector2 frontWheel;
        public Vector2 backWheel;

        [Header("Suspension")]
        public float dampingRatio;
        public float frequency;
        public float angle;
    }
}