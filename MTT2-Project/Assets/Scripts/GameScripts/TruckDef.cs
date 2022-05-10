using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/TruckDef")]
    public class TruckDef : ScriptableObject
    {
        public Sprite chasisSprite;
        public PhysicsMaterial2D chasisMaterial;

        public float mass;

        [Header("TruckController Settings")]
        public float maxFuel;
        public float baseFuelConsumptionRate;
        public float baseSpeed;
        public float fuelMultiplierCoefficient;

        [Header("Suspension")]
        public float dampingRatio;
        public float frequency;
        public float angle;
    }
}