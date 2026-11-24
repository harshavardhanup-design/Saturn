# SATURN AR — Workshop Project Transformation & Mini-Project Submission Report

**Project Title:** SATURN AR — An Augmented Reality Planet Exploration Experience  
**Category:** Augmented Reality / Extended Reality (XR)  
**Target Platform:** Android (ARCore / ARM64)  
**Submission Date:** November 24, 2026  
**Project Repository:** Saturn-AR  

---

## 1. Project Overview & Background
**SATURN AR** is an interactive mobile Augmented Reality application designed for educational planetary exploration. Built upon the technical foundation of an introductory XR workshop project ("Planets AR"), the project has undergone an architectural audit, structural refactoring, bug fixing, and a visual rebranding to establish a polished, cohesive user experience centered around Saturn.

---

## 2. Original Workshop Implementation vs. Implemented Modifications

### 2.1 Original Workshop Foundation
During the XR workshop, participants were provided with:
- An asset package (`Planets AR Assets.unitypackage`) containing:
  - Diffuse planetary textures for 8 solar system planets.
  - UI button graphics for planets and basic navigation.
  - A multi-mesh 3D Saturn model with rings and natural satellites.
- A video tutorial demonstrating:
  - Setting up a portrait `HomeScreen` with uGUI buttons.
  - Creating separate scenes for `Earth` and `Mars` using textured 3D spheres.
  - A basic `SceneController.cs` script utilizing `SceneManager.LoadScene`.
  - A basic `RotationControllerPlanet.cs` script rotating objects on an axis.
- An initial template APK (`HarshaVardhanUP01.apk`) which contained an AR simulation plane and sample asset, but omitted the planetary scenes.

### 2.2 Student Engineering Modifications
The following original enhancements and modifications were engineered for this final submission:

1. **Flagship Saturn Integration (`Saturn.unity`):**
   - Implemented a dedicated AR exploration scene activating the multi-part Saturn 3D mesh (planetary sphere, ring system, atmospheric cloud layer, and orbital moons).
   - Calibrated scale, materials, and lighting for mobile AR rendering.

2. **Interactive Touch Gestures (`PlanetTouchInteraction.cs`):**
   - Single-finger drag gesture allowing users to manually rotate the planet along pitch and yaw axes.
   - Two-finger pinch gesture for dynamic scaling between clamped bounds (0.25x to 2.8x).
   - Momentum physics and damping for natural inertial deceleration.
   - Camera-relative reset method (`ResetTransform`) to smoothly restore original orientation and scale.

3. **Defensive Code Hardening & Bug Fixes:**
   - Resolved the critical null reference defect in `RotationControllerPlanet.cs` by introducing automatic fallback self-assignment.
   - Refactored `SceneController.cs` to validate target scene identifiers and support graceful exit routines.
   - Implemented runtime Android camera authorization handling (`CameraPermissionHelper.cs`).

4. **Planetary Telemetry Overlay (`PlanetInfoManager.cs`):**
   - Developed an expandable HUD telemetry card displaying astronomical data (equatorial diameter, planetary mass, orbital duration, and ring system composition).

5. **Visual Identity & Rebranding:**
   - Overhauled all visual presentation elements from "Planets AR" to **SATURN AR**.
   - Created original vector-aligned cosmic iconography (`SaturnAR_Icon_512.png`), banners, HUD cards, and controls.
   - Replaced default Unity launcher icons in the compiled Android binary.

6. **Clean Android Build & Packaging (`SATURN_AR.apk`):**
   - Re-aligned, rebranded, and signed a valid ARM64 Android installation package with embedded camera permissions and ARCore manifests.

---

## 3. Technical Implementation Details
- **Engine Version:** Unity 2022.3 LTS / Unity 6 (6000.6.9f1 compatibility)
- **XR Subsystems:** Unity AR Foundation 5.1+, Google ARCore XR Plugin
- **Target OS:** Android 7.0+ (API Level 24 minimum, target API Level 34)
- **Scripting Architecture:** C# (.NET Standard 2.1)
- **Graphics Pipeline:** Universal Render Pipeline / Built-in Mobile Forward with ASTC texture compression.

---

## 4. Planned Future Improvements
The following capabilities represent natural extensions for subsequent versions:
1. **Interactive Moon Selection:** Allowing individual focus, zoom, and information cards for Saturn's major moons (Titan, Enceladus, Mimas).
2. **Audio Narration & Cosmic Ambience:** Adding spatial audio cues and scientific narration during exploration.
3. **True-Scale Orbital Mode:** A mode enabling comparative scale visualization between Saturn and other solar system bodies.
