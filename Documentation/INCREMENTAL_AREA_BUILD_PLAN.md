# JANTAR MANTAR RUN — Incremental Area Build Plan

## Working rule
Build, inspect, and approve one geographically connected area at a time. Do not create the whole city as one guessed scene. Do not claim exactness until checked against references.

## Area sequence

### Area 1 — Jantar Mantar Protest Site entry and open protest area
- Establish entry approach, open plaza, protest tents, tarpaulins, water crates, mats, barricades and nearby sidewalk.
- Add an in-game location board: **JANTAR MANTAR PROTEST SITE**.
- Compare road approach, site boundaries and nearby objects against the user's supplied map screenshot and ground-level references.
- Acceptance: clear far view, no fog/DOF blur, stable ground, landmarks in plausible positions, readable board.

### Area 2 — First connecting road and junction
- Build only the road immediately connected to Area 1.
- Preserve the verified road width, curb, sidewalk and turn direction.
- Add a junction board and directional arrows only when the direction is verified.
- Acceptance: walking/running transition from Area 1 has no gap or sudden geometry pop.

### Area 3 — Sansad Marg approach
- Extend the route only along the verified connection.
- Add **SANSAD MARG** location board and realistic roadside details.
- Do not invent building names or distances.

### Area 4 — Janpath / nearby connecting route
- Add only after the exact connection is verified against map/street references.
- Add **JANPATH** board only at a confirmed route point.

### Area 5+ — Additional streets and recognizable landmarks
- Continue outward one connected block at a time.
- Keep a route graph and asset list so intersections and landmarks remain consistent.

## Endless-runner loop design
The real-world map route and the gameplay loop are separate concerns. Preserve a recognizable first route, then recycle compatible road/environment segments behind the player. Match segment start/end ground height, road width, lane markings, curb and lighting. Hide transitions at natural turns or occluded boundaries. Never teleport or rotate a named real landmark merely to make the loop fit. Test repeated cycles for visible gaps, popping, duplicated signs and misplaced buildings.

## Visual requirements
- No distance fog, haze, depth-of-field blur, bokeh or motion blur.
- Keep renderer camera settings and image clarity consistent across segments.
- Use actual UI/text for location boards if Gaussian-splat signage is not legible.
- Avoid generating fake readable signs or unsupported distances.

## Current status
- PINOC v3 sharp/loop-ready scene: asset ID `6aadf020-d370-4542-9f2a-74503946edfd`; browser integration and visual QA pending.
- PINOC v4 route/signage scene: asset ID `38f4b153-87d1-4ab2-bb05-4fb911da5f2d`; generated scene ready, browser integration and map comparison pending.
- Current browser renderer still uses procedural Three.js scenery; it does not yet load the SPZ asset.
- Lane-marking recycling exists as a first code step, but complete seamless environment-segment looping is not verified.
- Character work remains paused until Area 1, its route connection, and loop behaviour pass inspection.

## Definition of done for each area
1. Reference imagery/map reviewed.
2. Road geometry and landmark positions created.
3. Location board placed at a verified point.
4. Browser rendering tested at near/mid/far distance.
5. Android viewport and performance checked.
6. Route entry/exit tested from adjacent segment.
7. Notes and screenshots recorded before moving to the next area.
