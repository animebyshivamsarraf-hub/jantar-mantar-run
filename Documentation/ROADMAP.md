# Development Roadmap

## Phase 0 — Repository and asset intake
- [ ] Upload `Endless Runner (1).fbx` using a binary-safe method.
- [ ] Record asset source and usage rights.
- [ ] Choose Unity Editor version and target device baseline.

## Phase 1 — Environment validation
- [ ] Import the FBX into `Assets/Environment/`.
- [ ] Inspect scale, axis orientation, meshes, materials, textures, and normals.
- [ ] Add or simplify colliders.
- [ ] Confirm a clear three-lane path.
- [ ] Measure performance on a target device.

## Phase 2 — Playable greybox
- [ ] Create a gameplay scene and placeholder player.
- [ ] Implement three-lane movement, jump, and slide.
- [ ] Add camera follow and collision handling.
- [ ] Add reusable road segments and obstacle spawning.
- [ ] Add Game Over and restart UI.

## Phase 3 — Chase loop
- [ ] Add police chase visuals and a clear catch condition.
- [ ] Add opening scene and gameplay transition.
- [ ] Add short detention cutscene.
- [ ] Add run summary and restart flow.

## Phase 4 — Art and polish
- [ ] Add approved art and animations.
- [ ] Add coins, score, and optional difficulty ramp.
- [ ] Add original/licensed sound and cleared meme clips.
- [ ] Add mobile touch UI, pause/settings, and accessibility.
- [ ] Optimize textures, lighting, memory, and draw calls.

## Phase 5 — Testing
- [ ] Test on a real Android device.
- [ ] Check frame rate, loading, touch response, memory, heat, and crashes.
- [ ] Make a development build and document results.
- [ ] Evaluate WebGL separately; do not claim browser support before testing.

## First prototype is done when
The player can start, run in three lanes, jump/slide, collide with or avoid obstacles, be caught, see Game Over, and restart without errors.
