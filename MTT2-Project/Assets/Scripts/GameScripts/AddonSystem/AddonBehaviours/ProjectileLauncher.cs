using Nebby.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2.Addons
{
    public class ProjectileLauncher : AddonBehaviourBase
    {
        [SerializeField]
        private GameObject projectilePrefab;

        public Transform pivot;
        public float angle;
        private Vector2 mousePos;
        private ChildLocator locator;
        private void Awake()
        {
            locator = GetComponent<ChildLocator>();    
        }

        private void Update()
        {
            Vector2 direction = CameraController.Instance.Camera.ScreenToWorldPoint(mousePos) - pivot.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public override void MouseControl(Vector2 mousePos)
        {
            this.mousePos = mousePos;
        }

        public override void LeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                ShootProjectile();
        }

        private void ShootProjectile()
        {
            var projectileInstance = Instantiate(projectilePrefab, locator.FindChild("Nozzle").position, pivot.rotation);
        }
    }
}

