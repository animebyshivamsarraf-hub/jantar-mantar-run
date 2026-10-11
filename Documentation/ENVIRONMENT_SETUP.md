# Unity Environment Setup — FBX Intake

This guide assumes access to a desktop Unity Editor. An Android phone can manage this repository, but importing and inspecting a 3D scene requires a supported computer/editor workflow.

## 1. Add the asset
1. Upload `Endless Runner (1).fbx` using GitHub's web uploader or another binary-safe workflow.
2. Preferred path: `Assets/Environment/EndlessRunnerEnvironment.fbx`.
3. Keep source and license/permission information.
4. Do not paste binary FBX data into a text editor or create a text placeholder with an FBX extension.

## 2. Import in Unity
1. Create/open the Unity project using the selected Editor version.
2. Place the FBX in `Assets/Environment/`.
3. Select it in the Project window and inspect Model, Rig, Materials, and Animation settings as applicable.
4. Apply changes and inspect it in a scene.

## 3. Validate
- Check scale and forward direction.
- Check materials and any separately required textures.
- Inspect normals, pivots, and hierarchy.
- Add colliders to walkable surfaces and obstacles.
- Prefer simple colliders; avoid MeshColliders on every decorative object.
- Verify the runner has a clear lane path.
- Determine whether the FBX contains a static environment, one combined mesh, or modular pieces; the filename alone does not tell us.

## 4. Mobile performance
- Inspect polygon counts and texture sizes.
- Reduce unnecessary materials and transparent surfaces.
- Use baked lighting where appropriate.
- Profile on a real Android device.

## Checklist
- [ ] FBX uploaded and verified
- [ ] Textures included if needed
- [ ] Asset permissions recorded
- [ ] Scale/orientation checked
- [ ] Collision surfaces tested
- [ ] Mobile performance measured

An FBX is an asset, not a complete playable game. Scenes, scripts, prefabs, animations, UI, audio, and project settings still need to be added.
