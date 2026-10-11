# Work Log and Asset Status

## Collected so far
- Project title: **JANTAR MANTAR RUN — Delhi Chase**.
- Core loop: start in a protest gathering area, allow short free movement, tap a protest/meme action, then trigger a police chase toward Sansad Marg.
- Target platform priority: Android first; browser prototype is an early interaction test, not a production build.
- Visual direction: realistic Delhi-inspired street, Jantar Mantar observatory forms, traffic, barricades, crowd, police chase, funny non-graphic detention/Game Over.
- User-provided character photo: new PINOC character generated as **Man on Red Carpet** (asset ID `d865e973-282d-4ba1-a1c6-1fd42d692a7c`). The existing **Man with Backpack** remains (asset ID `26e1e01a-229d-482d-a41c-5a4c93761bb8`). Neither is integrated into the prototype yet.
- PINOC generated Delhi street environment (asset ID `33cab3aa-eacb-4c77-b6e7-79f1d66895ce`).
- PINOC generated **Jantar Mantar Run — Protest Street Environment** (asset ID `401c799c-952d-4499-93fe-95e87eed00c6`).
- PINOC generated **Jantar Mantar Run — Protest Start Zone and Sansad Marg Route** (asset ID `baab4fe6-3604-4f06-80be-36e8a3f28590`).
- Five library animation clips are available: Running, Sprinting Forward, Run Jump, Vault Over Obstacle, and Jump. IDs and integration notes are in `Documentation/GENERATED_ASSETS.md`.
- A first browser prototype is now in `index.html`: Three.js scene, protest-side starting plaza, barricades, Delhi-inspired scenery, movement controls, protest button, three lanes, jump/slide, obstacle collisions, escalating chase HUD and detention/restart screen.
- Added `.github/workflows/deploy-pages.yml` to attempt deployment to GitHub Pages when the prototype changes.

## Not completed / not verified
- Browser prototype has been committed but has **not yet been browser-tested**. Do not claim it works until the deployment and actual interaction are checked.
- GitHub Pages deployment status is not confirmed; the Actions API showed no workflow runs immediately after commits.
- The prototype currently uses procedural Three.js geometry and does not load the PINOC Gaussian-splat environment or either PINOC character.
- `COCKROACH` is currently a prototype label on a simple placeholder avatar, not verified on the final characters.
- The user-provided FBX named `Endless Runner (1)(1).fbx` has not been validated in Unity.
- PINOC Gaussian-splat scenes/characters require Viggle Splat Engine and are not ordinary FBX/GLB meshes. Performance and correct motion binding still need testing.
- The user's protest/meme audio and dog chase are intentionally deferred until they provide the desired material.
- This repository still does not contain a verified Android build or production-ready game.

## Immediate next steps
1. Confirm GitHub Pages workflow succeeds and open the deployed page on desktop/mobile.
2. Fix prototype bugs discovered by actual playtesting; confirm touch controls, start/protest transition, obstacles, difficulty escalation and detention/restart.
3. Improve looping world sections so Delhi scenery and protest/traffic details continue as the runner advances.
4. Decide how to render PINOC splat assets alongside performant collision/gameplay geometry; benchmark Android.
5. Add the user's reference-video details and meme audio when supplied; then build/test a real mobile package.
