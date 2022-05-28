using System.Collections;
using UnityEngine;

namespace MTT2
{
    [RequireComponent(typeof(Rigidbody2D), typeof(WheelJoint2D))]
    public class WheelController : MonoBehaviour
    {
        public float maxAngularVelocity;
        public Rigidbody2D RigidBody => _rigidBody;
        private Rigidbody2D _rigidBody;
        private Collider2D _collider2D;
        private WheelJoint2D _wheelJoint;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _wheelJoint = GetComponent<WheelJoint2D>();
        }

        private void FixedUpdate()
        {
            if(RigidBody.angularVelocity < -maxAngularVelocity) { RigidBody.angularVelocity = -maxAngularVelocity; }
            if(RigidBody.angularVelocity > maxAngularVelocity) { RigidBody.angularVelocity = maxAngularVelocity; }
        }
    }
}