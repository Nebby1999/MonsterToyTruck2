﻿using UnityEngine;

namespace MTT2
{
    [CreateAssetMenu(menuName = "MonsterToyTruck2/WheelDef")]
    public class WheelDef : ScriptableObject
    {
        public Sprite wheelSprite;
        public PhysicsMaterial2D physicsMaterial;

        [Range(0f, 1f)]
        public float breakStrength;
        public float maxTorqueSpeed;

        [Header("Rigidbody Settings")]
        public float wheelMass;
        public float linearDrag;
        public float angularDrag;
    }
}