# SATURN AR — Installation & Deployment Guide

This guide covers building the Unity project from source and sideloading the pre-built `SATURN_AR.apk` onto an Android smartphone.

---

## Method 1: Sideloading the Pre-Built APK (Fastest)

### Requirements
- Android smartphone running Android 7.0 (API Level 24) or newer.
- ARM64 processor architecture (`arm64-v8a`).
- Google Play Services for AR (ARCore) installed or updated from Google Play Store.

### Step-by-Step Instructions
1. Download `SATURN_AR.apk` from the repository root or from `Submission/SATURN_AR.apk`.
2. Connect your Android device to your computer via USB (or transfer via Google Drive / local file transfer).
3. If installing an APK for the first time on your device:
   - Go to **Settings > Security** (or **Apps & notifications**).
   - Enable **Install unknown apps** for your file manager or browser.
4. Open your device file manager, tap `SATURN_AR.apk`, and confirm the installation.
5. Launch **SATURN AR** from your app drawer.
6. Grant camera access when prompted by the Android OS.

---

## Method 2: Building from Source in Unity Editor

### Requirements
- Unity 2022.3 LTS or Unity 6 (6000.6.9f1).
- Unity Android Build Support module with:
  - Android SDK & NDK Tools.
  - OpenJDK 17.
- Visual Studio 2022 / JetBrains Rider / VS Code.

### Step-by-Step Instructions
1. Clone or open the repository folder `Saturn-AR/UnityProject/` in Unity Hub.
2. Ensure the active editor version is set to **Unity 2022.3 LTS** or **Unity 6**.
3. Open **File > Build Settings**:
   - Switch Platform to **Android**.
   - Verify the following scenes are in the build list (in exact order):
     - `Assets/SaturnAR/Scenes/HomeScreen.unity` (Index 0)
     - `Assets/SaturnAR/Scenes/Saturn.unity` (Index 1)
     - `Assets/SaturnAR/Scenes/Earth.unity` (Index 2)
     - `Assets/SaturnAR/Scenes/Mars.unity` (Index 3)
4. Open **Project Settings > Player > Android Settings**:
   - Company Name: `Saturn AR Team`
   - Product Name: `SATURN AR`
   - Package Name: `com.saturnar.planetexplorer`
   - Minimum API Level: Android 7.0 'Nougat' (API Level 24)
   - Target API Level: Automatic (highest installed, e.g. API 34)
   - Scripting Backend: `IL2CPP`
   - Target Architectures: `ARM64` checked (`ARMv7` unchecked)
5. Under **XR Plug-in Management**:
   - Ensure **Google ARCore** is enabled under the Android tab.
6. Connect your phone via USB with **USB Debugging** enabled, then click **Build And Run** (or click **Build** to generate an APK).
