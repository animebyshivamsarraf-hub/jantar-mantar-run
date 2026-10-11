# Stage 2 — Area 01 visual target and asset checklist

Status: **SPECIFICATION ONLY — not yet a finished or visually approved scene**

## Visual direction

Build a Delhi-inspired urban protest-route environment with a grounded, readable road corridor. The scene should feel like a coherent place, not disconnected props on a blank plane. Use believable scale, consistent daylight, restrained material roughness, and a clear silhouette at mobile resolution. This is an inspiration target, not a claim of a survey-accurate reconstruction.

## Required visual layers

1. **Road corridor:** asphalt with subtle variation, readable edge lines and dashed center markings; no abrupt material or geometry seams.
2. **Street edge:** curb/footpath transitions and verge material that remain aligned across segment boundaries.
3. **Route identity:** Sansad Marg / route sign with legible text, mounted at plausible height and facing the approach direction.
4. **Protest-site dressing:** fabric canopies/tarps, poles, restrained banners and ground clutter arranged outside the drivable corridor.
5. **Delhi-inspired streetscape:** trees, streetlights, distant institutional-looking buildings and yellow police-style barricades; avoid unsupported claims that a specific building is an exact model.
6. **Player/readability:** player silhouette remains visible against the road; obstacles contrast enough to read on a small phone screen.
7. **Depth and atmosphere:** distant haze and a stable sky/lighting profile; no blurry foreground assets or abrupt fog bands.

## Asset acceptance rules

- Prefer authored or properly licensed assets with durable source URLs/files; do not ship expiring signed asset URLs.
- Keep textures sharp at the expected camera distance and use mobile-sized texture budgets.
- Name each asset and record source/licence, dimensions, scale, material assumptions and intended segment attachment point.
- Use the same ground elevation, road width, segment length, lighting and material palette as `MODULAR_WORLD_CONNECTOR_CONTRACT.md`.
- Every repeating segment must be previewed with its previous and next segment together, not only as a standalone render.
- Fix z-fighting, gaps, floating props, road-edge discontinuities, texture stretching and abrupt LOD changes before approval.
- Place dressing outside the collision corridor unless it is intentionally a gameplay obstacle.

## Build order

- [ ] Replace greybox sign with a final legible sign asset/material.
- [ ] Replace shelter placeholders with authored tarpaulin geometry/materials.
- [ ] Add curb/footpath and consistent road markings.
- [ ] Add streetlights and varied trees with shared lighting.
- [ ] Add layered distant architecture and controlled atmospheric depth.
- [ ] Make one segment plus two connector test segments.
- [ ] Capture same-camera screenshots at the entry, middle and exit seams.
- [ ] Review on desktop Chromium and a real Android phone.
- [ ] Record draw calls, approximate triangle counts and texture memory before adding more set dressing.

## Current limitations

The Stage 1 scene remains a lightweight Three.js greybox prototype. The sign and shelters recently added are primitive placeholders. Automated Chromium smoke tests do not establish visual quality, photorealism, device performance or seam-free modular assembly.
