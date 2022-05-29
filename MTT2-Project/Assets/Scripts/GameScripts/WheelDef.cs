using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/WheelDef")]
    public class WheelDef : ScriptableObject
    {
        [TextArea]
        public string wheelName, wheelDescription;
        public GameObject wheelPrefab;

        [Range(0f, 1f)]
        public float breakStrength;
        public float maxAngularVelocity;

        [Header("Rigidbody Settings")]
        public float wheelMass;
        public float linearDrag;
        public float angularDrag;
    }
}