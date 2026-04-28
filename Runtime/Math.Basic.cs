using UnityEngine;

namespace Aori
{
    public static partial class Math
    {
        public static float Normalized(this float value, float min, float max)
        {
            if (max - min == 0)
            {
                return 0;
            }

            return (value - min) / (max - min);
        }

        public static Vector3 Flatten(this Vector3 v)
        {
            v.y = 0f;
            return v;
        }
    }
}