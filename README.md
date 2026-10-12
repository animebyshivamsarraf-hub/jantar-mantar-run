# JANTAR MANTAR RUN — Delhi Chase

A mobile-first, third-person 3D endless runner set in a fictional Delhi streetscape. Dodge traffic and barricades while police officers chase the player.

`index.html` is a playable browser prototype with a protest-area opening, touch/keyboard controls, lane movement, jump/slide, obstacles, a chase loop, and restart. The current scene includes procedural landmark-inspired geometry for the Samrat Yantra, Jai Prakash Yantra and Ram Yantra forms. It is **not** a sem-to-sem reconstruction and has not been verified as a production or Android build. The PINOC Gaussian-splat environments and rigged character/motion assets are not yet loaded into this runtime.

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
- [Open the browser prototype](https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/)
- [Open the 3D Jutsu source scene](https://higgsfield.ai/3d-jutsu/a500419b-8e26-4837-bba6-f1911675b305)

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
5. Keep the browser prototype as the quick review build while the PINOC SPZ transfer path and real Android build are completed.

Use only assets you created or have permission to use. A public repository exposes its contents to everyone; the MIT license does not automatically relicense third-party assets.
