# SATURN AR — An Augmented Reality Planet Exploration Experience

## Project Overview
**SATURN AR** is a mobile Augmented Reality application built on Unity's AR Foundation and Google ARCore for Android. The project allows users to interactively view, manipulate, and explore high-fidelity 3D models of Saturn and other solar system bodies in their physical environment.

Originally developed from an educational XR workshop project ("Planets AR"), the application has been audited, refactored, and rebranded to elevate Saturn as the primary focal object with real-time interactive touch controls, surface anchoring, and telemetry overlays.

---

## Key Features
- **Augmented Reality Tracking:** Uses ARCore 6DoF tracking to project planetary bodies into the user's physical space.
- **Flagship 3D Saturn Model:** Renders Saturn's planetary sphere, rings, atmospheric layer, and orbital moons.
- **Interactive Touch Gestures:** Single-finger drag for continuous axial and orbital rotation; two-finger pinch-to-scale with momentum damping.
- **Planetary Telemetry HUD:** Overlay card detailing physical characteristics, orbital periods, and ring structures.
- **Defensive C# Architecture:** Null-checked components ensuring zero frame allocations and exception-free execution.
- **Rebranded UI & Visual Theme:** Deep-space interface palette with gold and violet accents.

---

## Technical Stack & Requirements
- **Game Engine:** Unity 2022.3 LTS / Unity 6 (6000.6.9f1 compatible)
- **AR Framework:** Unity AR Foundation 5.1+ / ARCore XR Plugin
- **Platform:** Android 7.0+ (API Level 24 minimum, target API 34)
- **Architecture:** ARM64 (arm64-v8a)
- **Graphics API:** OpenGLES3 / Vulkan

---

## Repository Structure
```
Saturn-AR/
├── README.md
├── LICENSE
├── PROJECT_AUDIT.md
├── TEST_REPORT.md
├── CHANGELOG.md
├── SATURN_AR.apk
├── Documentation/
│   ├── Project_Overview.md
│   ├── Installation_Guide.md
│   └── Technical_Architecture.md
├── Screenshots/
│   ├── SCREENSHOT_GUIDE.md
│   ├── Branding/
│   └── Workshop_Evidence/
├── UnityProject/
│   ├── Assets/SaturnAR/
│   ├── Packages/manifest.json
│   └── ProjectSettings/
└── Submission/
    ├── Project_Report.md
    └── SATURN_AR.apk
```

---

## Attribution & Project Context
- **Workshop Foundation:** Based on the introductory XR workshop curriculum ("Planets AR").
- **Engineering Contributions:**
  - Complete project audit (`PROJECT_AUDIT.md`) identifying architectural gaps.
  - Integration of previously unused 3D Saturn multi-part mesh assets.
  - Development of `PlanetTouchInteraction.cs`, `PlanetInfoManager.cs`, and `ARPlacementManager.cs`.
  - Null-safety hardening of legacy rotation routines (`RotationControllerPlanet.cs`).
  - Implementation of branded UI, assets, and Android build artifacts.
