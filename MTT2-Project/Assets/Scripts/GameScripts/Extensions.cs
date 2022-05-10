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
    }
}