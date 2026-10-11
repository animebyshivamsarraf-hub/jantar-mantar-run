# Unity Environment Setup

## Before importing assets
1. Install a current Unity LTS release with Android Build Support (SDK/NDK and OpenJDK modules).
2. Create or open a Unity project in this repository. Do not assume the repository already contains a Unity project.
3. Set the project to 3D (URP is an option if mobile lighting/performance is profiled).
4. Create folders under `Assets/`: Scenes, Scripts, Prefabs, Materials, Characters, Environment, UI and Audio.

## Import the supplied FBX
1. Add `Endless Runner (1)(1).fbx` to `Assets/Environment/Source/` using a binary-safe transfer.
2. Inspect Model, Rig, Animation and Materials import tabs.
3. Check scale and forward axis; inspect mesh normals and material/texture references.
4. Add colliders only where needed; use simplified colliders for gameplay.
5. Place it in an empty scene and confirm scale, pivot, orientation and visual appearance.
6. Keep the original source unchanged; record derived/optimized versions separately.

## First scene
Create a simple test scene with a straight road, three lane guides, player placeholder, follow camera, one obstacle and one collectible. Validate movement and collision before detailed art production.

## Build checks
- Test on a real Android device at the intended screen orientation.
- Profile frame rate, memory, texture sizes and draw calls.
- WebGL is optional and must be tested separately; a Unity editor scene is not a browser preview.
