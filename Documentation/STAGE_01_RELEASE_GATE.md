# Stage 1 — Preview and release gate

Status: **NOT PASSED — runtime verification still required**

This checklist intentionally prevents an untested prototype from replacing production.

## Current known facts

- Production source is on `main`; its Pages workflow deploys on pushes to `main` that touch `index.html`, `area-01-preview.html`, `public/**`, or the workflow file.
- The Stage 1 prototype is `stage-01-foundation.html` on `scratch/new-game-v1`.
- The existing workflow does not automatically publish arbitrary scratch-branch commits. Opening the raw GitHub file is not a browser runtime test.
- Source inspection confirms the prototype imports Three.js from jsDelivr, creates a WebGL renderer, includes touch and keyboard movement, reset, resize handling, and visible load/runtime error messages.
- No real-browser session has yet verified rendering, input, mobile layout, performance, or console/network errors. Therefore this stage is not approved for production.

## Required preview test

Open the page through a temporary non-production preview host or a separately configured preview deployment. Do not point the production Pages site at this branch.

- [ ] Page loads over HTTPS and Three.js module import succeeds.
- [ ] WebGL scene renders on Android Chrome and desktop Chromium.
- [ ] Start overlay dismisses and movement begins.
- [ ] Left/right/forward touch controls work and remain usable on narrow screens.
- [ ] Keyboard arrows and WASD work on desktop; R resets the player.
- [ ] Pointer swipe movement works without trapping normal page navigation outside the canvas.
- [ ] Reset returns the player and camera to a predictable start position.
- [ ] Resize/orientation changes preserve the correct camera aspect ratio and full-screen canvas.
- [ ] Test on a device/browser without WebGL or with the CDN blocked; a readable error is shown.
- [ ] No uncaught exceptions, failed local assets, horizontal overflow, or hidden controls.
- [ ] Test a low-end Android device for sustained rendering and heat/battery impact.
- [ ] Verify the experience remains clearly labelled as a greybox prototype, not the final realistic world.

## Release gate

Production may only be changed after:
1. The browser preview URL is accessible and the checklist above is completed.
2. Bugs found during testing are fixed on `scratch/new-game-v1`.
3. The connected-environment contract and Area 01 connector tests are in place.
4. The proposed release is reviewed as a pull request against `main`.
5. A rollback commit/backup is identified and the production deployment result is checked.

## Immediate blocker

The current deployment workflow only deploys `main`. A safe browser preview needs a separate preview deployment/host or a dedicated preview workflow. Until that is available and the page is actually exercised in a browser, this gate remains **NOT PASSED**.
