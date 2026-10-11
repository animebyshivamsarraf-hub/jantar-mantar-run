# AI Build Instructions

Use this as a production brief for AI-assisted asset creation and implementation. Do not claim a feature is complete unless its files are committed and tested.

## Project
Create **JANTAR MANTAR RUN — Delhi Chase**, a third-person three-lane endless runner for Android, with a Delhi-inspired setting and realistic/cinematic 3D art direction.

## First playable milestone
- Main menu with Play.
- Continuous forward movement and smooth three-lane switching.
- Jump and slide actions.
- Obstacles (barriers and traffic) with collision handling.
- Collectible coins and score/distance HUD.
- Police chase pressure and a short non-graphic capture/game-over sequence.
- Restart, pause and resume.
- Keyboard controls for editor testing and touch controls for Android.

## Visual brief
Use original fictional characters. Use Delhi-inspired street props, bus/auto-rickshaw traffic, road markings and a Jantar Mantar-inspired landmark. Favor realistic materials and lighting, readable silhouettes and mobile-friendly performance. Do not copy the identity of a person in a reference video.

## Implementation rules
1. Inspect the repository before creating or replacing files.
2. Preserve existing work; implement one small, testable milestone at a time.
3. Keep scripts and assets organized under `Assets/`.
4. Record source and usage rights for every non-original asset.
5. Test collisions, restart, lane switching, jump/slide and mobile controls.
6. Do not claim the game is playable or share a live URL until a build is produced and verified.

## Asset import checklist
For every FBX/GLB, verify units, forward axis, pivot, materials/textures, rig/animations, collider setup and file size. Create simplified colliders and LODs where appropriate.
