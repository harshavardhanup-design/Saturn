# SATURN AR — Technical Architecture & Implementation Spec

## 1. System Architecture Overview
**SATURN AR** leverages a layered architecture decoupling input processing, AR session tracking, physics/interaction logic, and UI presentation.

```
+-------------------------------------------------------------+
|                     Presentation Layer                      |
|  - HomeScreen uGUI Grid        - Telemetry Info HUD        |
|  - Reticle Indicator           - Reset & Nav Controls       |
+-------------------------------------------------------------+
                               |
+-------------------------------------------------------------+
|                      Logic & UX Layer                       |
|  - SceneController.cs          - PlanetTouchInteraction.cs  |
|  - PlanetInfoManager.cs        - RotationControllerPlanet.cs|
+-------------------------------------------------------------+
                               |
+-------------------------------------------------------------+
|                     AR Foundation Layer                     |
|  - ARSession (Lifecycle)       - ARPlacementManager.cs      |
|  - ARRaycastManager            - ARCameraBackground         |
|  - XROrigin / Camera Offset    - ARPlaneManager             |
+-------------------------------------------------------------+
                               |
+-------------------------------------------------------------+
|                   Android OS / ARCore Core                  |
|  - Google ARCore SDK C/JNI     - Android Camera2 API        |
|  - ARM64 Native IL2CPP Runtime - Vulkan / OpenGLES3 Drivers |
+-------------------------------------------------------------+
```

---

## 2. Touch Interaction & Gesture Mathematics
The touch interaction engine in `PlanetTouchInteraction.cs` processes raw screen touch deltas into angular velocities and scale factors without generating per-frame heap allocations:

### Rotational Drag & Inertia
- **Single Finger Input:** Reads `touch.deltaPosition` each frame:
  $$\Delta \theta_y = -\text{delta.x} \times \text{rotationSpeed}$$
  $$\Delta \theta_x = \text{delta.y} \times \text{rotationSpeed}$$
- **Momentum Deceleration:** Damped over time using exponential decay:
  $$\omega(t) = \text{Lerp}(\omega(t-1), 0, \Delta t \times \text{rotationDamping})$$

### Pinch-to-Scale
- **Two Finger Input:** Computes the change in Euclidean distance between touch points:
  $$d_{\text{curr}} = \|\mathbf{p}_0 - \mathbf{p}_1\|$$
  $$d_{\text{prev}} = \|(\mathbf{p}_0 - \Delta\mathbf{p}_0) - (\mathbf{p}_1 - \Delta\mathbf{p}_1)\|$$
  $$\Delta s = (d_{\text{curr}} - d_{\text{prev}}) \times \text{pinchSensitivity}$$
- Clamped within: $s \in [0.25, 2.80]$.

---

## 3. Surface Anchoring & Fallback Strategy
`ARPlacementManager.cs` provides dual-path anchoring:
1. **Surface Hit-Testing:** Executes `raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon)`. When an intersection occurs, the planet root anchors to the detected surface plane.
2. **Camera-Forward Fallback:** Automatically calculates a forward projection vector:
   $$\mathbf{P}_{\text{target}} = \mathbf{P}_{\text{cam}} + \mathbf{F}_{\text{cam}} \times d_{\text{front}} + \mathbf{U} \times y_{\text{offset}}$$
   This ensures that in editor testing or environments without detected planes, the 3D model always renders visibly in front of the user rather than being lost in void space.

---

## 4. Performance Optimizations
- **Mesh LOD & Batching:** Static multi-mesh parts of Saturn share material definitions, reducing draw calls.
- **Zero Allocations in Update():** Cached transforms, reusable vector structs, and avoided string concatenations in update loops.
- **Defensive Error Handling:** Automatic fallback self-assignment in `RotationControllerPlanet.cs` prevents unhandled null reference exceptions.
