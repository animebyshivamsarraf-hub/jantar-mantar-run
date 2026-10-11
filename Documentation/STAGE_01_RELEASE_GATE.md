# Stage 1 — Preview and release gate

Status: **PARTIAL — isolated Chromium smoke tests passed; real preview/device verification remains required**

This checklist intentionally prevents an untested prototype from replacing production.

## Current known facts

- Production source is on `main`; its Pages workflow deploys on pushes to `main` that touch `index.html`, `area-01-preview.html`, `public/**`, or the workflow file.
- The Stage 1 prototype is `stage-01-foundation.html` on `scratch/new-game-v1`.
- The existing workflow does not automatically publish arbitrary scratch-branch commits. Opening the raw GitHub file is not a browser runtime test.
- Source inspection confirms the prototype imports Three.js from jsDelivr, creates a WebGL renderer, includes touch and keyboard movement, reset, resize handling, and visible load/runtime error messages.
- GitHub Actions run 3 passed on 2026-10-11: Chromium/Playwright smoke checks verified scene startup/status, left/right input, reset, narrow viewport canvas visibility, absence of page errors in that path, and the readable WebGL-unavailable fallback.
- This is an automated Chromium test, not a visual review of the game and not a test on a physical Android phone. Performance, camera reset behavior, swipe behavior, and network/CDN failure still need separate verification.

## Required preview and device tests

Open the page through a temporary non-production preview host or a separately configured preview deployment. Do not point the production Pages site at this branch.

- [x] Automated Chromium smoke test: page/scene startup and expected status.
- [x] Automated Chromium smoke test: left/right input and reset key.
- [x] Automated Chromium smoke test: 390×844 mobile-sized viewport, canvas visible and fitting.
- [x] Automated Chromium smoke test: readable error when WebGL context creation fails.
- [ ] Open an actual HTTPS preview URL; confirm the Three.js module import succeeds outside the CI runner.
- [ ] Visually inspect scene rendering on Android Chrome and desktop Chromium.
- [ ] Verify left/right/forward touch controls work and remain usable on narrow screens.
- [ ] Verify keyboard arrows and WASD work on desktop; R resets the player.
- [ ] Verify pointer swipe movement works without trapping normal page navigation outside the canvas.
- [ ] Confirm reset returns both player and camera to a predictable start position.
- [ ] Verify resize/orientation changes preserve camera aspect ratio and full-screen canvas.
- [ ] Test with CDN blocked / network failure and confirm a readable error.
- [ ] Check for uncaught exceptions, failed assets, horizontal overflow, or hidden controls in a visual session.
- [ ] Test a low-end Android device for sustained rendering and heat/battery impact.
- [ ] Keep the experience clearly labelled as a greybox prototype, not the final realistic world.

## Release gate

Production may only be changed after:
1. A non-production browser preview URL is accessible and the checklist above is completed.
2. Bugs found during testing are fixed on `scratch/new-game-v1`.
3. The connected-environment contract and Area 01 connector tests are in place.
4. The proposed release is reviewed as a pull request against `main`.
5. A rollback commit/backup is identified and the production deployment result is checked.

## Immediate blocker

The current deployment workflow only deploys `main`. A safe browser preview needs a separate preview deployment/host or a dedicated preview workflow. The automated Chromium checks are now passing, but this gate remains **PARTIAL** until a real preview and Android/device testing are complete.
