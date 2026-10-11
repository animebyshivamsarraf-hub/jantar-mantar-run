# Work Log and Asset Status

## Collected so far
- Project title: **JANTAR MANTAR RUN — Delhi Chase**.
- Core loop and three-lane endless-runner direction documented.
- Target platform priority: Android first; browser/WebGL only after feasibility testing.
- Visual direction: realistic/cinematic 3D, Delhi-inspired environment, traffic, coins, obstacles and police chase.
- User uploaded an FBX in chat named `Endless Runner (1)(1).fbx`. It has not yet been committed to the GitHub repository.
- A user-provided video was discussed as loose inspiration; it is not included in the repository.
- PINOC generated a Delhi street environment (asset ID `33cab3aa-eacb-4c77-b6e7-79f1d66895ce`).
- PINOC generated an original fictional adult runner, “Man with Backpack” (asset ID `26e1e01a-229d-482d-a41c-5a4c93761bb8`).
- Five library animation clips are available: Running, Sprinting Forward, Run Jump, Vault Over Obstacle, and Jump. Their asset IDs and integration notes are in `Documentation/GENERATED_ASSETS.md`.
- Generated PINOC assets are currently in the PINOC asset library; they are not yet imported into Unity or checked into this repository.

## Not completed yet
- No confirmed Unity project, scenes, gameplay scripts or playable build in the repository.
- FBX has not been validated in Unity for scale, orientation, rig, materials or colliders.
- PINOC's environment and character are Gaussian-splat assets, not ordinary FBX/GLB meshes; they require Viggle Splat Engine and matching splat-skeleton motion exports to animate.
- No live browser preview or deployed game URL exists yet.
- No generated model/texture/audio should be treated as integrated until its file is present in the repository and tested.

## Immediate next steps
1. Decide the rendering path: conventional Unity meshes for reliable Android gameplay, or Viggle Splat Engine for splat-based character/environment rendering.
2. Export/download the required assets from PINOC; signed CDN URLs expire and must not be hardcoded into a permanent build.
3. Import and test assets in the chosen runtime.
4. Implement and test the three-lane greybox loop, then add traffic, coins, obstacles, police chase, Game Over and restart.
5. If a web build is requested, produce and test an actual WebGL build before sharing a URL.
