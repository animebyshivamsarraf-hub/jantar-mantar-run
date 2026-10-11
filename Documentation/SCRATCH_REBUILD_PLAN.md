# Scratch Rebuild Plan — Jantar Mantar Run

Date: 2026-10-11

## Safety rules
- The `main` branch is production and must remain untouched during the rebuild.
- `backup/pre-scratch-2026-10-11` is a point-in-time backup of the original `main` commit `4c8f567233d4e8dc887e8148e229946d69a3fbec`.
- All new work belongs on `scratch/new-game-v1`.
- Do not delete or replace legacy files on `main`.
- Do not merge or switch GitHub Pages production deployment until the scratch build passes desktop and Android browser checks.
- Treat external signed PINOC URLs as temporary; never embed an expiring URL in the finished game.

## Current baseline
The original repository has 26 tracked files. The old game is a Three.js browser prototype; `area-01-preview.html` is a separate Gaussian-splat preview and currently relies on a signed PINOC asset URL. The existing SPZ asset is retained in the backup.

## Fresh build direction
Build a clean, mobile-first 3D exploration game in this branch, not a continuation of the old runner implementation. Use Three.js for browser gameplay and rendering. Use PINOC for environment capture/reference where the environment file can be stored or served reliably. Use Blender only for authored/exported meshes that are needed (GLB/glTF), not as a prerequisite for every asset.

## Implementation stages
1. Create a minimal, clean browser entry point with a loading/error state, resize handling, touch + keyboard input, and a small diagnostics panel.
2. Establish a stable environment asset pipeline: verify asset format, size, license/ownership, and a durable repository or hosting URL before integrating it.
3. Build the playable area: ground/collision bounds, camera, navigation, landmark composition, barriers, tents, trees, lights and signage; keep performance appropriate for Android.
4. Add gameplay systems incrementally: player controller, collisions, objectives, NPC/police chase only after exploration works.
5. Validate the scratch page on desktop and Android; check network failures, console errors, frame pacing and touch controls.
6. Publish a scratch preview URL only if the repository's Pages workflow can serve the branch safely; otherwise use a dedicated preview workflow or branch deployment. Do not change production Pages configuration blindly.
7. Replace production only after explicit review and passing checks. Keep the backup branch available for rollback.

## Acceptance checks
- No temporary signed URL is required for the first render.
- The game opens in a mobile browser and displays a useful error if WebGL or an asset fails.
- Touch drag/look and movement controls work; keyboard controls also work.
- The main environment is visibly loaded before gameplay is called complete.
- Production `main` remains unchanged until the new build is approved.
