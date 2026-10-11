# Jantar Mantar Run — World Assembly Pipeline

## Primary target

The main target is a believable, location-led recreation of the Jantar Mantar protest-area entrance and the connected route toward Sansad Marg, New Delhi. The existing `fresh-world-3d.html` is only a navigable geometry blockout/reference viewer. Do not treat it as the final environment or replace the location target with a generic Delhi street.

## Asset strategy

- Use PINOC environment scans as visual reference / Gaussian-splat environment assets.
- Use real, separately authored game-ready meshes for collision-critical and gameplay objects: road modules, curbs, barricades, tents, trees, lamp posts, signs, traffic and buildings.
- Keep collision meshes independent of splats. Do not assume a Gaussian splat is a conventional editable polygon mesh or a Unity-ready collision surface.
- Preserve Android performance: stream/load modules in sections, use LODs, texture atlases, instancing for repeated props, simple colliders, and a mobile quality preset.
- Connect modules using a shared coordinate convention, consistent road width/ground height, and explicit entry/exit anchors. Never claim surveyed exactness unless measured reference data is available.
- Keep temporary protest objects (tents, water crates, banners and crowd-control barriers) as removable edge props rather than permanent architecture.
- Use no artificial blur to hide missing detail. Keep the near and middle ground crisp and readable.

## Current PINOC environment library

The following asset IDs were found in the connected PINOC library. Signed download URLs expire; retrieve fresh URLs through PINOC when importing.

| Module / role | Asset ID | Notes |
|---|---|---|
| Module A — Jantar Mantar entrance / starting environment | `459e7450-ed5e-4ece-9b42-d50b28f74deb` | Existing fresh-world build; normalized, identity placement |
| Area 01 — realistic Delhi upgrade | `5f7a1776-2762-43c0-a5a9-2e1d03b21d2c` | Alternative location reference; normalized |
| Area 01 — protest site | `587b4059-90c0-49ee-aedf-7e3cd3df844b` | Alternative starting-plaza reference; normalized |
| Delhi route landmarks and location boards | `38f4b153-87d1-4ab2-bb05-4fb911da5f2d` | Wider route/signage reference; normalized |
| Sharp clear endless protest world | `6aadf020-d370-4542-9f2a-74503946edfd` | Alternate long corridor reference; normalized |
| Realistic protest site v2 | `afe93872-0a38-4c79-831f-9062fac432de` | Plaza-to-road visual reference; normalized |
| Photoreal Delhi protest route master | `b7a4ff75-5735-42f7-86a6-651ea1101b2b` | Wide route reference; normalized |

A new Module B scene is currently being generated for the road continuation toward Sansad Marg. Add its asset ID here after PINOC reports it ready.

## Assembly order

1. Lock the Module A starting view and verify the Jantar Mantar entrance / protest forecourt silhouette against reliable reference photos.
2. Finish Module A's separate game-ready road, paving, building and edge-prop meshes.
3. Add Module B as a connected Sansad Marg road corridor; validate road width, ground height and entry/exit orientation before blending.
4. Build reusable prop families: metal crowd-control barricades, beige tents, water crates, streetlights, trees, Indian traffic, police van and bus.
5. Add landmark-specific structures and location boards only where reference material supports their placement.
6. Integrate assets into the actual game scene and run a real Android performance check. The browser viewer is for quick visual review, not proof of Android performance.

## Accuracy and quality gates

- No unverified claims of exact surveying or exact real-world dimensions.
- No placeholder boxes presented as final landmark buildings.
- No Gaussian splat described as an editable polygon mesh.
- No environment asset is considered integrated until a browser/game scene loads it and its coordinates are checked.
- Keep old project files and history; use incremental commits.
