# PINOC Jantar Mantar Environment Integration — Implementation Brief

Status: generated PINOC environment assets exist in the PINOC library, but are NOT yet loaded by the playable game. Do not describe them as integrated until verified in the browser.

## Target
Repository: https://github.com/animebyshivamsarraf-hub/jantar-mantar-run
Playable build: https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/
3D scene viewer: https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/jantar-world-3d.html

## Required outcome
Make the existing runner visibly use PINOC-generated Gaussian-splat environment imagery for the Jantar Mantar / Sansad Marg setting. The main playable game must show the generated environment, not merely link to a separate viewer. Preserve lane switching, jump, slide, obstacles, police chase, detention, restart and mobile touch controls.

## PINOC environment assets
These are real generated PINOC scene IDs. Resolve current download URLs from PINOC at build time / manual asset-prep time; signed URLs expire and MUST NOT be committed as permanent URLs.

- Roadside trees and Delhi buildings: 441d4c79-c5cd-40fe-aefb-e8ade7c1787f
- Observatory approach and road: 79fb09ab-845b-4dd6-a298-2e074aef5dc1
- Observatory garden and Yantra surroundings: 4161f98a-9367-47e6-ad27-b34c2061c1a7
- Module A: 459e7450-ed5e-4ece-9b42-d50b28f74deb
- Module B: 2824d991-051d-4e44-b7cb-142586d81fa4
- Module C: d2691515-bcbb-464d-90f4-2cffc175a256
- Area 01: 5f7a1776-2762-43c0-a5a9-2e1d03b21d2c
- Protest site: 587b4059-90c0-49ee-aedf-7e3cd3df844b

All listed scenes were marked normalized by PINOC: load at identity transform (no scale, rotation or offset) unless a fresh PINOC response says otherwise.

## Implementation approach
1. Obtain fresh PINOC download URLs and save the selected SPZ files into a durable asset host or repository release. Never embed expiring signed URLs in game HTML.
2. Determine and pin a browser-compatible version of the Viggle/PINOC Splat Engine. Bundle its worker files locally or use a documented stable CDN path. Do not ship an unverified esm.sh worker import; a prior smoke test failed because the worker module returned an invalid response.
3. Build a dedicated environment loader module with a clear loading state, errors, and a low-memory fallback for Android.
4. Integrate the roadside/road scene as the visual world for the runner first. Use the observatory scene as the later landmark segment or an accessible visual preview. Avoid drawing two overlapping full scenes.
5. Keep gameplay collision/lane geometry separate from Gaussian splats. Gaussian splats are visual-only and must not be treated as collision meshes. Preserve existing invisible ground, lane, obstacle and collision systems.
6. Add conservative mobile defaults: limit pixel ratio, avoid loading multiple high-resolution scenes at once, dispose unused resources, and gracefully fall back if WebGL/worker support fails.
7. Test on a narrow Android-sized viewport and desktop Chromium. Confirm game start, continuous rendering, lane changes, jump/slide, obstacle collisions, chase/detention/restart, and no console errors. Verify the PINOC scene is visibly rendered inside the actual gameplay canvas.
8. Do not claim exact sem-to-sem reconstruction. Current PINOC scenes are generated plausible reconstructions, not surveyed scans. For faithful placement and proportions, acquire real location photographs or a panorama / 360 capture and reconstruct from that reference.

## Acceptance checklist
- [ ] PINOC-generated scene is visible in the main playable game canvas.
- [ ] Scene assets are hosted durably; no expiring signed URLs in deployed code.
- [ ] Mobile browser loads within reasonable memory limits and keeps touch controls responsive.
- [ ] Existing game mechanics still pass smoke tests.
- [ ] Failure to load the splat renderer displays a useful fallback instead of a blank page.
- [ ] Documentation and UI clearly distinguish generated approximation from exact scanned geometry.