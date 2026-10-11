# PINOC Realistic Protest Site v2

## Asset
- Name: `JANTAR MANTAR RUN — Realistic Protest Site v2`
- PINOC environment ID: `afe93872-0a38-4c79-831f-9062fac432de`
- Format: normalised SPZ Gaussian splat
- Placement: load at identity; one unit is one metre; ground is at origin; do not scale, offset, or rotate.
- Generated with PINOC on 2026-10-11.

## Intended look
Photorealistic Delhi protest gathering area inspired by Jantar Mantar and the route toward Sansad Marg. Environment prompt prioritises worn stone paving and asphalt, metal police barricades/checkpoint lanes, temporary shade tents and tarpaulins, water crates, paper cups and ordinary bottles, cloth banners and placards without legible writing, police vans, Indian traffic, mature trees, utility poles, streetlights, government-district buildings, and red sandstone/terracotta observatory forms. The plaza transitions toward a realistic road. No people are baked into the scene; runner, police characters, collision, and gameplay systems must be integrated separately.

## Collectible change requested
Replace the existing generic coin collectible with a **Lahori Zeera / Lahori Jeera-style fizzy drink bottle collectible**. Implement it as a separate game asset/object, not painted into the Gaussian-splat environment, so the label, collision shape, pickup animation, score value, and mobile rendering can be edited independently. Use a small recognisable chilled drink bottle with a green/yellow label and the text `Lahori Zeera` only if the project's asset/licensing approach permits the brand artwork; otherwise use a clearly labelled placeholder bottle and swap in approved artwork later. Keep pickup collision and score logic independent from the visual mesh.

## Status and limitations
- The environment generation succeeded in PINOC.
- This is an environment asset, not a complete playable game world.
- The SPZ file is hosted by PINOC and is not committed as a binary to this repository; signed URLs expire, so refresh from PINOC's environment library when needed.
- Still required: integrate/load the environment in the browser game, add the bottle collectible object, and test on Android/mobile browsers. Do not describe these steps as complete until verified.
