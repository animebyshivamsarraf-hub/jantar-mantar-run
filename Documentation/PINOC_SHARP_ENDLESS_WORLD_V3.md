# PINOC Sharp Clear Endless Protest World v3

- **Project:** JANTAR MANTAR RUN — Delhi Chase
- **PINOC asset:** `6aadf020-d370-4542-9f2a-74503946edfd`
- **Generation task:** `860ccd9f-4f09-442b-9c26-7ff0fe8f2d66`
- **Status:** PINOC environment generation succeeded; browser/game integration and visual QA are still pending.
- **Format:** Normalised SPZ Gaussian splat.
- **Placement:** Load at identity; 1 unit = 1 metre; ground at origin. Do not apply scale, offset, or rotation.
- **Account:** Primary.

## Required visual criteria
- Bright daylight and crisp details from foreground through the distant vanishing point.
- Explicitly avoid depth-of-field blur, distance blur, bokeh, fog, mist, haze, smeared far objects, and artificial motion blur.
- Delhi protest-route details: tents and tarpaulins, crates and bottles, paper cups and rolled mats, blank banners/placards, metal police barricades, parked Indian traffic and police vans, roadside trees, streetlights, utility wires, government-district buildings, and Jantar Mantar-inspired red sandstone/terracotta architecture.
- No characters, blood, collectible coins, HUD, overlaid title graphics, or generated readable text.

## Endless road loop: important limitation
This asset is a **scene asset**, not a verified endless-runner implementation. The generation prompt requested a long straight corridor with consistent road width and ground height, compatible entry/exit directions, and repeated roadside rhythm. That does not prove the SPZ's end joins seamlessly to its start. The game still needs a runtime loop system (for example, repeating road/environment segments with matching transforms) and a visual seam test.

## Acceptance checklist before characters
- [ ] Load this SPZ in the browser renderer.
- [ ] Inspect near, middle, and far details on a real camera path; confirm distant detail remains sharp.
- [ ] Disable renderer-side fog, depth-of-field, and motion blur if present.
- [ ] Implement endless road/segment recycling and confirm no visible gap, pop, or seam.
- [ ] Test on Android performance and rendering.
- [ ] Only after those checks, integrate characters.

## Asset URL
PINOC returned a signed normalised SPZ URL at generation time. Signed CDN URLs expire, so refresh it from PINOC's environment list when needed rather than treating it as a permanent link. The SPZ binary is hosted by PINOC and is not committed to this GitHub repository.

## Related docs
- [Protest Site v2](./PINOC_PROTEST_SITE_V2.md)
- [Master Environment](./PINOC_MASTER_ENVIRONMENT.md)
