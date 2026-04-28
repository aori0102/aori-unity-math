# Aori.Unity.Math

## Package description

`com.aori.unity.math` is a small Unity math utility package that exposes the static partial `Aori.Math` helper class. It provides convenience methods for normalizing values, flattening vectors, and solving common 2D/3D geometry problems such as polygon tests, segment intersection, projection, and angle calculation.

## Features

- Normalize scalar values to a `0..1` range.
- Flatten `Vector3` values onto the XZ plane.
- Test whether a point lies inside a polygon.
- Convert world positions to XZ `Vector2` coordinates.
- Detect 2D segment intersections and collinear overlap.
- Compute triplet orientation and point-on-segment checks.
- Project a point onto a segment on the XZ plane.
- Calculate the angle between two edges sharing a center node.

## APIs

### Properties

None.

### Methods

#### `Aori.Math.Normalized(this float value, float min, float max)`

Normalizes a value into the range `[0, 1]` using the supplied bounds. Returns `0` when `min` and `max` are equal.

#### `Aori.Math.Flatten(this Vector3 v)`

Returns a copy of `v` flattened onto the XZ plane by setting its `Y` component to `0`.

#### `Aori.Math.IsPointInsidePolygon(Vector2 point, IReadOnlyList<Vector2> vertices)`

Determines whether `point` is inside the boundary of a polygon defined by `vertices`.

#### `Aori.Math.ToXZ(Vector3 position)`

Projects a world position to XZ-plane coordinates.

#### `Aori.Math.SegmentsIntersect(Vector2 firstStart, Vector2 firstEnd, Vector2 secondStart, Vector2 secondEnd)`

Determines whether two finite 2D segments intersect, including collinear overlap cases.

#### `Aori.Math.Orientation(Vector2 first, Vector2 second, Vector2 third)`

Computes the orientation of an ordered triplet in 2D.

#### `Aori.Math.OnSegment(Vector2 first, Vector2 point, Vector2 second)`

Checks whether a point lies on the closed segment defined by two endpoints.

#### `Aori.Math.Cross2D(Vector2 first, Vector2 second)`

Returns the 2D scalar cross product of two vectors.

#### `Aori.Math.TryProjectPointToSegmentXZ(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd, out float t, out float distanceSquared)`

Projects a point to a segment on XZ and returns the normalized parameter plus squared distance. Rejects projections that are too close to segment endpoints.

#### `Aori.Math.CalculateAngleBetweenEdges(Vector3 center, Vector3 firstNeighbor, Vector3 secondNeighbor)`

Calculates the angle in radians between two edges sharing the same center node.

## Dependencies

No dependencies.