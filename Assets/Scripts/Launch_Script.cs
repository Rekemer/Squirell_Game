using UnityEngine;
namespace Launch
{
    public struct LaunchData
    {
        public readonly Vector3 initialVelocity; // начальная скорость
        public readonly float timeToTarget;  // время полета

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}