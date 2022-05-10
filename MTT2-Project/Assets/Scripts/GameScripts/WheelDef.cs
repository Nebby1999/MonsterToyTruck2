using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/WheelDef")]
    public class WheelDef : ScriptableObject
    {
        public Sprite wheelSprite;
        public PhysicsMaterial2D physicsMaterial;

        [Header("Rigidbody Settings")]
        public float wheelMass;
        public float linearDrag;
        public float angularDrag;
    }
}