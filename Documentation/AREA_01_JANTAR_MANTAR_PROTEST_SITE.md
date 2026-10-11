# Area 01 — Jantar Mantar Protest Site

**Project:** JANTAR MANTAR RUN — Delhi Chase  
**Scope:** Build only the first area; do not expand into adjacent streets until this area is reviewed.

## Reference
Use the user's supplied Google Maps screenshot as the initial orientation reference. It shows a route starting at “Jantar Mantar Protest Site” and pointing toward Sansad Marg, with nearby road geometry and junctions. The screenshot is not sufficient to reconstruct exact building footprints, so compare with additional map/street-level references before claiming a one-to-one match.

## First-area boundaries
- Entry/open protest-site approach.
- Plaza edge and pedestrian sidewalk.
- Barricade/checkpoint zone.
- Tent/tarpaulin details and small street props.
- One visible location board: “JANTAR MANTAR PROTEST SITE”.
- A clear exit edge toward the first verified road junction; stop the reconstruction at that boundary.

## Existing candidate PINOC scenes
- Route landmarks and signage v4: `38f4b153-87d1-4ab2-bb05-4fb911da5f2d`
- Realistic protest site v2: `afe93872-0a38-4c79-831f-9062fac432de`
- Sharp clear route v3: `6aadf020-d370-4542-9f2a-74503946edfd`

All are normalised SPZ assets, so load at identity (1 unit = 1 metre; ground at origin). Signed CDN URLs expire. The game currently does not load these assets; this is not yet a playable environment integration.

## Visual acceptance checks
- No scene fog, depth-of-field blur, bokeh or artificial motion blur.
- Inspect the site at near, medium and far camera distances.
- Location board text remains readable; use game-rendered text rather than relying on generated texture text.
- Props remain grounded; no floating signs or intersecting barricades.
- Maintain navigable running space.

## Route and loop
Do not force the entire plaza into a loop. Keep Area 01 geographically recognizable, then connect its exit to Area 02 at the verified road junction. Endless gameplay should recycle generic compatible road segments farther along the route, not move the real-world landmarks to fake a loop. Test the transition for gaps, popping, abrupt turns and duplicate boards.

## Current progress
- [x] PINOC candidate environments generated.
- [x] Location board concept added to the procedural browser scene.
- [ ] Load selected SPZ in the browser renderer.
- [ ] Match the scene against map and street-level references.
- [ ] Test Android clarity and performance.
- [ ] Validate the Area 01 exit into Area 02.
- [ ] Only after review, begin Area 02.
