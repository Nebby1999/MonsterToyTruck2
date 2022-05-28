using System.Collections.Generic;
using UnityEngine;

namespace MTT2
{
    public static class Extensions
    {
        public static void SetMotorSpeed(this WheelJoint2D joint2d, float speed)
        {
            var motor = joint2d.motor;
            motor.motorSpeed = speed;
            joint2d.motor = motor;
        }

        public static void SetSuspension(this WheelJoint2D joint2d, float dampingRatio, float frequency, float angle)
        {
            var suspension = joint2d.suspension;
            suspension.dampingRatio = dampingRatio;
            suspension.frequency = frequency;
            suspension.angle = angle;
            joint2d.suspension = suspension;
        }
        public static void UpdateShapeToSprite(this PolygonCollider2D collider, Sprite sprite)
        {
            // ensure both valid
            if (collider != null && sprite != null)
            {
                // update count
                collider.pathCount = sprite.GetPhysicsShapeCount();

                // new paths variable
                List<Vector2> path = new List<Vector2>();

                // loop path count
                for (int i = 0; i < collider.pathCount; i++)
                {
                    // clear
                    path.Clear();
                    // get shape
                    sprite.GetPhysicsShape(i, path);
                    // set path
                    collider.SetPath(i, path.ToArray());
                }
            }
        }
    }
}