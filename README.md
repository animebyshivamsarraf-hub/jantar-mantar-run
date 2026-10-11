# JANTAR MANTAR RUN — Delhi Chase

A mobile-first, third-person endless runner set in a fictional, comedic Delhi streetscape. Dodge traffic and barricades while police officers chase the player.

> **Status:** planning and setup documentation. This repository is not yet a playable game and does not yet contain the supplied FBX or a complete Unity project.

## Start here
- [Game design](Documentation/GAME_DESIGN.md)
- [Development roadmap](Documentation/ROADMAP.md)
- [Unity environment setup](Documentation/ENVIRONMENT_SETUP.md)
- [AI build instructions](Documentation/AI_BUILD_INSTRUCTIONS.md)
- [Controls](Documentation/CONTROLS.md)

## Target and tools
- **Engine:** Unity (Editor version to be selected)
- **Initial target:** Android
- **Possible later target:** WebGL, only after feasibility and mobile-browser performance testing
- **Environment asset:** `Endless Runner (1).fbx` (not yet committed)

## Planned core loop
Opening scene → three-lane run → dodge/jump/slide and collect coins → police catch the player → short, non-graphic detention cutscene → Game Over → restart.

## Planned repository structure
```text
Assets/
  Audio/ Characters/ Environment/ Materials/ Prefabs/ Scenes/ Scripts/ UI/
Documentation/
  AI_BUILD_INSTRUCTIONS.md
  CONTROLS.md
  ENVIRONMENT_SETUP.md
  GAME_DESIGN.md
  ROADMAP.md
```

## Next steps
1. Upload the FBX with GitHub's web uploader or another binary-safe method.
2. Import it into a desktop Unity Editor and check scale, orientation, materials, and colliders.
3. Build and test a small playable greybox before adding detailed art and audio.
4. Test touch controls and performance on an Android device.

Only use audio, clips, models, fonts, and textures you created or have permission to use. A public repository makes its contents visible to everyone. The MIT license does not automatically relicense third-party assets.
