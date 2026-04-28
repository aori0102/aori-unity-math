using UnityEngine;

namespace Aori
{
    /// <summary>
    /// Provides general-purpose math utility helpers for the Aori math package.
    /// </summary>
    public static partial class Math
    {
        /// <summary>
        /// Normalizes a value into the range [0, 1] using the supplied bounds.
        /// </summary>
        /// <param name="value">Value to normalize.</param>
        /// <param name="min">Lower bound of the source range.</param>
        /// <param name="max">Upper bound of the source range.</param>
        /// <returns>
        /// The normalized value, or 0 when <paramref name="min"/> and <paramref name="max"/> are equal.
        /// </returns>
        public static float Normalized(this float value, float min, float max)
        {
            if (max - min == 0)
            {
                return 0;
            }

            return (value - min) / (max - min);
        }

        /// <summary>
        /// Returns a copy of <paramref name="v"/> flattened onto the XZ plane.
        /// </summary>
        /// <param name="v">Vector to flatten.</param>
        /// <returns>A vector with its Y component set to 0.</returns>
        public static Vector3 Flatten(this Vector3 v)
        {
            v.y = 0f;
            return v;
        }
    }
}