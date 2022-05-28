using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTT2.Addons
{
    public class ProjectileLauncher : AddonBehaviourBase
    {
        [SerializeField]
        private GameObject projectilePrefab;

        public Transform pivot;

        private Vector2 mousePos;
        private void Update()
        {
            Vector2 direction = mousePos - (Vector2)transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }

        public override void MouseControl(Vector2 mousePos)
        {
            this.mousePos = mousePos;
        }
    }
}

