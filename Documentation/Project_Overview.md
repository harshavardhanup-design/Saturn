# SATURN AR — Project Overview

## 1. Project Title & Abstract
**SATURN AR** is an Augmented Reality (XR) planetary exploration mobile application designed for Android devices using Unity AR Foundation and Google ARCore. The project enables users to visualize, rotate, scale, and inspect high-detail 3D models of Saturn, its planetary ring system, and natural satellites within their real-world physical space.

Originally based on an educational XR workshop project ("Planets AR"), SATURN AR has been redesigned, hardened, and branded to deliver a focused, interactive space exploration experience.

---

## 2. Problem Statement & Motivation
Astronomy education frequently relies on static two-dimensional textbook diagrams or flat screen graphics that fail to convey the complex spatial geometry, ring dynamics, and scale of solar system bodies. In particular, Saturn's ring system and orbital moons exhibit intricate three-dimensional features that are best understood through free-form spatial observation.

**SATURN AR** bridges this gap by providing:
1. Spatial 6DoF immersion allowing users to physically walk around Saturn in their room.
2. Tactile multi-touch manipulation for examining planetary rings and cloud layers from custom angles.
3. Accessible planetary telemetry detailing physical, orbital, and atmospheric properties.

---

## 3. Project Objectives
- **Preserve Workshop Foundations:** Maintain core scene transition and axial rotation mechanisms introduced during the workshop.
- **Architectural Stabilization:** Eliminate unhandled runtime exceptions (`NullReferenceException`) and ensure clean scene linkage.
- **Centerpiece Integration:** Bring the multi-mesh 3D Saturn asset (previously unused in workshop demonstration scenes) into active interactive AR.
- **Touch Interaction & Physics:** Introduce single-finger drag rotation with momentum damping and two-finger pinch scaling.
- **Brand Consistency:** Replace placeholder branding with a dark cosmic aesthetic, custom iconography, and responsive UI.

---

## 4. How the AR Experience Works
1. **Launch & Menu:** User launches SATURN AR and views the planetary grid on `HomeScreen`.
2. **Scene Activation:** Selecting Saturn transitions to `Saturn.unity` via `SceneController.cs`.
3. **Session Initialization:** `ARSession` activates the device camera and initializes ARCore motion tracking.
4. **Surface Grounding / Fallback:** `ARPlacementManager.cs` detects horizontal planes (floors/tables) or positions Saturn 1.2m directly in front of the camera.
5. **Interactive Exploration:**
   - Single-finger drag rotates Saturn in yaw and pitch.
   - Two-finger pinch scales the model between 0.25x and 2.8x.
   - Info button expands scientific data on Saturn's diameter, mass, orbital period, and 146 moons.
   - Reset button smoothly lerps the model back to default orientation and scale.
   - Back button returns smoothly to `HomeScreen`.
