# Changelog — SATURN AR

## [1.0.0] - 2026-11-24

### Rebranding & Identity Overhaul
- Rebranded project title, display name, and UI headings from "Planets AR" to **SATURN AR — An Augmented Reality Planet Exploration Experience**.
- Designed and generated custom cosmic app icon set (512px, 192px, 96px, 48px), logo, and glassmorphic UI banners.
- Updated Android application label and replaced launcher icon binaries in the compiled APK.

### Flagship Features & Improvements
- **Saturn AR Scene:** Integrated the unused multi-part 3D Saturn model with planetary rings, atmosphere, and natural satellites (Dione, Enceladus, Mimas, Rhea, Tethys).
- **Touch Gesture Interaction:** Authored `PlanetTouchInteraction.cs` supporting single-finger drag rotation, two-finger pinch-to-scale, and smooth position reset.
- **Telemetry HUD:** Authored `PlanetInfoManager.cs` to present expandable astronomical information cards.
- **Surface Anchoring:** Authored `ARPlacementManager.cs` supporting AR plane hit-testing with fallback camera-forward positioning.
- **Permissions Management:** Authored `CameraPermissionHelper.cs` to handle runtime Android camera authorization requests.

### Stability & Bug Fixes
- **DEF-01 (APK Disconnect):** Re-architected project structure and build settings to include all planetary scenes rather than the placeholder sample scene.
- **DEF-02 (Null Reference):** Fixed unhandled null pointer risk in `RotationControllerPlanet.cs` with defensive self-assignment fallback.
- **DEF-03 (Build Metadata):** Rebranded package identifier and build configuration for modern Android 14 (API 34) targets.
