# Delhi Route Landmarks & Location Boards v4

- Project: JANTAR MANTAR RUN — Delhi Chase
- PINOC environment asset: `38f4b153-87d1-4ab2-bb05-4fb911da5f2d`
- Generation task: `5b713545-e5c4-4c48-a7e3-b4719350109c`
- Status: PINOC reports the normalised SPZ environment is ready. Browser integration and visual comparison against the supplied map screenshot are not yet verified.
- Account: Primary
- Format: normalised SPZ Gaussian splat; 1 unit = 1 metre; load at identity with no scale, offset, or rotation.
- Signed SPZ URL expires; refresh via PINOC environment listing when needed. Binary is not committed to GitHub.

## Incremental map plan
1. Jantar Mantar Protest Site entry/open area.
2. Adjacent road and junction.
3. Route toward Sansad Marg.
4. Nearby connecting streets and landmarks, added only after map/reference validation.

## Visibility and signage
The generation prompt requests crisp near/mid/far detail and no fog, haze, depth-of-field blur, bokeh, or motion blur. It requests signs for Jantar Mantar, Sansad Marg, Janpath, and entry. Generated text may be imperfect; in-game labels should be rendered as actual text so they stay readable.

## Loop and truthfulness
The environment is a scene asset, not proof of a surveyed exact reconstruction or a completed endless loop. The current browser game has procedural scenery. Road-marking recycling is a first step; seamless road/scene-segment looping and visual seam testing remain to be implemented. The supplied screenshot is an initial route reference, not enough to claim every street/building is reconstructed exactly.

## Next checks
- Compare the first segment to the supplied map and ground-level street references.
- Load the SPZ in the browser renderer.
- Confirm distant sharpness on Android.
- Implement and test continuous road and compatible scenery-segment recycling.
- Add the next adjacent area only after the first segment is checked.
- Characters remain lower priority until environment and loop are verified.
