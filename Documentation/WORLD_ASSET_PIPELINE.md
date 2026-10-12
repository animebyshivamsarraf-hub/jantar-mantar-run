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
| Module B — Sansad Marg connected road | `2824d991-051d-4e44-b7cb-142586d81fa4` | Connected road extension; normalized, identity placement |
| Module C — observatory landmark reference | `d2691515-bcbb-464d-90f4-2cffc175a256` | Newly generated reference focused on Samrat Yantra, Jai Prakash and Ram Yantra forms; normalized |

## Character and motion

- PINOC character asset: `334cfc33-cd69-4f33-9fe6-37c6245976f1` — “Man in Casual Wear”, generated as the initial adult runner protagonist reference. It is a rigged Gaussian-splat character, not a polygon mesh.
- PINOC motion asset: `32c1d7ba-29d6-466c-a2bc-56ff17ca4d1a` — “Running”, selected from the free curated motion library. A compatible splat-skeleton export or the web Splat Engine still needs to be connected before this character can animate inside the game.
- PINOC library: https://viggle.ai/pinoc/app. Signed file URLs expire; refresh them in PINOC when transferring assets.

## Current 3D scene work

The hosted 3D Jutsu source scene is [JANTAR MANTAR RUN — Delhi Protest Environment Base](https://higgsfield.ai/3d-jutsu/a500419b-8e26-4837-bba6-f1911675b305).

Revision 4 contained the first detail pass: 213 window instances, roadside streetlights, bollards, three temporary tent structures, tree foliage/branches, a Delhi-style bus, a police van, and roadside location boards.

Revision 5 adds procedural, editable landmark-inspired geometry for a Samrat Yantra-style triangular gnomon with steps, a Jai Prakash-style concave bowl with radial/concentric scales, and a Ram Yantra-style circular instrument wall. It also repositions the temporary shelter concept to the route edges, adds twelve roadside bollards, and adjusts the delivery camera/daylight setup. A 640×400 preview render was published as part of the scene operation.

These are reference-guided procedural approximations, not a photogrammetry-accurate or surveyed reconstruction. The generated PINOC Module A/B/C environment files are still separate Gaussian-splat assets and are **not yet integrated into the runtime**. The character and running clip are also not yet connected to the browser game. The hosted scene is not yet a tested Android game build.

## Assembly order

1. Lock the Module A starting view and verify the Jantar Mantar entrance / protest forecourt silhouette against reliable reference photos.
2. Refine landmark geometry and camera composition using real reference views; avoid symmetrical or generic shapes where the actual instrument form differs.
3. Finish separate game-ready road, paving, building and edge-prop meshes.
4. Add Module B as a connected Sansad Marg road corridor; validate road width, ground height and entry/exit orientation before blending.
5. Treat Module C as a landmark visual reference until a supported durable asset-transfer path is confirmed.
6. Connect the character and a compatible running clip through the PINOC Splat Engine path, then profile it on mobile.
7. Build reusable prop families: metal crowd-control barricades, beige tents, water crates, streetlights, trees, Indian traffic, police van and bus.
8. Integrate assets into the actual game scene and run a real Android performance check. The browser viewer is for quick visual review, not proof of Android performance.

## Accuracy and quality gates

- No unverified claims of exact surveying or exact real-world dimensions.
- No placeholder boxes presented as final landmark buildings.
- No Gaussian splat described as an editable polygon mesh.
- No environment asset is considered integrated until a browser/game scene loads it and its coordinates are checked.
- Keep old project files and history; use incremental commits.


## 2026-10-12 — PINOC visual environment pack (generated)

Created three new PINOC Gaussian-splat environments in the existing connected PINOC account for the next visual upgrade pass:

| Module | Asset ID | Visual target |
|---|---|---|
| Roadside trees and Delhi buildings | `441d4c79-c5cd-40fe-aefb-e8ade7c1787f` | Mature roadside trees, planted edges, cream/sandstone office buildings, sidewalk/kerb and Delhi street furniture |
| Observatory approach and road | `79fb09ab-845b-4dd6-a298-2e074aef5dc1` | Sansad Marg approach, worn asphalt, footpath, boundary walls, railings, bollards, trees and shrubs |
| Observatory garden and yantra surroundings | `4161f98a-9367-47e6-ad27-b34c2061c1a7` | Red-sandstone observatory instruments, lawn, paths, shrubs, mature trees and nearby urban context |

All three tasks completed successfully. Their files are normalised SPZ scenes (one unit = one metre, ground at origin); they are Gaussian splats, not editable polygon meshes. The generation prompts target a plausible reference-guided visual match, not a surveyed sem-to-sem scan.

**Integration status: generated, not yet runtime-integrated.** PINOC returns expiring signed URLs for the SPZ binaries. Do not commit those signed URLs into the game. Next, establish a durable asset-hosting path and a browser-compatible splat renderer/worker bundle, then load and position the modules at the road edges and observatory section. Keep the existing Three.js collision/gameplay geometry as the gameplay/navigation layer while the splats supply visual detail, and profile Android memory/GPU performance before enabling all modules by default.
