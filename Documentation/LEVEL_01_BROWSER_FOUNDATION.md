# Level 1 — Browser Foundation

Branch: `scratch/new-game-v1`

## Goal
Create the smallest reliable browser entry point before adding final art or gameplay.

## Requirements
- Three.js scene and responsive renderer.
- Explicit loading and WebGL/network error states.
- Touch buttons and keyboard movement.
- Reset and resize handling.
- Lightweight diagnostics for renderer state and frame rate.
- No dependency on temporary PINOC signed URLs.
- The scene must label itself as a greybox prototype until real environment assets are integrated.

## Environment connection contract (required before Level 2)
Every environment module must declare:
- coordinate origin and forward direction;
- units/metres and ground height;
- road and sidewalk width;
- entry/exit anchors and heading;
- lighting/time-of-day and material palette;
- collision boundaries and playable corridor;
- asset version and source.

Adjacent modules are not approved until the two are tested together in-browser from both travel directions. The connector should be designed as part of both modules, not patched after generation.

## Acceptance tests
1. Page opens on a clean mobile browser.
2. Three.js import failure shows a readable error.
3. WebGL failure shows a readable error.
4. Touch controls, keyboard controls and reset work.
5. Resize does not distort the scene.
6. No temporary signed asset URL is required.
7. Production `main` remains untouched.

## Scope note
This stage is only the browser foundation and greybox. It is not the finished realistic world, final player model, or complete game.
