# JANTAR MANTAR RUN — Delhi Chase

A mobile-first, third-person 3D endless runner set in a fictional Delhi streetscape. Dodge traffic and barricades while police officers chase the player.

> **Current status:** concept and production documentation are being collected here. The repository is not yet a playable game, and the uploaded FBX still needs to be added through a binary-safe upload route and checked in Unity.

## Project decisions so far
- **Game type:** third-person, three-lane endless runner.
- **Setting:** Delhi-inspired urban road with Jantar Mantar-inspired landmarks, buses, street props and traffic.
- **Gameplay loop:** run, change lanes, jump, slide, collect coins, avoid obstacles, get caught, Game Over, restart.
- **Tone:** cinematic, realistic 3D visuals with a light comedic chase; no graphic arrest/violence.
- **Target:** Android first. Browser/WebGL is a later option after a real build and performance test.
- **Current model supplied:** `Endless Runner (1)(1).fbx` (uploaded in chat; not yet committed to this repository).

## Start here
- [Game design](Documentation/GAME_DESIGN.md)
- [Development roadmap](Documentation/ROADMAP.md)
- [Unity environment setup](Documentation/ENVIRONMENT_SETUP.md)
- [AI build instructions](Documentation/AI_BUILD_INSTRUCTIONS.md)
- [Controls](Documentation/CONTROLS.md)
- [Visual direction / art bible](Documentation/VISUAL_DIRECTION.md)
- [Work log and asset status](Documentation/WORK_LOG.md)
- [World asset assembly pipeline](Documentation/WORLD_ASSET_PIPELINE.md)

## Planned Unity layout
```text
Assets/
  Audio/
  Characters/
  Environment/
  Materials/
  Prefabs/
  Scenes/
  Scripts/
  UI/
Documentation/
```

## Next practical steps
1. Add the supplied FBX using GitHub's web uploader or Git LFS/binary-safe Git workflow.
2. Import it into Unity and inspect scale, orientation, mesh, materials, rig and colliders.
3. Build a playable greybox with three lanes, camera follow, movement, obstacles, coins and restart.
4. Replace greybox elements with licensed/generated visuals and test on Android.
5. Only then decide whether a browser build is viable.

Use only assets you created or have permission to use. A public repository exposes its contents to everyone; the MIT license does not automatically relicense third-party assets.
