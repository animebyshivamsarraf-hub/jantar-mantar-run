# PINOC Master Environment Asset

## Selected environment
- **Name:** JANTAR MANTAR RUN — Photoreal Delhi Protest Route (Master Environment)
- **PINOC asset ID:** `b7a4ff75-5735-42f7-86a6-651ea1101b2b`
- **PINOC task ID:** `604915b2-093e-4fcb-a817-f8370f5d9256`
- **Environment type:** normalised Gaussian splat (SPZ)
- **Load transform:** identity only — no scale, offset, or rotation; one unit is one metre and ground is at origin.
- **Generated:** 2026-10-11
- **PINOC environment library:** open https://viggle.ai/pinoc/app and locate the asset by name or ID.
- **Game repository:** https://github.com/animebyshivamsarraf-hub/jantar-mantar-run

## Why this replaces the blockout as the visual target
The earlier Blender geometry is only a layout prototype and is not the intended final look. This PINOC asset is the new master visual environment target: realistic Delhi daylight, street wear, sidewalks, traffic and government-district massing, Jantar Mantar-inspired red sandstone observatory forms, barricades and a continuous forward route.

## Generation prompt / intent
Create a highly photorealistic, immersive 3D Gaussian-splat environment for a third-person endless-runner game set on the real urban streets around Jantar Mantar and Sansad Marg in New Delhi, India. Visual quality should resemble real captured location footage, not a low-poly game or simplified primitive geometry. Include worn asphalt and authentic Indian road markings; dusty stone footpaths; mature roadside trees; streetlights, utility poles, signs and cables; Indian cars, scooters, buses and parked police vans; metal police barricades; low boundary walls; government-district buildings; warm red sandstone / terracotta observatory forms; pavement wear and distant Delhi haze. Start in an open protest gathering plaza on the Jantar Mantar side and continue along Sansad Marg as a connected forward road. Wide navigable environment, natural materials and lighting, grounded shadows, realistic proportions. No people baked into the environment, no readable slogans, logos, watermarks or HUD.

## Important asset handling
- PINOC returned a signed CDN URL for `normalised.spz`; such URLs expire and must not be treated as permanent repository links.
- This repository manifest stores the stable asset ID and integration notes. The SPZ binary itself has **not** yet been copied into GitHub; do not claim it has.
- The 3D Jutsu procedural blockout remains useful as a layout reference, but it should not be used as the hero visual environment when the PINOC splat is available.
- Integration still needs to be tested in the game's actual renderer / Viggle Splat Engine, including Android performance, collision proxies, player route alignment, and how to stream/repeat road sections. A Gaussian splat is primarily a visual environment and does not automatically provide gameplay collision meshes or true modular road geometry.
