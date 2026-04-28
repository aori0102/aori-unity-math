using System.Collections.Generic;
using UnityEngine;

namespace Aori
{
    /// <summary>
    /// Provides geometry-focused math helpers for polygons, segments, and vector calculations.
    /// </summary>
    public static partial class Math
    {
        /// <summary>
        /// Determines whether <paramref name="point"/> is inside the boundary of a polygon
        /// defined by <paramref name="vertices"/>.
        /// </summary>
        /// <param name="point">Point to test in XZ coordinates.</param>
        /// <param name="vertices">Vertices that forms a polygon.</param>
        /// <returns>True if <paramref name="point"/> lies inside the polygon.</returns>
        public static bool IsPointInsidePolygon(
            Vector2 point,
            IReadOnlyList<Vector2> vertices
        )
        {
            if (vertices == null || vertices.Count < 3)
            {
                return false;
            }

            var inside = false;
            for (var i = 0; i < vertices.Count; i++)
            {
                // Get edge end points.
                var first = vertices[i];
                var second = vertices[(i + 1) % vertices.Count];

                // Checks for intersection by casting a ray horizontally to the
                // right from the point.
                //
                // first.y > point.y != second.y > point.y checks for possible
                // intersection that happens only if the segment's end points lies
                // on either side vertically of the ray.
                //
                // (second.x - first.x) * (point.y - first.y) /
                // (second.y - first.y) + first.x
                // is the linear interpolation for point on the current edge. We only
                // count intersection to the right of the point, hence the comparison
                // with point.x
                var intersects
                    = first.y > point.y != second.y > point.y &&
                      point.x < (second.x - first.x) * (point.y - first.y) /
                      (second.y - first.y) + first.x;

                // Flips inside flag. The point is within the polygon only when
                // casting a ray from the point to the right, it intersects with
                // the polygon's edges an odd number of time.
                if (intersects)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        /// <summary>
        /// Projects a world position to XZ-plane coordinates.
        /// </summary>
        /// <param name="position">World position.</param>
        /// <returns>XZ plane coordinates as a Vector2.</returns>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public static Vector2 ToXZ(Vector3 position)
        {
            return new Vector2(position.x, position.z);
        }

        /// <summary>
        /// Determines whether two finite 2D segments intersect, including collinear overlap cases.
        /// </summary>
        /// <param name="firstStart">First segment start.</param>
        /// <param name="firstEnd">First segment end.</param>
        /// <param name="secondStart">Second segment start.</param>
        /// <param name="secondEnd">Second segment end.</param>
        /// <returns>True if segments intersect or touch.</returns>
        public static bool SegmentsIntersect(
            Vector2 firstStart,
            Vector2 firstEnd,
            Vector2 secondStart,
            Vector2 secondEnd
        )
        {
            var o1 = Orientation(firstStart, firstEnd, secondStart);
            var o2 = Orientation(firstStart, firstEnd, secondEnd);
            var o3 = Orientation(secondStart, secondEnd, firstStart);
            var o4 = Orientation(secondStart, secondEnd, firstEnd);

            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            if (o1 == 0 && OnSegment(firstStart, secondStart, firstEnd))
            {
                return true;
            }

            if (o2 == 0 && OnSegment(firstStart, secondEnd, firstEnd))
            {
                return true;
            }

            if (o3 == 0 && OnSegment(secondStart, firstStart, secondEnd))
            {
                return true;
            }

            return o4 == 0 && OnSegment(secondStart, firstEnd, secondEnd);
        }

        /// <summary>
        /// Computes orientation of ordered triplet in 2D.
        /// </summary>
        /// <param name="first">First point.</param>
        /// <param name="second">Second point.</param>
        /// <param name="third">Third point.</param>
        /// <returns>0 for collinear, 1 for clockwise, 2 for counter-clockwise.</returns>
        public static int Orientation(Vector2 first, Vector2 second, Vector2 third)
        {
            var value = (second.y - first.y) * (third.x - second.x) -
                        (second.x - first.x) * (third.y - second.y);

            if (Mathf.Abs(value) <= 0.0001f)
            {
                return 0;
            }

            return value > 0f ? 1 : 2;
        }

        /// <summary>
        /// Checks whether a point lies on the closed segment defined by two endpoints.
        /// </summary>
        /// <param name="first">Segment start.</param>
        /// <param name="point">Point to test.</param>
        /// <param name="second">Segment end.</param>
        /// <returns>True if point lies within segment bounds (with epsilon tolerance).</returns>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public static bool OnSegment(Vector2 first, Vector2 point, Vector2 second)
        {
            return
                point.x <= Mathf.Max(first.x, second.x) + 0.0001f &&
                point.x >= Mathf.Min(first.x, second.x) - 0.0001f &&
                point.y <= Mathf.Max(first.y, second.y) + 0.0001f &&
                point.y >= Mathf.Min(first.y, second.y) - 0.0001f;
        }

        /// <summary>
        /// Returns the 2D scalar cross product of two vectors.
        /// </summary>
        /// <param name="first">First vector.</param>
        /// <param name="second">Second vector.</param>
        /// <returns>Scalar cross product value.</returns>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public static float Cross2D(Vector2 first, Vector2 second)
        {
            return first.x * second.y - first.y * second.x;
        }

        /// <summary>
        /// Projects a point to a segment on XZ and returns normalized parameter plus squared distance.
        /// Rejects projections that are too close to segment endpoints.
        /// </summary>
        /// <param name="point">Point to project.</param>
        /// <param name="segmentStart">Segment start.</param>
        /// <param name="segmentEnd">Segment end.</param>
        /// <param name="t">Normalized parameter of projection on segment.</param>
        /// <param name="distanceSquared">Squared distance from point to projected point.</param>
        /// <returns>True if projection is valid and within open segment bounds.</returns>
        public static bool TryProjectPointToSegmentXZ(
            Vector3 point,
            Vector3 segmentStart,
            Vector3 segmentEnd,
            out float t,
            out float distanceSquared
        )
        {
            t = 0f;
            distanceSquared = float.MaxValue;

            var point2D = ToXZ(point);
            var start2D = ToXZ(segmentStart);
            var end2D = ToXZ(segmentEnd);

            var segment = end2D - start2D;
            var lengthSquared = segment.sqrMagnitude;
            if (lengthSquared <= 0.000001f)
            {
                return false;
            }

            t = Vector2.Dot(point2D - start2D, segment) / lengthSquared;
            const float endpointEpsilon = 0.0001f;
            if (t <= endpointEpsilon || t >= 1f - endpointEpsilon)
            {
                return false;
            }

            var projection = start2D + segment * t;
            distanceSquared = (point2D - projection).sqrMagnitude;
            return true;
        }

        /// <summary>
        /// Calculates the angle in radians between two edges sharing the same center node.
        /// </summary>
        /// <param name="center">Shared center node.</param>
        /// <param name="firstNeighbor">Endpoint of the first edge.</param>
        /// <param name="secondNeighbor">Endpoint of the second edge.</param>
        /// <returns>Angle in radians in the range [0, PI].</returns>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public static float CalculateAngleBetweenEdges(
            Vector3 center,
            Vector3 firstNeighbor,
            Vector3 secondNeighbor
        )
        {
            var directionToFirst = (firstNeighbor - center).normalized;
            var directionToSecond = (secondNeighbor - center).normalized;

            var dotProduct = Vector3.Dot(directionToFirst, directionToSecond);
            dotProduct = Mathf.Clamp(dotProduct, -1f, 1f);

            return Mathf.Acos(dotProduct);
        }
    }
}