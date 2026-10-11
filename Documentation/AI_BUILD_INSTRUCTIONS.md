# AI Build Instructions

## Required workflow
1. Inspect the existing repository before editing.
2. Report what exists before adding files.
3. Work in small, reviewable steps; do not rewrite the whole project without approval.
4. Never claim a build or test succeeded unless it was actually run.
5. Keep binary assets binary-safe; never generate a fake FBX or use a text placeholder with an FBX extension.
6. Ask before adding paid services, third-party assets, analytics, or network features.

## Goal
Build a Unity mobile endless runner titled **JANTAR MANTAR RUN — Delhi Chase**, a fictional stylized action-comedy experience.

## Build order
1. Validate the Unity project and imported environment.
2. Create a minimal playable greybox scene.
3. Implement three-lane movement, jump, slide, camera follow, and obstacle collisions.
4. Implement road recycling and obstacle spawning.
5. Implement chase/caught state, Game Over, and restart.
6. Add opening and detention cutscenes.
7. Add approved art/audio, UI, optimization, and Android testing.

## Code expectations
- Use small C# MonoBehaviour scripts with inspector-assignable fields.
- Avoid brittle hard-coded scene lookups.
- Guard against repeated collision/Game Over triggers.
- Separate gameplay logic from UI and cutscene logic.
- Make movement states deterministic.
- Use pooling or segment recycling rather than unlimited instantiation.
- Include setup instructions and a manual verification checklist.

## Deliverables per step
List files changed, Unity scene/prefab setup, Editor version tested, actual compile/build results, known limitations, and next steps.

## Browser preview
A public GitHub repository is not a playable browser preview. WebGL export requires a configured Unity project and testing. If browser access is important, validate WebGL feasibility and mobile-browser performance before making it a target.
