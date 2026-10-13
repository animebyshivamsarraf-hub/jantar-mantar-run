# Free Game Asset Sources — JANTAR MANTAR RUN

This project is a browser-based Three.js prototype. These are vetted free sources for upgrading its environment, characters, traffic, and materials. Download the original pack from the source and import only the files you actually need; this repository currently does **not** contain those third-party binary model packs.

## Priority asset packs

| Game use | Pack | Format / notes | License notes |
|---|---|---|---|
| Cars and traffic | [Kenney — Car Kit](https://kenney.nl/assets/car-kit) | 45 3D files; vehicle props | CC0 on pack page |
| Trees, rocks, foliage | [Kenney — Nature Kit](https://kenney.nl/assets/nature-kit) | 330 3D files; vegetation and rocks | CC0 on pack page |
| City buildings | [Kenney — Modular Buildings](https://kenney.nl/assets/modular-buildings) | 100 modular building parts | CC0 on pack page |
| General prototype props | [Kenney — Prototype Kit](https://kenney.nl/assets/prototype-kit) | 145 3D files; characters, vehicles, walls and buildings | CC0 on pack page |
| Street layout pieces | [Quaternius — Modular Streets](https://quaternius.com/packs/modularstreets.html) | Modular road pieces; FBX/OBJ/Blend listed | Verify current pack page license before shipping |
| Rigged human characters | [Quaternius — Universal Base Characters](https://quaternius.com/packs/universalbasecharacters.html) | 6 humanoid base models, rigged; glTF listed | Check current [Quaternius license](https://quaternius.com/license.html); don't redistribute raw assets |
| Larger city block kit | [Quaternius — Downtown City MegaKit](https://quaternius.com/packs/downtowncitymegakit.html) | 300+ modular city pieces; glTF listed | Check current license and free download subset |
| Realistic materials and HDRIs | [Poly Haven](https://polyhaven.com/) | PBR textures, HDRIs, 3D models | [CC0 license](https://polyhaven.com/license) |

## Integration notes

- Current prototype uses Three.js from jsDelivr and procedurally creates roads, buildings, trees, vehicles, and the player model in `environment/index.html`.
- Best first upgrade: swap the simple procedural cars/trees/buildings for a small number of optimized glTF/GLB assets; keep the procedural fallback so the game still loads if a remote asset fails.
- For a browser game, prefer GLB/glTF, small textures (1K–2K), and compressed/low-poly models for Android performance.
- Keep third-party asset files and their license files/attribution with the project when applicable. Quaternius' current license page should be checked because license wording can change.
- This catalog is a source list, not a claim that the models have already been downloaded, uploaded to GitHub, or wired into gameplay. The repository connector can commit text files, but does not upload arbitrary binary GLB/ZIP assets.

## Main game

- [Play the current browser prototype](https://animebyshivamsarraf-hub.github.io/jantar-mantar-run/environment/)
- [Environment source code](https://github.com/animebyshivamsarraf-hub/jantar-mantar-run/blob/main/environment/index.html)
