# Environment Module Contract — prototype v1

Status: design contract; dimensions are prototype values, not a surveyed map of Jantar Mantar.

Every environment segment must follow this contract before its assets are authored. Do not generate independent scenes and try to align them afterward.

## Coordinate system

- Units: metres.
- Up axis: +Y.
- Route axis: Z.
- Player travel direction: toward negative Z.
- Ground reference: Y = 0.
- Route centerline: X = 0.
- Prototype road width: 11 m, from X = -5.5 to X = +5.5.
- Lighting target: clear late-morning daylight, neutral sky fill, sun from upper-left/front.
- Shared material palette: warm Delhi stone/ochre-red masonry, neutral grey asphalt, dusty beige pavement, Delhi Police yellow/black accents, natural tree greens.
- These are starter dimensions for testing and must be adjusted against verified reference images before final art.

## Module interface

Each module declares:
- `moduleId`, `version`, `lengthM`, `roadWidthM`.
- Entry anchor and exit anchor: position, heading, ground height.
- Walkable/playable corridor bounds.
- Ground/road meshes and collision bounds.
- Light/time-of-day/material profile identifiers.
- Required overlap/connector zone length.
- Asset budget and target device tier.

Prototype anchor convention for a module beginning at local Z=0 and ending at local Z=-40:
- Entry anchor: (0, 0, 0), heading toward -Z.
- Exit anchor: (0, 0, -40), heading toward -Z.
- Connector zone: last 4 m of the outgoing module and first 4 m of the incoming module, matched by the same transform; one ground surface must be authoritative to avoid z-fighting.
- Adjacent modules may not each add a full road slab over the same space. Join geometry/materials at a defined seam or use an intentional overlap hidden by shared connector geometry.

## Join validation

Before accepting any two modules:
1. Transform the incoming entry anchor to the outgoing exit anchor and compare position (<= 1 cm), heading (<= 0.5 degrees), and ground height (<= 1 cm).
2. Verify road edges and sidewalk edges match within 1 cm.
3. Inspect the seam from forward and reverse camera approaches.
4. Check for visible gaps, z-fighting, texture scale changes, lighting jumps, and abrupt skyline/object pop-in.
5. Check collision boundaries across the join and ensure the playable corridor remains continuous.
6. Run on a low-end Android profile and confirm no large asset upload or frame-time spike at the seam.

## Scene content rules

- Use surveyed/reference-grounded landmark proportions when available; label speculative blockout geometry as such.
- Do not use temporary signed asset URLs as release dependencies.
- Gaussian-splat scenes are reference/hero environment assets, not automatically modular road meshes. They need a deliberate seam/transition plan and separate collision/playable geometry.
- Repeated modular props must have controlled variation without changing the shared road, horizon, lighting, or scale contract.
- Never approve the environment from a single screenshot alone; inspect a continuous route traversal.

## Release gate

No environment segment is release-ready until its interface data and both-direction join test are checked in, and the joined route has been traversed in an actual browser.
