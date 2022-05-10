using System.Collections;
using UnityEngine;

namespace MTT2
{
    [RequireComponent(typeof(Rigidbody2D), typeof(WheelJoint2D))]
    public class WheelController : MonoBehaviour
    {
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
        public SpriteRenderer spriteRenderer;
        private Rigidbody2D _rigidBody;
        private Collider2D _collider2D;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
        }
        private void Start()
        {
            OnWheelDefChanged();
        }

        private void OnWheelDefChanged()
        {
            if (!_wheelDef)
                return;

            if(spriteRenderer)
                spriteRenderer.sprite = _wheelDef.wheelSprite;

            _rigidBody.mass = _wheelDef.wheelMass;
            _rigidBody.angularDrag = _wheelDef.angularDrag;
            _rigidBody.drag = _wheelDef.linearDrag;

            _rigidBody.sharedMaterial = _wheelDef.physicsMaterial;
        }
    }
}