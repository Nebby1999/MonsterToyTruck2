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
    }
}