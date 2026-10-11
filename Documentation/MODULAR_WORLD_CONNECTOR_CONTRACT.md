# Modular World Connector Contract — Scratch Game V1

Status: specification only; not a claim that modules have been built or visually tested.

## Non-negotiable rule
Every environment segment must be authored against the same coordinate, scale, road, lighting, and connector contract. Do not place independently generated scenery together and hope the seams disappear. A module is not accepted until its entry and exit connectors are tested with its neighbors in both travel directions.

## Coordinate and scale
- Engine: Three.js, right-handed scene convention.
- World up: +Y. Ground top: Y = 0.
- Road direction: +Z for all authored modules. Player begins near Z = 0 and progresses toward increasing Z.
- Road centerline: X = 0.
- Target road width: 10 world units; road edges X = -5 and X = +5.
- Each module's nominal length: 80 world units; scenery may extend into an agreed overlap zone but must not create a visible ground or road edge.
- Player collision corridor: X = -4.2 to +4.2; keep mandatory path clear of solid scenery.

## Connector definition
Each module must declare:
- `entry`: center point (0, 0, 0), heading +Z.
- `exit`: center point (0, 0, 80), heading +Z.
- `road_width`: 10.
- `ground_y`: 0.
- `overlap_length`: 8 units at each end for compatible ground/road blending.
- `collision_corridor`: X [-4.2, 4.2].
- `lighting_profile`: one shared daylight profile and one shared fog/sky profile for the full route.
- `material_profile`: shared asphalt, curb, soil, foliage, and concrete material definitions.

## Seam prevention
1. Road and ground must share exact connector coordinates and elevations.
2. Road markings and curb edges must meet at the same X positions; markings cannot restart visibly at module boundaries.
3. Blend ground/scenery only in the 8-unit overlap zone; avoid duplicate large objects in overlaps.
4. Keep exposure, tone mapping, fog, sun direction, and shadow settings global rather than per module.
5. Use shared material instances or matching color/roughness/texture scale; no arbitrary per-module changes.
6. Place trees, barricades, tents, signs, and poles outside the player collision corridor unless they are intentional obstacles with explicit collision metadata.
7. Never use blurred low-resolution scenery as a substitute for connector geometry. Distant background assets may be low-detail, but the playable road and connector must remain crisp.
8. Each segment must include a test pairing: preceding module → segment → following module. Inspect both entry and exit at ground level and from the player camera.

## Performance baseline for Android
- Prefer instanced repeated props and shared geometry/materials.
- Dispose of geometries, materials, textures, and listeners when a module unloads.
- Avoid creating new meshes every animation frame.
- Keep a stable fallback if WebGL cannot initialize and show a readable error.

## Acceptance gate for every module
- [ ] Entry/exit coordinates and headings match this contract.
- [ ] Road/ground show no gap, height step, visible edge, or abrupt material seam.
- [ ] Player and camera cross both connectors without a snap.
- [ ] No scenery blocks the mandatory route unexpectedly.
- [ ] No console/page errors during traversal.
- [ ] Android portrait viewport and touch controls remain usable.
- [ ] Adjacent-module pair is tested; a standalone screenshot is insufficient.

## Scope
This document sets the shared contract. It does not assert that a photorealistic Jantar Mantar environment, final assets, or multi-segment endless world are complete.
