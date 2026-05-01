# Package description

`com.aori.unity.math` is a small Unity math utility package that provides convenience 
methods for normalizing values, flattening vectors, and solving common 2D/3D
geometry problems such as polygon tests, segment intersection, projection,
and angle calculation.

# Features

- Normalize scalar values to a `0..1` range.
- Flatten `Vector3` values onto the XZ plane.
- Test whether a point lies inside a polygon.
- Convert world positions to XZ `Vector2` coordinates.
- Detect 2D segment intersections and collinear overlap.
- Compute triplet orientation and point-on-segment checks.
- Project a point onto a segment on the XZ plane.
- Calculate the angle between two edges sharing a center node.

# APIs

## Properties

None.

## Methods

### Table of Contents

* [
  <b>Aori.Math.Normalized(</b><i>this float value, float min, float max</i><b>)</b>
  ](#aorimathnormalized)

* [
  <b>Aori.Math.Flatten(</b><i>this Vector3 v</i><b>)</b>
  ](#aorimathflatten)

* [
  <b>Aori.Math.IsPointInsidePolygon(</b><i>Vector2 point, IReadOnlyList<Vector2>
vertices</i><b>)</b>
  ](#aorimathispointinsidepolygon)

* [
  <b>Aori.Math.ToXZ(</b><i>Vector3 position</i><b>)</b>
  ](#aorimathtoxz)

* [
  <b>Aori.Math.SegmentsIntersect(</b><i>Vector2 firstStart, Vector2 firstEnd,
Vector2secondStart, Vector2 secondEnd</i><b>)</b>
  ](#aorimathsegmentsintersect)

* [
  <b>Aori.Math.Orientation(</b><i>Vector2 first, Vector2 second, Vector2 third</i><b>)</b>
  ](#aorimathorientation)

* [
  <b>Aori.Math.OnSegment(</b><i>Vector2 first, Vector2 point, Vector2 second</i><b>)</b>
  ](#aorimathonsegment)

* [
  <b>Aori.Math.Cross2D(</b><i>Vector2 first, Vector2 second</i><b>)</b>
  ](#aorimathcross2d)

* [
  <b>Aori.Math.TryProjectPointToSegmentXZ(</b><i>Vector3 point, Vector3 segmentStart, 
Vector3 segmentEnd, out float t,out float distanceSquared</i><b>)</b>
  ](#aorimathtryprojectpointtosegmentxz)

* [
  <b>Aori.Math.CalculateAngleBetweenEdges(</b><i>Vector3 center, Vector3 firstNeighbor, 
Vector3 secondNeighbor</i><b>)</b>
  ](#aorimathcalculateanglebetweenedges)

***

### Aori.Math.Normalized

### Declaration

`Aori.Math.Normalized(this float value, float min, float max)`

### Description

Normalizes a value into the range `[0, 1]` using the supplied bounds. Returns 
`0` when `min` and `max` are equal.

### Parameters

| Parameter | Type         | Description                                 |
|:----------|:-------------|:--------------------------------------------|
| `value`   | `this float` | The value to normalize.                     |
| `min`     | `float`      | The lower bound of the normalization range. |
| `max`     | `float`      | The upper bound of the normalization range. |

### Returns

A normalized value in the range `[0, 1]` corresponding to `value`'s position
between `min` and `max`. Returns `0`
if `min` and `max` are equal.

If `value < min` or `value > max`, the returned value will be clamped into the 
range `[0, 1]`.

***

### Aori.Math.Flatten

### Declaration

`Aori.Math.Flatten(this Vector3 v)`

### Description

Returns a copy of `v` flattened onto the XZ plane by setting its `Y` component to `0`.

### Parameters

| Parameter | Type           | Description            |
|:----------|:---------------|:-----------------------|
| `v`       | `this Vector3` | The vector to flatten. |

### Returns

A copy of `v` with its `Y` component set to `0`.

***

### Aori.Math.IsPointInsidePolygon

### Declaration

`Aori.Math.IsPointInsidePolygon(Vector2 point, IReadOnlyList<Vector2> vertices)`

### Description

Determines whether `point` is inside the boundary of a polygon defined by `vertices`.

### Parameters

| Parameter  | Type                     | Description                      |
|:-----------|:-------------------------|:---------------------------------|
| `point`    | `Vector2`                | Point to test in XZ coordinates. |
| `vertices` | `IReadOnlyList<Vector2>` | Vertices that form the polygon.  |

### Returns

`true` if `point` lies inside the polygon; otherwise, `false`.

***

### Aori.Math.ToXZ

### Declaration

`Aori.Math.ToXZ(Vector3 position)`

### Description

Projects a world position to XZ-plane coordinates.

### Parameters

| Parameter  | Type      | Description     |
|:-----------|:----------|:----------------|
| `position` | `Vector3` | World position. |

### Returns

XZ plane coordinates as a `Vector2`.

***

### Aori.Math.SegmentsIntersect

### Declaration

`Aori.Math.SegmentsIntersect(Vector2 firstStart, Vector2 firstEnd, Vector2 
secondStart, Vector2 secondEnd)`

### Description

Determines whether two finite 2D segments intersect, including collinear overlap cases.

### Parameters

| Parameter     | Type      | Description           |
|:--------------|:----------|:----------------------|
| `firstStart`  | `Vector2` | First segment start.  |
| `firstEnd`    | `Vector2` | First segment end.    |
| `secondStart` | `Vector2` | Second segment start. |
| `secondEnd`   | `Vector2` | Second segment end.   |

### Returns

`true` if the segments intersect or touch; otherwise, `false`.

***

### Aori.Math.Orientation

### Declaration

`Aori.Math.Orientation(Vector2 first, Vector2 second, Vector2 third)`

### Description

Computes the orientation of an ordered triplet in 2D.

### Parameters

| Parameter | Type      | Description   |
|:----------|:----------|:--------------|
| `first`   | `Vector2` | First point.  |
| `second`  | `Vector2` | Second point. |
| `third`   | `Vector2` | Third point.  |

### Returns

`0` for collinear points, `1` for clockwise, and `-1` for counter-clockwise.

***

### Aori.Math.OnSegment

### Declaration

`Aori.Math.OnSegment(Vector2 first, Vector2 point, Vector2 second)`

### Description

Checks whether a point lies on the closed segment defined by two endpoints.

### Parameters

| Parameter | Type      | Description    |
|:----------|:----------|:---------------|
| `first`   | `Vector2` | Segment start. |
| `point`   | `Vector2` | Point to test. |
| `second`  | `Vector2` | Segment end.   |

### Returns

`true` if `point` lies within the segment bounds; otherwise, `false`.

***

### Aori.Math.Cross2D

### Declaration

`Aori.Math.Cross2D(Vector2 first, Vector2 second)`

### Description

Returns the 2D scalar cross product of two vectors.

### Parameters

| Parameter | Type      | Description    |
|:----------|:----------|:---------------|
| `first`   | `Vector2` | First vector.  |
| `second`  | `Vector2` | Second vector. |

### Returns

The scalar cross product value.

***

### Aori.Math.TryProjectPointToSegmentXZ

### Declaration

`Aori.Math.TryProjectPointToSegmentXZ(Vector3 point, Vector3 segmentStart, 
Vector3 segmentEnd, out float t, out float distanceSquared)`

### Description

Projects a point to a segment on XZ and returns the normalized parameter plus 
squared distance. Rejects projections that
are too close to segment endpoints.

### Parameters

| Parameter         | Type        | Description                                |
|:------------------|:------------|:-------------------------------------------|
| `point`           | `Vector3`   | Point to project.                          |
| `segmentStart`    | `Vector3`   | Segment start.                             |
| `segmentEnd`      | `Vector3`   | Segment end.                               |
| `t`               | `out float` | Normalized parameter of the projection.    |
| `distanceSquared` | `out float` | Squared distance from point to projection. |

### Returns

`true` if the projection is valid and lies strictly within the segment bounds; 
otherwise, `false`.

***

### Aori.Math.CalculateAngleBetweenEdges

### Declaration

`Aori.Math.CalculateAngleBetweenEdges(Vector3 center, Vector3 firstNeighbor,
 Vector3 secondNeighbor)`

### Description

Calculates the angle in radians between two edges sharing the same center node.

### Parameters

| Parameter        | Type      | Description                  |
|:-----------------|:----------|:-----------------------------|
| `center`         | `Vector3` | Shared center node.          |
| `firstNeighbor`  | `Vector3` | Endpoint of the first edge.  |
| `secondNeighbor` | `Vector3` | Endpoint of the second edge. |

### Returns

The angle in radians, in the range `[0, PI]`.

## Dependencies

No dependencies.