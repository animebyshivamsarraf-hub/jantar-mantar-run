# JANTAR MANTAR RUN — Delhi Chase

A mobile-first, third-person 3D endless runner set in a fictional Delhi streetscape. Dodge traffic and barricades while police officers chase the player.

`index.html` is a playable browser prototype with a protest-area opening, touch/keyboard controls, lane movement, jump/slide, obstacles, a chase loop, and restart. The current scene includes procedural landmark-inspired geometry for the Samrat Yantra, Jai Prakash Yantra and Ram Yantra forms. The live browser prototype was smoke-tested in mobile-sized Chromium on 2026-10-12, including the start/protest chase, movement controls, HUD, detention and restart. It is **not** a sem-to-sem reconstruction, production QA pass, or Android build. The PINOC Gaussian-splat environments and rigged character/motion assets are not yet loaded into this runtime.

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
- [Open the full 3D Jutsu world preview](https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/jantar-world-3d.html)
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
5. Keep the browser prototype as the quick review build while the PINOC SPZ transfer path and real Android build are completed. The current live prototype has been smoke-tested; PINOC asset integration and Android packaging remain open.

Use only assets you created or have permission to use. A public repository exposes its contents to everyone; the MIT license does not automatically relicense third-party assets.


## PINOC environment integration
- [Open PINOC 3D environment viewer](https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/pinoc-world.html)
- Bundled asset archive: https://d2ol7oe51mr4n9.cloudfront.net/user_3JkuoLy4uHNDjSxNT7XRC1ogkuo/4fe31d28-dbde-49a5-a0c6-f471e586a4b5.zip (road, trees, buildings, observatory, runner character and seven motion GLBs).
- The PINOC scenes are available in the linked 3D viewer. The original gameplay renderer remains separate; the Gaussian-splat scenes are not yet the active gameplay renderer or animated player rig.
